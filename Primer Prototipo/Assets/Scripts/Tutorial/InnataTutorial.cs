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
	
	//Panel para salir del tutorial
	public GameObject panelSalida;


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








	// Use this for initialization
	void Start () {
	
		estadoActual = estado;
		Estados= new EstadoTutorial[5];
		Estados [0] = new EstadoTutorial (flechaDendritica,Dendritica,"Células dendríticas" ,"Las células dendríticas (DC) son células fagocíticas presentes en tejidos que están en contacto con el medio externo, " +
			"principalmente la y el revestimiento mucoso interno de la nariz, pulmones, estómago e intestino. Se llaman así por su analogía con las dendritas neuronales, pero las células dendríticas no están conectadas " +
			"al sistema nervioso. Las células dendríticas son muy importantes en la presentación de antígenos, y sirven como enlace entre los sistemas inmunitarios innatos y adaptativo.");

		Estados [1] = new EstadoTutorial (flechaNeutrofilo,Neutrofilo,"Neutrófilos" ,"Los neutrófilos son conocidos como granulocitos debido a la presencia de gránulos en su citoplasma, o como células polimorfo nuclear (PMNs) " +
			"debido a sus distintivos núcleos lobulados. Los gránulos del neutrófilo contienen una variedad de sustancias tóxicas que matan o inhiben el crecimiento de bacterias y hongos. Similares a los macrófagos, los neutrófilos " +
			"atacan a los patógenos mediante la activación de una 'brecha respiratoria'. Los productos principales de la brecha respiratoria del neutrófilo son fuertes agentes oxidantes incluyendo el peróxido de hidrógeno, los radicales " +
			"libres de oxígeno y el hipoclorito. Los Neutrófilos son los tipos celulares fagocíticos más abundantes, normalmente representan el 50 a 60% del total de leucocitos circulantes, y son usualmente las primeras células en llegar al " +
			"sitio de una infección.");

		Estados [2] = new EstadoTutorial (flechaMacrofago,Macrofago,"Macrófagos","Los macrófagos, vocablo proveniente del Griego, significa 'gran célula comedora', son leucocitos fagocíticos grandes, que son capaces de moverse al exterior del " +
			"sistema vascular al atravesar la membrana celular de los vasos capilares y entrando en áreas intercelulares en persecución de los patógenos invasores. Son los fagocitos más eficientes, y pueden fagocitar números substanciales de bacterias " +
			"u otras células o microbios.");

		Estados [3] = new EstadoTutorial (flechaNK,Nk, "Natural Killer", "Las células NK (por las siglas de su denominación en inglés, natural killer, 'asesina natural' en español) también conocidas como células asesinas son un tipo de linfocito pertenecientes al sistema inmunitario. " +
			"Estas células no destruyen los microorganismos patógenos directamente, teniendo una función más relacionada con la destrucción de células infectadas o que puedan ser cancerígenas. No son células fagocíticas. Destruyen las otras células a través del ataque a su " +
			"membrana plasmática causando difusión de iones y agua para el interior de la célula aumentando su volumen interno hasta un punto de ruptura en el cual ocurre la lisis.");

		zoom = this.GetComponent<Zoom> ();	

		if (estadoActual> 0) {
			Estados[estadoActual].Preparar();
			panelPrincipal_1.SetActive(false);
			textos.SetActive(true);
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
					
					Estados[estadoActual].Activar(estadoActual+1);
					tituloZoom.text=Estados[estadoActual].titulo;
					textoZoom.text=Estados[estadoActual].texto;
					panelZoom.GetComponent<RectTransform>().anchoredPosition=new Vector2(100,4);
					zoom.enfocar(Dendritica.transform,1,0,40);
					estadoActual++;
				}
				if (hit.collider.name.Equals ("Neutrofilo")) {	
					
					Estados[estadoActual].Activar(estadoActual);
					tituloZoom.text=Estados[estadoActual].titulo;
					textoZoom.text=Estados[estadoActual].texto;
					panelZoom.GetComponent<RectTransform>().anchoredPosition=new Vector2(-100,4);
					zoom.enfocar(Neutrofilo.transform,-15,0,40);
					estadoActual++;
				}
				
				if (hit.collider.name.Equals ("Macrofago")) {	
					
					Estados[estadoActual].Activar(estadoActual);
					tituloZoom.text=Estados[estadoActual].titulo;
					textoZoom.text=Estados[estadoActual].texto;
					panelZoom.GetComponent<RectTransform>().anchoredPosition=new Vector2(100,4);
					zoom.enfocar(Macrofago.transform,15,0,40);
					estadoActual++;
				}

				if (hit.collider.name.Equals ("NaturalK")) {	
					
					Estados[estadoActual].Activar(estadoActual);
					tituloZoom.text=Estados[estadoActual].titulo;
					textoZoom.text=Estados[estadoActual].texto;
					panelZoom.GetComponent<RectTransform>().anchoredPosition=new Vector2(-100,4);
					zoom.enfocar(Nk.transform,-3,0,30);
					estadoActual++;
				}
				

				
			}
		}
	}

	public void cambiarPanelPrincipal(int num){
		
		switch (num) 
		{
			
		case 1:
			panelPrincipal_11.SetActive(false);
			panelPrincipal_12.SetActive(true);
			break;
		case 2:
			panelPrincipal_11.SetActive(true);
			panelPrincipal_12.SetActive(false);
			break;
			
		case 3:
			panelPrincipal_1.SetActive(false);
			panelPrincipal_11.SetActive(true);
			panelPrincipal_12.SetActive(false);
			empezarVisualizar();
			break;
			//llamado por panel zoom
		case 5:
			panelZoom.SetActive(false);
			if(estadoActual<4){
				Destroy(GameObject.Find("Canvas"));
				Application.LoadLevel(estadoActual+5);
			}
			else{
				
				zoom.desenfocar(false);
			}
			break;
			
		case 6:
			Application.LoadLevel(0);
			break;
			
		}
	}

	public void empezarVisualizar(){

		textos.SetActive (true);
		flechaDendritica.SetActive (true);
		Dendritica.GetComponent<Collider> ().enabled = true;
	}



}
