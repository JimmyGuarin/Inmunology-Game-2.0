using UnityEngine;
using System.Collections;

public class ManejadorVirus : MonoBehaviour {

	public static ArrayList celulas = new ArrayList();
	public static ArrayList celulas_objetivos = new ArrayList();
	public  GameObject virus;
	public static int numeroVirus;
	// Use this for initialization
	public static bool analizado;


	private int i=1;
	void Start () {


			NotificationCenter.DefaultCenter ().AddObserver (this, "VirusDestruido");
			analizado = false;
			numeroVirus = 0;
			InvokeRepeating("invocar",10,16f);
	}

	void invocar(){
			
		if (numeroVirus <= 30) {

			if(i==1){

				Instantiate (virus);
				i++;

			}
			else{
				CancelInvoke ();
			}

		} else {

			CancelInvoke ();
			if(numeroVirus>=30){
				ControladorRecursos.invadido();
			}
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (numeroVirus == 0&&i>10) {

			ControladorRecursos.ganar();
			Debug.Log("aa");
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