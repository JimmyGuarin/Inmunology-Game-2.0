using UnityEngine;
using System.Collections;

public class MoverPuntoEncuentro : MonoBehaviour {



	// Variable que almacena la posicion del punto de encuentro
	public static Vector2 posicion;
	public static Vector2 puntoDestino;
	Ray pulsacion;
	RaycastHit hit ;
	//si el objeto es seleccionado.
	public  bool isSeleted;



	//Se llama cuando se hace click en el Fondo

	// Use this for initialization
	void Start () {

		NotificationCenter.DefaultCenter ().AddObserver (this, "cambiarPosCelula");
		transform.position = new Vector3 (0, 0, -1);
		isSeleted = false;

				
	}
	

	void cambiarPosCelula(Notification notification)
	{
		if (isSeleted == true) {
			transform.position=new Vector3 (Fondo1.puntoDestino.x, Fondo1.puntoDestino.y, -1f);
			isSeleted=false;
			Fondo1.seleccionada=false;
		}

		
	}

}	