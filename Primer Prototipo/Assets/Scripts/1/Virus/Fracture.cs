using UnityEngine;
using System.Collections;

public class Fracture : MonoBehaviour {

	public float speed=3f;
	public Vector3 destino;
	public bool ganglio;

	// Use this for initialization
	void Start () {
	
		speed = 4f;
		destino = new Vector3 (47.8f, -22.2f, -5f);
		ganglio = true;
		Invoke ("fracturar", 1f);

	}
	
	// Update is called once per frame
	void Update () {
	

		if (this.transform.position.Equals (destino)) {
				
				if(ManejadorVirus.analizado==false&&ganglio==true){
					
					ControladorRecursos.virusAnalizado();
					ManejadorVirus.analizado=true;
				}

				ControladorRecursos.puntaje+=50;
				ManejadorVirus.numeroVirus--;
				Debug.Log(ManejadorVirus.numeroVirus);
				Destroy(this.gameObject);

			
		} else{
			
			float step = speed * Time.deltaTime;
			transform.position = Vector3.MoveTowards (transform.position, destino, step);
			
			

			}
		
	}

	void fracturar(){

		GetComponent<Animator> ().enabled = true;
	}
}
