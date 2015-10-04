using UnityEngine;
using System.Collections;

public class SeleccionarUnidad : MonoBehaviour {


	public GameObject macrofago;
	public GameObject neutro;
	public GameObject killer;
	public GameObject linfoB;
	public GameObject tcd4;
	public GameObject tcd8;
	public GameObject celula;

	
	// Use this for initialization
	void Start () {
	

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Macrofago(){
	
		int nut = 40;
		int oxigeno = 50;
		int puntajes = 90;
		
		if (DisminuirRecursos (nut, oxigeno, puntajes) == true) {
			
			
			GameObject neutrofilo=(GameObject)Instantiate(macrofago);
			MovimientoElipse me=neutrofilo.GetComponent<MovimientoElipse>();
			me.salir=true;
			Debug.Log(me.salir);
			
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
			Debug.Log(me.salir);
		
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
			Debug.Log(me.salir);

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
				Debug.Log(me.salir);
			
			
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
				Debug.Log(me.salir);
			
			
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
}
