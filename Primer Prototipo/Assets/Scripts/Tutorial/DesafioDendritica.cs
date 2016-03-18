using UnityEngine;
using System.Collections;

public class DesafioDendritica : MonoBehaviour {

	// Paneles Primarios de navegacion
	public GameObject panelPrincipal_1;
	public GameObject panelPrincipal_11;
	public GameObject panelPrincipal_12;
	public GameObject panelPrincipal_13;
	public GameObject panelPrincipal_14;
	public GameObject panelPrincipal_15;

	// Use this for initialization
	void Start () {
	
		NotificationCenter.DefaultCenter ().AddObserver (this, "celulaMuerta");
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
			GameObject.Find("ManejadorVirus").GetComponent<ManejadorVirus>().enabled=true;
			break;

		case 5:
			InnataTutorial.estado=1;
			Destroy(GameObject.Find("Canvas"));
			Destroy(GameObject.Find("Creador"));
			Application.LoadLevel(5);
			break;

		case 6:
			Destroy(GameObject.Find("Canvas"));
			Destroy(GameObject.Find("Creador"));
			Application.LoadLevel(6);	
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
}
