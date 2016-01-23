﻿using UnityEngine;
using System.Collections;

public class LinfocitoB : MonoBehaviour {

	//Velocidad  a la se me mueve la celula
	public float speed;

	//Bala Linfocito
	public Rigidbody Bala;

	//Vector de destino cuando movemos la celula
	private  Vector3 destino;
	
	//Variable que almacena si la celula esta seleccionada o no.
	public  bool isSeleted;

	public static int seleccionadas;

	public float vida=100;

	public Texture2D imagen;

	public Rect r;

	public int ubicada;

	public GameObject fase2;

	public GameObject ayudador;

	public bool esperando_ayudador;

	void Start () {

		esperando_ayudador = false;

		if(Bala!=null)
			InvokeRepeating ("disparar", 1.0f, 2.0f);
		ControladorRecursos.defensas++;
		isSeleted = false;
		// el primer destino es el Punto de Encuentro
		destino = new Vector3(MoverPuntoEncuentro.posicion.x,MoverPuntoEncuentro.posicion.y,-5f); 

		// Metodos que va a observar 
		NotificationCenter.DefaultCenter ().AddObserver (this, "cambiarPosCelula");
		NotificationCenter.DefaultCenter ().AddObserver (this, "activarMiraLB");
		NotificationCenter.DefaultCenter ().AddObserver (this, "desactivarMiraLB");
	
	
		
		
		
	}


	// Update is called once per frame
	void Update () {
		
		
		if (Input.GetMouseButtonDown (1)) {
			
			
			Ray pulsacion;
			RaycastHit hit;
			pulsacion = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (pulsacion, out hit) && hit.collider == this.GetComponent<Collider>()) {
				
				if (isSeleted == false&&this.GetComponent<Collider>()!=null) {

					seleccionadas++;
					if(seleccionadas==1){
						PosicionSeleccionada.posicionar++;
					}
					ubicada=PosicionSeleccionada.posicionar;
					isSeleted = true;
					
				}
			}
			
		}

		if(Input.GetMouseButtonDown (0)) {
			
			
			Ray pulsacion;
			RaycastHit hit;
			pulsacion = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (pulsacion, out hit) && hit.collider == this.GetComponent<Collider>()) {
				
				if(ayudador!=null){

					esperando_ayudador=true;
					ayudador.GetComponent<TCD4>().ayudado=this.gameObject;
					NotificationCenter.DefaultCenter().PostNotification(this,"desactivarMiraLB");
				}
					
			}
			
		}
		
		//se esta cambiando la posicion hasta que llega a destino
		float step = speed * Time.deltaTime;
		this.transform.position = Vector3.MoveTowards (transform.position, destino, step);

	}









	void OnGUI(){
		
		if (isSeleted == true) {

			if (ubicada == 1) {
				
				GUI.Label (new Rect (10, 30, 110, 60), imagen);
				GUI.Box(new Rect(10,10,110,100),""+seleccionadas);
			}
			if (ubicada == 2) {
				
				GUI.Label (new Rect (130, 30, 110, 60), imagen);
				GUI.Box(new Rect(130,10,110,100),""+seleccionadas);
			}
			if (ubicada == 3) {
				
				GUI.Label (new Rect (250, 30, 110, 60), imagen);
				GUI.Box(new Rect(250,10,110,100),""+seleccionadas);
			}
			if (ubicada == 4) {
				
				GUI.Label (new Rect (370, 30, 110, 60), imagen);
				GUI.Box(new Rect(370,10,110,100),""+seleccionadas);
			} 
		}
	}


	//Metodollamado desde el script Fondo1 (Script del fondo)
	void cambiarPosCelula(Notification notification)
	{
		
		//Si esta celula esta seleccionada	
		
		if (isSeleted == true) {

				destino = new Vector3 (Fondo1.puntoDestino.x, Fondo1.puntoDestino.y, -5f);
				isSeleted = false;
				

				
			}
			
	}
	

	void activarMiraLB(Notification notification){


		this.transform.FindChild ("mira").gameObject.SetActive (true);
		ayudador = (GameObject) notification.data;
	}

	void desactivarMiraLB(Notification notification){
		
		
		this.transform.FindChild ("mira").gameObject.SetActive (false);
		ayudador = null;
	}



	void OnTriggerStay(Collider MyTrigger) {
		
		
		if (MyTrigger.gameObject.name.Equals ("VirusFinal(Clone)")) {
			
			vida -=2f;
			if (vida <= 0) {
				
				if (isSeleted == true)

				ControladorRecursos.defensas--;
				Destroy (this.gameObject);
				
				
			}
		
		}
		if (MyTrigger.gameObject.name.Equals ("LinfoncitoTCD4(Clone)")) {

			if(esperando_ayudador==true){

				this.GetComponent<Collider>().enabled=false;
				Vector3 aux=this.gameObject.transform.position;
				destino=aux;
				Eventos.explosion(aux);
				
				StartCoroutine(Wait(2,fase2,aux));
			}		
		}


	

	}

	IEnumerator Wait(int tiempo,GameObject fase2, Vector3 aux){
		
		yield return new WaitForSeconds(tiempo);
		Destroy(this.gameObject);
		Instantiate(fase2,aux,fase2.transform.rotation);
	}

	void disparar(){
		
		Rigidbody clone=(Rigidbody) Instantiate (Bala,(this.transform.position+new Vector3(-3,0,0)),Bala.transform.rotation);
		clone.velocity = transform.TransformDirection ((new Vector3(-1,0,0))*10);
		
		
	}
	
}
