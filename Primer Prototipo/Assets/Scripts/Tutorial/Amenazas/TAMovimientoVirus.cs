using UnityEngine;
using System.Collections;

public class TAMovimientoVirus : MonoBehaviour {


	//Declaramos un vector que controle la gravedad, no usaremos la fisica de unity
	public Vector3 gravedad;
	//Declaramos un vector que define el salto (aleteo) del pajaro
	public Vector3 velocidadAleteo;
	//Declaramos si se debe aletear, si se toco la pantalla o se presiono espacio
	bool arriba = false;
	bool abajo = false;
	bool adelante = false;
	public Sprite celula_muerta;

	public float vida;

	public GameObject Fracturado;

	private bool atrapado;
	private int atrapadas;

	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update (){

		if (!atrapado) {

			//Si la persona presiona el boton de espacio o hace clic en la pantalla con el mouse
			if (Input.GetKeyDown("up")) {
				
				arriba=true;
					
			}

			if (Input.GetKeyDown("down")) {
				
				abajo=true;
				
			}
			if (Input.GetKey("right")) {
				
				adelante=true;
				
			}
		}

		if (vida <= 0) {

			GameObject.Find("Canvas").GetComponent<ManejadorTutorialAmenazas>().DesplegarVirus();
			Destroy(this.gameObject);
		}
		
		
	}
	
	//Este es el update de la fisica, que es ligeramente mas lento que el update del juego
	void FixedUpdate () {

		//A la velocidad le sumamos la gravedad (Para que el pajaro caiga)
		//velocidad += gravedad * Time.deltaTime;
		
		//Si presionaron espacio o hicieron clic
		if (arriba == true)
		{
			//Que solo sea una vez
			arriba = false;
			transform.Translate(Vector3.up*3);
		}
		if (abajo) {
		
			//Que solo sea una vez
			abajo = false;
			//El vector velocidad recibe el impulso hacia arriba al pajaro
			transform.Translate(Vector3.down*3);
		
		}

		if (adelante) {
			
			//Que solo sea una vez
			adelante = false;
			transform.Translate(Vector3.forward);
		}
		if (transform.position.y > 30) {

			transform.position=new Vector3(transform.position.x,30,-5);
		}
		if (transform.position.y < -30) {
			
			transform.position=new Vector3(transform.position.x,-30,-5);
		}

		//Hacemos que el pajaro reciba la velocidad (la gravedad lo hace caer mas rapido)
		//transform.position += velocidad * Time.deltaTime;
		transform.Translate (Vector3.forward * Time.deltaTime);
		transform.position = new Vector3 (transform.position.x, transform.position.y, -5);

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
				GetComponentInChildren<BarraVida>().modificarSpriteDesafioAmenaza();
		
		}
			
	}

	void OnTriggerEnter (Collider MyTrigger) {
		
		if(MyTrigger.name.Equals("celula")){

			atrapadas++;
			MyTrigger.GetComponent<Collider>().enabled=false;
			MyTrigger.GetComponent<SpriteRenderer>().sprite=celula_muerta; 
			GameObject.Find("Canvas").GetComponent<ManejadorTutorialAmenazas>().DesplegarVirus();
			Destroy(this);


		}

	}


	void OnTriggerStay (Collider MyTrigger) {
	
	
		if (MyTrigger.gameObject.name.Equals ("Net")) {
			
			atrapado=true;		
			float xAux=MyTrigger.gameObject.transform.position.x;
			float yAux=MyTrigger.gameObject.transform.position.y;
			float zAux=MyTrigger.gameObject.transform.position.z;
			transform.position= new Vector3(Random.Range(xAux-1,xAux+1),Random.Range(yAux-1,yAux+1),zAux);
			vida-=5f;
			GetComponentInChildren<BarraVida>().modificarSpriteDesafioAmenaza();
		}

		if (MyTrigger.gameObject.name.Equals ("Dentrica")) {

			if(MyTrigger.GetComponent<TAmovimientoDefenzas>().captura==false){

			atrapado=true;		
			transform.parent=MyTrigger.transform;
			transform.localPosition=new Vector3(0,2,0);
			Instantiate(Fracturado,this.transform.position,transform.rotation);
			MyTrigger.GetComponent<Animator>().SetInteger("vaso",1);
			MyTrigger.GetComponent<TAmovimientoDefenzas>().enabled=false;
			MyTrigger.GetComponent<TAmovimientoDefenzas>().captura=true;

				if(GameObject.Find("Canvas").GetComponent<ManejadorTutorialAmenazas>().numero_virus==1){

					GameObject.Find("Canvas").GetComponent<ManejadorTutorialAmenazas>().Ultimo();

				}else{
					GameObject.Find("Canvas").GetComponent<ManejadorTutorialAmenazas>().DesplegarVirus();
				}
			
			Destroy(this.gameObject);
			}
		}
	
		if (MyTrigger.gameObject.name.Equals ("Neutrofilo")) {


			vida-=2.5f;
			GetComponentInChildren<BarraVida>().modificarSpriteDesafioAmenaza();
		}

		if (MyTrigger.gameObject.name.Equals ("Macrofago")) {
			
			if(MyTrigger.GetComponent<TAmovimientoDefenzas>().captura==false){
				
				atrapado=true;		
				transform.parent=MyTrigger.transform;
				transform.localPosition=new Vector3(0,0,0);
				vida-=2.5f;
				GetComponentInChildren<BarraVida>().modificarSpriteDesafioAmenaza();
			}
		}
	
	}


}
