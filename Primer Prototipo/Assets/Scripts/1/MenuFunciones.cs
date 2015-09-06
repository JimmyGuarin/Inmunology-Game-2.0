using UnityEngine;
using System.Collections;

public class MenuFunciones : MonoBehaviour {

	public Vector3 posicion;
	public bool activar;
	// Use this for initialization
	void Start () {
	
		activar = false;	
		posicion = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
			posicion = transform.position;	
			
			
		if(Input.GetMouseButtonDown(2)){

			activar=true;
			Ray pulsacion;
			RaycastHit hit;
			pulsacion = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (pulsacion, out hit) && hit.collider == this.GetComponent<Collider>()) {

			
			
			Debug.Log("posicion "+posicion);
			Vector3 aux = Camera.main.WorldToScreenPoint (posicion);
			Debug.Log("screenPos"+aux);
			Debug.Log("posicionscreen "+ Camera.main.WorldToScreenPoint(aux));
			Debug.Log("posicionviewport"+Camera.main.ScreenToWorldPoint(aux));
			Debug.Log("pulsacion"+pulsacion);
			}
		}

		if (Input.GetKey(KeyCode.Mouse1)) {



			Ray pulsacion;
			RaycastHit hit;
			pulsacion = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (pulsacion, out hit) && hit.collider == this.GetComponent<Collider>()) {
				

			}
					
		} 
			
			
	}


	void OnGUI(){



		if (activar==true) {
				
			Vector3 aux = Camera.main.WorldToScreenPoint(posicion);
			aux.y=Screen.height-aux.y;

			GUI.Label (new Rect (aux.x, aux.y, 110, 60),"nada");
			GUI.Box (new Rect (aux.x, aux.y, 110, 100), "" + 2);
		}
	}
	

}
