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
	public static int celulas=12;
	public int misVirus;
	public bool muerta;


	// Use this for initialization
	void Start () {
	
		misVirus = 0;
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
				InvokeRepeating("invocar",4,20f);
			this.gameObject.name="muerta";


		}
	}

	//Invocacion del Virus que sale de la celula
	void invocar(){


		if (ManejadorVirus.numeroVirus <= 30&&ManejadorVirus.numeroVirus>0) {

			if(misVirus<10){


				Instantiate (viruss,this.transform.position,viruss.transform.rotation);

				misVirus++;
			}else{

				CancelInvoke ();
			}

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

				DefenzaFuera(this.gameObject.name);
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
						DefenzaFuera(this.gameObject.name);
						this.gameObject.tag="muerta";
						cambiar();
						
					}
					if(this.gameObject.tag.Equals("celula")&&MyTrigger.gameObject.name.Equals ("Neutrofilo(Clone)")){

						audio1.Stop();
						DefenzaFuera(this.gameObject.name);
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

	public void DefenzaFuera(string name){
		
		
		for (int i=0; i<ManejadorVirus.celulas.Length; i++) {

			if(ManejadorVirus.celulas[i]!=null){
			if (ManejadorVirus.celulas [i].gameObject.name.Equals (name)) {
				
				ManejadorVirus.celulas [i] = null;
					Debug.Log("defenzafuera");

				break;
			}
			}
		}
		ManejadorVirus.actualizarDefenza ();
		imprimirArreglo ();
	}

	public void imprimirArreglo(){

		string cadena = "";
		{

			for (int i=0; i<ManejadorVirus.celulas.Length; i++) {

				if (ManejadorVirus.celulas [i] != null) {
				cadena += ManejadorVirus.celulas [i].name + "-";
				}
		}
		Debug.Log (cadena);
	}
}
}
