using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ControladorBacterias : MonoBehaviour {

	public static Slider invasion;

	// Use this for initialization
	void Start () {
	
		invasion = GameObject.Find ("CanvasGanglio").transform.GetChild (1).GetComponent<Slider> ();		
		invasion.value = 0;
	}
	

	public static void aumentar(){
	
		invasion.value++;
		verificar_Invasion ();
	}

	public static void  verificar_Invasion(){

		if(invasion.value>=invasion.maxValue){

			ControladorRecursos.invadido();
		}

	}

    public static void disminuir()
    {

        invasion.value--;
    }

}
