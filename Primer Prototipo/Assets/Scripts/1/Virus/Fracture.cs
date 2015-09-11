using UnityEngine;
using System.Collections;

public class Fracture : MonoBehaviour {

	public float speed=4f;
	public Vector3 destino;
	public bool ganglio;

	// Use this for initialization
	void Start () {
	
		speed = 4f;
		if(Vector3.Distance(transform.position,new Vector3(47.8f ,-22.2f  ,-10f  ))<
		   Vector3.Distance(transform.position,new Vector3(47.7f,10.8f,-10f  )))
			destino=new Vector3(47.8f ,-22.2f  ,this.transform.position.z );
		else destino=new Vector3(47.7f,10.8f,this.transform.position.z );
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

	void OnTriggerStay (Collider MyTrigger) {
		
		if (MyTrigger.gameObject.tag.Equals ("celula")) {
			
			MyTrigger.gameObject.GetComponent<ManejarCelula> ().audio1.Stop ();
			
		}
	}

	void OnTriggerEnter (Collider MyTrigger) {
			
		if (MyTrigger.gameObject.tag.Equals ("celula")) {
				
			MyTrigger.gameObject.GetComponent<ManejarCelula> ().audio1.Stop ();
				
		}
	}

	void OnTriggerExit (Collider MyTrigger) {
				
		if (MyTrigger.gameObject.tag.Equals ("celula")) {
					
			MyTrigger.gameObject.GetComponent<ManejarCelula> ().audio1.Stop ();
					
		}
	}
}
