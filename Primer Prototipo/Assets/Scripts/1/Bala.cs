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
