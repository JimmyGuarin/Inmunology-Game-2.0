using UnityEngine;
using System.Collections;

public class TAmovimientoDefenzas : MonoBehaviour {

	public int rango_x;
	public Rigidbody bala;
	public int velocidad;

	public bool captura=false;
	private Vector3 destino;
	private float x;
	// Use this for initialization
	void Start () {
	
		x = transform.position.x;
		destino = new Vector3 (Random.Range (transform.position.x - rango_x, transform.position.x + rango_x),transform.position.y+
		                       Random.Range (10,-10), -5);

		if(bala!=null)
			InvokeRepeating ("disparar", 1.0f, 2.0f);
	}

	
	// Update is called once per frame
	void Update () {
	
		//se esta cambiando la posicion hasta que llega a destino
		float step = velocidad * Time.deltaTime;

		this.transform.position = Vector3.MoveTowards (transform.position, destino, step);


		if (destino.y > 30) {
		
			destino=new Vector3(destino.x,30,-5);  
		}
		if (destino.y <- 30) {
			
			destino=new Vector3(destino.x,-30,-5);  
		}



		if (transform.position == destino)
			destino = new Vector3 (Random.Range (x - rango_x,x + rango_x),
			                       transform.position.y+
			                       Random.Range (10,-10), -5);
	}

	void disparar(){

		if (this.gameObject.name.Equals ("LFTransformado")) {
			Rigidbody clone=(Rigidbody) Instantiate (bala,(this.transform.position+new Vector3(-3,0,0)),bala.transform.rotation);
			clone.velocity = transform.TransformDirection ((new Vector3(0,1,0))*20);
			Rigidbody clone1=(Rigidbody) Instantiate (bala,(this.transform.position+new Vector3(-3,-1,0)),bala.transform.rotation);
			clone1.velocity = transform.TransformDirection ((new Vector3(-0.2f,1,0))*20);
			Rigidbody clone2=(Rigidbody) Instantiate (bala,(this.transform.position+new Vector3(-3,1,0)),bala.transform.rotation);
			clone2.velocity = transform.TransformDirection ((new Vector3(0.2f,1,0))*20);
		} else {
		
			Rigidbody clone=(Rigidbody) Instantiate (bala,(this.transform.position+new Vector3(-3,0,0)),bala.transform.rotation);
			clone.velocity = transform.TransformDirection ((new Vector3(-1,0,0))*10);

		}
	}

}
