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
	public GameObject mivirus;
	public GameObject ayudador;
	public bool esperando_ayudador;
	public float daño=0.2f;
	void Start () {
		
		mivirus = null;
		ControladorRecursos.defensas++;
		enColision = false;
		isSeleted = false;
		animator = GetComponent<Animator> ();
		speed = 3f;
		destino = new Vector3(MoverPuntoEncuentro.posicion.x,MoverPuntoEncuentro.posicion.y,-5f); // el primer destino es el Punto de Encuentro
		//PatronObserver:
		// Metodos que va a observar 
		NotificationCenter.DefaultCenter ().AddObserver (this, "cambiarPosCelula");
		NotificationCenter.DefaultCenter ().AddObserver (this, "activarMiraMA");
		NotificationCenter.DefaultCenter ().AddObserver (this, "desactivarMiraMA");

		
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (speed == 0.5f) {
		
			if(mivirus==null){

				llevarBase=false;
				enColision=false;
				daño=0.2f;
				destino=this.transform.position;
				GetComponent<FuncionesMacrofago>().enabled=true;
				speed=3f;
				animator.enabled=false;
			}
			else{

				mivirus.GetComponent<InteligenciaVirus>().vida-=daño;
			}



		}


		
		if (Input.GetMouseButtonDown (1)) {
			
			
			Ray pulsacion;
			RaycastHit hit;
			pulsacion = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (pulsacion, out hit) && hit.collider == this.GetComponent<Collider>()&&
			    mivirus==null) {
				
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

		if(llevarBase==true&&this.transform.position==destino){

				ControladorRecursos.defensas--;
				ManejadorVirus.numeroVirus--;
				Debug.Log("virus"+ManejadorVirus.numeroVirus);
				ControladorRecursos.puntaje+=300;
				Destroy(this.gameObject);
		}

		if (transform.position != destino) {
			
			this.transform.position = Vector3.MoveTowards (transform.position,destino, step);
		}
	
		//Click Izquierdo
		if(Input.GetMouseButtonDown (0)) {
			
			
			Ray pulsacion;
			RaycastHit hit;
			pulsacion = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (pulsacion, out hit) && hit.collider == this.GetComponent<Collider>()) {
				
				
				if(ayudador!=null&& this.GetComponent<FuncionesMacrofago>().enabled==false){
					
					esperando_ayudador=true;
					ayudador.GetComponent<TCD4>().ayudado=this.gameObject;
					NotificationCenter.DefaultCenter().PostNotification(this,"desactivarMiraMA");
				}
				
			}
			
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

	void activarMiraMA(Notification notification){
		
		if (mivirus != null) {
			
			this.transform.FindChild ("mira").gameObject.SetActive (true);
			ayudador = (GameObject) notification.data;
		}
		
	}
	
	void desactivarMiraMA(Notification notification){
		
		this.transform.FindChild ("mira").gameObject.SetActive (false);
		ayudador = null;
	}





	void OnTriggerEnter (Collider MyTrigger) {

		
		if (MyTrigger.gameObject.name.Equals ("VirusFinal(Clone)") || 
		    MyTrigger.gameObject.name.Equals ("VirusFinalCelula(Clone)")) {
			
			if(enColision==false&&mivirus==null){
				

				destino=new Vector3(47.8f ,-22.2f  ,-10f  );
				GetComponent<FuncionesMacrofago>().enabled=false;
				speed=0.5f;
				enColision = true;
				llevarBase=true;
				// Si no esta capturado
				if (mivirus==null&&MyTrigger.gameObject.GetComponent<InteligenciaVirus>().capturado == false) {

					mivirus=MyTrigger.gameObject;
					mivirus.transform.position=this.transform.position;
					mivirus.gameObject.name="capturado";
					mivirus.GetComponent<InteligenciaVirus>().capturado=true;
					mivirus.GetComponent<InteligenciaVirus>().speed=0.5f;
					mivirus.GetComponent<InteligenciaVirus>().destino=new Vector3(47.8f ,-22.2f  ,-10f  );
					mivirus.GetComponent<ColisionesVirus>().enabled=false;
				}

			}
		}
		
		
		
		if (MyTrigger.gameObject.name.Equals ("LinfoncitoTCD4(Clone)")) {

			if(esperando_ayudador==true){
				
				animator.enabled=true;
				esperando_ayudador=false;
				daño=3f;

				
				
			}	
		}
		
		
	}
	
	void OnTriggerExit(Collider MyTrigger) {
		
		enColision = false;
		
	}
	void OnTriggerStay(Collider MyTrigger) {
		
		

		
			if(MyTrigger.gameObject==mivirus){

				mivirus.GetComponent<InteligenciaVirus>().vida-=daño;
			}
		
	}
	
	
}
