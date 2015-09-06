using UnityEngine;
using System.Collections;

public class Nuevo : MonoBehaviour {


	Vector3 destino;
	public float speed;
	// Use this for initialization
	void Start () {
	
		destino = new Vector3(MoverPuntoEncuentro.posicion.x,MoverPuntoEncuentro.posicion.y,-5f);
		speed=3f;

	}
	
	// Update is called once per frame
	void Update () {
	


		float step = speed * Time.deltaTime;
		this.transform.position = Vector3.MoveTowards (transform.position, destino, step);
	}



		

}
