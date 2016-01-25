using UnityEngine;
using System.Collections;

public class Fracture : MonoBehaviour {

	public float speed=3f;
	public Vector3 destino;
	public bool identificado;
	public int mutacion;
	public bool ganglio;

	void Awake(){
	
		this.gameObject.name = "virusFinalFracture(Clone)";
	
	}

	// Use this for initialization
	void Start () {
	

		Invoke ("fracturar", 1f);

	}
	void Update()
    {
        if (GetComponentInParent<CrearUnidadInnata>()!=null&&
            GetComponentInParent<CrearUnidadInnata>().enabled==false)
        {
			ManejadorVirus.numeroVirus--;
			Debug.Log("virus: "+ManejadorVirus.numeroVirus);
			Destroy(this.gameObject);
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
