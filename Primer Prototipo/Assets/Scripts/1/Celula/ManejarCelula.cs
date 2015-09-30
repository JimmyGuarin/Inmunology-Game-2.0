using UnityEngine;
using System.Collections;

public class ManejarCelula : MonoBehaviour {



	//Imagenes celulas
	public Sprite sprite1; 
	public Sprite sprite2; 
	public Sprite sprite3;
	public Sprite sprite4; 
	public Sprite sprite5; 
	public Sprite sprite6;
	public Sprite sprite7;


	public Sprite [] Asprite;
	SpriteRenderer spriteRenderer; 

	public AudioSource audio1;
	public AudioSource destruida;

	public float vida=7;
	private int nSprite=6;//indice de imagen

	public GameObject viruss;

	public bool muerta;

	public Celula c;
	// Use this for initialization
	void Start () {
	
		c = new Celula (this.transform.position, this.GetHashCode ());
		ManejadorVirus.celulas.Add (c);
		Asprite = new Sprite[7];
		Asprite[0]=sprite1; 
		Asprite[1]=sprite2; 
		Asprite[2]=sprite3;
		Asprite[3]=sprite4;
		Asprite[4]=sprite5;
		Asprite[5]=sprite6;
		Asprite[6]=sprite7;
		muerta = false;

		spriteRenderer = GetComponent<SpriteRenderer>(); 
		spriteRenderer.sprite = sprite7;
	}
	
	// Update is called once per frame
	void Update () {
	

		spriteRenderer.sprite = Asprite [nSprite];

	}

	//Cambiar Imagen de celula
	void cambiar(){



		if (vida == 0&&muerta==true) {

			muerta=false;
			if(!this.gameObject.name.Equals("muerta"))
				//InvokeRepeating("invocar",4,20f);
			this.gameObject.name="muerta";


		}
	}

	//Invocacion del Virus que sale de la celula
	void invocar(){


		if (ManejadorVirus.numeroVirus <= 30&&ManejadorVirus.numeroVirus>0) {


				Instantiate (viruss,this.transform.position,viruss.transform.rotation);

				
		

		} else {
			CancelInvoke ();
			ControladorRecursos.invadido();
		}

		
	}


	void OnTriggerEnter (Collider MyTrigger) {
		
		if (MyTrigger.gameObject.name.Equals ("VirusFinal(Clone)") || 
		    MyTrigger.gameObject.name.Equals ("VirusFinalCelula(Clone)") ||
		    MyTrigger.gameObject.name.Equals ("Neutrofilo(Clone)")
			|| MyTrigger.gameObject.name.Equals ("NaturalKiller(Clone)")) {

			if(this.gameObject.tag.Equals("celula"))

				audio1.Play ();

		}

		if (MyTrigger.gameObject.name.Equals ("LinfoncitoTCD8(Clone)")) {

			if(this.gameObject.tag.Equals("muerta")){

				DefenzaFuera(c);
				Destroy(this.gameObject);

			}

		}

	}
	void OnTriggerStay (Collider MyTrigger) {
			
		if (MyTrigger.gameObject.name.Equals ("VirusFinal(Clone)")||
		    MyTrigger.gameObject.name.Equals ("VirusFinalCelula(Clone)")||
		    MyTrigger.gameObject.name.Equals ("Neutrofilo(Clone)")
		    ||MyTrigger.gameObject.name.Equals ("NaturalKiller(Clone)")&&this.gameObject.tag.Equals("celula")) {

			vida-=0.01f;

			if(vida>=0.5&&vida<=6){
				nSprite=(int)vida;


			}
				

			else{

				if(vida<=0){

					if(this.gameObject.tag.Equals("celula")&&(MyTrigger.gameObject.name.Equals ("VirusFinal(Clone)")||
					   MyTrigger.gameObject.name.Equals ("VirusFinalCelula(Clone)"))){

						audio1.Stop();
						vida=0;
						nSprite=0;
						muerta=true;
						DefenzaFuera(c);
						this.gameObject.tag="muerta";
						cambiar();
						
					}
					if(this.gameObject.tag.Equals("celula")&&MyTrigger.gameObject.name.Equals ("Neutrofilo(Clone)")){

						audio1.Stop();
						DefenzaFuera(c);
						Destroy(this.gameObject);

					}


				}


			}
		}
		if (MyTrigger.gameObject.name.Equals ("LinfoncitoTCD8(Clone)")) {

			if(vida<6){
				vida+=0.02f;
				cambiar();

			}
			else{
				vida=7;
			}

		}
			
	}
	void OnTriggerExit (Collider MyTrigger) {


			
		
	}

	public void DefenzaFuera(Celula cm){

		ManejadorVirus.actualizarDefenza();
		for (int i=ManejadorVirus.celulas.Count-1; i>=0; i--) {
		
			Celula c=ManejadorVirus.celulas[i] as Celula;

			if(cm.m_identificador==c.m_identificador){
				
				ManejadorVirus.celulas.Remove(c);
				Debug.Log("borra");
			}
		}
		Debug.Log ("celulastam" + ManejadorVirus.celulas.Count);
		ManejadorVirus.actualizarDefenza ();
		//imprimirArreglo ();
	}

	public void imprimirArreglo(){

		string cadena = "";
		{

			for (int i=0; i<ManejadorVirus.celulas.Count; i++) {


		}
		Debug.Log (cadena);
	}
}
}
