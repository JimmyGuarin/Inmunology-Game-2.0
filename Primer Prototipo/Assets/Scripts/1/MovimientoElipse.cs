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
		rango = Random.Range (4f, 5f);
	}
	
	// Update is called once per frame
	void Update () {
	
		Vector3 v=new Vector3(centroX + Mathf.Sin (angulo) * radioX, centroY + Mathf.Cos (angulo) * radioY, -5f);
		this.transform.position = v;
		angulo+=vel;

	
		if (salir==true&&angulo>rango&&angulo <(rango+0.05)){


			if(gameObject.name.Equals("Neutrofilo(Clone)"))
				GetComponent<ManejarNeutrofilo>().enabled=true;
			if(gameObject.name.Equals("LinfoncitoTCD4(Clone)"))
				GetComponent<TCD4>().enabled=true;
			if(gameObject.name.Equals("LinfoncitoTCD8(Clone)"))
				GetComponent<TCD8>().enabled=true;
			if(gameObject.name.Equals("LinfoncitoB(Clone)"))
				GetComponent<LinfocitoB>().enabled=true;
			if(gameObject.name.Equals("MacrofagoFinal(Clone)"))
				GetComponent<Macrofago>().enabled=true;


			
			Destroy(this);

		
		}

	}
	void OnTriggerEnter(Collider MyTrigger) {
		
		
		if (MyTrigger.gameObject.name.Equals ("Eliminador")) {
			
			Destroy(this.gameObject);
			
			
		}
		
	}
	
}
