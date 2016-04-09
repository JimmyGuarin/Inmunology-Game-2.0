using UnityEngine;
using System.Collections;

public class ManejarNeutrofilo : MonoBehaviour {

	public  bool isSeleted;
	public Vector3 subir;
	public float life=500;
	public Texture2D imagen;
	private Rect r;
	public bool llevarBase;
	public float speed=4;
	public bool enColision;
	public GameObject mivirus;
	public GameObject ayudador;
	public bool esperando_ayudador;
	public float daño_a_virus=2.5f;

	//Neutrofilo de desafio neutrofilo 
	public bool desafio_neutrofilo;

	private Vector3 posicion_capturado;

	// Use this for initialization
	void Start () {
	
		esperando_ayudador = false;
		llevarBase = false;
		ControladorRecursos.defensas++;
		isSeleted = false;
		subir = new Vector3(MoverPuntoEncuentro.posicion.x+Random.Range(-3,3),MoverPuntoEncuentro.posicion.y+Random.Range(-3,3),-5f);

		NotificationCenter.DefaultCenter ().AddObserver (this, "cambiarPosCelula");
		NotificationCenter.DefaultCenter ().AddObserver (this, "llevarABase");
		NotificationCenter.DefaultCenter ().AddObserver (this, "activarMiraNE");
		NotificationCenter.DefaultCenter ().AddObserver (this, "desactivarMiraNE");

		imagen = Resources.Load ("Neutrofilo4") as Texture2D;

		if(Application.loadedLevelName.Equals ("2"))
			posicion_capturado=new Vector3(-0.75f,0,0);
		else 
			posicion_capturado=new Vector3(0,0,1);

	}
	
