using UnityEngine;
using System.Collections;

public class MovimientoElipse : MonoBehaviour {

	public float centroX=78;
	public float centroY=9;
	public float angulo=3.5f;
	public int radioX=28;
	public int radioY=28;
	public float vel=0.02f;
	public bool salir=false;
	public float rango;

	// Use this for initialization
	void Start () {
	

		centroX = Random.Range (77f, 78f);
		rango = Random.Range (10.6f, 11.4f);
	}
	
	// Update is called once per frame
	void Update () {
	
		Vector3 v=new Vector3(centroX + Mathf.Sin (angulo) * radioX, centroY + Mathf.Cos (angulo) * radioY, -5f);
		this.transform.position = v;
		angulo+=vel;

	
		if (salir==true&&angulo>rango&&angulo <(rango+0.05)){


			GetComponent<Nuevo>().enabled=true;
			Destroy(this);

		
		}

	}
	void OnTriggerEnter(Collider MyTrigger) {
		
		
		if (MyTrigger.gameObject.name.Equals ("Eliminador")) {
			
			Destroy(this.gameObject);
			
			
		}
		
	}
	
}
