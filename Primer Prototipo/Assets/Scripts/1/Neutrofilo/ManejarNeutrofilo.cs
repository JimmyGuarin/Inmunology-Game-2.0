using UnityEngine;
using System.Collections;

public class ManejarNeutrofilo : MonoBehaviour {

	public  bool isSeleted;
	public Vector3 subir;
	public float life=500;
	public Texture2D imagen;
	public static int seleccionadas=0;
	private Rect r;
	public int ubicada;
	public bool llevarBase;
	public float speed;
	public bool enColision;
	public GameObject mivirus;
	public GameObject ayudador;
	public bool esperando_ayudador;

	// Use this for initialization
	void Start () {
	
		esperando_ayudador = false;

		speed = 4;
		llevarBase = false;
		ControladorRecursos.defensas++;
		life = 500;
		isSeleted = false;
		subir = new Vector3(MoverPuntoEncuentro.posicion.x,MoverPuntoEncuentro.posicion.y,-5f);

		NotificationCenter.DefaultCenter ().AddObserver (this, "cambiarPosCelula");
		NotificationCenter.DefaultCenter ().AddObserver (this, "llevarABase");
		NotificationCenter.DefaultCenter ().AddObserver (this, "activarMiraNE");
		NotificationCenter.DefaultCenter ().AddObserver (this, "desactivarMiraNE");

		imagen = Resources.Load ("Neutrofilo4") as Texture2D;


	}
	
	// Update is called once per frame
	void Update () {



	
		//Click Derecho
		if (Input.GetMouseButtonDown (1)) {
			
			
			Ray pulsacion;
			RaycastHit hit;
			pulsacion = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (pulsacion, out hit) && hit.collider == this.GetComponent<Collider>()&&
			    mivirus==null) {
				


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

		//Click Izquierdo
		if(Input.GetMouseButtonDown (0)) {
			
			
			Ray pulsacion;
			RaycastHit hit;
			pulsacion = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (pulsacion, out hit) && hit.collider == this.GetComponent<Collider>()) {


				if(ayudador!=null&& this.GetComponent<FuncionesNeutrofilo>().enabled==false){
					
					esperando_ayudador=true;
					ayudador.GetComponent<TCD4>().ayudado=this.gameObject;
					NotificationCenter.DefaultCenter().PostNotification(this,"desactivarMiraNE");
				}
				
			}
			
		}



		float step = speed* Time.deltaTime;

		if (llevarBase == true && transform.position == subir) {

			ControladorRecursos.defensas--;

			ManejadorVirus.numeroVirus--;
			Debug.Log("virus"+ManejadorVirus.numeroVirus);
			ControladorRecursos.puntaje+=300;
			Destroy(this.gameObject);
		
		}

		if (transform.position != subir) {

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


	void activarMiraNE(Notification notification){
		
		if (mivirus != null) {

			this.transform.FindChild ("mira").gameObject.SetActive (true);
			ayudador = (GameObject) notification.data;
		}

	}
	
	void desactivarMiraNE(Notification notification){
		


			this.transform.FindChild ("mira").gameObject.SetActive (false);
			ayudador = null;


	}


	void OnTriggerEnter(Collider MyTrigger){
	
		if (MyTrigger.gameObject.name.Equals ("VirusFinal(Clone)") || 
			MyTrigger.gameObject.name.Equals ("VirusFinalCelula(Clone)")) {

			if(GetComponent<ParticleSystem>().enableEmission==false&&
			   enColision==false&&mivirus==null){

			
				subir=new Vector3(47.8f ,-22.2f  ,-10f  );
				GetComponent<FuncionesNeutrofilo>().enabled=false;
				speed=0.5f;
				enColision = true;
				llevarBase=true;
				// Si no esta capturado
				if (mivirus==null&&MyTrigger.gameObject.GetComponent<InteligenciaVirus>().capturado == false) {

					mivirus=MyTrigger.gameObject;
					mivirus.GetComponent<InteligenciaVirus>().capturado=true;
					mivirus.GetComponent<InteligenciaVirus>().speed=0.5f;
					mivirus.GetComponent<InteligenciaVirus>().destino=new Vector3(47.8f ,-22.2f  ,-10f  );
					mivirus.GetComponent<Collider>().enabled=false;
				}
				mivirus.transform.position=transform.position;
			}
		}

		if (MyTrigger.gameObject.name.Equals ("LinfoncitoTCD4(Clone)")) {
			
			if(esperando_ayudador==true){
					

					llevarBase=false;
					Debug.Log("elimina");
					ControladorRecursos.puntaje+=40;
					subir=transform.position;
					speed=4f;
					esperando_ayudador=false;
					mivirus.GetComponent<InteligenciaVirus>().vida=0;
					GetComponent<FuncionesNeutrofilo>().enabled=true;
					mivirus=null;
					

			}		
		}
			
	}

	void OnTriggerStay (Collider MyTrigger) {

		if (MyTrigger.gameObject.name.Equals ("VirusFinal(Clone)")|| 
		    MyTrigger.gameObject.name.Equals ("VirusFinalCelula(Clone)")) {
				  
			if(GetComponent<ParticleSystem>().enableEmission==true){
	

				MyTrigger.GetComponent<InteligenciaVirus> ().vida-=2.5f;
				life-=1;

			}



		}

	



		if (MyTrigger.gameObject.name.Equals ("NaturalKiller(Clone)")) {
			
			life-=2f;
		
		}
   }


	void OnTriggerExit(){

		enColision = false;
	}
}
