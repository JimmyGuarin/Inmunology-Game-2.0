using UnityEngine;
using System.Collections;

public class ManejarCelula : MonoBehaviour {

	public Sprite sprite1; 
	public Sprite sprite2; 
	public Sprite sprite3;
	public Sprite sprite4; 
	public Sprite sprite5; 
	public Sprite sprite6;
	public Sprite sprite7;
	public Sprite [] Asprite;
	SpriteRenderer spriteRenderer; 
	private int nSprite;
	public AudioSource audio1;
	public AudioSource destruida;
	public float vida;
	public GameObject viruss;
	public static int celulas;
	public int misVirus;
	public bool muerta;


	// Use this for initialization
	void Start () {
	
		misVirus = 0;
		vida = 7;
		Asprite = new Sprite[7];
		Asprite[0]=sprite1; 
		Asprite[1]=sprite2; 
		Asprite[2]=sprite3;
		Asprite[3]=sprite4;
		Asprite[4]=sprite5;
		Asprite[5]=sprite6;
		Asprite[6]=sprite7;
		celulas = 12;
		muerta = false;

		spriteRenderer = GetComponent<SpriteRenderer>(); 
		spriteRenderer.sprite = sprite7;
	}
	
	// Update is called once per frame
	void Update () {
	



	}
	void cambiar(){


		spriteRenderer.sprite = Asprite [(int)vida];
		if (vida == 0&&muerta==true) {

			muerta=false;
			this.gameObject.name="muerta";
			//Destroy(this.GetComponent<Collider>());
			invocar();

			//invocar();
			//InvokeRepeating("invocar",10,16f);


		}
	}

	void invocar(){


		if (ManejadorVirus.numeroVirus <= 30&&ManejadorVirus.numeroVirus>0) {

			if(misVirus<7){


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
		
		if (MyTrigger.gameObject.name.Equals ("VirusFinal(Clone)") || MyTrigger.gameObject.name.Equals ("Neutrofilo(Clone)")
			|| MyTrigger.gameObject.name.Equals ("NaturalKiller(Clone)")) {

			if(this.gameObject.tag.Equals("celula")){

				audio1.Play ();
				if (vida >= 0) {
					vida -= 0.01f;
					cambiar ();
				} else {
					
					
				}
			}



		}

	}
	void OnTriggerStay (Collider MyTrigger) {
			
		if (MyTrigger.gameObject.name.Equals ("VirusFinal(Clone)")||MyTrigger.gameObject.name.Equals ("VirusFinalCelula(Clone)")||
		    MyTrigger.gameObject.name.Equals ("Neutrofilo(Clone)")
		    ||MyTrigger.gameObject.name.Equals ("NaturalKiller(Clone)")) {

			if(vida>=0){
				vida-=0.01f;
					cambiar();
			}
			else{



				if(this.gameObject.tag.Equals("celula")&&(MyTrigger.gameObject.name.Equals ("VirusFinal(Clone)")||
				                                          MyTrigger.gameObject.name.Equals ("VirusFinalCelula(Clone)"))){

					audio1.Stop();
					vida=0;
					muerta=true;
					DefenzaFuera(this.gameObject.name);
					this.gameObject.tag="muerta";
					cambiar();

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
		
		if (MyTrigger.gameObject.name.Equals ("VirusFinal(Clone)"))
			audio1.Stop();
		
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
