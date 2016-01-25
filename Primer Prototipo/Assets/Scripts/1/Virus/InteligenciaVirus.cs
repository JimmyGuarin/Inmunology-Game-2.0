using UnityEngine;
using System.Collections;

public class InteligenciaVirus : MonoBehaviour {

	public float speed=2f;
	public Vector3 destino;
	public bool capturado;
	public float vida;
	//ultima mutacion
	public bool ultima;
	public GameObject fracturado0;
	public GameObject fracturado1;
	public GameObject fracturado2;
	public GameObject fracturado3;
	public GameObject [] fracturados;
	public bool comiendo;
	public int  celulaObjetivo;



	// Use this for initialization 
	public void Start () {

		fracturados=new GameObject[]{fracturado0,fracturado1,fracturado2,fracturado3};
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
				comiendo=false;
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

