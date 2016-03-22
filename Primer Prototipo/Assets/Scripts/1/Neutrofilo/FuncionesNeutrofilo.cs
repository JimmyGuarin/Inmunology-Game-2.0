using UnityEngine;
using System.Collections;

public class FuncionesNeutrofilo : MonoBehaviour {

	public Vector3 posicion;
	public bool activar;
	public GameObject net;
	public string texto_descrip;

	// Use this for initialization
	void Start () {

		NotificationCenter.DefaultCenter ().AddObserver (this, "QuitarFunciones");
		activar = false;	
		posicion = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
		posicion = transform.position;	
		
		
		if(Input.GetMouseButtonDown(1)){
			

			Ray pulsacion;
			RaycastHit hit;
			pulsacion = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (pulsacion, out hit) && hit.collider == this.GetComponent<Collider>()) {
				
				if(activar==false){
					activar=true;
					NotificationCenter.DefaultCenter().PostNotification(this,"CambiarGuiaNeutrofilo",3);
					texto_descrip="";
				}
					
				else activar=false;
			}
		}
		
	}
	
	
	void OnGUI(){

		if (activar==true) {

			Vector3 aux = Camera.main.WorldToScreenPoint(posicion);
			aux.y=Screen.height-aux.y;

			if(GetComponent<ParticleSystem>().enableEmission==false){

				if(GUI.Button(new Rect(aux.x,aux.y,130,30), "Degranulacion")){
					
					liberarCaptura();
					GetComponent<ParticleSystem>().enableEmission=true;
					GetComponent<SphereCollider>().radius=1.6f;
					NotificationCenter.DefaultCenter().PostNotification(this,"CambiarGuiaNeutrofilo",4);
					activar=false;
					GetComponent<ManejarNeutrofilo>().desafio_neutrofilo=false;

				}

			}
			if(GetComponent<ManejarNeutrofilo>().desafio_neutrofilo==false){
				if(GUI.Button(new Rect(aux.x,aux.y+30,130,30), "Trampa Extracelular")){
					
					///instanciar trampa
					Invoke("createNET",0.5f); 	
					activar=false;
					NotificationCenter.DefaultCenter().PostNotification(this,"CambiarGuiaNeutrofilo",5);	
				}

			}
		
		}
	}
	
	void createNET(){
		liberarCaptura ();
		Instantiate(net,this.transform.position,net.transform.rotation);
		if (this.GetComponent<ManejarNeutrofilo> ().daño_a_virus == 5.0f)
			net.GetComponent<Net> ().tiempo = 30;
		Destroy(this.gameObject);
	}

	void liberarCaptura(){

		if (GetComponent<ManejarNeutrofilo> ().mivirus != null) {
		
			GetComponent<ManejarNeutrofilo> ().mivirus.transform.parent=null;

			if(	GetComponent<ManejarNeutrofilo> ().mivirus.GetComponent<InteligenciaVirus>()!=null){

				GetComponent<ManejarNeutrofilo> ().mivirus.GetComponent<InteligenciaVirus>().speed=2f;
				GetComponent<ManejarNeutrofilo> ().mivirus.GetComponent<InteligenciaVirus>().capturado=false;
				GetComponent<ManejarNeutrofilo> ().mivirus.GetComponent<InteligenciaVirus>().enabled=true;
				GetComponent<ManejarNeutrofilo> ().mivirus.GetComponent<ColisionesVirus>().enabled=true;
				GetComponent<ManejarNeutrofilo> ().mivirus.name="VirusFinal(Clone)";
			}
			else{

				GetComponent<ManejarNeutrofilo> ().mivirus.GetComponent<BacteriaMov>().speed=1f;
				GetComponent<ManejarNeutrofilo> ().mivirus.GetComponent<BacteriaColis>().capturado=false;
				GetComponent<ManejarNeutrofilo> ().mivirus.GetComponent<BacteriaMov>().enabled=true;
				GetComponent<ManejarNeutrofilo> ().mivirus.GetComponent<BacteriaMov>().Start();
				GetComponent<ManejarNeutrofilo> ().mivirus.GetComponent<BacteriaColis>().enabled=true;
				GetComponent<ManejarNeutrofilo> ().mivirus.GetComponent<BacteriaDisparar>().enabled=true;
				GetComponent<ManejarNeutrofilo> ().mivirus.GetComponent<BacteriaDisparar>().InvokeRepeating("disparar", 6.0f, 6.0f);
				GetComponent<ManejarNeutrofilo> ().mivirus.name="Bacteria(Clone)";
			}

			GetComponent<ManejarNeutrofilo> ().mivirus.GetComponent<Collider>().enabled=true;

			GetComponent<ManejarNeutrofilo> ().mivirus=null;
			GetComponent<ManejarNeutrofilo> ().llevarBase=false;
			Debug.Log("elimina");
			GetComponent<ManejarNeutrofilo> ().subir=transform.position;
			GetComponent<ManejarNeutrofilo> ().speed=4f;
			GetComponent<ManejarNeutrofilo> ().esperando_ayudador=false;
		}


	}

	void QuitarFunciones(Notification notification)
	{	
		Destroy (this);
	}

}


