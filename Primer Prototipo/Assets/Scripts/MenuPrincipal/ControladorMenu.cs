using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ControladorMenu : MonoBehaviour {

	public GameObject imagen_carga;
	public Slider slider_carga;
	private AsyncOperation asyng;
	private GameObject CanvasJuego;
	private GameObject Creador;
	public GameObject panel_tutorial;

	void Start(){
	
		CanvasJuego = GameObject.Find ("Canvas");
		Creador = GameObject.Find ("Creador");
		if (CanvasJuego != null) {
			Destroy(CanvasJuego);
			Destroy(Creador);
		}
			
		AudioListener.volume = 1;
			
	}


	public void jugar(){

	
		imagen_carga.SetActive (true);
		StartCoroutine (LoadLevelslider());


	}



	IEnumerator LoadLevelslider(){

		asyng = Application.LoadLevelAsync ("1");

		while (!asyng.isDone) {
			slider_carga.value=asyng.progress;
			yield return null;
			
		}


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

	public void tutorial(){

		panel_tutorial.SetActive (true);
	}

	public void cerrarTutorial(){
		
		panel_tutorial.SetActive (false);
	}

	public void cargarTutorial(int numero){

		Application.LoadLevel (numero);
	}
}
