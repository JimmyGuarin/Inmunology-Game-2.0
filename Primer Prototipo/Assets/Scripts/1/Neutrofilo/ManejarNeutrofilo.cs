using UnityEngine;
using System.Collections;

public class ManejarNeutrofilo : MonoBehaviour {

	public  bool isSeleted;
	public bool empezar;
	public Vector3 subir;
	public float life=500;
	public Texture2D imagen;
	public static int seleccionadas=0;
	private Rect r;
	public int ubicada;
	// Use this for initialization
	void Start () {
	
		ControladorRecursos.defensas++;
		life = 500;
		isSeleted = false;
		empezar = true;
		subir = new Vector3(MoverPuntoEncuentro.posicion.x,MoverPuntoEncuentro.posicion.y,-5f);;
		NotificationCenter.DefaultCenter ().AddObserver (this, "cambiarPosCelula");
		imagen = Resources.Load ("Neutrofilo4") as Texture2D;


	}
	
	// Update is called once per frame
	void Update () {
	

		if (Input.GetMouseButtonDown (1)) {
			
			
			Ray pulsacion;
			RaycastHit hit;
			pulsacion = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (pulsacion, out hit) && hit.collider == this.GetComponent<Collider>()) {
				
				
				if (isSeleted == false) {

					seleccionadas++;
					if(seleccionadas==1){
						PosicionSeleccionada.posicionar++;

					}
					ubicada=PosicionSeleccionada.posicionar;
					isSeleted = true;
					
				} else {
					isSeleted = true;
					
				}
			}
			
		}



		float step = 4* Time.deltaTime;

		if (empezar = true && transform.position != subir) {

			this.transform.position = Vector3.MoveTowards (transform.position, subir, step);
		} else {

			this.transform.position = Vector3.MoveTowards (transform.position, subir, step);

		}
		if (life <= 0) {

			if(isSeleted==true)
				seleccionadas--;
			ControladorRecursos.defensas--;
			Destroy(this.gameObject);

		}

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
			
			

			
			subir = new Vector3 (Fondo1.puntoDestino.x, Fondo1.puntoDestino.y, -5f);
			isSeleted = false;
			seleccionadas--;
		}
		
		
	}

	void OnTriggerStay (Collider MyTrigger) {

		
		if (MyTrigger.gameObject.name.Equals ("VirusFinal(Clone)")) {

			InteligenciaVirus v= (InteligenciaVirus)MyTrigger.GetComponent<InteligenciaVirus> ();

			life-=2;
			if(v.vida==900||v.vida==800||v.vida==700||v.vida==600||v.vida==500||v.vida==400||v.vida==300||v.vida==200||v.vida==100){
				

				v.BroadcastMessage("ChangeTheDamnSprite");
			}
			v.vida-=2;

			
		}
		if (MyTrigger.gameObject.name.Equals ("NaturalKiller(Clone)")) {
			
			life-=2f;
			if(life<=0){
				ControladorRecursos.defensas--;
				Destroy(this.gameObject);
				
				
			}
		
		
		
	}

   }
}
