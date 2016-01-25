using UnityEngine;
using System.Collections;

public class PosicionSeleccionada : MonoBehaviour {

	public static int posicionar=0;
     public static ArrayList index;
	public static Rect [] Posiciones;
	public Texture2D imagen;
	public bool isSeleted;
	// Use this for initialization
	void Start () {
	

	}

	void OnTriggerEnter (Collider MyTrigger) {


		if (MyTrigger.gameObject.name.Equals ("virusFinalFracture(Clone)")) {

			GameObject virus=MyTrigger.gameObject.transform.FindChild("Fracture(Clone)").gameObject;

			if(virus!=null){

				Debug.Log("Tengo virus");
			}

		}

	}

}
