using UnityEngine;
using System.Collections;

public class TutorialEntorno : MonoBehaviour {

	//ZOOM
	public Zoom zoom;
	Ray pulsacion;
	RaycastHit hit ;
	

	// Paneles Primarios de navegacion
	public GameObject panelPrincipal_1;
	public GameObject panelPrincipal_11;
	public GameObject panelPrincipal_12;

	//Panel de cierre
	public GameObject panelPrincipal_2;

	//Flechas apuntando a objetos de Objetos
	public GameObject flechaVaso;

	//ObjetosInteractivos
	public GameObject vaso;

	// Use this for initialization
	void Start () {
	
		zoom = this.GetComponent<Zoom> ();	
	}
	
	// Update is called once per frame
	void Update () {
	
		//Si el click es el clik derecho(0)
		if (Input.GetMouseButtonDown (0)) {

			pulsacion = Camera.main.ScreenPointToRay (Input.mousePosition);

			if (Physics.Raycast (pulsacion, out hit)) {


				/*
				 * Condicion que pregunta si el collider
				 * con el que colisiona el rayo es el 
				 * collider de algun elemento interactivo
				 * */
				
				if (hit.collider.name.Equals ("VasoFinal")) {	
					
					zoom.enfocar(vaso.transform,-20,0,70);

				}
				
			}
		}
	}
	

  public void cambiarPanelPrincipal(int num){

		switch (num) 
		{
		
			case 1:
				panelPrincipal_11.SetActive(false);
				panelPrincipal_12.SetActive(true);
				break;
			case 2:
				panelPrincipal_11.SetActive(true);
				panelPrincipal_12.SetActive(false);
				break;

			case 3:
				panelPrincipal_1.SetActive(false);
				panelPrincipal_11.SetActive(true);
				panelPrincipal_12.SetActive(false);
				Invoke("empezarVisualizar",5f);
				break;
		}
	}

	public void empezarVisualizar(){

		flechaVaso.SetActive (true);
		vaso.GetComponent<Collider> ().enabled = true;
	}
}
