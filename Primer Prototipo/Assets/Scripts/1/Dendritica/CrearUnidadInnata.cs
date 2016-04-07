using UnityEngine;
using System.Collections;

public class CrearUnidadInnata : MonoBehaviour {
	
	
	//Velocidad  a la se me mueve la celula
	public float speed;
	
	//Vector de destino cuando movemos la celula
	public  Vector3 destino;
	
	//Variable que almacena si la celula esta seleccionada o no.
	public bool isSeleted;

	//Dendritica de tutorial
	public bool tutorial;
	//Dendritica de desafio Dendritica
	public bool desafio_dendritica;
	//Dendritica de inmunidad adquirida
	public bool tutorial_adquirida;  
	
	//Variable que representa el animator de la celula
	private Animator animator;

	private Fracture virus;
	// si va para base
	public  bool llevarBase=false;

    public GameObject canvas_Ganglio;
	
	public float vida=1000;
	//Si esta en colision
	public bool enColision;
	public Texture2D imagen;
	public Rect r;

	public ControladorInmuAdquirida activarAdquirida;
	void Start () {



		if(GameObject.Find("CanvasGanglio").transform.childCount>0)
       		 canvas_Ganglio = GameObject.Find("CanvasGanglio").transform.GetChild(0).gameObject;
        ControladorRecursos.defensas++;
		enColision = false;
		isSeleted = false;
		animator = GetComponent<Animator> ();
		destino = this.transform.position;
		//PatronObserver:
		// Metodos que va a observar 
		NotificationCenter.DefaultCenter ().AddObserver (this, "cambiarPosCelula");
		NotificationCenter.DefaultCenter ().AddObserver (this, "llevarAVaso");
		NotificationCenter.DefaultCenter ().AddObserver (this, "llevarABase");
		
		
		
		
	}
	
	// Update is called once per frame
	void Update () {

	
		//se esta cambiando la posicion hasta que llega a destino
		float step = speed * Time.deltaTime;
		this.transform.position = Vector3.MoveTowards (transform.position, destino, step);
			if(llevarBase==true&&this.transform.position==destino){
				
				ControladorRecursos.defensas--;
                
					canvas_Ganglio.SetActive(true);
					
					canvas_Ganglio.GetComponent<BarraProgresoGanglio>().activar(virus.GetComponent<Fracture>().mutacion);	
					Debug.Log("Llamar al metodo");

				
				ManejadorVirus.numeroVirus-=(transform.childCount-4);
				Debug.Log("virus"+ManejadorVirus.numeroVirus);
				ControladorRecursos.puntaje+=300;
				Destroy(this.gameObject);
			}

		
	}

	void OnGUI(){
		
		if (isSeleted == true) {

				GUI.Label (new Rect (10, 30, 110, 60), imagen);
				GUI.Box(new Rect(10,10,110,100),"");
		}
	}
	

	
	//Metodollamado desde el script Fondo1 (Script del fondo)
	void cambiarPosCelula(Notification notification)
	{
		
		//Si esta celula esta seleccionada	
		
		if (isSeleted == true&&Fondo1.seleccionada==true) {
			
			
			//animator.SetInteger("vaso",0);
			destino = new Vector3 (Fondo1.puntoDestino.x, Fondo1.puntoDestino.y, -5f);
			isSeleted = false;
			transform.FindChild("seleccionada").gameObject.SetActive(false);
			Fondo1.seleccionada=false;

			if(desafio_dendritica==true)
				NotificationCenter.DefaultCenter().PostNotification(this,"CambiarGuiaDendritica",2);

		}
		
		
	}
	//metodo que se llama desde el script virus
	//para llevar la celula con el virus al ganglio o al vaso
	void llevarABase(Notification notificacion){
		
		
		if (llevarBase == false&&enColision==true) {
			

			llevarBase = true;
			GetComponent<FuncionesDendritica>().enabled=true;
			Vector3 nuevaPos=(Vector3)notificacion.data;
			animator.SetInteger("vaso",1);
			speed=6f;
			Invoke("nada",1.8f);
			this.transform.position=new Vector3(nuevaPos.x,nuevaPos.y,-7f);

			if(Vector3.Distance(transform.position,new Vector3(47.8f ,-22.2f  ,this.transform.position.z  ))<
			   Vector3.Distance(transform.position,new Vector3(47.7f,10.8f,this.transform.position.z  )))
				destino=new Vector3(47.8f ,-22.2f  ,-10f  );
			else destino=new Vector3(47.7f,this.transform.position.y,-5f  );

			if(tutorial==true){

				if(transform.position.y<-10)
					destino=new Vector3(47.7f,Random.Range(-10,0),-5f  );
				else
					destino=new Vector3(47.7f,this.transform.position.y,-5f  );
			}
			if(tutorial_adquirida)
				destino=new Vector3(47.8f ,-22.2f  ,-10f  );

			if(isSeleted==true){
				Fondo1.seleccionada=false;
				transform.FindChild("seleccionada").gameObject.SetActive(false);
			}
				
			isSeleted=false;
			NotificationCenter.DefaultCenter().PostNotification(this,"atrapado",destino);
			
			
		}
		
		
	}

	public void nada(){

		animator.SetInteger("vaso",2);
	}


	//index =0ganglio index=1 vaso
	//llamado desde el script de funcionesDendritica
	public void llevarA(int index,Vector3 v){

        destino = v;

		if (virus != null) {
		
			if (index == 1)
			{
				virus.ganglio = false;
				destino.z = this.transform.position.z;
			}
			
			else virus.ganglio=true;
			virus.destino = destino;
		
		
		}
        

    }
	

	void OnTriggerEnter (Collider MyTrigger) {
		


		if (MyTrigger.gameObject.name.Equals("Bacteria(Clone)")||
			MyTrigger.gameObject.name.Equals ("VirusFinal(Clone)") || 
		    MyTrigger.gameObject.name.Equals ("VirusFinalCelula(Clone)"))
		{
				if (llevarBase == false) {
					speed = 6f;
					enColision = true;
					NotificationCenter.DefaultCenter().PostNotification(this,"MacrofagoTutorial",false);
					NotificationCenter.DefaultCenter().PostNotification(this,"CambiarGuiaDendritica",3);
				}
				
		}	
	


		if (MyTrigger.gameObject.name.Equals ("LinfoncitoTCD4(Clone)")) {

			speed=11;
			animator.SetBool("mejorada",true);

		}
			
		
	}
	
	void OnTriggerExit(Collider MyTrigger) {
		
		enColision = false;
		
	}
	void OnTriggerStay(Collider MyTrigger) {
		
		
		if (MyTrigger.gameObject.name.Equals ("VirusFinal(Clone)")||
		    MyTrigger.gameObject.name.Equals("VirusFinalCelula(Clone)")) {
			
			if (llevarBase == false) {
			
				enColision = true;
				speed=6f;
			}
			else{

				vida-=0.1f;
				if(vida==0){

					ControladorRecursos.defensas--;
					Destroy(this.gameObject);
				}
					
			}

			
		}
		if (MyTrigger.gameObject.name.Equals ("virusFinalFracture(Clone)")) {
			
			virus = MyTrigger.GetComponent < Fracture>();
            Destroy(virus.GetComponent<Collider>());
            llevarBase = true;
            MyTrigger.gameObject.transform.parent = this.transform;
            

        }


		if (MyTrigger.gameObject.name.Equals ("NaturalKiller(Clone)")) {
			
			vida -= 2f;
			if (vida <= 0) {
				ControladorRecursos.defensas--;
				Destroy (this.gameObject);
				
				
			}
		
		}
	
	}


}
