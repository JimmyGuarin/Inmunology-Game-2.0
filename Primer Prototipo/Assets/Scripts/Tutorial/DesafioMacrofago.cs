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
	
	public GameObject flecha_macrofago;
	public GameObject boton_macrofago;
	public Text textos;

	private int fagocitados;
	private int atrapados;
	public int tiempo;

	// Use this for initialization
	void Start () {

		NotificationCenter.DefaultCenter ().AddObserver (this, "celulaMuerta");
		NotificationCenter.DefaultCenter ().AddObserver (this, "crearMacrofago");
		NotificationCenter.DefaultCenter ().AddObserver (this, "MacrofagoTutorial");

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
		if(flecha_macrofago.activeSelf==true)
			flecha_macrofago.SetActive (false);
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

}
	