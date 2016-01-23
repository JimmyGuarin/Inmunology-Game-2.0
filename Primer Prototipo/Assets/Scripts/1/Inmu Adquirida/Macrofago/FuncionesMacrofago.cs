using UnityEngine;
using System.Collections;

public class FuncionesMacrofago : MonoBehaviour {

	public Vector3 posicion;
	public bool activar;
	public GameObject dendritica;
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
		
		
		if (activar == true) {
			
			Vector3 aux = Camera.main.WorldToScreenPoint (posicion);
			aux.y = Screen.height - aux.y;
			if (GUI.Button (new Rect (aux.x, aux.y, 160, 30), "Transformar a Dendritica")) {

				liberarCaptura();
				Instantiate(dendritica,this.transform.position,dendritica.transform.rotation);
				Destroy(this.gameObject);
				
			
			}
		}
	
	}

	void liberarCaptura(){
		
		if (GetComponent<Macrofago> ().mivirus != null) {
			
			GetComponent<Macrofago> ().mivirus.transform.parent=null;

			if(GetComponent<Macrofago> ().mivirus.GetComponent<InteligenciaVirus>()!=null){
				GetComponent<Macrofago> ().mivirus.GetComponent<InteligenciaVirus>().speed=2f;
				GetComponent<Macrofago> ().mivirus.GetComponent<InteligenciaVirus>().capturado=false;
				GetComponent<Macrofago> ().mivirus.GetComponent<InteligenciaVirus>().enabled=true;
				GetComponent<Macrofago> ().mivirus.GetComponent<ColisionesVirus>().enabled=true;
				GetComponent<Macrofago> ().mivirus.name="VirusFinal(Clone)";


			}
			else{
				GetComponent<Macrofago> ().mivirus.GetComponent<BacteriaMov>().speed=1f;
				GetComponent<Macrofago> ().mivirus.GetComponent<BacteriaColis>().capturado=false;
				GetComponent<Macrofago> ().mivirus.GetComponent<BacteriaColis>().enabled=true;
				GetComponent<Macrofago> ().mivirus.GetComponent<BacteriaMov>().enabled=true;
				GetComponent<Macrofago> ().mivirus.name="Bacteria(Clone)";

			}
			GetComponent<Macrofago> ().mivirus.GetComponent<Collider>().enabled=true;
			GetComponent<Macrofago> ().mivirus=null;
			GetComponent<Macrofago> ().llevarBase=false;
			Debug.Log("elimina");
			GetComponent<Macrofago> ().destino=transform.position;
			GetComponent<Macrofago> ().speed=4f;
			GetComponent<Macrofago> ().esperando_ayudador=false;
		}
		
		
	}
}


