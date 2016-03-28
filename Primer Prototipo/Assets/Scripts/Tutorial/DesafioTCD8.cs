using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DesafioTCD8 : MonoBehaviour {

	private AdquiridaTutorial manejador;
	private int celulas_infectadas;
	private int celulas_desinfectadas;
	// Use this for initialization
	void Start () {
	

		celulas_infectadas = 0;
		manejador = GetComponent<AdquiridaTutorial> ();
		manejador.enabled = false;
		manejador.panelInfo.SetActive (false);

		NotificationCenter.DefaultCenter ().AddObserver (this, "celulaMuerta");
		NotificationCenter.DefaultCenter ().AddObserver (this, "TutorialTCD8");


		manejador.text_guia.transform.parent.gameObject.GetComponent<RectTransform>().anchoredPosition=new Vector2(
			manejador.text_guia.transform.parent.gameObject.GetComponent<RectTransform>().anchoredPosition.x,
			manejador.text_guia.transform.parent.gameObject.GetComponent<RectTransform>().anchoredPosition.y+80);


	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void celulaMuerta(Notification notification)
	{	
		celulas_infectadas++;
		if (celulas_infectadas>=4) {

			Debug.Log("tcd8");
			StartCoroutine(Wait());
		}
			
		
	}

	void TutorialTCD8(Notification notification)
	{	
		celulas_desinfectadas++;

		if (celulas_desinfectadas == 3) {
		
			Time.timeScale = 0;
			QuitarSonidos ();
			manejador.text_guia.transform.parent.gameObject.SetActive(false);
			manejador.panelInfo.SetActive (false);
			manejador.panelPrincipal.SetActive (true);
			manejador.panelPrincipal_victoria.SetActive (true);

		}

		
	}
	
	
	IEnumerator Wait(){

		yield return new WaitForSeconds(2);
		manejador.panelPrincipal.SetActive (true);
		manejador.panelPrincipal_victoria.SetActive (false);
		manejador.panelPrincipal_ceulainfe.SetActive (true);
		manejador.boton_personaje.GetComponent<Button>().interactable=false;
		manejador.text_guia.transform.parent.gameObject.SetActive(false);
		manejador.panelInfo.SetActive (false);
		manejador.flecha_boton.SetActive (false);
		Time.timeScale = 0;
	}

	public void QuitarSonidos(){
		
		GameObject [] celulas = GameObject.FindGameObjectsWithTag ("celula");
		Camera.main.transform.FindChild ("Audio Source").gameObject.SetActive (false);
		foreach (GameObject celu in celulas) {
			
			celu.GetComponent<AudioSource> ().enabled = false;
			
		}
	}
}
