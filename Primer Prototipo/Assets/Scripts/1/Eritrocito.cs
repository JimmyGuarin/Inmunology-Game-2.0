using UnityEngine;
using System.Collections;

public class Eritrocito : MonoBehaviour {


	public float centroX=78;
	public float centroY=9;
	public float angulo=3.5f;
	public int radioX=28;
	public int radioY=28;
	public float vel=0.02f;



	// Use this for initialization
	void Start () {
	
		transform.rotation = Random.rotation;
		centroX = Random.Range (77f, 78f);

	}
	
	// Update is called once per frame
	void Update () {
	

		Vector3 v=new Vector3(centroX + Mathf.Sin (angulo) * radioX, centroY + Mathf.Cos (angulo) * radioY, -5f);
		this.transform.position = v;
		angulo+=vel;


	}

	void OnTriggerEnter(Collider MyTrigger) {
		
		
		if (MyTrigger.gameObject.name.Equals ("Eliminador")) {
			
			Destroy(this.gameObject);

				
			}
			
		}
}
