using UnityEngine;
using System.Collections;

public class CrearUnidadInnata : MonoBehaviour {
	
	
	//Velocidad  a la se me mueve la celula
	public float speed;
	
	//Vector de destino cuando movemos la celula
	public  Vector3 destino;
	
	//Variable que almacena si la celula esta seleccionada o no.
	public bool isSeleted;

	
	//Variable que representa el animator de la celula
	private Animator animator;

	private Fracture virus;
	// si va para base
	private bool llevarBase=false;
	
	public float vida=1000;
	//Si esta en colision
	private bool enColision;
	public static int seleccionadas=0;
	public Texture2D imagen;
	public Rect r;
	public int ubicada;
	void Start () {


		ControladorRecursos.defensas++;
		enColision = false;
		isSeleted = false;
		animator = GetComponent<Animator> ();
		speed = 7f;
		destino = this.transform.position;
		//PatronObserver:
		// Metodos que va a observar 
		NotificationCenter.DefaultCenter ().AddObserver (this, "cambiarPosCelula");
		NotificationCenter.DefaultCenter ().AddObserver (this, "llevarAVaso");
		NotificationCenter.DefaultCenter ().AddObserver (this, "llevarABase");
		
		
		
		
	}
	
	// Update is called once per frame
	void Update () {


			
	 if (Input.GetMouseButtonDown (1)) {
				
			
			Ray pulsacion;
			RaycastHit hit;
			pulsacion = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (pulsacion, out hit) && hit.collider == this.GetComponent<Collider>()) {
				
				if (isSeleted == false && this.GetComponent<Collider>() != null) {

					seleccionadas++;
					if(seleccionadas==1){
						PosicionSeleccionada.posicionar++;
						ubicada=PosicionSeleccionada.posicionar;
						GUI.Label (new Rect (10, 30, 110, 60), imagen);

					}
					isSeleted = true;
				} 
			}
		
		}
		//se esta cambiando la posicion hasta que llega a destino
		float step = speed * Time.deltaTime;
		this.transform.position = Vector3.MoveTowards (transform.position, destino, step);
			if(llevarBase==true&&this.transform.position==destino){
				
				ControladorRecursos.defensas--;
				Destroy(this.gameObject);
				
			}
			
			
			

		
	}

	void OnGUI(){
		
		if (isSeleted == true) {
			if (ubicada == 1) {
				
				GUI.Label (new Rect (10, 30, 110, 60), imagen);
				GUI.Box(new Rect(10,10,110,100),""+seleccionadas);
			}
			if (ubicada == 2) {
				
				GUI.Label (new Rect (130, 30, 110, 60), imagen);
				GUI.Box(new Rect(130,10,110,100),""+seleccionadas);
			}
			if (ubicada == 3) {
				
				GUI.Label (new Rect (250, 30, 110, 60), imagen);
				GUI.Box(new Rect(250,10,110,100),""+seleccionadas);
			}
			if (ubicada == 4) {
				
				GUI.Label (new Rect (370, 30, 110, 60), imagen);
				GUI.Box(new Rect(370,10,110,100),""+seleccionadas);
			} 
		}
	}
	

	
	//Metodollamado desde el script Fondo1 (Script del fondo)
	void cambiarPosCelula(Notification notification)
	{
		
		//Si esta celula esta seleccionada	
		
		if (isSeleted == true) {
			
			
			animator.SetInteger("vaso",0);
			destino = new Vector3 (Fondo1.puntoDestino.x, Fondo1.puntoDestino.y, -5f);
			isSeleted = false;
			seleccionadas--;

		}
		
		
	}
	//metodo que se llama desde el script virus
	//para llevar la celula con el virus al ganglio o al vaso
	void llevarABase(Notification notificacion){
		
		
		if (llevarBase == false&&enColision==true) {
			
			//Destroy(this.GetComponent<Collider>());
			llevarBase = true;
			GetComponent<FuncionesDendritica>().enabled=true;
			Vector3 nuevaPos=(Vector3)notificacion.data;
			animator.SetInteger("vaso",1);
			speed=4f;
			Invoke("nada",1.8f);
			this.transform.position=new Vector3(nuevaPos.x,nuevaPos.y,-7f);

			if(Vector3.Distance(transform.position,new Vector3(47.8f ,-22.2f  ,this.transform.position.z  ))<
			   Vector3.Distance(transform.position,new Vector3(47.7f,10.8f,this.transform.position.z  )))
				destino=new Vector3(47.8f ,-22.2f  ,-10f  );
			else destino=new Vector3(47.7f,10.8f,-5f  );
			
			if(isSeleted==true)
				seleccionadas--;
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

		if (index == 1) 
			virus.ganglio=false;
		else virus.ganglio=true;
		destino = v;
		virus.destino = destino;
	}
	

	void OnTriggerEnter (Collider MyTrigger) {
		
		Debug.Log (MyTrigger.gameObject.name);

		if (MyTrigger.gameObject.name.Equals ("VirusFinal(Clone)") || 
		    MyTrigger.gameObject.name.Equals ("VirusFinalCelula(Clone)"))
		{
				if (llevarBase == false) {


				speed = 4f;
				enColision = true;
				}
				
		}	
	


		if (MyTrigger.gameObject.name.Equals ("LinfoncitoTCD4(Clone)")) {

			speed=6;
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
				speed=4f;
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
