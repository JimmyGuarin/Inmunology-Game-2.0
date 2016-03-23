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
	
	//Estrellas
	public RawImage Innata;
	
	// Use this for initialization
	void Start () {
	
		NotificationCenter.DefaultCenter ().AddObserver (this, "TerminarTutorial");
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
			ControladorMenu.in_tutorial=true;
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

	void TerminarTutorial(Notification notification)
	{	
		QuitarSonidos ();
		panelPrincipal_1.SetActive (true);
		panelPrincipal_14.SetActive (true);

		PlayerPrefs.SetString("Innata","0");

		if (ControladorRecursos.puntaje > 5000) {

			PlayerPrefs.SetString("Innata","1");
		
		}
		if (ControladorRecursos.puntaje > 10000) {
			
			PlayerPrefs.SetString("Innata","2");
			
		}
		if (ControladorRecursos.puntaje > 20000) {
			
			PlayerPrefs.SetString("Innata","3");
			
		}
		Innata.texture = Resources.Load (PlayerPrefs.GetString("Innata")) as Texture;

				
	}

	public void QuitarSonidos(){
		
		GameObject [] celulas=GameObject.FindGameObjectsWithTag ("celula");
		Camera.main.transform.FindChild("Audio Source").gameObject.SetActive(false);
		foreach(GameObject celu in celulas){
			
			celu.GetComponent<AudioSource>().enabled=false;
			
		}
	}

}
