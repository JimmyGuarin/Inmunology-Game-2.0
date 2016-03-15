using UnityEngine;
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
	public Text numero_neutrofilos;
	private int numero_nuetros;

	public GameObject boton_neutrofilo;
	// Use this for initialization
	void Start () {
		
		NotificationCenter.DefaultCenter ().AddObserver (this, "celulaMuerta");
		NotificationCenter.DefaultCenter ().AddObserver (this, "crearNeutrofilo");
		numero_nuetros = 0;
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
			numero_neutrofilos.gameObject.SetActive(true);
			GameObject.Find("ManejadorVirus").GetComponent<ManejadorVirus>().enabled=true;
			break;
			
		case 5:
			InnataTutorial.estado=1;
			Destroy(GameObject.Find("Canvas"));
			Application.LoadLevel(5);
			break;
			
		case 6:
			Destroy(GameObject.Find("Canvas"));
			Application.LoadLevel(6);	
			break;
			
		case 7:
			Destroy(GameObject.Find("Canvas"));
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
		numero_nuetros++;
		if (numero_nuetros == 1)
			flecha_neutrofilo.SetActive (false);

		if (numero_nuetros < 13)
			numero_neutrofilos.text = "" + numero_nuetros;
		else {

			boton_neutrofilo.SetActive(false);
		}
		
	}
}
