using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TutorialEntorno : MonoBehaviour {

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

	//Panel de Zoom
	public GameObject panelZoom;
	public Text	tituloZoom;
	public Text textoZoom;

	//Flechas apuntando a objetos de Objetos
	public GameObject flechaVaso;
	public GameObject flechaGanglio;
	public GameObject flechaZAfectada;

	//ObjetosInteractivos
	public GameObject vaso;
	public GameObject ganglio;
	public GameObject ZAfectada;
	public ManejadorVirus activarVirus;
	//Manejador de Estados
	public int estadoActual;

	//ContadorVirus
	public int contadorVirus;

	// Use this for initialization
	void Start () {
	
		estadoActual = 0;
		contadorVirus=10;
		Estados= new EstadoTutorial[4];
		Estados [0] = new EstadoTutorial (flechaVaso, vaso,"VASO SANGUINEO" ,"Un vaso sanguíneo es una estructura hueca y tubular que " +
			"conduce la sangre impulsada por la acción del corazón, que recogen la sangre de todos los rincones " +
			"del cuerpo. ");
		Estados [1] = new EstadoTutorial (flechaGanglio,ganglio," GANGLIO LINFATICO" ,"Los nódulos linfáticos o ganglios linfáticos son unas estructuras nodulares que forman parte del sistema linfático y " +
			"forman agrupaciones en forma de racimos. Los nódulos linfáticos actúan como filtros de la linfa, al poseer una estructura interna de tejido conectivo fino, en forma de red, rellena de linfocitos " +
			"que recogen y destruyen bacterias y virus, por lo que estos nódulos también forman parte del sistema inmunitario, ayudando al cuerpo a reconocer y combatir gérmenes, infecciones y otras sustancias extrañas.");
		Estados [2] = new EstadoTutorial (flechaZAfectada, ZAfectada, "ZONA AFECTADA", "¡SORPRESA! haz encontrado el lugar donde se rompió la matriz extracelular, de ahí llegaran los diferentes patógenos, no permitas que te invadan, destrúyelos con un CLICK.");
		zoom = this.GetComponent<Zoom> ();	
	}
	
	// Update is called once per frame
	void Update () {
	
		//Si el click es el clik derecho(0)
		if (Input.GetMouseButtonDown (0)) {

			pulsacion = Camera.main.ScreenPointToRay (Input.mousePosition);

			if (Physics.Raycast (pulsacion, out hit)) {


				/*
				 * Condicion que pregunta si el collider
				 * con el que colisiona el rayo es el 
				 * collider de algun elemento interactivo
				 * */
				
				if (hit.collider.name.Equals ("VasoFinal")) {	

					Estados[estadoActual].Activar(estadoActual);
					tituloZoom.text=Estados[estadoActual].titulo;
					textoZoom.text=Estados[estadoActual].texto;
					zoom.enfocar(vaso.transform,-20,0,70);
					estadoActual++;
				}
				if (hit.collider.name.Equals ("Ganglio")) {	
					
					Estados[estadoActual].Activar(estadoActual);
					tituloZoom.text=Estados[estadoActual].titulo;
					textoZoom.text=Estados[estadoActual].texto;
					zoom.enfocar(ganglio.transform,-10,2,70);
					estadoActual++;
				}

				if (hit.collider.name.Equals ("Zona Afectada")) {	
					
					Estados[estadoActual].Activar(estadoActual);
					tituloZoom.text=Estados[estadoActual].titulo;
					textoZoom.text=Estados[estadoActual].texto;
					panelZoom.GetComponent<RectTransform>().anchoredPosition=new Vector2(100,4);
					zoom.enfocar(ZAfectada.transform,10,5,70);
					estadoActual++;
				}

				if (hit.collider.name.Equals ("VirusFinal(Clone)")) {	
					
					contadorVirus--;
					Destroy(hit.collider.gameObject);
					if(contadorVirus==0){
						panelSalida.SetActive(true);
					}
				}
				
			}
		}
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
				panelPrincipal_13.SetActive(false);
				break;
			case 3:
				panelPrincipal_11.SetActive(false);
				panelPrincipal_13.SetActive(true);
				panelPrincipal_12.SetActive(false);
				break;

			case 4:
				panelPrincipal_1.SetActive(false);
				panelPrincipal_11.SetActive(true);
				panelPrincipal_12.SetActive(false);
				Invoke("empezarVisualizar",5f);
				break;
			//llamado por panel zoom
			case 5:
				panelZoom.SetActive(false);
				if(estadoActual<3){
					zoom.desenfocar(false);
					Estados[estadoActual].Preparar();
				}
				else{
					
					zoom.desenfocar(false);
					activarVirus.GetComponent<ManejadorVirus>().enabled=true;
				}
				break;
				
			case 6:
				Application.LoadLevel(0);
				break;
				
		}
	}

	public void empezarVisualizar(){

		flechaVaso.SetActive (true);
		vaso.GetComponent<Collider> ().enabled = true;
	}


}
