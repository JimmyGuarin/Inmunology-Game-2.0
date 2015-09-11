using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ControladorInmuAdquirida : MonoBehaviour {



	public Button linfoB;
	public Button Killer;
	public Button TCD4;
	public Button TCD8;
	public SeleccionarUnidad seleccionador;


	void Start(){


	}



	public void desbloquearLinfocitos(){



		linfoB.GetComponent<Button> ().interactable = true;
		Killer.GetComponent<Button> ().interactable = true;
		TCD4.GetComponent<Button> ().interactable = true;
		TCD8.GetComponent<Button> ().interactable = true;


	}


}
