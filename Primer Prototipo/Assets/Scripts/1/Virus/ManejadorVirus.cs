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

	public int virus_zona_afectada;
	private int i;

	void Start () {

			i = 0;
			celulas_infectadas = 0;
			NotificationCenter.DefaultCenter ().AddObserver (this, "VirusDestruido");
			analizado = false;
			numeroVirus = 0;
			InvokeRepeating("invocar",10,16f);
	}

	void invocar(){
			
			if(i<virus_zona_afectada){

				Instantiate (virus);
				i++;

			}
			

		} 
	
	// Update is called once per frame
	void Update () {
	
		if (numeroVirus == 0&&i>=virus_zona_afectada&&celulas_infectadas==0) {
			
			CancelInvoke();
			ControladorRecursos.ganar();
			
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
		if(celulas_objetivos.Count==0)
			ControladorRecursos.invadido ();

	}
		

}