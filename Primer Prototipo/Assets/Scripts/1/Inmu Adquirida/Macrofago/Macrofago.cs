using UnityEngine;
using System.Collections;

public class Macrofago : MonoBehaviour {

	//Velocidad  a la se me mueve la celula
	public float speed;
	
	//Vector de destino cuando movemos la celula
	public  Vector3 destino;
	
	//Variable que almacena si la celula esta seleccionada o no.
	public bool isSeleted;
	
	
	//Variable que representa el animator de la celula
	private Animator animator;
	
	private Fracture virus;
	// si va para base
	private bool llevarBase=false;
	
	public float vida=1000;
	//Si esta en colision
	private bool enColision;
	public static int seleccionadas=0;
	public Texture2D imagen;
	public Rect r;
	public int ubicada;
	void Start () {
		
		
		ControladorRecursos.defensas++;
		enColision = false;
		isSeleted = false;
		animator = GetComponent<Animator> ();
		speed = 6f;
		destino = new Vector3(MoverPuntoEncuentro.posicion.x,MoverPuntoEncuentro.posicion.y,-5f); // el primer destino es el Punto de Encuentro
		//PatronObserver:
		// Metodos que va a observar 
		NotificationCenter.DefaultCenter ().AddObserver (this, "cambiarPosCelula");
		
		
		
		
	}
	
	// Update is called once per frame
	void Update () {
		
		
		
		if (Input.GetMouseButtonDown (1)) {
			
			
			Ray pulsacion;
			RaycastHit hit;
			pulsacion = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (pulsacion, out hit) && hit.collider == this.GetComponent<Collider>()) {
				
				if (isSeleted == false && this.GetComponent<Collider>() != null) {
					
					seleccionadas++;
					if(seleccionadas==1){
						PosicionSeleccionada.posicionar++;
						ubicada=PosicionSeleccionada.posicionar;
						GUI.Label (new Rect (10, 30, 110, 60), imagen);
						
					}
					isSeleted = true;
				} 
			}
			
		}
		//se esta cambiando la posicion hasta que llega a destino
		float step = speed * Time.deltaTime;
		this.transform.position = Vector3.MoveTowards (transform.position, destino, step);
		if(llevarBase==true&&this.transform.position==destino){
			
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
			
			

			destino = new Vector3 (Fondo1.puntoDestino.x, Fondo1.puntoDestino.y, -5f);
			isSeleted = false;
			seleccionadas--;
			
		}
		
		
	}
	void OnTriggerEnter (Collider MyTrigger) {

		
		if (MyTrigger.gameObject.name.Equals ("VirusFinal(Clone)") || 
		    MyTrigger.gameObject.name.Equals ("VirusFinalCelula(Clone)"))
		{

			
		}	
		
		
		
		if (MyTrigger.gameObject.name.Equals ("LinfoncitoTCD4(Clone)")) {
			
		}
		
		
	}
	
	void OnTriggerExit(Collider MyTrigger) {
		
		enColision = false;
		
	}
	void OnTriggerStay(Collider MyTrigger) {
		
		
		if (MyTrigger.gameObject.name.Equals ("VirusFinal(Clone)")||
		    MyTrigger.gameObject.name.Equals("VirusFinalCelula(Clone)")) {
			
		
			
			
		}
		if (MyTrigger.gameObject.name.Equals ("virusFinalFracture(Clone)")) {
			
					
		}
		
		

		
	}
	
	
}
