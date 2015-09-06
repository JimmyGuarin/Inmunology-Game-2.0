using UnityEngine;
using System.Collections;

public class virus : MonoBehaviour {


	public float speed=3f;
	private Vector3 destino;
	public bool capturado;
	public int vida;
	//ultima mutacion
	public bool ultima;
	private int multipicarse;
	public GameObject gemelo;
	public int dañoKiller;

	// Use this for initialization
	void Start () {

		ManejadorVirus.numeroVirus++;
		Debug.Log (ManejadorVirus.numeroVirus);
		NotificationCenter.DefaultCenter ().AddObserver (this, "atrapado");
		ultima = false;
		capturado=false;
		vida = 1000;
		destino = new Vector3 (Random.Range (-46f,-28f), Random.Range (-19f, 28f),-5f);


			
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
			this.transform.position=destino;
			destino = new Vector3 (Random.Range (-48f,-28f), Random.Range (-20f, 28f), -5f);


		} else {


			float step = speed * Time.deltaTime;
			transform.position = Vector3.MoveTowards (transform.position, destino, step);

		}



	}

	void ultimaMutacion(){

		ultima = true;
	}


	void OnCollisionEnter(Collision colision) {
		

		if(colision.collider.name.Equals("BalaVaso(Clone)")||colision.collider.name.Equals("balaLifoncitoB(Clone)"))
		{
	

			vida-=100;
			BroadcastMessage("ChangeTheDamnSprite");
		}
		
		
	}
	
	void OnTriggerEnter (Collider MyTrigger) {


		if (MyTrigger.gameObject.name.Equals ("Dentrica(Clone)")) {


			if (capturado == false) {
			
				if (ultima == true) {
					capturado = true;
					Destroy(this.GetComponent<Collider>());
					NotificationCenter.DefaultCenter ().PostNotification (this, "llevarABase", this.transform.position);
				} else {


				}
			}
		}

	}
		
	void atrapado(Notification notification){

	
		if (capturado==true) {
			speed = 6f;
			destino = new Vector3 (47.8f, -25.2f, -5f);
		} else {


		}
	}

	void OnTriggerStay(Collider MyTrigger) {
		if (MyTrigger.gameObject.name.Equals ("NaturalKiller(Clone)")) {
		
			dañoKiller++;
			if(dañoKiller==10){

				dañoKiller=0;
				vida-=100;
				BroadcastMessage("ChangeTheDamnSprite");
			}
			
		}

	}
}
