using UnityEngine;
using System.Collections;

public class TutorialTCD4 : MonoBehaviour {

	public GameObject linfocitoB;

	public GameObject macrofago;
	public GameObject virus_macrofago;

	public GameObject neutrofilo;
	public GameObject virus_neutrofilo;

	public AdquiridaTutorial manejador;

	// Use this for initialization
	void Start () {
	
		manejador = GetComponent<AdquiridaTutorial> ();
		NotificationCenter.DefaultCenter ().AddObserver (this, "TCD4Tutorial");

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void EmpezarDesafio(){

		Invoke ("EmpezarAyuda", 4);

	}

	public void EmpezarAyuda(){

		macrofago.SetActive (true);
		virus_macrofago.SetActive (true);
		manejador.info_macrofago.text="El Linfoncito TCD4 ayuda al macrófago para que la fagocitosis sea más eficiente.";
		manejador.text_guia.text="Presiona click derecho sobre el Linfocito TCD4 para activar la habilidad de ayudar al Macrófago.";

	}

	void TCD4Tutorial(Notification notification)
	{
		int index_receiver = (int)notification.data;

		if (index_receiver == 1) {
		
			manejador.info_macrofago.text="El Linficito TCD4 ayuda al Neutrófilo, liberando citoquinas o moléculas mensajeras del sistema inmune.";
			manejador.text_guia.text="Presiona click derecho sobre el Linfocito TCD4 para activar la habilidad de ayudar al Neutrófilo.";

			neutrofilo.SetActive(true);
			if(virus_neutrofilo!=null)
				virus_neutrofilo.SetActive(true);
		}

		if (index_receiver == 2) {
			
			manejador.info_macrofago.text="El linfoncito TCD4 ayuda al Linfocito B convirtiéndolo en una celula plasmática,la cual es una célula profesional que solo produce anticuerpos. ";
			manejador.text_guia.text="Presiona click derecho sobre el Linfocito TCD4 para activar la habilidad de ayudar al Linfocito B.";
			linfocitoB.SetActive(true);
			
		}

		if (index_receiver == 3) {
			
			manejador.panelInfo.SetActive(false);
			manejador.text_guia.text=PlayerPrefs.GetString("name")+" Defiendete de los patogenos ";
			manejador.text_guia.transform.parent.gameObject.GetComponent<RectTransform>().anchoredPosition=new Vector2(
				manejador.text_guia.transform.parent.gameObject.GetComponent<RectTransform>().anchoredPosition.x,
				manejador.text_guia.transform.parent.gameObject.GetComponent<RectTransform>().anchoredPosition.y+80);
			GameObject.Find("ManejadorVirus").GetComponent<ManejadorVirus>().enabled=true;
			
		}

	}
}
