using UnityEngine;
using System.Collections;

public class BacteriaDisparar : MonoBehaviour {

	public Rigidbody Bala;


	// Use this for initialization
	void Start () {
	
		InvokeRepeating ("disparar", 6.0f, 6.0f);

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void disparar(){
		
		Rigidbody clone=(Rigidbody) Instantiate (Bala,(this.transform.position+new Vector3(3,0,0)),Bala.transform.rotation);
		clone.velocity = transform.TransformDirection ((new Vector3(1,0,0))*5);
		
		
	}

}
