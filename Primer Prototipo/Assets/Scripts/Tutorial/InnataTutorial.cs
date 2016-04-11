using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InnataTutorial : MonoBehaviour {

	
	//ZOOM
	public Zoom zoom;
	Ray pulsacion;
	RaycastHit hit ;

	
	//Estados
	public EstadoTutorial[] Estados;
	
	// Paneles Primarios de navegacion
	public GameObject panelPrincipal_1;
	public GameObject panelPrincipal_11;
	public GameObject panelPrincipal_12;
	public GameObject panelPrincipal_13;

	//Panel para salir del tutorial
	public GameObject panelSalida;

	//Panel Innata
	public GameObject panel_innata; 


	//Textos en Pantalla
	public GameObject textos;

	//Panel de Zoom
	public GameObject panelZoom;
	public Text	tituloZoom;
	public Text textoZoom;
	
	//Flechas apuntando a objetos de Objetos
	public GameObject flechaDendritica;
	public GameObject flechaNeutrofilo;
	public GameObject flechaMacrofago;
	public GameObject flechaNK;
	
	//ObjetosInteractivos
	public GameObject Dendritica;
	public GameObject Neutrofilo;
	public GameObject Macrofago;
	public GameObject Nk;


	//Manejador de Estados
	public static int estado;
	public int estadoActual;

	//Sonido al Hacer zoom
	public AudioSource zoom_sound;

	//Boton para empezar el desafio
	public GameObject boton_empezar;

	//Boton para mostrar o no el panel.
	public static bool mostrarPanel;

	// Use this for initialization
	void Start () {
	
		estadoActual = estado;
		Estados= new EstadoTutorial[5];
		Estados [0] = new EstadoTutorial (flechaDendritica,Dendritica,"Células dendríticas" ,"Las células dendríticas están presentes en los tejidos y patrullan constantemente tu cuerpo en busca de alguna enfermedad o daño");

		Estados [1] = new EstadoTutorial (flechaNeutrofilo,Neutrofilo,"Neutrófilos" ,"Los neutrófilos hacen parte de los granulocitos debido a la presencia de gránulos en su citoplasma. \n\n"+
		                                  "Son las células  más abundantes del sistema inmune, normalmente representan el 50 a 60% del total de leucocitos circulantes.\n\n"+
		                                  "Los gránulos del neutrófilo contienen una variedad de sustancias tóxicas que matan o inhiben el crecimiento de bacterias, hongos y virus.");

		Estados [2] = new EstadoTutorial (flechaMacrofago,Macrofago,"Macrófagos","Los macrófagos son células cuya función  principal es la fagocitosis y la reparación. \n\n"+
		                                  "La fagocitosis  es la función de ingerir elementos externos y digerirlos en su interior. \n\n"+
		                                  "Los macrófagos son los fagocitos más eficientes, y pueden fagocitar bacterias, hongos, y otras células muertas.");

		Estados [3] = new EstadoTutorial (flechaNK,Nk, "Natural Killer", "Las células NK, Natural Killer o asesina natural no destruyen los bichos directamente, pero si son capaces de destruir la célula infectada.");

		zoom = this.GetComponent<Zoom> ();


		if (estadoActual==1) {
			Estados[0].objeto.GetComponent<Collider>().enabled=true;
			panelPrincipal_1.SetActive(false);
			textos.SetActive(true);
			panel_innata.SetActive(true);
			flechaDendritica.SetActive(true);
			flechaMacrofago.SetActive(true);
			flechaNeutrofilo.SetActive(true);
			flechaNK.SetActive(true);
		}


	}
	
	// Update is called once per frame
	void Update () {
	
		//Si el click es el clik derecho(0)
		if (Input.GetMouseButtonDown (0)) {
			
			pulsacion = Camera.main.ScreenPointToRay (Input.mousePosition);
			
			if (Physics.Raycast (pulsacion, out hit)) {

				textos.SetActive(false);
				
				/*
				 * Condicion que pregunta si el collider
				 * con el que colisiona el rayo es el 
				 * collider de algun elemento interactivo
				 * */
				
				if (hit.collider.name.Equals ("Dentrica")) {	
					
					Estados[0].Activar(1);
					tituloZoom.text=Estados[0].titulo;
					textoZoom.text=Estados[0].texto;
					panelZoom.GetComponent<RectTransform>().anchoredPosition=new Vector2(100,4);
					zoom_sound.Play();
					estadoActual=1;
					panel_innata.SetActive(false);
					boton_empezar.SetActive(false);

					zoom.enfocar(Dendritica.transform,1,0,40);
				}
				if (hit.collider.name.Equals ("Neutrofilo")) {	
					
					Estados[1].Activar(1);
					tituloZoom.text=Estados[1].titulo;
					textoZoom.text=Estados[1].texto;
					panelZoom.GetComponent<RectTransform>().anchoredPosition=new Vector2(-100,4);
					zoom_sound.Play();
					panel_innata.SetActive(false);
					boton_empezar.SetActive(false);
					estadoActual=2;
					zoom.enfocar(Neutrofilo.transform,-15,0,40);
				}
				
				if (hit.collider.name.Equals ("Macrofago")) {	
					
					Estados[2].Activar(2);
					tituloZoom.text=Estados[2].titulo;
					textoZoom.text=Estados[2].texto;
					panelZoom.GetComponent<RectTransform>().anchoredPosition=new Vector2(100,4);
					zoom_sound.Play();
					panel_innata.SetActive(false);
					boton_empezar.SetActive(false);
					estadoActual=3;
					zoom.enfocar(Macrofago.transform,15,0,40);
				}

				if (hit.collider.name.Equals ("NaturalK")) {	
					
					Estados[3].Activar(3);
					tituloZoom.text=Estados[3].titulo;
					textoZoom.text=Estados[3].texto;
					panelZoom.GetComponent<RectTransform>().anchoredPosition=new Vector2(-100,4);
					zoom_sound.Play();
					panel_innata.SetActive(false);
					estadoActual++;
					boton_empezar.SetActive(false);
					zoom.enfocar(Nk.transform,-3,0,30);
					estadoActual=4;
				}
				

				
			}
		}
	}

	public void cambiarPanelPrincipal(int num){
		
		switch (num) {
			
		case 1:
			panelPrincipal_11.SetActive (false);
			panelPrincipal_12.SetActive (true);
			panelPrincipal_13.SetActive (false);
			break;
		case 2:
			panelPrincipal_11.SetActive (true);
			panelPrincipal_12.SetActive (false);
			break;
		
		case 3:
			panelPrincipal_13.SetActive (true);
			panelPrincipal_12.SetActive (false);
			break;
		
		case 4:
			panelPrincipal_1.SetActive (false);
			panelPrincipal_11.SetActive (true);
			panelPrincipal_13.SetActive (false);
			break;
		


		//llamado por panel zoom
		case 5:
			panelZoom.SetActive (false);

			if (estadoActual != 4) {
				Destroy (GameObject.Find ("Canvas"));
				Destroy (GameObject.Find ("Creador"));
				Destroy (GameObject.Find ("ManejadorVirus"));
				ManejadorVirus.celulas.Clear ();
				Application.LoadLevelAsync (estadoActual + 5);
			}
			if (estadoActual == 4) {
				zoom.desenfocar (false);
				textos.SetActive (true);
				zoom_sound.Play();
				panel_innata.SetActive(true);
				boton_empezar.SetActive(true);
			}
			break;
			
		case 6:
			Application.LoadLevel (0);
			break;

		case 7:
			Destroy (GameObject.Find ("Canvas"));
			Destroy (GameObject.Find ("Creador"));
			Destroy (GameObject.Find ("ManejadorVirus"));
			Application.LoadLevelAsync (9);
			break;
		}
	}

	public void empezarVisualizar(){

		textos.SetActive (true);
		flechaDendritica.SetActive (true);
		Dendritica.GetComponent<Collider> ().enabled = true;
	}



}
