using UnityEngine;
using System.Collections;

public class ManejadorVirus : MonoBehaviour {

	public static ArrayList celulas = new ArrayList();
	public static ArrayList celulas_objetivos = new ArrayList();
	public static int celulas_infectadas;
	public  GameObject virus;
	public static int numeroVirus;
	// Use this for initialization
	public static bool analizado;
	public static bool mutando;
	public float tiempoSalidaPrimerVirus;
	public float tiempoSalidaVirus;
	public int virus_zona_afectada;
	public int i;
	public bool tutorial;
	public GameObject PanelTutorial;
	public static bool tutorialStatic;
	void Start () {

			tutorialStatic = tutorial;
			Debug.Log (tutorialStatic);
			i = 0;
			celulas_infectadas = 0;
			NotificationCenter.DefaultCenter ().AddObserver (this, "VirusDestruido");
			analizado = false;
			numeroVirus = 0;
			InvokeRepeating("invocar",tiempoSalidaPrimerVirus,tiempoSalidaVirus);
			if(Application.loadedLevelName.Equals("3"))
		   		mutando=true;
	}

	void invocar(){
			
			if(i<virus_zona_afectada){

				Instantiate (virus);
				i++;

			}
			

		} 
	
	// Update is called once per frame
	void Update () {
	
		if (numeroVirus <= 0&&i>=virus_zona_afectada&&celulas_infectadas==0) {
			
			CancelInvoke();
			if(tutorial==false)
				ControladorRecursos.ganar();
			else{
				Time.timeScale = 0;
				PanelTutorial.SetActive(true);
			}

			Destroy(this);
		}
		
	}

	public ArrayList m_celulas
	{
		get { return celulas; }
		set { celulas = value; }
	}	
	
	public static void actualizarDefenza(){


		celulas_objetivos = celulas;
		if (celulas_objetivos.Count == 0) {
		
				if(tutorialStatic==false)
					ControladorRecursos.invadido ();
				else
					Camera.main.GetComponent<DestresaInnata>().celulasMuertas();
		
		}
			

	}
		

}