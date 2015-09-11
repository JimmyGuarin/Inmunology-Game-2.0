﻿using UnityEngine;
using System.Collections;

public class VasoGrande : MonoBehaviour {

	public GameObject eritrocito;
	public GameObject neutrofilo;
	public GameObject macrofago;
	public GameObject linfocitoB;
	public GameObject tcd4;
	public GameObject tcd8;
	public static bool activarLinfocitos;

	// Use this for initialization
	void Start () {

		activarLinfocitos = false;
		InvokeRepeating ("crearEritrocito", 0, 0.3f);
		InvokeRepeating ("crearNeutrofilo", 3f, 1f);
		InvokeRepeating ("crearMacrofago", 2f, 1.5f);
	
	}
	
	// Update is called once per frame
	void Update () {

		if (activarLinfocitos == true) {
			activar ();
			activarLinfocitos=false;
		}
			
	
	}
	
	public  void activar(){


		//InvokeRepeating ("crearLinfocitoB", 0, 2f);
		InvokeRepeating ("crearTCD4", 2f, 2.5f);
		//InvokeRepeating ("crearTCD8", 1f, 2.5f);
	}


	void crearEritrocito(){

		int cantidad = Random.Range (1, 4);

		for(int i=0;i<cantidad;i++){

			Instantiate(eritrocito);
		}

	}
	void crearNeutrofilo(){

			Instantiate(neutrofilo);	
	}
	void crearMacrofago(){
		
		Instantiate(macrofago);	
	}

	void crearLinfocitoB(){
		
		Instantiate(linfocitoB);
	}
	
	void crearTCD4(){
		
		Instantiate(tcd4);
	}
	
	void crearTCD8(){
		
		Instantiate(tcd8);
	}

}
