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
	public static int puntaje=0;
	public static int timpo_total=0;
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
	public static int defensas;

	private static bool gameOver;

	// Use this for initialization


	//Inmunidad Adquirida
	public static ControladorInmuAdquirida cia;

	void Awake() {
	
	}



	void Start () {

		InvokeRepeating("contar", 0.0f, 1.0f);


		cia = GetComponent<ControladorInmuAdquirida>();
		gameOver = false;
		//defensas = 4;
	
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
			

		nutrientes =600;
		oxigeno = 600;

	

	}
	
	// Update is called once per frame
	void Update () {
		if (c.alpha == 4||gameOver==true)
			Time.timeScale = 0;

		if (mostrarAlerta.alpha > 0)
			mostrarAlerta.alpha -= 0.01f;
			
		if (textPuntaje != null)
			textPuntaje.text = "" + puntaje;
		if (textNutrientes != null)
			textNutrientes.text = "" + nutrientes;
		if (textOxigeno != null)
			textOxigeno.text = "" + oxigeno;



	}


	public  void contar(){

		timpo_total += 1;
		tiempo.text = timpo_total + "s.";
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
		c.alpha = 0;
		if (gameOver == true) {

			
			OnLevelComplete (puntaje);
			PlayerPrefs.Save();
			Application.LoadLevel("Inicio");

		}
		Time.timeScale = 1;
		 
	}

	public static void invadido(){

		gameOver = true;
		AudioListener.volume = 0;
		c.alpha = 4;
		Time.timeScale = 0;
		string mensaje=finalizarJuego ();
		if (puntaje < 0)
			puntaje = 0;
		mensajesPantalla.text="¡ GAME OVER !  \n Te han Invadido \n"+mensaje+puntaje;

	}
	public static void ganar(){

		cia.bloqueartodo ();
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


	}

	public void chm2(){

		Time.timeScale = 1;


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
		puntaje += oxigeno + nutrientes;
		oxigeno = 0;
		nutrientes = 0;
		
		if (PlayerPrefs.GetInt ("highscorePos1") < puntaje) {
			
			return "Nuevo Récord: ";
		} 
		else {
			
			 return "Puntaje: ";
		}

	}


}
