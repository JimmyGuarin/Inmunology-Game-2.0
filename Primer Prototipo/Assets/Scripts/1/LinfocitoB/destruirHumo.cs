using UnityEngine;
using System.Collections;

public class destruirHumo : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
		Invoke ("destruir", 2f);
	}
	
	void destruir(){

		Destroy (this.gameObject);
	}
}
