using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ControladorMenu : MonoBehaviour {

	public GameObject [] imagenes_carga;
	public Slider slider_carga;
	private AsyncOperation asyng;
	private GameObject CanvasJuego;
	private GameObject Creador;
	public GameObject panel_tutorial;
	public GameObject panel_registro;

	//Nombre del player
	public static string username;
	public static bool in_tutorial;
	
	//Boton agregar player
	public Button AddPlayer;





	//Estrellas por tutorial
	public RawImage Entorno;
	public RawImage Innata;
	public RawImage Adquirida;
	public RawImage Amenazas;

	void Start(){

		//PlayerPrefs.DeleteAll ();


		ControladorRecursos.puntaje = 0;
		ControladorRecursos.timpo_total = 0;
		username = PlayerPrefs.GetString ("name");
		if (username.Equals ("")) {
			panel_registro.SetActive (true);
			PlayerPrefs.SetString("Entorno","0");
			PlayerPrefs.SetString("Innata","0");
			PlayerPrefs.SetString("Adquirida","0");
			PlayerPrefs.SetString("Amenazas","0");
			Entorno.texture = Resources.Load ("0") as Texture;
			Entorno.texture = Resources.Load ("0") as Texture;
		}
			
		else {
			if(in_tutorial){
				panel_tutorial.SetActive(true);
			}
		}	
		Entorno.texture = Resources.Load (PlayerPrefs.GetString("Entorno")) as Texture;
		Innata.texture = Resources.Load (PlayerPrefs.GetString("Innata")) as Texture;
		Adquirida.texture = Resources.Load (PlayerPrefs.GetString("Adquirida")) as Texture;
		Amenazas.texture = Resources.Load (PlayerPrefs.GetString("Amenazas")) as Texture;


		CanvasJuego = GameObject.Find ("Canvas");
		Creador = GameObject.Find ("Creador");
		if (CanvasJuego != null) {
			Destroy(CanvasJuego);
			Destroy(Creador);
		}
			
		AudioListener.volume = 1;
			
	}


	public void jugar(string name_level){

		if (name_level.Equals ("1"))
			imagenes_carga [0].SetActive (true);
		if (name_level.Equals ("2"))
			imagenes_carga [1].SetActive (true);
		if (name_level.Equals ("3"))
			imagenes_carga [2].SetActive (true);
		
		Application.LoadLevelAsync (name_level);

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
		in_tutorial = false;
	}

	public void cargarTutorial(int numero){

		ControladorRecursos.puntaje = 0;
		ControladorRecursos.timpo_total = 0;
		Application.LoadLevel (numero);
	}

	public void OnchangeName(Text nombre){

		Debug.Log (nombre.text);

		if (nombre.text != "") {
			
			AddPlayer.interactable = true;
		} else 
			AddPlayer.interactable = false;
		
	}

	public void SavePlayer(Text nombre1){

		username = nombre1.text;
		PlayerPrefs.SetString ("name", username);
		panel_registro.SetActive (false);
	}



}
