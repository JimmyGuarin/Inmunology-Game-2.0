﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ControladorInmuAdquirida : MonoBehaviour {


	public Button macrofago;
	public Button neutrofilo;
	public Button linfoB;
	public Button Killer;
	public Button TCD4;
	public Button TCD8;

	public VasoGrande animador_vaso;

	public SeleccionarUnidad seleccionador;



	void Start(){



	}



	public void desbloquearLinfocitos(){



		linfoB.GetComponent<Button> ().interactable = true;
		TCD4.GetComponent<Button> ().interactable = true;
		TCD8.GetComponent<Button> ().interactable = true;


	}

	public void desbloquearInnata(){


		macrofago.GetComponent<Button> ().interactable = true;
		neutrofilo.GetComponent<Button> ().interactable = true;

	}

	public  void bloqueartodo(){
		Debug.Log ("me llaman");
		linfoB.GetComponent<Button> ().interactable = false;
		TCD4.GetComponent<Button> ().interactable = false;
		TCD8.GetComponent<Button> ().interactable = false;
		macrofago.GetComponent<Button> ().interactable = false;
		neutrofilo.GetComponent<Button> ().interactable = false;

	}

}
