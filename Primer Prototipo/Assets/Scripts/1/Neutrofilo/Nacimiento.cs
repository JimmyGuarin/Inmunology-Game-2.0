using UnityEngine; 
using System.Collections; 

	 public class Nacimiento: MonoBehaviour {
	private Vector3 screenPoint; 
	//private Vector3 offset; 
	public static bool seleccionado;
	public static bool eliminar;
	public static GameObject neutrofilo;
	public static int naciendo=0;




	void Start(){

		naciendo++;
		eliminar = false;
		seleccionado = true;
		neutrofilo = this.gameObject;
	}


	void OnMouseDrag() { 
		Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z); 
		Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint); 
		transform.position = curPosition; 
	} 
	void Update(){
		 
		if (eliminar == true) {

			Destroy(this.gameObject);
			Destroy(this);
		}



		if (seleccionado==true) {
			Vector3 curScreenPoint = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenPoint.z); 
			Vector3 curPosition = Camera.main.ScreenToWorldPoint (curScreenPoint) ; 
			curPosition.z = -5;
			transform.position = curPosition; 
		}
		else{
			if(eliminar==false){

				this.gameObject.GetComponent<Collider>().enabled=true;
				this.gameObject.AddComponent<ManejarNeutrofilo>();
				naciendo=0;
				Destroy(this);
			}
		
		}
	}


} 