	// Update is called once per frame
	void Update () {


		if (mivirus != null) {
		

			mivirus.transform.localPosition=posicion_capturado;
		
		}

		if (speed == 0.5f) {
			
			if(mivirus==null){
				
				llevarBase=false;
				enColision=false;
				subir=this.transform.position;
				speed=8f;

			}
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
					NotificationCenter.DefaultCenter().PostNotification(this,"desactivarMiraNE");
				}
				
			}
			
		}



		float step = speed* Time.deltaTime;

		if (llevarBase == true && transform.position == subir) {

			ControladorRecursos.defensas--;
			Destroy(this.gameObject);
			ControladorRecursos.puntaje += 50;
			ManejadorVirus.numeroVirus--;
		}

		if (transform.position != subir) {

			this.transform.position = Vector3.MoveTowards (transform.position, subir, step);
		}

		if (life <= 0) {

			if(isSeleted==true)
				Fondo1.seleccionada=false;
			ControladorRecursos.defensas--;
			if(mivirus!=null)
				ManejadorVirus.numeroVirus-=(transform.childCount-3);
			Destroy(this.gameObject);

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

			subir = new Vector3 (Fondo1.puntoDestino.x, Fondo1.puntoDestino.y, -5f);
			isSeleted = false;
			transform.FindChild("seleccionada").gameObject.SetActive(false);
			Fondo1.seleccionada=false;

			if(desafio_neutrofilo==true)
				NotificationCenter.DefaultCenter().PostNotification(this,"CambiarGuiaNeutrofilo",2);
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


		if (MyTrigger.gameObject.name.Equals ("Bacteria(Clone)")) {
		
			if(GetComponent<ParticleSystem>().enableEmission==false&&llevarBase==false&&mivirus==null){

				subir=new Vector3(47.8f ,-22.2f  ,-10f  );
				speed=0.5f;
				enColision = true;
				llevarBase=true;
				// Si no esta capturado
				if (mivirus==null&&MyTrigger.gameObject.GetComponent<BacteriaColis>().capturado == false) {
					
					mivirus=MyTrigger.gameObject;
					mivirus.gameObject.name="capturado";
					mivirus.transform.parent=this.transform;
					mivirus.transform.localPosition=posicion_capturado;
					mivirus.GetComponent<BacteriaMov>().speed=0;
					mivirus.GetComponent<BacteriaMov>().CancelInvoke();
					mivirus.GetComponent<BacteriaDisparar>().CancelInvoke();
					mivirus.GetComponent<BacteriaDisparar>().enabled=false;
					mivirus.GetComponent<BacteriaMov>().enabled=false;
					mivirus.GetComponent<BacteriaColis>().capturado=true;
					//mivirus.GetComponent<Collider>().enabled=false;
				}


			}

		}



		if (MyTrigger.gameObject.name.Equals ("VirusFinal(Clone)") || 
			MyTrigger.gameObject.name.Equals ("VirusFinalCelula(Clone)")) {

			if(GetComponent<ParticleSystem>().enableEmission==false&&llevarBase==false&&mivirus==null){

			
				subir=new Vector3(47.8f ,-22.2f  ,-10f  );
				//GetComponent<FuncionesNeutrofilo>().enabled=false;
				speed=0.5f;
				enColision = true;
				llevarBase=true;
				// Si no esta capturado
				if (mivirus==null&&MyTrigger.gameObject.GetComponent<InteligenciaVirus>().capturado == false) {

					mivirus=MyTrigger.gameObject;
					mivirus.gameObject.name="capturado";
					mivirus.transform.parent=this.transform;
					mivirus.transform.localPosition=new Vector3(0,0,1);
					mivirus.GetComponent<InteligenciaVirus>().speed=0;
					//mivirus.GetComponent<InteligenciaVirus>().enabled=false;
					mivirus.GetComponent<ColisionesVirus>().enabled=false;
					//mivirus.GetComponent<Collider>().enabled=false;
				}

			}
		}

		if (MyTrigger.gameObject.name.Equals ("LinfoncitoTCD4(Clone)")) {
			
			if(esperando_ayudador==true){
					

					llevarBase=false;
					Debug.Log("elimina");
					subir=transform.position;
					speed=8f;
					esperando_ayudador=false;
				if(mivirus.GetComponent<InteligenciaVirus>()!=null){
					mivirus.GetComponent<InteligenciaVirus>().enabled=true;
					mivirus.GetComponent<InteligenciaVirus>().vida=0;
					NotificationCenter.DefaultCenter().PostNotification(this,"TCD4Tutorial",2);
				}
				else
					mivirus.GetComponent<BacteriaColis>().vida=0;
					
				GetComponent<FuncionesNeutrofilo>().enabled=true;

			}		
		}
			
	}

	void OnTriggerStay (Collider MyTrigger) {

		if (MyTrigger.gameObject.name.Equals ("VirusFinal(Clone)")|| 
		    MyTrigger.gameObject.name.Equals ("VirusFinalCelula(Clone)")) {
				  
			if(GetComponent<ParticleSystem>().enableEmission==true){
	

				MyTrigger.GetComponent<InteligenciaVirus> ().vida-=daño_a_virus;
				MyTrigger.GetComponentInChildren<BarraVida>().modificarSprite();
				life-=1;

			}
		}
		if (MyTrigger.gameObject.name.Equals ("Bacteria(Clone)")) {
			
			if(GetComponent<ParticleSystem>().enableEmission==true){
				
				
				MyTrigger.GetComponent<BacteriaColis> ().vida-=2.5f;
				MyTrigger.GetComponentInChildren<BarraVida>().modificarSprite();
				life-=1;
				
			}
		}


		if (mivirus != null) {
			
			if(MyTrigger.gameObject==mivirus){
				
				if(mivirus.GetComponent<InteligenciaVirus>()!=null)
					mivirus.GetComponent<InteligenciaVirus>().vida-=0.2f;
				
				else
					mivirus.GetComponent<BacteriaColis>().vida-=0.2f;
				mivirus.GetComponentInChildren<BarraVida>().modificarSprite();
				life-=0.05f;
			}
		}



		if (MyTrigger.gameObject.name.Equals ("NaturalKiller(Clone)")) {
			
			life-=2f;
		
		}
   }


	void OnTriggerExit(Collider MyTrigger){



		enColision = false;
	}

	void OnCollisionEnter(Collision colision) {
		
		if (colision.collider.name.Equals ("BacteriaBala(Clone)")) {
			life -= 200;
			Destroy(colision.gameObject);

			Debug.Log("bala Neutrogilo");
		}
		
	}
}
