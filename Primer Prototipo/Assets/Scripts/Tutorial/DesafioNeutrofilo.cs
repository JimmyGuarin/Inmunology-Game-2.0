using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DesafioNeutrofilo : MonoBehaviour {

	// Paneles Primarios de navegacion
	public GameObject panelPrincipal_1;
	public GameObject panelPrincipal_11;
	public GameObject panelPrincipal_12;
	public GameObject panelPrincipal_13;
	public GameObject panelPrincipal_14;
	public GameObject panelPrincipal_15;

	public GameObject panelInfoNeutrofilo;
	public Text info_neutrofilo;


	public GameObject flecha_neutrofilo;
	public Text tiempo_text;
	private int tiempo;

	public Text text_guia;
	public int index_guia;

	public GameObject boton_neutrofilo;

	public GameObject comenzarDesafio;
	// Use this for initialization
	void Start () {

		tiempo = 90;
		index_guia = 1;
		NotificationCenter.DefaultCenter ().AddObserver (this, "celulaMuerta");
		NotificationCenter.DefaultCenter ().AddObserver (this, "crearNeutrofilo");
		NotificationCenter.DefaultCenter ().AddObserver (this, "TerminarTutorial");
		NotificationCenter.DefaultCenter ().AddObserver (this, "CambiarGuiaNeutrofilo");
		flecha_neutrofilo.SetActive(true);
		boton_neutrofilo.GetComponent<Button>().interactable=true;
		text_guia.transform.parent.gameObject.SetActive(true);

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

			panelPrincipal_1.SetActive(true);
			panelInfoNeutrofilo.SetActive(false);
			text_guia.transform.parent.gameObject.SetActive(false);
			boton_neutrofilo.GetComponent<Button>().interactable=false;
			comenzarDesafio.SetActive(false);
			break;
		case 3:

			flecha_neutrofilo.SetActive(true);
			tiempo_text.gameObject.SetActive(true);
			InvokeRepeating("ManejarTiempo",1f,1f);
			break;

		case 4:

			panelPrincipal_1.SetActive(false);
			panelPrincipal_14.SetActive(true);
			panelPrincipal_13.SetActive(false);
			flecha_neutrofilo.SetActive(true);
			boton_neutrofilo.GetComponent<Button>().interactable=true;
			text_guia.text="Presiona click izquierdo para desplegar los Neutrófilos";
			text_guia.transform.parent.gameObject.SetActive(true);

			text_guia.transform.parent.gameObject.GetComponent<RectTransform>().anchoredPosition=new Vector2(
				text_guia.transform.parent.gameObject.GetComponent<RectTransform>().anchoredPosition.x,
				text_guia.transform.parent.gameObject.GetComponent<RectTransform>().anchoredPosition.y+50);

			tiempo_text.gameObject.SetActive(true);
			InvokeRepeating("ManejarTiempo",1f,1f);
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
			Application.LoadLevel(7);	
			break;
			
		case 7:
			ControladorMenu.in_tutorial=true;
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
		text_guia.transform.parent.gameObject.SetActive(false);
		Time.timeScale = 0;
	}





	void crearNeutrofilo(Notification notification)
	{	
		if (flecha_neutrofilo.activeSelf == true) {
			flecha_neutrofilo.SetActive (false);
			text_guia.text="Presiona click izquierdo sobre el Neutrófilo para seleccionarlo";
		}
		if (index_guia == 1) {
			
			panelInfoNeutrofilo.SetActive (true);
			info_neutrofilo.gameObject.SetActive (true);
			boton_neutrofilo.GetComponent<Button>().interactable=false;
		} else {

			GameObject neutrofilo=(GameObject)notification.data; 
			neutrofilo.GetComponent<ManejarNeutrofilo>().desafio_neutrofilo=false;

			text_guia.transform.parent.gameObject.SetActive(false);
		}
			
			
	}

	void ManejarTiempo(){
	
		tiempo--;

		tiempo_text.gameObject.GetComponent<Text>().text=""+tiempo+".s";

		if (tiempo == 0) {
		
			tiempo_text.gameObject.SetActive(false);
			text_guia.text= PlayerPrefs.GetString("name")+" Esperemos que tu defenza funcione.";
			text_guia.transform.parent.gameObject.SetActive(true);
			boton_neutrofilo.SetActive(false);
			GameObject.Find("ManejadorVirus").GetComponent<ManejadorVirus>().enabled=true;
			NotificationCenter.DefaultCenter ().PostNotification (this, "QuitarFunciones");
			GameObject.Find("Fondo1").GetComponent<Fondo1>().enabled=false;
		}
	}

	void TerminarTutorial(Notification notification)
	{	
		QuitarSonidos ();
		text_guia.transform.parent.gameObject.SetActive(false);
		panelPrincipal_1.SetActive (true);
		panelPrincipal_14.SetActive (true);
		
	}
	
	public void QuitarSonidos(){
		
		GameObject [] celulas = GameObject.FindGameObjectsWithTag ("celula");
		Camera.main.transform.FindChild ("Audio Source").gameObject.SetActive (false);
		foreach (GameObject celu in celulas) {
			
			celu.GetComponent<AudioSource> ().enabled = false;
			
		}
	}

	void CambiarGuiaNeutrofilo(Notification notification)
	{	


		if(index_guia<=5){

			index_guia = (int)notification.data;

			if (index_guia == 1) {
				
				text_guia.text="Presiona click izquierdo en el lugar a mover el Neutrófilo";			
			}
			if (index_guia == 2) 
				text_guia.text="Presiona click derecho sobre el Neutrófilo para ver su primera habilidad";
			if (index_guia == 3) 
				text_guia.text="Activa con click izquierdo la habilidad";
			if (index_guia == 4){
			
				text_guia.text="Ahora activa la habilidad de trampa extracelular";
				info_neutrofilo.text="En este estado neutrófilos emiten granulocitos que atacan directamente al virus, " +
					"recuerda que estas partículas también dañan tus células.";
			}
			if (index_guia == 5){
				
				info_neutrofilo.text="En este estado el neutrófilo se suicida formando una red o malla " +
					"que captura el virus y disminuye la vida del mismo, pero ten cuidado pues al realizarlo perderás tu " +
						"neutrófilo y le net desaparecerá con el paso del tiempo. ";
				text_guia.text="Es hora de empezar el desafío";
				comenzarDesafio.SetActive(true);

			} 

			index_guia++;	
		}
	}
}
