using UnityEngine;
using System.Collections;

public class Eventos : MonoBehaviour {


	public GameObject explosion1;
	public static GameObject explosion2;
	public  float dañooVirus;
	public static float dañoVirus;
	// Use this for initialization
	void Start () {
	
		explosion2 = explosion1;
		dañoVirus = dañooVirus;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static void explosion(Vector3 v) {

		Instantiate(explosion2,new Vector3(v.x,v.y-2,-7),explosion2.transform.rotation);
	}
}
