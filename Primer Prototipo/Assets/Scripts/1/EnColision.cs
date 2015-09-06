using UnityEngine;
using System.Collections;

public class EnColision : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision c){

		if (c.gameObject.tag.Equals("CelulaDentrita"))
			Debug.Log ("Celula Dentrita");
		if (c.gameObject.tag.Equals ("Virus"))
			Debug.Log ("Virus");


	}
}
