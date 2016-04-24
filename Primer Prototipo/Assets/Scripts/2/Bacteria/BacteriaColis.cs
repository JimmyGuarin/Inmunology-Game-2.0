using UnityEngine;
using System.Collections;

public class BacteriaColis : MonoBehaviour {

	public float vida;
	public int aleatorio;
	public bool comiendo;
	public bool capturado;
	public int ataque;
	// Use this for initialization
	void Start () {
	
		vida = 1000;
		aleatorio = 0;
		comiendo = false;
		capturado = false;
		this.gameObject.name="Bacteria(Clone)";
	}
	
	// Update is called once per frame
	void Update () {
	

		// Su vida se agoto ?
		if (vida <= 0) {
			
			ControladorRecursos.puntaje+=40;
			ManejadorVirus.numeroVirus--;
			Debug.Log("virusMuerto...Virus: "+ManejadorVirus.numeroVirus);
            ControladorBacterias.disminuir();
			Destroy(this.gameObject);
			
		}

        if (transform.parent == null)
            capturado = false;


	}


	void OnTriggerEnter (Collider MyTrigger) {
	
	if (capturado == false) {

			if (MyTrigger.gameObject.tag.Equals ("bacteria")) {
				aleatorio = Random.Range (1, 4);
			}

			if (MyTrigger.gameObject.tag.Equals ("celula")) {
				comiendo = true;
				this.transform.parent = null;
				GetComponent<BacteriaMov> ().enabled = true;
				GetComponent<BacteriaMov> ().CancelInvoke ();
				GetComponent<BacteriaMov> ().enabled = false;
				despegar();
			}


			if (MyTrigger.gameObject.name.Equals ("Neutrofilo(Clone)")) {
			
				if (this.gameObject.GetComponent<BacteriaColis> ().capturado == false &&
					MyTrigger.GetComponent<ParticleSystem> ().enableEmission == false&&
				    MyTrigger.GetComponent<ManejarNeutrofilo>().mivirus==null) {
				
					//transform.position = MyTrigger.gameObject.transform.position;
					GetComponent<BacteriaColis> ().capturado = true;
				}
			}


			if (MyTrigger.gameObject.name.Equals ("Macrofago(Clone)")) {
			
				if (this.gameObject.GetComponent<BacteriaColis> ().capturado == false) {

					//transform.position = MyTrigger.gameObject.transform.position;
					GetComponent<BacteriaColis> ().capturado = true;
				}
			}
			// Colisionado con celula denditrica ?
			if (MyTrigger.gameObject.name.Equals ("Dentrica(Clone)")) {
			
			
				if (this.gameObject.GetComponent<BacteriaColis> ().capturado == false) {
				
						
					if(transform.childCount>3){
						int hijos=transform.childCount;
						int index=hijos-3;
						while(hijos>3){
							index=hijos-1;
							transform.GetChild(index).GetComponent<BacteriaColis>().despegar2();
							hijos--;
						}

					}
					GetComponent<Animator>().enabled=false;
					MyTrigger.GetComponent<CrearUnidadInnata>().enColision=true;
					NotificationCenter.DefaultCenter ().PostNotification (this, "llevarABase", this.transform.position);
					GetComponent<BacteriaMov> ().enabled = false;
					GetComponent<BacteriaMov> ().comiendo = false;
					GetComponent<BacteriaColis> ().capturado = true;
					//transform.position=MyTrigger.gameObject.transform.position;
					this.transform.parent = MyTrigger.gameObject.transform;
					transform.localPosition = new Vector3 (-1.5f,1.5f,0.25f);
					GetComponent<BacteriaMov> ().speed = 0;
					GetComponent<BacteriaMov> ().CancelInvoke ();
					GetComponent<BacteriaDisparar> ().CancelInvoke ();
					GetComponent<BacteriaDisparar> ().enabled = false;
					GetComponent<BacteriaMov> ().enabled = false;

					GetComponent<BacteriaColis> ().CancelInvoke();
					GetComponent<BacteriaColis> ().enabled = false;
					GetComponent<Collider> ().enabled = false;

				}
			}

		}

	}
	void OnCollisionEnter(Collision colision) {

		if (colision.collider.name.Equals ("balaLifoncitoB(Clone)")) {
			this.gameObject.GetComponent<BacteriaColis> ().vida -= 200;
			GetComponentInChildren<BarraVida>().modificarSprite();
			colision.gameObject.GetComponent<Rigidbody> ().isKinematic = true;
			colision.gameObject.GetComponent<Collider> ().enabled = false;
			colision.gameObject.GetComponent<Bala> ().CancelInvoke ();
			colision.gameObject.GetComponent<Bala> ().enabled = false;
			colision.gameObject.transform.parent = this.transform;
		
			Debug.Log("bala");
		}

	}

