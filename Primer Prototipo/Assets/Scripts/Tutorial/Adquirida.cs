using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Adquirida : MonoBehaviour {


	
	//ZOOM
	public Zoom zoom;
	Ray pulsacion;
	RaycastHit hit ;
	
	
	//Estados
	public EstadoTutorial[] Estados;
	

	//Panel Innata
	public GameObject panel_innata; 
	
	
	//Textos en Pantalla
	public GameObject textos;
	
	//Panel de Zoom
	public GameObject panelZoom;
	public Text	tituloZoom;
	public Text textoZoom;
	
	//Flechas apuntando a objetos de Objetos
	public GameObject flechaLinfoB;
	public GameObject flechaTCD4;
	public GameObject flechaTCD8;
	
	
	//ObjetosInteractivos
	public GameObject LinfoB;
	public GameObject TCD4;
	public GameObject TCD8;

	
	

	public int estadoActual;
	
	//Sonido al Hacer zoom
	public AudioSource zoom_sound;
	
	public GameObject boton_empezar;
	
	
	// Use this for initialization
	void Start () {
	
		VasoGrande.activarLinfocitos = false;
		Estados= new EstadoTutorial[3];
		Estados [0] = new EstadoTutorial (flechaLinfoB,LinfoB, "Linfocito B", "Los linfocitos B son los leucocitos que producen unas moléculas llamadas anticuerpos. \n\n"+
											"Los anticuerpos te permiten atacar y neutralizar a los bichos.\n\n"+
											"Actívalo ¡! Y combate directamente el virus.");
		Estados [1] = new EstadoTutorial (flechaTCD4,TCD4,"Linfocito TCD4" ,"Colaboradores o ayudadores. Son células encargadas de modular la respuesta inmune y ayudar a otras células a que funcionen mejor.");
		Estados [2] = new EstadoTutorial (flechaTCD8,TCD8,"Linfocito TCD8","Los linfocitos TCD8 o citotóxicos son los encargados de neutralizar las células infectadas por microorganismos intracelulares, mediante un ataque directo a estas células");
		zoom = this.GetComponent<Zoom> ();	

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
				
				if (hit.collider.name.Equals ("LinfoncitoB")) {	
					
					Estados[0].Activar(1);
					tituloZoom.text=Estados[0].titulo;
					textoZoom.text=Estados[0].texto;
					panelZoom.GetComponent<RectTransform>().anchoredPosition=new Vector2(100,4);
					zoom_sound.Play();
					estadoActual=1;
					panel_innata.SetActive(false);
					boton_empezar.SetActive(false);
					flechaLinfoB.SetActive(false);
					zoom.enfocar(LinfoB.transform,1,0,30);
				}
				if (hit.collider.name.Equals ("LinfoncitoTCD4")) {	
					
					Estados[1].Activar(1);
					tituloZoom.text=Estados[1].titulo;
					textoZoom.text=Estados[1].texto;
					panelZoom.GetComponent<RectTransform>().anchoredPosition=new Vector2(-100,4);
					zoom_sound.Play();
					panel_innata.SetActive(false);
					boton_empezar.SetActive(false);
					estadoActual=2;
					flechaTCD4.SetActive(false);
					zoom.enfocar(TCD4.transform,-8,0,30);
				}
				
				if (hit.collider.name.Equals ("LinfoncitoTCD8")) {	
					
					Estados[2].Activar(2);
					tituloZoom.text=Estados[2].titulo;
					textoZoom.text=Estados[2].texto;
					panelZoom.GetComponent<RectTransform>().anchoredPosition=new Vector2(-100,4);
					zoom_sound.Play();
					panel_innata.SetActive(false);
					boton_empezar.SetActive(false);
					estadoActual=3;
					flechaTCD8.SetActive(false);
					zoom.enfocar(TCD8.transform,-3,0,30);
				}
			}
		}
	}
	
	public void cambiarPanelPrincipal(int num){
		
		switch (num) {
			
		case 1:
			break;
			//llamado por panel zoom
		case 2:
				Destroy (GameObject.Find ("Canvas"));
				Destroy (GameObject.Find ("Creador"));
				Destroy (GameObject.Find ("ManejadorVirus"));
				ManejadorVirus.celulas.Clear ();
				Application.LoadLevelAsync (estadoActual + 10);
			break;
			
		case 3:
			Destroy (GameObject.Find ("Canvas"));
			Destroy (GameObject.Find ("Creador"));
			Destroy (GameObject.Find ("ManejadorVirus"));
			ManejadorVirus.celulas.Clear ();
			Application.LoadLevelAsync (14);

			break;

		}
	}
}

