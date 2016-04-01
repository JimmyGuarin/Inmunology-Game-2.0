using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class SeleccionarUnidad : MonoBehaviour {


	public GameObject macrofago;
	public GameObject neutro;
	public GameObject killer;
	public GameObject linfoB;
	public GameObject tcd4;
	public GameObject tcd8;
	public GameObject celula;
    public Image macrofago_imagen;
    public Image neutro_imagen;
    public Image killer_imagen;
    public Image linfoB_imagen;
    public Image tcd4_imagen;
    public Image tcd8_imagen;

	//En tutorial
	public bool tutorial;
	
	//En destresa innata
	public bool Innata;
	
	private float time_neutrofilo=0.005f;
    // Use this for initialization
    void Start() {

		if (tutorial == true) {
		
			time_neutrofilo=0.01f;
		}
		DontDestroyOnLoad(transform.gameObject);
	}
	
	// Update is called once per frame
	void Update () {



        //if (macrofago_imagen.fillAmount <= 0)
          //  macrofago_imagen.gameObject.SetActive(false);
        //else macrofago_imagen.fillAmount -= 005f;


        bajarInhabilitador(macrofago_imagen, time_neutrofilo);
        bajarInhabilitador(neutro_imagen, time_neutrofilo);
        bajarInhabilitador(killer_imagen, 0.01f);
        bajarInhabilitador(linfoB_imagen, 0.003f);
        bajarInhabilitador(tcd4_imagen, 0.001f);
        bajarInhabilitador(tcd8_imagen, 0.001f);


    }

	public void Macrofago(){
	
		int nut = 40;
		int oxigeno = 50;
		int puntajes = 90;
		
		if (DisminuirRecursos (nut, oxigeno, puntajes) == true) {
			
			
			GameObject mac=(GameObject)Instantiate(macrofago);
			MovimientoElipse me=mac.GetComponent<MovimientoElipse>();
			me.salir=true;
            inhabilitarButton(macrofago_imagen);
			if(tutorial){
				mac.GetComponent<Macrofago>().daño=0.6f;
				mac.GetComponent<Macrofago>().speed=6;
				mac.GetComponent<Animator>().enabled=true;
				mac.GetComponent<Macrofago>().desafio_macrofago=true;
				mac.GetComponent<FuncionesMacrofago>().dendritica.GetComponent<CrearUnidadInnata>().tutorial=true;
				NotificationCenter.DefaultCenter().PostNotification(this,"crearMacrofago",mac);
			}

			if(Innata){
				mac.GetComponent<FuncionesMacrofago>().dendritica.GetComponent<CrearUnidadInnata>().tutorial=true;
				mac.GetComponent<Macrofago>().daño=0.6f;
				mac.GetComponent<Macrofago>().speed=6;
			}

			if(tutorial==false&&Innata==false){

				mac.GetComponent<FuncionesMacrofago>().dendritica.GetComponent<CrearUnidadInnata>().tutorial=false;
			}

		}
	
	}

	public void neutrofilo(){

		int nut = 30;
		int oxigeno = 40;
		int puntajes = 70;

		if (DisminuirRecursos (nut, oxigeno, puntajes) == true) {
		
			GameObject neutrofilo=(GameObject)Instantiate(neutro);
			MovimientoElipse me=neutrofilo.GetComponent<MovimientoElipse>();
			me.salir=true;
            inhabilitarButton(neutro_imagen);

			if(tutorial){
				neutrofilo.GetComponent<ManejarNeutrofilo>().daño_a_virus=5.0f;
				neutrofilo.GetComponent<ManejarNeutrofilo>().life=600;
				neutrofilo.GetComponent<ManejarNeutrofilo>().desafio_neutrofilo=true;
				neutrofilo.GetComponent<ManejarNeutrofilo>().speed=6;
				NotificationCenter.DefaultCenter().PostNotification(this,"crearNeutrofilo",neutrofilo);
			}
			if(Innata){
				neutrofilo.GetComponent<ManejarNeutrofilo>().daño_a_virus=5.0f;
				neutrofilo.GetComponent<ManejarNeutrofilo>().life=600;
				neutrofilo.GetComponent<ManejarNeutrofilo>().speed=6;
			}
				

        }
	}

	public void LinfocitoB(){
		
		int nut = 70;
		int oxigeno = 40;
		int puntajes = 110;
		
		if (DisminuirRecursos (nut, oxigeno, puntajes) == true) {
			
			GameObject neutrofilo=(GameObject)Instantiate(linfoB);
			MovimientoElipse me=neutrofilo.GetComponent<MovimientoElipse>();
			me.salir=true;
            inhabilitarButton(linfoB_imagen);

        }
		
	}
	public void TCD4(){
		
		int nut = 80;
		int oxigeno = 80;
		int puntajes = 160;
	
		if (DisminuirRecursos (nut, oxigeno, puntajes) == true) {
			
				GameObject neutrofilo=(GameObject)Instantiate(tcd4);
				MovimientoElipse me=neutrofilo.GetComponent<MovimientoElipse>();
				me.salir=true;
                inhabilitarButton(tcd4_imagen);


        }	

	
	}
	public void TCD8(){

		int nut = 80;
		int oxigeno = 80;
		int puntajes = 160;
	
		if (DisminuirRecursos (nut, oxigeno, puntajes) == true) {
			
				GameObject neutrofilo=(GameObject)Instantiate(tcd8);
				MovimientoElipse me=neutrofilo.GetComponent<MovimientoElipse>();
				me.salir=true;
                inhabilitarButton(tcd8_imagen);


        }
	
	}

	public void Killer(){
		
		int nut = 80;
		int oxigeno = 80;
		int puntajes = 160;
		
		if (DisminuirRecursos (nut, oxigeno, puntajes) == true) {
			
			GameObject neutrofilo = (GameObject)Instantiate (killer);
			MovimientoElipse me = neutrofilo.GetComponent<MovimientoElipse> ();
			me.salir = true;
			inhabilitarButton (killer_imagen);
			
			
		}
	}




	public void Celula(){

		int nut = 100;
		int oxigeno = 100;
		int puntajes = 200;
		
		if (DisminuirRecursos (nut, oxigeno, puntajes) == true) {
			
			Instantiate (celula);
			celula.GetComponent<Nacimiento> ().enabled = true;
	
		}
	}


    //metodo que disminuye los recursos y oxigeno que ingresa por parametros
    // Aumenta el puntaje que recibe por parametro
	public bool DisminuirRecursos(int nut,int oxigeno,int puntaje){


		if (ControladorRecursos.nutrientes >= nut &&
			ControladorRecursos.oxigeno >= oxigeno) {

			ControladorRecursos.puntaje += puntaje;
			ControladorRecursos.nutrientes -= nut;
			ControladorRecursos.oxigeno -= oxigeno;
			return true;
		} else {
		
			ControladorRecursos.sinRecursos();
			return false;
		}


	}

    public void inhabilitarButton(Image Personaje)
    {
        Personaje.fillAmount = 1f;
        Personaje.gameObject.SetActive(true);
    }

    public void bajarInhabilitador(Image personaje,float tiempo)
    {
        if (personaje.fillAmount <= 0)
            personaje.gameObject.SetActive(false);
        else personaje.fillAmount -= tiempo;
    }
}
