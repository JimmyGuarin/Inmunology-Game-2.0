using UnityEngine;
using System.Collections;

public class FuncionesNeutrofilo : MonoBehaviour {

	public Vector3 posicion;
	public bool activar;
	public GameObject net;

	// Use this for initialization
	void Start () {
		
		activar = false;	
		posicion = transform.position;

	}
	
	// Update is called once per frame
	void Update () {
		
		posicion = transform.position;	
		
		
		if(Input.GetMouseButtonDown(2)){
			
			
			Ray pulsacion;
			RaycastHit hit;
			pulsacion = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (pulsacion, out hit) && hit.collider == this.GetComponent<Collider>()) {
				
				if(activar==false)
					activar=true;
				else activar=false;
			}
		}
		
	}
	
	
	void OnGUI(){
		
		
		if (activar==true) {
			
			Vector3 aux = Camera.main.WorldToScreenPoint(posicion);
			aux.y=Screen.height-aux.y;
			if(GUI.Button(new Rect(aux.x,aux.y,130,30), "Degranulacion")){

				GetComponent<ParticleSystem>().enableEmission=true;
				GetComponent<SphereCollider>().radius=1.6f;
				activar=false;
			}
			if(GUI.Button(new Rect(aux.x,aux.y+30,130,30), "Trampa Extracelular")){
				
				///instanciar trampa
				Invoke("createNET",0.5f);
				  activar=false;
			}
			
			
			
			
		}
	}
	
	void createNET(){

		Instantiate(net,this.transform.position,net.transform.rotation);
		Destroy(this.gameObject);
	}
}


