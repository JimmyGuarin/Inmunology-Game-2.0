using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DestresaAdquirida : MonoBehaviour {

	public GameObject panel_ppal;
	public GameObject panel_victoria;
	public GameObject panel_derrota;

	//Estrellas
	public RawImage Adquirida;

	// Use this for initialization
	void Start () {
	

		Time.timeScale = 0;
		NotificationCenter.DefaultCenter ().AddObserver (this, "TerminarTutorial");


	}



	// Update is called once per frame
	void Update () {
	
	}

	public void cambiarNivel(int num){
		
		ControladorMenu.in_tutorial=true;
		Destroy(GameObject.Find("Canvas"));
		Destroy(GameObject.Find("Creador"));
		Application.LoadLevel(num);	


	}

	public void celulasMuertas()
	{	
		
		StartCoroutine(Wait());
		
	}
	
	
	IEnumerator Wait(){
		
		yield return new WaitForSeconds(2);
		panel_derrota.SetActive (true);
		panel_ppal.SetActive (true);
		PlayerPrefs.SetString("Adquirida","0");
		Time.timeScale = 0;
	}
	
	
	void TerminarTutorial(Notification notification)
	{	
		QuitarSonidos ();
		panel_victoria.SetActive (true);
		panel_ppal.SetActive (true);
		
		PlayerPrefs.SetString("Adquirida","0");
		
		if (ControladorRecursos.puntaje > 5000) {
			
			PlayerPrefs.SetString("Adquirida","1");
			
		}
		if (ControladorRecursos.puntaje > 10000) {
			
			PlayerPrefs.SetString("Adquirida","2");
			
		}
		if (ControladorRecursos.puntaje > 20000) {
			
			PlayerPrefs.SetString("Adquirida","3");
			
		}
		Adquirida.texture = Resources.Load (PlayerPrefs.GetString("Adquirida")) as Texture;
		
		
	}
	
	public void QuitarSonidos(){
		
		GameObject [] celulas=GameObject.FindGameObjectsWithTag ("celula");
		Camera.main.transform.FindChild("Audio Source").gameObject.SetActive(false);
		foreach(GameObject celu in celulas){
			
			celu.GetComponent<AudioSource>().enabled=false;
			
		}
	}

	public void Empezar(){

		Time.timeScale = 1;
	}

}
