using UnityEngine;
using System.Collections;

public class InteligenciaVirus : MonoBehaviour {

	public float speed=2f;
	public Vector3 destino;
	public bool capturado;
	public float vida;
	//ultima mutacion
	public bool ultima;
	public GameObject fracturado;
	public bool comiendo;
	public int  celulaObjetivo;

	// Use this for initialization 
	public void Start () {


		speed = 2f;
		comiendo = false;
		ManejadorVirus.numeroVirus++;
		Debug.Log ("virus:"+ManejadorVirus.numeroVirus);
		NotificationCenter.DefaultCenter ().AddObserver (this, "atrapado");
		ultima = false;
		capturado=false;
		vida = 1000;

		if (this.gameObject.name.Equals ("VirusFinal(Clone)")) {
			destino = new Vector3 (Random.Range (this.transform.position.x - 6, this.transform.position.x + 10), Random.Range (-22f, 28f), -5f);
		} else {
			destino = new Vector3 (Random.Range (this.transform.position.x - 20, this.transform.position.x + 20), Random.Range (-22f, 28f), -5f);
		}

		
		
		
	}
	
	// Update is called once per frame
	void Update () {
		

		// Su vida se agoto ?
		if (vida <= 0) {

			ControladorRecursos.puntaje+=40;
			ManejadorVirus.numeroVirus--;
			Debug.Log("virus: "+ManejadorVirus.numeroVirus);
			Destroy(this.gameObject);
		
		}


		if (this.transform.position.Equals (destino)) { //llego a su destino ?


			if (capturado == true) { // Esta capturado ? 
				
				if (ManejadorVirus.analizado == false) { // Ya se ha analizado el virus ?
					
					ControladorRecursos.virusAnalizado (); // analizar
					ManejadorVirus.analizado = true;  
				}
				ControladorRecursos.puntaje += 50;
				ManejadorVirus.numeroVirus--;
				
				Destroy (this.gameObject);
			}

			if (comiendo == false && capturado == false) { // No esta comiendo ni ha sido capturado ?

				ManejadorVirus.actualizarDefenza(); // actualizar la defenza ( celulas )
				if (this.gameObject != null){ // Si existo ?

					Vector3 destino1=destino;
					destino = buscarObjetivo (this.gameObject.transform.position); // Buscar otra celula para comer
					if(destino1==destino){
						Debug.Log ("Error");
						destino = buscarObjetivo (this.gameObject.transform.position); // Buscar otra celula para comer
					}


				}

			}
			
		} else { // No ha llegado a su destino 

			float step = speed * Time.deltaTime;
			transform.position = Vector3.MoveTowards (transform.position, destino, step);

			if (comiendo == true) { // Esta comiendo? ( sin llegar al destino )

				bool muerta = true;
				ManejadorVirus.actualizarDefenza();
				foreach (Celula i in ManejadorVirus.celulas) {
					
					if (i.m_identificador == celulaObjetivo) {

						muerta = false;
					}
					
				}
				if (muerta ==true) {
					destino = transform.position;
					comiendo = false;
				}
	
			}
		
		}
		
	}
	
	void ultimaMutacion(){
		
		ultima = true;
	}
	
	
	void OnCollisionEnter(Collision colision) {
		
		
		if(colision.collider.name.Equals("balaLifoncitoB(Clone)"))
		{
			vida-=200;
			colision.gameObject.GetComponent<Rigidbody>().isKinematic=true;
			colision.gameObject.GetComponent<Collider>().enabled=false;
			colision.gameObject.GetComponent<Bala>().CancelInvoke();
			colision.gameObject.GetComponent<Bala>().enabled=false;
			colision.gameObject.transform.parent=this.transform;


		}
		
		
	}
	
