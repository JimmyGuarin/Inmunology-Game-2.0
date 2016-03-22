using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DesafioMacrofago : MonoBehaviour {

	
	// Paneles Primarios de navegacion
	public GameObject panelPrincipal_1;
	public GameObject panelPrincipal_11;
	public GameObject panelPrincipal_12;
	public GameObject panelPrincipal_13;
	public GameObject panelPrincipal_14;
	public GameObject panelPrincipal_15;
	public GameObject panelPrincipal_16;

	//Flecha apuntando al boton
	public GameObject flecha_macrofago;

	//Boton para crear macrofago
	public GameObject boton_macrofago;

	//Texto de numeros del desafio
	public Text textos;

	private int fagocitados;
	private int atrapados;
	public int tiempo;

	//Panel de informacion sobre el macrofago
	public GameObject panelInfoMacrofago;
	//Texto del panelInfoMacrofago
	public Text info_macrofago;


	//Texto guia e index de la guia 
	public Text text_guia;
	public int index_guia;

	//Boton para empezar el desafio
	public GameObject comenzarDesafio;

	// Use this for initialization
	void Start () {

		NotificationCenter.DefaultCenter ().AddObserver (this, "celulaMuerta");
		NotificationCenter.DefaultCenter ().AddObserver (this, "crearMacrofago");
		NotificationCenter.DefaultCenter ().AddObserver (this, "MacrofagoTutorial");
		NotificationCenter.DefaultCenter ().AddObserver (this, "CambiarGuiaNeutrofilo");

		flecha_macrofago.SetActive(true);
		boton_macrofago.GetComponent<Button>().interactable=true;
		text_guia.transform.parent.gameObject.SetActive(true);

		index_guia = 1;
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
			panelPrincipal_13.SetActive(false);
			flecha_macrofago.SetActive(true);
			boton_macrofago.GetComponent<Button>().interactable=true;
			textos.gameObject.SetActive(true);
			GameObject.Find("ManejadorVirus").GetComponent<ManejadorVirus>().enabled=true;
			InvokeRepeating("ManejarTiempo",1f,1f);
			break;
			
		case 5:
			InnataTutorial.estado=3;
			Destroy(GameObject.Find("Canvas"));
			Destroy(GameObject.Find("Creador"));
			Application.LoadLevel(5);
			break;
			
		case 6:
			Destroy(GameObject.Find("Canvas"));
			Destroy(GameObject.Find("Creador"));
			Application.LoadLevel(7);	
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

	void crearMacrofago(Notification notification)
	{	
		if (flecha_macrofago.activeSelf == true) {
			flecha_macrofago.SetActive (false);
			text_guia.text="Preciona click izquierdo sobre el Macrófago para seleccionarlo";
		}
		if (index_guia == 1) {
			
			panelInfoMacrofago.SetActive (true);
			info_macrofago.gameObject.SetActive (true);
			boton_macrofago.GetComponent<Button>().interactable=false;
		} else {
			
			GameObject macrofago=(GameObject)notification.data; 
			macrofago.GetComponent<Macrofago>().desafio_macrofago=false;
			
			text_guia.transform.parent.gameObject.SetActive(false);
		}
	}

	void MacrofagoTutorial(Notification notification)
	{	
		bool fagocitado = (bool)notification.data;

		if (fagocitado == true)
			fagocitados++;
		else
			atrapados++;

		textos.text = "" + atrapados;
		textos.gameObject.transform.FindChild ("fagocitados").GetComponent<Text> ().text = "" + fagocitados;

			
		if (atrapados >= 3 && fagocitados >= 3) {
				
			Time.timeScale=0;
			panelPrincipal_14.SetActive(true);
			panelPrincipal_1.SetActive(true);
		}

	}

	void ManejarTiempo(){
		
		tiempo--;
		textos.gameObject.transform.FindChild ("tiempo").GetComponent<Text>().text=""+tiempo+".s";
		
		if (tiempo == 0) {
			
			textos.gameObject.SetActive(false);
			Time.timeScale=0;
			panelPrincipal_1.SetActive(true);
			if (atrapados >= 3 && fagocitados >= 3) {

				panelPrincipal_14.SetActive(true);

			}
			else{

				panelPrincipal_16.SetActive(true);

			}

		}
	}


	void CambiarGuiaMacrofago(Notification notification)
	{	
		if (index_guia == (int)notification.data) {
			
			
			if (index_guia == 1) {
				
				text_guia.text="Preciona click izquierdo en el lugar a mover el Macrófago";			
			}
			if (index_guia == 2) 
				text_guia.text="Preciona click derecho sobre el Macrófago para ver su primera habilidad";
			if (index_guia == 3) 
				text_guia.text="Activa con click izquierdo la habilidad de degranulación";
			if (index_guia == 4){
				
				text_guia.text="Ahora activa la habilidad de trampa extracelular";
				info_macrofago.text="En este estado neutrófilos emiten granulocitos que atacan directamente al virus, " +
					"recuerda que estas partículas también dañan tus células.";
			}
			if (index_guia == 5){
				
				info_macrofago.text="En este estado el neutrófilo se suicida formando una red o malla " +
					"que captura el virus y disminuye la vida del mismo, pero ten cuidado pues al realizarlo perderás tu " +
						"neutrófilo y le net desaparecerá con el paso del tiempo. ";
				text_guia.text="Es hora de empezar el desafío";
				comenzarDesafio.SetActive(true);
				
			} 
			
			index_guia++;	
		}
	}
}
	