using UnityEngine;
using System.Collections;

public class FuncionesDendritica : MonoBehaviour {

	public Vector3 posicion;
	public bool activar;
	private CrearUnidadInnata dendritica;
	public GUISkin custom;
	// Use this for initialization
	void Start () {
	
		activar = false;	
		posicion = transform.position;
		dendritica = GetComponent<CrearUnidadInnata> ();
	}
	
	// Update is called once per frame
	void Update () {
	
			posicion = transform.position;	
			
			
		if(Input.GetMouseButtonDown(1)){


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
			
		GUI.skin = custom;

		if (activar==true) {
				
			Vector3 aux = Camera.main.WorldToScreenPoint(posicion);
			aux.y=Screen.height-aux.y;
			if(GUI.Button(new Rect(aux.x,aux.y,100,30), "Alertar Vaso")){

                if (this.gameObject.transform.position.y < -11)
      
                    dendritica.llevarA(1, new Vector3(47.7f, Random.Range(-11f,4f), -5f));

                else dendritica.llevarA(1, new Vector3(47.7f, this.gameObject.transform.position.y, -5f));

                activar =false;
			}
			if(GUI.Button(new Rect(aux.x,aux.y+30,100,30), "Alertar Ganglio")){
				
				dendritica.llevarA(0,new Vector3(47.8f ,-22.2f  ,-10f  ));
				activar=false;
			}
		}
	}
	

}

