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

    //Arreglo donde se almacenan las imagenes de la celula
	public Sprite [] Asprite;
    //Renderer de la Celula
	SpriteRenderer spriteRenderer; 

    //Sonido cuando se comen la celula
	public AudioSource audio1;
    //Sonido cuando se destruye la celula
	public AudioSource destruida;

    //vida de la celula
	public float vida=7;
    //indice de imagen
    private int nSprite=6;

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

	//Cambiar El estado de la celula
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

				Instantiate (viruss,this.transform.position,viruss.transform.rotation);
	}


	void OnTriggerEnter (Collider MyTrigger) {

        if (MyTrigger.gameObject.name.Equals("VirusFinal(Clone)") ||
            MyTrigger.gameObject.name.Equals("VirusFinalCelula(Clone)")) {

            if (this.gameObject.tag.Equals("celula"))

                audio1.Play();

        }

        if( MyTrigger.gameObject.name.Equals("Neutrofilo(Clone)")){

                if (MyTrigger.gameObject.GetComponent<ParticleSystem>().enableEmission == true)
                {
                if (this.gameObject.tag.Equals("celula"))

                    audio1.Play();
                }

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
           (MyTrigger.gameObject.name.Equals("Neutrofilo(Clone)")&& MyTrigger.gameObject.GetComponent<ParticleSystem>().enableEmission == true) 
           &&this.gameObject.tag.Equals("celula")) {

            if (audio1.isPlaying == false)
                audio1.Play();
			vida-=0.005f;

			if(vida>=1&&vida<=6){
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
                        destruida.Play();
                        DefenzaFuera(c);
						this.gameObject.tag="muerta";
						cambiar();
						
					}
					if(this.gameObject.tag.Equals("celula")&&MyTrigger.gameObject.name.Equals ("Neutrofilo(Clone)")){

                        if(MyTrigger.gameObject.GetComponent<ParticleSystem>().enableEmission == true)
                        {
                            audio1.Stop();
                            destruida.Play();
                            DefenzaFuera(c);
                            Destroy(this.gameObject);
                        }
                        

					}

                    
				}


			}
		}
		if (MyTrigger.gameObject.name.Equals ("LinfoncitoTCD8(Clone)")) {


		}
			
	}
	void OnTriggerExit (Collider MyTrigger) {


        audio1.Stop();
		
	}


    //Eliminar la celula de la lista de celulas.
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

		if (ManejadorVirus.celulas.Count == 0) {

			CancelInvoke();
			ControladorRecursos.invadido();
		}
	
	}

}
