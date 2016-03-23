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
	public bool llevarBase=false;
	
	public float vida=1000;
	//Si esta en colision
	private bool enColision;
	public Texture2D imagen;
	public Rect r;
	public GameObject mivirus;
	public GameObject ayudador;
	public bool esperando_ayudador;
	public float daño=0.8f;

	public bool desafio_macrofago;

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

		if (mivirus != null) {
			
			mivirus.transform.localPosition=new Vector3(0,0,0);
			
		}

		
		if (vida <= 0) {

			if(mivirus!=null)
				ManejadorVirus.numeroVirus-=(transform.childCount-3);


			if(isSeleted==true)
				Fondo1.seleccionada=false;
			ControladorRecursos.defensas--;
			Destroy(this.gameObject);
			
		}



		if (speed == 0.5f) {
		
			if(mivirus==null){

				llevarBase=false;
				enColision=false;
				daño=0.8f;
				destino=this.transform.position;
				GetComponent<FuncionesMacrofago>().enabled=true;
				if(desafio_macrofago)
					NotificationCenter.DefaultCenter().PostNotification(this,"CambiarGuiaMacrofago",4);

				speed=3f;
				animator.enabled=false;
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
				
				
				if(ayudador!=null&& llevarBase==true){
					
					esperando_ayudador=true;
					ayudador.GetComponent<TCD4>().ayudado=this.gameObject;
					NotificationCenter.DefaultCenter().PostNotification(this,"desactivarMiraMA");
				}
				
			}
			
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
			
			

			destino = new Vector3 (Fondo1.puntoDestino.x, Fondo1.puntoDestino.y, -5f);
			isSeleted = false;
			Fondo1.seleccionada=false;

			if(desafio_macrofago)
				NotificationCenter.DefaultCenter().PostNotification(this,"CambiarGuiaMacrofago",2);
			
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

		if(MyTrigger.gameObject.name.Equals("Bacteria(Clone)")){

			if(enColision==false&&mivirus==null){
				
				
				destino=new Vector3(47.8f ,-22.2f  ,-10f  );
				speed=0.5f;
				enColision = true;
				llevarBase=true;
				// Si no esta capturado
				if (mivirus==null&&MyTrigger.gameObject.GetComponent<BacteriaColis>().capturado == false) {
					
					mivirus=MyTrigger.gameObject;
					
					mivirus.gameObject.name="capturado";
					mivirus.transform.parent=this.transform;
					mivirus.transform.localPosition=new Vector3(-0.75f,0,0);
					mivirus.GetComponent<BacteriaMov>().speed=0;
					mivirus.GetComponent<BacteriaMov>().CancelInvoke();
					mivirus.GetComponent<BacteriaDisparar>().CancelInvoke();
					mivirus.GetComponent<BacteriaDisparar>().enabled=false;
					mivirus.GetComponent<BacteriaMov>().enabled=false;
					mivirus.GetComponent<BacteriaColis>().capturado=true;
					//mivirus.GetComponent<BacteriaColis>().enabled=false;

				}
				
			}

		}
		
		if (MyTrigger.gameObject.name.Equals ("VirusFinal(Clone)") || 
		    MyTrigger.gameObject.name.Equals ("VirusFinalCelula(Clone)")) {
			
			if(enColision==false&&mivirus==null){
				

				destino=new Vector3(47.8f ,-22.2f  ,-10f  );
				speed=0.5f;
				enColision = true;
				llevarBase=true;
				// Si no esta capturado
				if (mivirus==null&&MyTrigger.gameObject.GetComponent<InteligenciaVirus>().capturado == false) {

					mivirus=MyTrigger.gameObject;

					mivirus.gameObject.name="capturado";
					mivirus.transform.parent=this.transform;
					mivirus.transform.localPosition=new Vector3(0,0,0);
					mivirus.GetComponent<InteligenciaVirus>().capturado=true;
					mivirus.GetComponent<InteligenciaVirus>().speed=0;
					mivirus.GetComponent<ColisionesVirus>().enabled=false;

					NotificationCenter.DefaultCenter().PostNotification(this,"MacrofagoTutorial",true);
					if(desafio_macrofago)
						NotificationCenter.DefaultCenter().PostNotification(this,"CambiarGuiaMacrofago",3);

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
		
		

		if (mivirus != null) {

			if(MyTrigger.gameObject==mivirus){

				if(mivirus.GetComponent<InteligenciaVirus>()!=null)
					mivirus.GetComponent<InteligenciaVirus>().vida-=daño;
				else
					mivirus.GetComponent<BacteriaColis>().vida-=daño;
				mivirus.GetComponentInChildren<BarraVida>().modificarSprite();
				vida-=0.05f;
			}
		}
			
		
	}
	
	
}
