using UnityEngine;
using System.Collections;

public class ControladorMenu : MonoBehaviour {



	void Start(){
	
		AudioListener.volume = 1;
	
	}


	public void jugar(){

		Application.LoadLevel("1");

	}
	void Update(){

		if( Input.GetKey( KeyCode.Escape ) )
		{
			Application.Quit();
		}

	}


	public void puntajes(){
		
		Puntajes.mostrar ();
		
	}


	public void instrucciones(){
		
		Instrucciones.mostrar ();
		
	}

	public void salir(){
		
		Application.Quit();
		
	}
}
