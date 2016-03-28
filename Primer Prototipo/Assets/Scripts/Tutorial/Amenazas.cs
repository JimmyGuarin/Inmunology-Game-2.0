using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Amenazas: MonoBehaviour {
	
	
	
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
	public GameObject flechavirus;
	public GameObject flechamutante;
	public GameObject flechabacteria;
	
	
	//ObjetosInteractivos
	public GameObject virus;
	public GameObject mutante;
	public GameObject bacteria;
	
	
	
	
	public int estadoActual;
	
	//Sonido al Hacer zoom
	public AudioSource zoom_sound;
	
	public GameObject boton_empezar;
	
	
	// Use this for initialization
	void Start () {
		
		Estados= new EstadoTutorial[3];
		Estados [0] = new EstadoTutorial (flechavirus,virus, "FLU Virus", "Descripcion FLU Virus");
		Estados [1] = new EstadoTutorial (flechamutante,mutante,"Mutador" ,"Descripcion mutante");
		Estados [2] = new EstadoTutorial (flechabacteria,bacteria,"Bacteria","Descripcion bacteria");
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
				
				if (hit.collider.name.Equals ("Virus")) {	
					
					Estados[0].Activar(1);
					tituloZoom.text=Estados[0].titulo;
					textoZoom.text=Estados[0].texto;
					panelZoom.GetComponent<RectTransform>().anchoredPosition=new Vector2(100,4);
					zoom_sound.Play();
					estadoActual=1;
					panel_innata.SetActive(false);
					boton_empezar.SetActive(false);
					flechavirus.SetActive(false);
					zoom.enfocar(virus.transform,1,0,30);
				}
				if (hit.collider.name.Equals ("Mutante")) {	
					
					Estados[1].Activar(1);
					tituloZoom.text=Estados[1].titulo;
					textoZoom.text=Estados[1].texto;
					panelZoom.GetComponent<RectTransform>().anchoredPosition=new Vector2(-100,4);
					zoom_sound.Play();
					panel_innata.SetActive(false);
					boton_empezar.SetActive(false);
					estadoActual=2;
					flechamutante.SetActive(false);
					zoom.enfocar(mutante.transform,-7,2,40);
				}
				
				if (hit.collider.name.Equals ("Bacteria")) {	
					
					Estados[2].Activar(2);
					tituloZoom.text=Estados[2].titulo;
					textoZoom.text=Estados[2].texto;
					panelZoom.GetComponent<RectTransform>().anchoredPosition=new Vector2(-100,4);
					zoom_sound.Play();
					panel_innata.SetActive(false);
					boton_empezar.SetActive(false);
					estadoActual=3;
					flechabacteria.SetActive(false);
					zoom.enfocar(bacteria.transform,-1,0,30);
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
			zoom.desenfocar (false);
			textos.SetActive (true);
			zoom_sound.Play();
			flechavirus.SetActive(true);
			flechamutante.SetActive(true);
			flechabacteria.SetActive(true);
			panel_innata.SetActive(true);
			boton_empezar.SetActive(true);
			break;
			
		case 3:
			break;
			
		}
	}
}


	

