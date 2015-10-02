using UnityEngine;
using System.Collections;

public class SeleccionarUnidad : MonoBehaviour {


	public GameObject dentritica;
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


	public void neutrofilo(){

		if(ControladorRecursos.nutrientes>=10&&
		   ControladorRecursos.oxigeno>=20)
		{
			
			ControladorRecursos.puntaje+=40;
			ControladorRecursos.nutrientes-=10;
			ControladorRecursos.oxigeno-=20;
			GameObject neutrofilo=(GameObject)Instantiate(neutro);
			MovimientoElipse me=neutrofilo.GetComponent<MovimientoElipse>();
			me.salir=true;
			Debug.Log(me.salir);
		}
		else{
			
			ControladorRecursos.sinRecursos();
		}
		
	
	}

	public void LinfocitoB(){
		
		if(ControladorRecursos.nutrientes>=10&&
		   ControladorRecursos.oxigeno>=20)
		{
			
			ControladorRecursos.puntaje+=40;
			ControladorRecursos.nutrientes-=10;
			ControladorRecursos.oxigeno-=20;
			GameObject neutrofilo=(GameObject)Instantiate(linfoB);
			MovimientoElipse me=neutrofilo.GetComponent<MovimientoElipse>();
			me.salir=true;
			Debug.Log(me.salir);
		}
		else{
			
			ControladorRecursos.sinRecursos();
		}
		
		
	}
	public void TCD4(){
		
		if(ControladorRecursos.nutrientes>=10&&
		   ControladorRecursos.oxigeno>=20)
		{
			
			ControladorRecursos.puntaje+=40;
			ControladorRecursos.nutrientes-=10;
			ControladorRecursos.oxigeno-=20;
			GameObject neutrofilo=(GameObject)Instantiate(tcd4);
			MovimientoElipse me=neutrofilo.GetComponent<MovimientoElipse>();
			me.salir=true;
			Debug.Log(me.salir);
		}
		else{
			
			ControladorRecursos.sinRecursos();
		}
		
		
	}
	public void TCD8(){
		
		if(ControladorRecursos.nutrientes>=10&&
		   ControladorRecursos.oxigeno>=20)
		{
			
			ControladorRecursos.puntaje+=40;
			ControladorRecursos.nutrientes-=10;
			ControladorRecursos.oxigeno-=20;
			GameObject neutrofilo=(GameObject)Instantiate(tcd8);
			MovimientoElipse me=neutrofilo.GetComponent<MovimientoElipse>();
			me.salir=true;
			Debug.Log(me.salir);
		}
		else{
			
			ControladorRecursos.sinRecursos();
		}
		
		
	}

	public void Celula(){

		GameObject celula1 = (GameObject)Instantiate (celula);
		celula.GetComponent<Nacimiento> ().enabled = true;
	}
}
