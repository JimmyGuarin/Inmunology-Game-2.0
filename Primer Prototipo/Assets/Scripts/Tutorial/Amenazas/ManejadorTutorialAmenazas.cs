using UnityEngine;
using System.Collections;

public class ManejadorTutorialAmenazas : MonoBehaviour {

	public  int numero_virus;
	public GameObject viruss;
	public int celulas_infectadas;

	public GameObject panel_ppal;
	public GameObject panel_fin;

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
		

			panel_ppal.SetActive(true);
			panel_fin.SetActive(true);
			Time.timeScale=0;


		}
		else{

			Invoke("crearVirus",2f);
		}


	}

	void crearVirus(){

		Instantiate(viruss);
	}

	public void Ultimo(){

		Invoke ("DesplegarVirus", 2f);
	}
}
