using UnityEngine;
using System.Collections;

public class Autodestruir : MonoBehaviour {

	// Use this for initialization
	void Start () {
	

		Invoke ("destruir", 3f);
	}
	
	// Update is called once per frame
	void Update () {
	

	}

	void destruir(){

		DestroyObject (this.gameObject);
	}
}
