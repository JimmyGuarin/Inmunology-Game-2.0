using UnityEngine;
using System.Collections;

public class InteligenciaVirus : MonoBehaviour {

	public float speed=2f;
	public Vector3 destino;
	public bool capturado;
	public int vida;
	//ultima mutacion
	public bool ultima;
	public GameObject fracturado;
	public bool comiendo;
	public int  celulaObjetivo;

	// Use this for initialization 
	public void Start () {



		comiendo = false;
		ManejadorVirus.numeroVirus++;
		Debug.Log (ManejadorVirus.numeroVirus);
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
		
		
		if (vida <= 0) {
			
			CancelInvoke();
			ControladorRecursos.puntaje+=40;
			ManejadorVirus.numeroVirus--;
			Destroy(this.gameObject);
			
			
			
		}
		
		if (this.transform.position.Equals (destino)) {


			if(capturado==true){
				
				if(ManejadorVirus.analizado==false){
					
					ControladorRecursos.virusAnalizado();
					ManejadorVirus.analizado=true;
				}
				ControladorRecursos.puntaje+=50;
				ManejadorVirus.numeroVirus--;
				Debug.Log(ManejadorVirus.numeroVirus);
				Destroy(this.gameObject);
			}

			if(comiendo==false&&capturado==false){



				Debug.Log ("linea de defenza" + ManejadorVirus.lineaDefenza);
				ManejadorVirus.actualizarDefenza ();
				if(ManejadorVirus.lineaDefenza==0)
					Destroy(this);
				else{
					ManejadorVirus.actualizarDefenza ();
					if(this.gameObject!=null)
					destino=buscarObjetivo(this.gameObject.transform.position);
				}



			}
			
		} else {

			float step = speed * Time.deltaTime;
			transform.position = Vector3.MoveTowards (transform.position, destino, step);

			if(comiendo==true&&ManejadorVirus.celulas[celulaObjetivo]==null){

				destino=transform.position;
				comiendo=false;
				Debug.Log("no vaya");
			}


			
		}
		
		
		
	}
	
	void ultimaMutacion(){
		
		ultima = true;
	}
	
	
	void OnCollisionEnter(Collision colision) {
		
		
		if(colision.collider.name.Equals("balaLifoncitoB(Clone)"))
		{
			vida-=100;
			BroadcastMessage("ChangeTheDamnSprite");
			colision.gameObject.GetComponent<Rigidbody>().isKinematic=true;
			colision.gameObject.GetComponent<Collider>().enabled=false;
			colision.gameObject.GetComponent<Bala>().CancelInvoke();
			colision.gameObject.GetComponent<Bala>().enabled=false;
			colision.gameObject.transform.parent=this.transform;


		}
		
		
	}
	
	void OnTriggerEnter (Collider MyTrigger) {
		
		
		if (MyTrigger.gameObject.name.Equals ("Dentrica(Clone)")) {
			
			
			if (capturado == false) {
				

					capturado = true;
					Destroy(this.GetComponent<Collider>());
					Instantiate(fracturado,this.transform.position,fracturado.transform.rotation);
					Destroy(this.gameObject);
					NotificationCenter.DefaultCenter ().PostNotification (this, "llevarABase", this.transform.position);
			}
		}
		if (MyTrigger.gameObject.tag.Equals ("celula")) {

			destino=transform.position;
			for(int i=0;i<ManejadorVirus.celulas.Length;i++){
				
				if(ManejadorVirus.celulas[i]!=null){
					if(MyTrigger.gameObject.name.Equals(ManejadorVirus.celulas[i].name)){
						
						celulaObjetivo=i;
						break;
					}
				}
			}
			Debug.Log("comiendo"+celulaObjetivo);
		}
	}

	void OnTriggerStay (Collider MyTrigger) {

		if (MyTrigger.gameObject.tag.Equals("celula")) {

			comiendo=true;
			
		}
		if (MyTrigger.gameObject.tag.Equals("muerta")&&comiendo==true) {

			comiendo=false;
			destino=transform.position;
			Debug.Log("La mate");
			
		}
		if (MyTrigger.gameObject.name.Equals ("Dentrica(Clone)")) {
			
			
			if (capturado == false) {
				
					

					capturado = true;
					Destroy(this.GetComponent<Collider>());

					NotificationCenter.DefaultCenter ().PostNotification (this, "llevarABase", this.transform.position);
			}
		}

	}
	void OnTriggerExit (Collider MyTrigger) {
		

		Debug.Log("No esta colisionando");
		comiendo = false;
		destino=transform.position;
	}


	public void atrapado(Notification notification){
		
		
		if (capturado==true) {
			speed = 6f;
			destino = (Vector3)notification.data;
		} else {
			
			
		}
	}

	public  Vector3 buscarObjetivo(Vector3 viru){
		
		int celula = 0;
		ManejadorVirus.actualizarDefenza ();
		
		if (ManejadorVirus.lineaDefenza == 1) {
			
			float a = 100000;
			celula=11;
			for (int i=0; i<=2; i++) {

				if (ManejadorVirus.celulas [i] != null) {

					float b = Mathf.Abs (Vector3.Distance (viru, ManejadorVirus.celulas [i].transform.position));
					if (b <= a) {

						a = b;
						celula = i;
					}

				}
			}

			celulaObjetivo = celula;
			return ManejadorVirus.celulas [celula].transform.position;

		}
		if (ManejadorVirus.lineaDefenza == 2) {
			

			float a = 1000;
			
			for (int i=3; i<=6; i++) {
				
				if (ManejadorVirus.celulas [i] != null) {

					float b = Mathf.Abs (Vector3.Distance (viru, ManejadorVirus.celulas [i].transform.position));
					if (b <= a) {
					
						a = b;
						celula = i;
					}
				}
			}

			celulaObjetivo = celula;
			
			return ManejadorVirus.celulas [celula].transform.position;
		}
		
		if (ManejadorVirus.lineaDefenza == 3) {

			float a = 1000;

			Debug.Log ("Entro a linea 3");
			for (int i=0; i<=11; i++) {
					
				if (ManejadorVirus.celulas [i] != null && ManejadorVirus.celulas [i].gameObject.tag.Equals ("celula")) {
						
					float b = Mathf.Abs (Vector3.Distance (viru, ManejadorVirus.celulas [i].transform.position));
					Debug.Log("distancia b"+b);
					if (b < a) {
							
						a = b;
						Debug.Log ("Voy a una" + ManejadorVirus.celulas [i].gameObject.tag);	
						celula = i;
					}
				}
			}
				
			celulaObjetivo = celula;
			return ManejadorVirus.celulas [celula].transform.position;

		}

			Debug.Log ("a 0 por que" + ManejadorVirus.lineaDefenza);
			return new Vector3 (0, 0, 0);


	}

}

