using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DesafioDendritica : MonoBehaviour {

	// Paneles Primarios de navegacion
	public GameObject panelPrincipal_1;
	public GameObject panelPrincipal_11;
	public GameObject panelPrincipal_12;
	public GameObject panelPrincipal_13;
	public GameObject panelPrincipal_14;
	public GameObject panelPrincipal_15;

	public GameObject panel_volver;

	public int index_guia;
	public Text text_guia;

	public GameObject first_virus;

	// Use this for initialization
	void Start () {
	
		index_guia = 1;
		NotificationCenter.DefaultCenter ().AddObserver (this, "celulaMuerta");
		NotificationCenter.DefaultCenter ().AddObserver (this, "CambiarGuiaDendritica");
		NotificationCenter.DefaultCenter ().AddObserver (this, "TerminarTutorial");
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
			panelPrincipal_11.SetActive(false);
			panelPrincipal_14.SetActive(true);
			panelPrincipal_13.SetActive(false);
			text_guia.transform.parent.gameObject.SetActive(true);
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
			ControladorMenu.in_tutorial=true;
			Destroy(GameObject.Find("Canvas"));
			Destroy(GameObject.Find("Creador"));
			Application.LoadLevel(0);	
			break;
		
	}
}


	void CambiarGuiaDendritica(Notification notification)
	{	
		if (index_guia == (int)notification.data) {
		

			if (index_guia == 1) 
				text_guia.text="Presiona click izquierdo en el lugar a mover la célula dendrítica";
			if (index_guia == 2) {
				
				text_guia.text="Intenta atrapar el virus moviento las células dendríticas";
				first_virus.SetActive(true);

			}
			if (index_guia == 3) 
				text_guia.text="Presiona click derecho sobre la célula dendrítica que atrapó el virus para alertar el vaso sanguineo";

			if (index_guia == 4) {

				text_guia.text="Ahora la célula dendrítica se dirige al vaso sanguineo";

			}
			if (index_guia == 5) {

				text_guia.text=PlayerPrefs.GetString("name")+ ", ya alertaste el vaso el cual activa dos unidades " +
					"que conoceremos mas adelante,por ahora captura los demás virus para ganar el desafío";
				GameObject.Find("ManejadorVirus").GetComponent<ManejadorVirus>().enabled=true;
			}
			index_guia++;	
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

	void TerminarTutorial(Notification notification)
	{	
		QuitarSonidos ();
		text_guia.transform.parent.gameObject.SetActive(true);
		panel_volver.SetActive(true);
		
	}

	public void QuitarSonidos(){
		
		GameObject [] celulas = GameObject.FindGameObjectsWithTag ("celula");
		Camera.main.transform.FindChild ("Audio Source").gameObject.SetActive (false);
		foreach (GameObject celu in celulas) {
			
			celu.GetComponent<AudioSource> ().enabled = false;
			
		}
	}
}
