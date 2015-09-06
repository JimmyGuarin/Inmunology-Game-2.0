using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;

public class ControladorRecursos : MonoBehaviour {

	public Text alertas;
	public static Text mensajesalertas;
	public static CanvasGroup mostrarAlerta;
	public  Text tiempo;
	public  Text textPuntaje;
	public static int puntaje;
	public  Text textNutrientes;
	public static int nutrientes;
	public  Text textOxigeno;
	public static int oxigeno;
	public  Text mensajes;
	public static Text mensajesPantalla;
	public CanvasGroup cc;
	public static CanvasGroup c;
	public Canvas cc1;
	public static Canvas c1;

	private static bool gameOver;
	public static int defensas;
	// Use this for initialization


	//Inmunidad Adquirida
	public ControladorInmuAdquirida cia;
	void Start () {

		cia = GetComponent<ControladorInmuAdquirida>();
		gameOver = false;
		defensas = 0;
	
		if (cc != null) {
			
			c = cc;
			c.alpha = 0;
		}
		if (cc1 != null) {
			
			c1 = cc1;

		}

		if (mensajes != null)
			mensajesPantalla = mensajes;
		if (alertas != null) {

			mensajesalertas = alertas;
			mostrarAlerta= alertas.GetComponent<CanvasGroup>();
		}
			
		puntaje = 0;
		nutrientes =60;
		oxigeno = 100;

	

	}
	
	// Update is called once per frame
	void Update () {
		if (c.alpha == 4||gameOver==true)
			Time.timeScale = 0;

		if (mostrarAlerta.alpha > 0)
			mostrarAlerta.alpha -= 0.01f;

		if (tiempo != null)
			tiempo.text = (int)Time.timeSinceLevelLoad + "s.";
		if (textPuntaje != null)
			textPuntaje.text = "" + puntaje;
		if (textNutrientes != null)
			textNutrientes.text = "" + nutrientes;
		if (textOxigeno != null)
			textOxigeno.text = "" + oxigeno;

		if ((nutrientes <= 10 || oxigeno <= 20) && defensas == 0) {

			defensas=1;
			recursosAgotados ();

		}

	}
	public static void sinRecursos(){


		mensajesalertas.text = "No posees los recursos necesarios";
		mostrarAlerta.alpha = 2;

	}
	public static void recursosAgotados(){

		gameOver = true;
		AudioListener.volume = 0;
		c.alpha = 4;
		Time.timeScale = 0;
		string mensaje=finalizarJuego ();
		mensajesPantalla.text="Te has quedado sin Recursos y Defensas. GAME OVER \n "+mensaje+puntaje;


	}

	public void cerrarVentana(){
		Debug.Log ("cerrar");
		c.alpha = 0;
		if (gameOver == true) {

			
			OnLevelComplete (puntaje);
			PlayerPrefs.Save();
			Application.LoadLevel("Inicio");

		}
		Time.timeScale = 1;
		 
	}
	public static void sinvasoEstimulado(){

		mensajesalertas.text ="No posees Vasos Estimulados";
		mostrarAlerta.alpha = 2;
	}

	public static void sinllevar(){
		
		mensajesalertas.text ="Lleva el Neutrofilo a un Vaso Estimulado";
		mostrarAlerta.alpha = 2;
	}
	public static void invadido(){

		gameOver = true;
		AudioListener.volume = 0;
		c.alpha = 4;
		Time.timeScale = 0;
		string mensaje=finalizarJuego ();
		mensajesPantalla.text="Te han Invadido. \n "+mensaje+puntaje;

	}
	public static void ganar(){
		
		gameOver = true;
		AudioListener.volume = 0;
		c.alpha = 4;
		Time.timeScale = 0;
		string mensaje=finalizarJuego ();
		puntaje += 300;
		mensajesPantalla.text="GANASTE. \n "+mensaje+puntaje;


	}

	public static void virusAnalizado(){
		
		c1.enabled = true;
		c1.gameObject.SetActive (true);

	}

	public void cmhi(){

		Time.timeScale = 1;

		cia.cmhi ();
	}

	public void chm2(){

		Time.timeScale = 1;

		cia.cmhii ();
	}



	public static void OnLevelComplete (int score) { 
		int temp=0;
		int s = score;
		 //values from your scoring logic 
		for(int i=1; i<=5; i++) //for top 5 highscores 
		{ if(PlayerPrefs.GetInt("highscorePos"+i)<s) //if cuurent score is in top 5 
			{ temp=PlayerPrefs.GetInt("highscorePos"+i); //store the old highscore in temp varible to shift it down 
				PlayerPrefs.SetInt("highscorePos"+i,s); //store the currentscore to highscores 
				if(i<5) //do this for shifting scores down 
				{ 	int j=i+1;
					s = PlayerPrefs.GetInt("highscorePos"+j); //Try and put this here 
					PlayerPrefs.SetInt("highscorePos"+j,temp); 
				} 
			} 
		} 
	}


	public static string finalizarJuego(){

		Time.timeScale = 0;
		Debug.Log ("final");
		int tiempo = ((int)Time.timeSinceLevelLoad);
		puntaje += oxigeno + nutrientes-tiempo;
		oxigeno = 0;
		nutrientes = 0;
		
		if (PlayerPrefs.GetInt ("highscorePos1") < puntaje) {
			
			return "Nuevo Record: ";
		} 
		else {
			
			 return "Puntaje: ";
		}

	}


}
