using UnityEngine;
using System.Collections;

public class FuncionesTCD4 : MonoBehaviour {

	public Vector3 posicion;
	public bool activar;
	public GUISkin custom;
	// Use this for initialization
	void Start () {
		
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
				
				if(activar==false)
					activar=true;
				else activar=false;
			}
		} 
		
	}
	
	
	void OnGUI(){
		
		GUI.skin = custom;
		GUI.skin.button.fontSize = 10;

		if (activar==true) {
			
			Vector3 aux = Camera.main.WorldToScreenPoint(posicion);
			aux.y=Screen.height-aux.y;
			if(GUI.Button(new Rect(aux.x,aux.y,150,30), "Ayudar LinfocitoB")){

				NotificationCenter.DefaultCenter().PostNotification(this,"activarMiraLB",this.gameObject);
				NotificationCenter.DefaultCenter().PostNotification(this,"desactivarMiraNE",this.gameObject);
				NotificationCenter.DefaultCenter().PostNotification(this,"desactivarMiraMA",this.gameObject);
				activar=false;

			}
			if(GUI.Button(new Rect(aux.x,aux.y+30,150,30), "Ayudar Neutrofilo")){

				NotificationCenter.DefaultCenter().PostNotification(this,"activarMiraNE",this.gameObject);
				NotificationCenter.DefaultCenter().PostNotification(this,"desactivarMiraLB",this.gameObject);
				NotificationCenter.DefaultCenter().PostNotification(this,"desactivarMiraMA",this.gameObject);
				activar=false;
			}
			if(GUI.Button(new Rect(aux.x,aux.y+60,150,30), "Ayudar Macrofago")){
				
				NotificationCenter.DefaultCenter().PostNotification(this,"activarMiraMA",this.gameObject);
				NotificationCenter.DefaultCenter().PostNotification(this,"desactivarMiraLB",this.gameObject);
				NotificationCenter.DefaultCenter().PostNotification(this,"desactivarMiraNE",this.gameObject);
				activar=false;
			}
			
			
			
			
		}
	}
	
	
}


