using UnityEngine;
using System.Collections;

public class MoverPuntoEncuentro : MonoBehaviour {



	// Variable que almacena la posicion del punto de encuentro
	public static Vector2 posicion;
	public static Vector2 puntoDestino;
	Ray pulsacion;
	RaycastHit hit ;
	//si el objeto es seleccionado.
	public static bool isSeleted;



	//Se llama cuando se hace click en el Fondo
	void OnMouseDown(){
		
		//Si el click es el clik derecho(0)
		if (Input.GetMouseButtonDown (0)) {
			

			pulsacion = Camera.main.ScreenPointToRay (Input.mousePosition);
			
			if (Physics.Raycast (pulsacion, out hit)) {

				puntoDestino = hit.point;
				
				Debug.Log (hit.collider.name);
			
				Debug.Log ("colliderPE" + hit.collider.name);
				if (hit.collider.name.Equals ("puntoEncuentro")) {	

					if(CrearUnidadInnata.seleccionadas==0){

						if (isSeleted == true) {
							isSeleted = false;
						} else {
							isSeleted = true;
						}

					}
					//Si el puntoDeEncuentro(MoverPuntoEncuentro) se encuentra seleccionado

						
				}
		
			}

		}
	}
	// Use this for initialization
	void Start () {

		posicion = new Vector3 (0, 0, -1);
		isSeleted = false;

				
	}
	
	// Update is called once per frame
	void Update () {
	
		transform.position = new Vector3 (posicion.x, posicion.y,-1);

	}

	/*
	 * Metodo para cambiar la posicion que tiene
	 * el punto de encuentro. Este metodo es
	 * llamado desde el script Fondo1.
	*/
	public static void cambiarPos(Vector2 v)
	{
		posicion = v;
		isSeleted = false;



	}



}	