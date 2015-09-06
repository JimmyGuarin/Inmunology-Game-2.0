using UnityEngine;
using System.Collections;

public class ManejadorVirus : MonoBehaviour {

	public  GameObject virus;
	public static int numeroVirus;
	// Use this for initialization
	public static bool analizado;
	public GameObject celula1;
	public GameObject celula2;
	public GameObject celula3;
	public GameObject celula4;
	public GameObject celula5;
	public GameObject celula6;
	public GameObject celula7;
	public GameObject celula8;
	public GameObject celula9;
	public GameObject celula10;
	public GameObject celula11;
	public GameObject celula12;
	public static GameObject[] celulas;
	public static int lineaDefenza=1;

	private int i=0;
	void Start () {

		celulas = new GameObject[]{celula1,celula2,celula3,celula4,celula5,celula6,celula7,celula8,
			celula9,celula10,celula11,celula12};
			
			Debug.Log (celulas.Length);
			NotificationCenter.DefaultCenter ().AddObserver (this, "VirusDestruido");
			analizado = false;
			numeroVirus = 0;
			InvokeRepeating("invocar",10,16f);
	}

	void invocar(){
			
		if (numeroVirus <= 30) {

			if(i<=0){

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
	
		if (numeroVirus == 0&&i>5) {

			ControladorRecursos.ganar();
			Debug.Log("aa");
			Destroy(this);
		}
		
	}



		public static void actualizarDefenza(){

			int count = 0;
			int index = 1;
			for(int i=0; i<celulas.Length; i++) {
				
				if(celulas[i]!=null){
					
					index=i+1;
					count++;
					break;
				}
				
			}
		Debug.Log ("index" + index);
		if (count == 0) {

			lineaDefenza = 0;
			ControladorRecursos.invadido ();
		}
				
				

			if (index < 4) {
				
				lineaDefenza=3;
				return;
			}
			if (index < 8) {
				
				lineaDefenza=3;
				return;
			} else {
				lineaDefenza=3;
				return;
			}

		}
		

}
