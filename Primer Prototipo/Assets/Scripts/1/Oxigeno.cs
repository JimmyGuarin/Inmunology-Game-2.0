using UnityEngine;
using System.Collections;

public class Oxigeno : MonoBehaviour {

	Rigidbody r;

	[Range(-1, 1)]
	public float y;
	// Use this for initialization
	void Start () {
	
		r = GetComponent<Rigidbody> ();


	}
	
	// Update is called once per frame
	void Update () {
	
		r.AddForce (new Vector3(-0.7f,y,-0.8f)*5, ForceMode.Force);
	
	}

	void OnCollisionEnter(Collision colision) {
		
		
		if((colision.collider.name.Equals("Techo")))
			
			Destroy (this.gameObject);	
		
	}
}