using UnityEngine; 
using System.Collections; 

	 public class Nacimiento: MonoBehaviour {
	private Vector3 screenPoint; 
	//private Vector3 offset; 
	public static bool seleccionado;
	public static bool eliminar;
	public static int naciendo=0;
	public bool sel=false;
	public bool creado=false;
	private Vector3 destino;
	private Vector3 origen;

	void Start(){
	
		seleccionado = true;
		sel = true;
		creado = false;
		NotificationCenter.DefaultCenter ().AddObserver (this, "crearCelula");

	}


	//void OnMouseDrag() { 
	//	Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z); 
	//	Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint); 
	//	transform.position = curPosition; 
	//} 
	void Update(){
		 
		if(creado==true){


			float step = 4f * Time.deltaTime;
			this.transform.position = Vector3.MoveTowards (transform.position, destino, step);

			if(this.transform.position==destino){

				GetComponent<Collider>().enabled=true;
				GetComponent<ManejarCelula>().enabled=true;
				GetComponentInChildren<nutritientes>().enabled=true;
				Destroy(this);
			}
		}


		if (seleccionado==true&&creado==false) {

		
				Vector3 posVec = Input.mousePosition; 
			    posVec.z = -5 - Camera.main.transform.position.z; 
				posVec = Camera.main.ScreenToWorldPoint(posVec); 
				Debug.Log (posVec);
				transform.position=posVec;

		}

	}

	//Metodollamado desde el script Fondo1 (Script del fondo)
	void crearCelula(Notification notification)
	{

		if (sel == true) {
		
			float a = 100000;
			destino = (Vector3)notification.data;
			destino.z = -3.6f;
			
			for (int i=ManejadorVirus.celulas.Count-1; i>=0; i--) {
				
				Celula c=ManejadorVirus.celulas[i] as Celula;
				float b = Mathf.Abs (Vector3.Distance (destino, c.m_posicion));
				if (b <= a) {
					
					origen=c.m_posicion;
					
					a = b;
				}
				
			}
			this.transform.position = origen;
			creado = true;
			sel=false;

		}

	}


} 