	void OnTriggerStay(Collider MyTrigger){
	
		if (capturado == false) {
		

			//NET.................................................

			if (MyTrigger.gameObject.name.Equals ("Net(Clone)")) {

				float xAux = MyTrigger.gameObject.transform.position.x;
				float yAux = MyTrigger.gameObject.transform.position.y;
				float zAux = MyTrigger.gameObject.transform.position.z;
				GetComponent<BacteriaMov> ().enabled = true;
				GetComponent<BacteriaMov> ().destino = new Vector3 (Random.Range (xAux - 1, xAux + 1), Random.Range (yAux - 2, yAux + 2), zAux);
				vida -= MyTrigger.GetComponent<Net> ().daño;
				GetComponentInChildren<BarraVida> ().modificarSprite ();
			}


			//LINFOCITO B...........................................

			if (MyTrigger.gameObject.name.Equals ("LinfoncitoB(Clone)")) {

				Debug.Log ("toca");
				MyTrigger.GetComponent<LinfocitoB> ().vida -= ataque;

				if (MyTrigger.GetComponent<LinfocitoB> ().vida <= 0) {

					if(MyTrigger.GetComponent<LinfocitoB>().isSeleted==true)
						Fondo1.seleccionada=false;
					DestruirDefensa (MyTrigger.gameObject);
				}
			}

			//LINFOCITO B MEJORADO.................................
			if (MyTrigger.gameObject.name.Equals ("linfocitoB(Clone)")) {
			
				Debug.Log ("toca");
				MyTrigger.GetComponent<LinfocitoB2> ().vida -= ataque;
			
				if (MyTrigger.GetComponent<LinfocitoB2> ().vida <= 0) {
				
					if(MyTrigger.GetComponent<LinfocitoB2>().isSeleted==true)
						Fondo1.seleccionada=false;

					DestruirDefensa (MyTrigger.gameObject);
				}
			}

			//LINFOCITO TCD4...........................................
		
			if (MyTrigger.gameObject.name.Equals ("LinfoncitoTCD4(Clone)")) {
			
				MyTrigger.GetComponent<TCD4> ().vida -= ataque;
				if (MyTrigger.GetComponent<TCD4> ().vida <= 0) {
				
					DestruirDefensa (MyTrigger.gameObject);
				}
			}

			//LINFOCITO TCD8...........................................
		
			if (MyTrigger.gameObject.name.Equals ("LinfoncitoTCD8(Clone)")) {
			
				MyTrigger.GetComponent<TCD8> ().vida -= ataque;
				if (MyTrigger.GetComponent<TCD8> ().vida <= 0) {
				
					DestruirDefensa (MyTrigger.gameObject);
				}
			}


		}
	
	
	}

	void OnTriggerExit (Collider MyTrigger) {

		if (MyTrigger.gameObject.tag.Equals ("bacteria")) {
		
			if(aleatorio==1&&comiendo==false&&MyTrigger.gameObject.GetComponent<BacteriaColis>().aleatorio!=1){

				unir(MyTrigger.gameObject,this.gameObject);

			}
			if(this.gameObject.transform.parent==null&&comiendo==false) 
				this.gameObject.GetComponent<BacteriaMov>().enabled=true;
			aleatorio=0;
		}



	}

	public void unir(GameObject bacteria, GameObject padre){
	
		if (padre != null && bacteria != null) {

			if (bacteria.transform.parent != null)
				unir (bacteria.transform.parent.gameObject, padre);
			else {
				bacteria.transform.parent = padre.transform;
				bacteria.gameObject.GetComponent<BacteriaMov> ().CancelInvoke ("multiplicar");
				bacteria.gameObject.GetComponent<BacteriaMov> ().enabled = false;
				bacteria.gameObject.GetComponent<BacteriaColis> ().despegar ();
		
			}
		}
	}

	public void DestruirDefensa(GameObject muerto){
	

			ControladorRecursos.defensas--;
			Destroy (muerto);

	}


	public void despegar(){

		Invoke("despegar2",Random.Range(10,20));

	}

	public void despegar2(){

		if (comiendo == false) {
			transform.parent = null;
			GetComponent<BacteriaMov> ().enabled = true;
			GetComponent<BacteriaMov> ().Start ();
		} else
			despegar ();
	}
}
