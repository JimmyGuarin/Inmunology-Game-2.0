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

    // Use this for initialization
    void Start() {

        
	}
	
	// Update is called once per frame
	void Update () {



        //if (macrofago_imagen.fillAmount <= 0)
          //  macrofago_imagen.gameObject.SetActive(false);
        //else macrofago_imagen.fillAmount -= 005f;


        bajarInhabilitador(macrofago_imagen, 0.005f);
        bajarInhabilitador(neutro_imagen, 0.005f);
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
			
			
			GameObject neutrofilo=(GameObject)Instantiate(macrofago);
			MovimientoElipse me=neutrofilo.GetComponent<MovimientoElipse>();
			me.salir=true;
            inhabilitarButton(macrofago_imagen);
            
			
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
        Debug.Log("entra");
        Personaje.gameObject.SetActive(true);
    }

    public void bajarInhabilitador(Image personaje,float tiempo)
    {
        if (personaje.fillAmount <= 0)
            personaje.gameObject.SetActive(false);
        else personaje.fillAmount -= tiempo;
    }
}
