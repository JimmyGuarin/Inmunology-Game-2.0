//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AdquiridaTutorial : MonoBehaviour{

	//Flecha apuntando al boton
	public GameObject flecha_boton;

	//Boton para crear personaje
	public GameObject boton_personaje;

	//Panel de informacion 
	public GameObject panelInfo;

	//Texto del panelInfo
	public Text info_macrofago;

	//Panel Contenedor
	public GameObject panelPrincipal_1;

	//Panel Victoria
	public GameObject panelPrincipal_13;

	//Panel Derrota
	public GameObject panelPrincipal_14;

	//Texto guia 
	public Text text_guia;

	// Use this for initialization
	void Start () {

		NotificationCenter.DefaultCenter ().AddObserver (this, "celulaMuerta");
		NotificationCenter.DefaultCenter ().AddObserver (this, "TerminarTutorial");

	
	}




	public void cambiarPanelPrincipal(int num){
		
		switch (num) 
		{

		case 1:
			break;

		//Ir a Menu Principal
		Destroy(GameObject.Find("Canvas"));
		Destroy(GameObject.Find("Creador"));
		case 2:
			ControladorMenu.in_tutorial=true;
			Application.LoadLevel(0);
			break;
		
			//Ir a Menu Innata
		case 3:
		
			break;
		
		//Repetir Desafio
		case 4:

			Application.LoadLevel(Application.loadedLevel);
			break;

		//ArrancarDesafio
		case 5:

			break;
			
		case 6:

			break;
			
		case 7:
		
			break;
			
		}
	}

	void celulaMuerta(Notification notification)
	{	
		
		StartCoroutine(Wait());
		
	}
	
	
	IEnumerator Wait(){
		
		yield return new WaitForSeconds(2);
		panelPrincipal_13.SetActive (false);
		panelPrincipal_1.SetActive (true);
		boton_personaje.GetComponent<Button>().interactable=false;
		text_guia.transform.parent.gameObject.SetActive(false);
		panelInfo.SetActive (false);
		flecha_boton.SetActive (false);
		Time.timeScale = 0;
	}

	void TerminarTutorial(Notification notification)
	{	
		QuitarSonidos ();
		text_guia.transform.parent.gameObject.SetActive(false);
		panelPrincipal_1.SetActive (true);
		panelPrincipal_13.SetActive (false);
		panelPrincipal_14.SetActive (true);
		
	}
	
	public void QuitarSonidos(){
		
		GameObject [] celulas = GameObject.FindGameObjectsWithTag ("celula");
		Camera.main.transform.FindChild ("Audio Source").gameObject.SetActive (false);
		foreach (GameObject celu in celulas) {
			
			celu.GetComponent<AudioSource> ().enabled = false;
			
		}
	}
}


