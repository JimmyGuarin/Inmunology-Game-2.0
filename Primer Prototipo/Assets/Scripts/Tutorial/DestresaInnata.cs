using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DestresaInnata : MonoBehaviour {

	// Paneles Primarios de navegacion
	public GameObject panelPrincipal_1;
	public GameObject panelPrincipal_11;
	public GameObject panelPrincipal_12;
	public GameObject panelPrincipal_13;
	public GameObject panelPrincipal_14;
	public GameObject panelPrincipal_15;

	public GameObject boton_celula;
	public GameObject boton_nk;
	
	// Use this for initialization
	void Start () {
	

		Time.timeScale = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
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
			Time.timeScale = 1;
			boton_celula.GetComponent<Button>().interactable=true;
			boton_nk.GetComponent<Button>().interactable=true;
			GameObject.Find("ManejadorVirus").GetComponent<ManejadorVirus>().enabled=true;
			break;
			
		case 5:
			InnataTutorial.estado=1;
			Destroy(GameObject.Find("Canvas"));
			Destroy(GameObject.Find("Creador"));
			Application.LoadLevel(0);
			break;
			
		case 6:
			Destroy(GameObject.Find("Canvas"));
			Destroy(GameObject.Find("Creador"));
			Application.LoadLevel(9);	
			break;
			
		case 7:
			Destroy(GameObject.Find("Canvas"));
			Destroy(GameObject.Find("Creador"));
			Application.LoadLevel(0);	
			break;
			
		}
	}

  public void celulasMuertas()
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
}
