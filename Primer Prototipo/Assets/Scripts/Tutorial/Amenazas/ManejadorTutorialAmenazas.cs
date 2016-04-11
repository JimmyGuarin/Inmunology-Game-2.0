using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ManejadorTutorialAmenazas : MonoBehaviour {

	public  int numero_virus;
	public GameObject viruss;
	public int celulas_infectadas;

	public GameObject panel_ppal;
	public GameObject panel_fin;
	public RawImage estrellasAmenazas;


	// Use this for initialization
	void Start () {
	
		Time.timeScale = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public  void DesplegarVirus(){

		numero_virus--;

		if (numero_virus == 0) {

			PlayerPrefs.SetString("Amenazas","0");
			Camera.main.transform.FindChild("Audio Source").gameObject.SetActive(false);

			switch(celulas_infectadas){

			case 1:
				PlayerPrefs.SetString("Amenazas","1");
				break;
			case 2:
				PlayerPrefs.SetString("Amenazas","2");
				break;
			case 3:
				PlayerPrefs.SetString("Amenazas","3");
				break;

			}
			estrellasAmenazas.texture = Resources.Load (PlayerPrefs.GetString("Amenazas")) as Texture;

			panel_ppal.SetActive(true);
			panel_fin.SetActive(true);
			Time.timeScale=0;

		}
		else{

			Invoke("crearVirus",2f);
		}


	}


	public void crearVirus(){

		Time.timeScale = 1;
		Instantiate(viruss);
	}

	public void Ultimo(){

		Invoke ("DesplegarVirus", 2f);
	}

	public void CambioEscena(int num){
	
	
		Time.timeScale = 1;
		ControladorMenu.in_tutorial = true;
		Application.LoadLevel (num);
	
	
	}



		


}
