﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DesafioNeutrofilo : MonoBehaviour {

	// Paneles Primarios de navegacion
	public GameObject panelPrincipal_1;
	public GameObject panelPrincipal_11;
	public GameObject panelPrincipal_12;
	public GameObject panelPrincipal_13;
	public GameObject panelPrincipal_14;
	public GameObject panelPrincipal_15;

	public GameObject flecha_neutrofilo;
	public Text tiempo_text;
	private int tiempo;


	public GameObject boton_neutrofilo;
	// Use this for initialization
	void Start () {

		tiempo = 90;
		NotificationCenter.DefaultCenter ().AddObserver (this, "celulaMuerta");
		NotificationCenter.DefaultCenter ().AddObserver (this, "crearNeutrofilo");

	}
	
	public void cambiarPanelPrincipal(int num){
		
		switch (num) 
		{
			
		case 1:
			panelPrincipal_11.SetActive(false);
			panelPrincipal_13.SetActive(false);
			panelPrincipal_12.SetActive(true);
			break;
		case 2:
			panelPrincipal_11.SetActive(true);
			panelPrincipal_12.SetActive(false);
			break;
		case 3:
			panelPrincipal_13.SetActive(true);
			panelPrincipal_12.SetActive(false);
			break;
			
		case 4:
			panelPrincipal_1.SetActive(false);
			panelPrincipal_14.SetActive(true);
			panelPrincipal_13.SetActive(false);
			flecha_neutrofilo.SetActive(true);
			boton_neutrofilo.GetComponent<Button>().interactable=true;
			tiempo_text.gameObject.SetActive(true);
			InvokeRepeating("ManejarTiempo",1f,1f);
			break;
			
		case 5:
			InnataTutorial.estado=2;
			Destroy(GameObject.Find("Canvas"));
			Destroy(GameObject.Find("Creador"));
			Application.LoadLevel(5);
			break;
			
		case 6:
			Destroy(GameObject.Find("Canvas"));
			Destroy(GameObject.Find("Creador"));
			Application.LoadLevel(7);	
			break;
			
		case 7:
			Destroy(GameObject.Find("Canvas"));
			Destroy(GameObject.Find("Creador"));
			Application.LoadLevel(0);	
			break;
			
		}
	}
	
	void celulaMuerta(Notification notification)
	{	
		
		StartCoroutine(Wait());
		
	}
	
	
	IEnumerator Wait(){
		
		yield return new WaitForSeconds(2);
		panelPrincipal_14.SetActive (false);
		panelPrincipal_15.SetActive (true);
		panelPrincipal_1.SetActive (true);
		Time.timeScale = 0;
	}

	void crearNeutrofilo(Notification notification)
	{	
		if(flecha_neutrofilo.activeSelf==true)
			flecha_neutrofilo.SetActive (false);
	}

	void ManejarTiempo(){
	
		tiempo--;

		tiempo_text.gameObject.GetComponent<Text>().text=""+tiempo+".s";

		if (tiempo == 0) {
		
			tiempo_text.gameObject.SetActive(false);
			boton_neutrofilo.SetActive(false);
			GameObject.Find("ManejadorVirus").GetComponent<ManejadorVirus>().enabled=true;
			NotificationCenter.DefaultCenter ().PostNotification (this, "QuitarFunciones");
			GameObject.Find("Fondo1").GetComponent<Fondo1>().enabled=false;
		}
	}
}
