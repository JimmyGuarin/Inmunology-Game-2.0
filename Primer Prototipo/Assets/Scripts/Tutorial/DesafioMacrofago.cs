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

	//Virus de pruebas
	public GameObject primer_virus;
	
	public Text texto_Desafio;


	private bool enCombate;

	// Use this for initialization
	void Start () {

		NotificationCenter.DefaultCenter ().AddObserver (this, "celulaMuerta");
		NotificationCenter.DefaultCenter ().AddObserver (this, "crearMacrofago");
		NotificationCenter.DefaultCenter ().AddObserver (this, "MacrofagoTutorial");
		NotificationCenter.DefaultCenter ().AddObserver (this, "CambiarGuiaMacrofago");
		NotificationCenter.DefaultCenter ().AddObserver (this, "TerminarTutorial");

		texto_Desafio.text += " "+PlayerPrefs.GetString ("name") + ", " +
			"tienes 100 segundos para fagocitar 3 bichos en forma de Macrófago y capturar 3 en forma de Célula Dendrítica. ";
		
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
			panelPrincipal_1.SetActive(true);
			panelInfoMacrofago.SetActive(false);
			text_guia.transform.parent.gameObject.SetActive(false);
			boton_macrofago.GetComponent<Button>().interactable=false;
			break;
			
		case 4:
			fagocitados=0;
			atrapados=0;
			textos.gameObject.transform.FindChild ("fagocitados").GetComponent<Text> ().text = " 0";
			comenzarDesafio.SetActive(false);
			panelPrincipal_1.SetActive(false);
			panelPrincipal_13.SetActive(false);
			flecha_macrofago.SetActive(true);
			boton_macrofago.GetComponent<Button>().interactable=true;
			textos.gameObject.SetActive(true);
			GameObject.Find("ManejadorVirus").GetComponent<ManejadorVirus>().enabled=true;
			enCombate=true;
			InvokeRepeating("ManejarTiempo",1f,1f);
			text_guia.text="Presiona click izquierdo para desplegar los Macrófagos";
			text_guia.transform.parent.gameObject.SetActive(true);
			
			text_guia.transform.parent.gameObject.GetComponent<RectTransform>().anchoredPosition=new Vector2(
				text_guia.transform.parent.gameObject.GetComponent<RectTransform>().anchoredPosition.x,
				text_guia.transform.parent.gameObject.GetComponent<RectTransform>().anchoredPosition.y+50);
			
			
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
			Application.LoadLevel(8);	
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
		panelPrincipal_13.SetActive (false);
		panelPrincipal_14.SetActive (false);
		panelPrincipal_15.SetActive (true);
		panelPrincipal_1.SetActive (true);
		boton_macrofago.GetComponent<Button>().interactable=false;
		text_guia.transform.parent.gameObject.SetActive(false);
		panelInfoMacrofago.SetActive (false);
		flecha_macrofago.SetActive (false);
		Time.timeScale = 0;
	}

	void crearMacrofago(Notification notification)
	{	
		if (flecha_macrofago.activeSelf == true) {
			flecha_macrofago.SetActive (false);
			text_guia.text="Presiona click izquierdo sobre el Macrófago para seleccionarlo";
			info_macrofago.text="En este estado los macrófagos capturan los bichos  y los fagocitan (comen) lentamente hasta destruirlos por completo.";
			if(!enCombate)
				GameObject.Find("Macrofago(Clone)").GetComponent<FuncionesMacrofago>().enabled=false;
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

			if(index_guia<=3){

				if(enCombate==false){

					GameObject.Find("Macrofago(Clone)").GetComponent<FuncionesMacrofago>().enabled=false;
				}
			}

			
			if (index_guia == 1) {
				
				text_guia.text="Presiona click izquierdo en el lugar a mover el Macrófago";			
			}
			if (index_guia == 2) {
				
				text_guia.text="Captura el bicho para que el Macrófago lo fagocite";
				primer_virus.SetActive(true);
			}

			if (index_guia == 3) {
				
				info_macrofago.text="En este momento puedes ver como el Macrófago fagocita el bicho mientras disminuye la vida de este";
				text_guia.text="Espera a que el bicho esté destruido";
			}



			if (index_guia == 4){

				GameObject.Find("Macrofago(Clone)").GetComponent<FuncionesMacrofago>().enabled=true;
				text_guia.text="Presiona click derecho sobre el Macrófago para ver la habilidad especial";
				info_macrofago.text="El Macrófago ha fagocitado al bicho, que impresionante no ?";
			}
			if (index_guia == 5){
				
				text_guia.text="Presiona click izquierdo para transformar el Macrófago a célula dendrítica";
				
			} 

			if (index_guia == 6){
				
				info_macrofago.text="El Macrófago ahora es una célula dendrítica con la habilidad de CAPTURAR " +
					"al patógeno y alertar al vaso sanguineo";
				text_guia.text="Es hora de empezar el desafio "+PlayerPrefs.GetString("name");
				comenzarDesafio.SetActive(true);
				
			} 

			index_guia++;	
		}
	}

	void TerminarTutorial(Notification notification)
	{	
		QuitarSonidos ();
		text_guia.transform.parent.gameObject.SetActive(false);
		panelPrincipal_1.SetActive (true);
		panelPrincipal_13.SetActive (false);
		panelPrincipal_14.SetActive (true);
		
	}
	
	public void QuitarSonidos(){
		
		GameObject [] celulas = GameObject.FindGameObjectsWithTag ("celula");
		Camera.main.transform.FindChild ("Audio Source").gameObject.SetActive (false);
		foreach (GameObject celu in celulas) {
			
			celu.GetComponent<AudioSource> ().enabled = false;
			
		}
	}
}
	