	void OnTriggerEnter (Collider MyTrigger) {

		if (MyTrigger.gameObject.name.Equals ("Net")) {
			
				

		}
		
		if (MyTrigger.gameObject.name.Equals ("Dentrica(Clone)")) {
			
			
			if (capturado == false) {
				

					capturado = true;
					
					Instantiate(fracturado,this.transform.position,fracturado.transform.rotation);
					Destroy(this.gameObject);
					NotificationCenter.DefaultCenter ().PostNotification (this, "llevarABase", this.transform.position);
			}
		}

		// Colisionado con celula denditrica ?
		if (MyTrigger.gameObject.name.Equals ("Neutrofilo(Clone)")) {

			if(capturado==false){

				transform.position=MyTrigger.gameObject.transform.position;
			}
				

		}

		if (MyTrigger.gameObject.tag.Equals ("celula")) {

			destino=transform.position;
			ManejarCelula mc=MyTrigger.GetComponent<ManejarCelula>();

			foreach (Celula i in ManejadorVirus.celulas_objetivos) {

				if(mc.c.m_identificador==i.m_identificador){
					celulaObjetivo=i.m_identificador;
					break;
				}
				
			}	

		}
	}

	void OnTriggerStay (Collider MyTrigger) {


		if (MyTrigger.gameObject.name.Equals ("Net(Clone)")) {


				
				float xAux=MyTrigger.gameObject.transform.position.x;
				float yAux=MyTrigger.gameObject.transform.position.y;
				float zAux=MyTrigger.gameObject.transform.position.z;
				
			destino= new Vector3(Random.Range(xAux-2,xAux+2),Random.Range(yAux-2,yAux+2),zAux);

			vida-=MyTrigger.GetComponent<Net>().daño;
		}

		if (MyTrigger.gameObject.tag.Equals("celula")) {

			comiendo=true;
			//Estoy comiendo

		}
		if (MyTrigger.gameObject.tag.Equals("muerta")&&comiendo==true) {

			comiendo=false;
			destino=transform.position;
			//Llegue y no estoy comiendo 
			
			
		}
		// Colisionado con celula denditrica ?
		if (MyTrigger.gameObject.name.Equals ("Neutrofilo(Clone)")) {

			if(MyTrigger.gameObject.GetComponent<ManejarNeutrofilo>().mivirus!=this.gameObject){

				if(capturado==true){

					speed=1f;
					capturado=false;
				}

			}
			
		}

		// Colisionado con celula denditrica ?
		if (MyTrigger.gameObject.name.Equals ("Dentrica(Clone)")) {
			
			// Si no esta capturado
			if (capturado == false) {

					capturado = true;
					Destroy(this.GetComponent<Collider>());

					NotificationCenter.DefaultCenter ().PostNotification (this, "llevarABase", this.transform.position);
			}
		}


	}

	// Ya no esta comiendo
	void OnTriggerExit (Collider MyTrigger) {
		


		comiendo = false;
		//destino=transform.position;
	}



	// El virus ha sido atrapado
	public void atrapado(Notification notification){
		
		
		if (capturado==true) {
			Destroy(this.GetComponent<Collider>());
			speed = 1f;
			destino = (Vector3)notification.data;
		} else {
			
			
		}
	}

	public  Vector3 buscarObjetivo(Vector3 viru){

		ManejadorVirus.actualizarDefenza ();
		Vector3 destino= new Vector3 (0, 0, 0);

			
			float a = 10000000;
			
		if (ManejadorVirus.celulas_objetivos.Count > 0) { // Quedan celulas ?
		
			ManejadorVirus.actualizarDefenza();
	
			for (int i=ManejadorVirus.celulas_objetivos.Count-1; i>=0; i--) { // recorrer las celulas
				
				Celula c=ManejadorVirus.celulas_objetivos[i] as Celula;
				float b = Mathf.Abs (Vector3.Distance (viru, c.m_posicion));
				if (b <= a) {
					
					destino=c.m_posicion;

					a = b;
					celulaObjetivo=c.m_identificador;
				}

			}

		} 
	
		return destino;
	}

}

