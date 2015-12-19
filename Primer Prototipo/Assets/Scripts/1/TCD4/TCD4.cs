using UnityEngine;
using System.Collections;

public class TCD4 : MonoBehaviour {

	
	//Velocidad  a la se me mueve la celula
	public float speed;

	//Vector de destino cuando movemos la celula
	private  Vector3 destino;
	
	//Variable que almacena si la celula esta seleccionada o no.
	public  bool isSeleted;
	
	public static int seleccionadas;

	public float vida=100;
	public Texture2D imagen;
	public Rect r;
	public int ubicada;

	public GameObject ayudado;
	
	void Start () {

		ControladorRecursos.defensas++;
		isSeleted = false;
		destino = new Vector3(MoverPuntoEncuentro.posicion.x,MoverPuntoEncuentro.posicion.y,-5f); // el primer destino es el Punto de Encuentro
		speed=6f;
		//this.transform.position = new Vector3 (38f, -25.5f,-5f);  //Posicion donde se crea la celula (ganglio)
		
		//PatronObserver:
		// Metodos que va a observar 
		NotificationCenter.DefaultCenter ().AddObserver (this, "cambiarPosCelula");
			
	}
	
	
	// Update is called once per frame
	void Update () {


		if (ayudado != null)
			destino = ayudado.transform.position;

		if (vida <= 0) {
			
			if(isSeleted==true)
				seleccionadas--;
			ControladorRecursos.defensas--;
			Destroy(this.gameObject);
			
		}
		
		if (Input.GetMouseButtonDown (1)) {
			
			
			Ray pulsacion;
			RaycastHit hit;
			pulsacion = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (pulsacion, out hit) && hit.collider == this.GetComponent<Collider>()) {
				
				if (isSeleted == false&&this.GetComponent<Collider>()!=null) {

					ayudado=null;
					seleccionadas++;
					if(seleccionadas==1){
						PosicionSeleccionada.posicionar++;
					}
					ubicada=PosicionSeleccionada.posicionar;
					Debug.Log("1"+ubicada);
					isSeleted = true;
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
	
	
	
	
	void OnTriggerStay(Collider MyTrigger) {
		
		

			if (MyTrigger.gameObject.name.Equals ("VirusFinal(Clone)")) {
				
				
				
				vida-=0.2f;
				
			
		}

	}

	void OnTriggerEnter(Collider MyTrigger) {

		if (MyTrigger.gameObject.name.Equals ("LinfocitoB(Clone)")) {

			ayudado=null;
			destino=transform.position;
			
		}
		if (MyTrigger.gameObject == ayudado) {
		
			ayudado=null;
			destino=transform.position;
		}

		

	}

	
}
