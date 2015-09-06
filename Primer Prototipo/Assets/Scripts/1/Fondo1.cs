using UnityEngine;
using System.Collections;

/// <summary>
/// Fondo1.Este script maneja todos los eventos relacionados con el Fondo del juego.
/// </summary>
public class Fondo1 : MonoBehaviour {

	/// <summary>
	/// The punto destino.
	/// Variable que almacena el punto donde se hace click en el fondo
	/// el cual puede ser utilizado como punto de destino para otros objetos
	/// que movemos en pantalla
	/// </summary>
	public static Vector2 puntoDestino;
	Ray pulsacion;
	RaycastHit hit ;


	// Use this for initialization
	void Start () {
		
	}
	
	
	
	
	// Update is called once per frame
	void Update () {

		//Si el click es el clik derecho(0)
		if (Input.GetMouseButtonDown (0)) {

			
			Ray pulsacion;
			RaycastHit hit ;
			pulsacion=Camera.main.ScreenPointToRay(Input.mousePosition);
			


			
			if (Physics.Raycast(pulsacion,out hit)) {
				
				PosicionSeleccionada.posicionar=0;
				CrearUnidadInnata.seleccionadas=0;
				ManejarNeutrofilo.seleccionadas=0;
				LinfocitoB.seleccionadas=0;
				LinfocitoB2.seleccionadas=0;
				TCD4.seleccionadas=0;
				TCD8.seleccionadas=0;
				Killer.seleccionadas=0;
				puntoDestino=hit.point;
				/*
				 * Condicion que pregunta si el collider
				 * con el que colisiona el rayo es el 
				 * collider que tiene el fondo
				 * hit.collider.name.Equals ("Fondo1")||hit.collider.name.Equals ("VirusFinal(Clone)")
				    ||hit.collider.name.Equals ("celula1")||hit.collider.name.Equals ("puntoEncuentro")
				 * */

				//if () {	
					
					NotificationCenter.DefaultCenter().PostNotification(this,"cambiarPosCelula");
					if(Nacimiento.seleccionado==true){

						ControladorRecursos.sinllevar();
					}

				//}
				
				
				
			}
			
		}
		
	}
	

	//Se llama cuando se hace click en el Fondo
	void OnMouseDown(){

		//Si el click es el clik derecho(0)
		if (Input.GetMouseButtonDown (0)) {



			pulsacion=Camera.main.ScreenPointToRay(Input.mousePosition);

			PosicionSeleccionada.posicionar=0;
			Debug.Log (PosicionSeleccionada.posicionar);
			if (Physics.Raycast(pulsacion,out hit)) {

				
				puntoDestino=hit.point;
				/*
				 * Condicion que pregunta si el collider
				 * con el que colisiona el rayo es el 
				 * collider que tiene el fondo
				 * */

				if (hit.collider.name.Equals ("Fondo1")||hit.collider.name.Equals ("VirusFinal(Clone)")) {	

					NotificationCenter.DefaultCenter().PostNotification(this,"cambiarPosCelula");
					if(Nacimiento.seleccionado==true){

						//Destroy(Nacimiento.neutrofilo);
						//ControladorRecursos.oxigeno+=20;
						//ControladorRecursos.nutrientes+=10;
						//ControladorRecursos.sinllevar();
					}
					//Si el puntoDeEncuentro(MoverPuntoEncuentroPuntoEncuentroPuntoEncuentro) se encuentra seleccionado
					if (MoverPuntoEncuentro.isSeleted == true){
						Debug.Log ("MoverPuntoEncuentro");

						//canviar la posicion del punto de encuentro
						MoverPuntoEncuentro.cambiarPos (hit.point);

					}
			}


			
			
		}
		
	}
	
}
}