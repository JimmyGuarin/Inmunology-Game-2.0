using UnityEngine;
using System.Collections;

public class LinfocitoB2 : MonoBehaviour {

	//Velocidad  a la se me mueve la celula
	public float speed;
	
	//Bala Linfocito
	public Rigidbody Bala;
	
	//Vector de destino cuando movemos la celula
	private  Vector3 destino;
	
	//Variable que almacena si la celula esta seleccionada o no.
	public  bool isSeleted;

	//Si ya llego a su destino 
	private bool llegoVaso=false;
	public float vida=200;
	public Texture2D imagen;
	public Rect r;

	
	void Start () {

		this.name="linfocitoB(Clone)";

		if(Bala!=null)
			InvokeRepeating ("disparar", 1.0f, 1.5f);
		isSeleted = false;
		destino = this.transform.position;
		NotificationCenter.DefaultCenter ().AddObserver (this, "cambiarPosCelula");
		
		NotificationCenter.DefaultCenter().PostNotification(this,"TCD4Tutorial",3);
		
		
		
	}
	
	
	// Update is called once per frame
	void Update () {
		
		

		
		//se esta cambiando la posicion hasta que llega a destino
		float step = speed * Time.deltaTime;
		this.transform.position = Vector3.MoveTowards (transform.position, destino, step);
		
		// Hasta que no llegue al vaso no se manda la notificacion que hace que el vaso dispare
		if (transform.position.Equals (destino) && llegoVaso == false) {
			
			
			
			
		}
		
	}
	void OnGUI(){
		
		if (isSeleted == true) {
							
				GUI.Label (new Rect (10, 30, 110, 60), imagen);
				GUI.Box(new Rect(10,10,110,100),"");
			
		}
	}
	
	
	//Metodollamado desde el script Fondo1 (Script del fondo)
	void cambiarPosCelula(Notification notification)
	{
		
		//Si esta celula esta seleccionada	
		
		if (isSeleted == true) {
			
			llegoVaso=false;
			destino = new Vector3 (Fondo1.puntoDestino.x, Fondo1.puntoDestino.y, -5f);
			isSeleted = false;
			transform.FindChild("seleccionada").gameObject.SetActive(false);
			Fondo1.seleccionada=false;
			
			
		}
		
	}
	
	
	
	
	void OnTriggerStay(Collider MyTrigger) {
		
		
		if (MyTrigger.gameObject.name.Equals ("VirusFinal(Clone)")) {
			
			vida -=2f;
			if (vida <= 0) {
				
				if (isSeleted == true)
					Fondo1.seleccionada=false;
					
				ControladorRecursos.defensas--;
				Destroy (this.gameObject);
				
				
			}
			
		}


	}
	
	void disparar(){
		
		Rigidbody clone=(Rigidbody) Instantiate (Bala,(this.transform.position+new Vector3(-3,0,0)),Bala.transform.rotation);
		clone.velocity = transform.TransformDirection ((new Vector3(0,1,0))*20);
		Rigidbody clone1=(Rigidbody) Instantiate (Bala,(this.transform.position+new Vector3(-3,-1,0)),Bala.transform.rotation);
		clone1.velocity = transform.TransformDirection ((new Vector3(-0.2f,1,0))*20);
		Rigidbody clone2=(Rigidbody) Instantiate (Bala,(this.transform.position+new Vector3(-3,1,0)),Bala.transform.rotation);
		clone2.velocity = transform.TransformDirection ((new Vector3(0.2f,1,0))*20);
	}

	


}
