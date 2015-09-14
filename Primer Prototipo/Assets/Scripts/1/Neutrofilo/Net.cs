using UnityEngine;
using System.Collections;

public class Net : MonoBehaviour {


	public float factor;
	public float darktotal;
	public MeshRenderer render;
	private float r;
	private float b;
	private float g;
	private bool desvan;
	public  float daño;

	// Use this for initialization
	void Start () {

		daño = 5;
		desvan = false;
		r = render.materials [0].color.r;
		b=	render.materials [0].color.b;
		g=	render.materials [0].color.g;
		darktotal=render.materials [1].color.a;
		Invoke ("desvanecer", 15f);
	}
	
	// Update is called once per frame
	void Update () {
	
				

		if (desvan== true) {
		
			daño=daño-(factor*5);
		
			if (darktotal > 0.05) {
				
				render.materials [0].color = new Color (r, g,b, darktotal);
				render.materials [1].color = new Color (1f, 1f, 1f, darktotal);
				darktotal -= factor;

				
			} else {

				Destroy(GetComponent<Collider>());
				Destroy(this.gameObject);
			}
		
		
		
		
		
		}



	}

	 void desvanecer(){

		desvan = true;
	}
}
