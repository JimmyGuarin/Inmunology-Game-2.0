using UnityEngine;
using System.Collections;

public class Oxigeno : MonoBehaviour {

	Rigidbody r;

	[Range(-1, 1)]
	public float y;
	// Use this for initialization
	void Start () {
	
		r = GetComponent<Rigidbody> ();
        
        r.AddForce(new Vector3(-0.7f, y, -0.2f) * 20, ForceMode.Impulse);

        Invoke("destruir", 1f);

    }
	
	// Update is called once per frame
	void Update () {
	
	
	}

    void destruir()
    {

        Destroy(this.gameObject);
    }
}