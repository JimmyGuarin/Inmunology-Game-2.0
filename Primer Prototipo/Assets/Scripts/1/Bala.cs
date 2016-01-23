using UnityEngine;
using System.Collections;

/// <summary>
/// Vala.
/// Controla los eventos que ocurren con la vala
/// </summary>
public class Bala : MonoBehaviour {


	void Start(){

		Invoke ("destruir", 2f);


	}

	void OnTriggerEnter(Collider MyTrigger){


		if (MyTrigger.gameObject.name.Equals ("Bacteria(Clone)")) {
			MyTrigger.gameObject.GetComponent<BacteriaColis> ().vida -= 200;
			MyTrigger.gameObject.GetComponentInChildren<BarraVida>().modificarSprite();
			GetComponent<Rigidbody> ().isKinematic = true;
			GetComponent<Collider> ().enabled = false;
			GetComponent<Bala> ().CancelInvoke ();
			GetComponent<Bala> ().enabled = false;
			transform.parent = MyTrigger.gameObject.transform;

		}
	}

	/**
	 * Cuando hay una colision con la vala
	 * esta siempre se destruye
	 * */
	void OnCollisionEnter(Collision colision) {

			
		if((colision.collider.name.Equals("balaLifoncitoB(Clone)")))
		
			Destroy (this.gameObject);	

	}

	void destruir(){

		Destroy (this.gameObject);
	}
}
