using UnityEngine;
using System.Collections;

public class Net : MonoBehaviour {


	public float factor;
	public float darktotal;
	public MeshRenderer renderer;
	private float r;
	private float b;
	private float g;
	private bool desvan;

	// Use this for initialization
	void Start () {


		desvan = false;
		r = renderer.materials [0].color.r;
		b=	renderer.materials [0].color.b;
		g=	renderer.materials [0].color.g;
		darktotal=renderer.materials [1].color.a;
		Invoke ("desvanecer", 15f);
	}
	
	// Update is called once per frame
	void Update () {
	
		if (desvan== true) {
		
		
			if (darktotal > 0.05) {
				
				renderer.materials [0].color = new Color (r, g,b, darktotal);
				renderer.materials [1].color = new Color (1f, 1f, 1f, darktotal);
				darktotal -= factor;
				
			} else {
				
				Destroy(this.gameObject);
			}
		
		
		
		
		
		}



	}

	 void desvanecer(){

		desvan = true;
	}
}
