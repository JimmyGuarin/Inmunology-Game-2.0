using UnityEngine;
using System.Collections;

public class BalaBacteria : MonoBehaviour {

	private int ataque;

	// Use this for initialization
	void Start () {
	
		Invoke ("destruir", 5f);
		ataque = 100;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void destruir(){
		
		Destroy (this.gameObject);
	}

	void OnTriggerEnter(Collider MyTrigger){

	
		if (MyTrigger.gameObject.name.Equals ("Neutrofilo(Clone)")) {
		
			MyTrigger.GetComponent<ManejarNeutrofilo> ().life -= ataque;
			Destroy(this.gameObject);
		}

		
		//MACROFAGO...........................................
		
		if (MyTrigger.gameObject.name.Equals ("Macrofago(Clone)")) {
			
			
			MyTrigger.GetComponent<Macrofago>().vida-=ataque;
			
			if (MyTrigger.GetComponent<Macrofago>().vida<= 0) {
				
				DestruirDefensa(MyTrigger.gameObject);
				if(MyTrigger.GetComponent<Macrofago>().mivirus!=null)
					ManejadorVirus.numeroVirus-=(transform.childCount-3);
			}
			Destroy(this.gameObject);
		}

		//Dendritica...........................................
		
		if (MyTrigger.gameObject.name.Equals ("Dentrica(Clone)")) {
			
			
			MyTrigger.GetComponent<CrearUnidadInnata>().vida-=ataque;
			
			if (MyTrigger.GetComponent<CrearUnidadInnata>().vida<= 0) {

				ManejadorVirus.numeroVirus-=(MyTrigger.transform.childCount-4);
				if(MyTrigger.GetComponent<ParticleSystem>().enableEmission ==true){


				}
				DestruirDefensa(MyTrigger.gameObject);
			}
			Destroy(this.gameObject);
		}

		//Natural Killer...........................................
		
		if (MyTrigger.gameObject.name.Equals ("NaturalK(Clone)")) {
			
			
			MyTrigger.GetComponent<TCD8>().vida-=ataque;
			
			if (MyTrigger.GetComponent<TCD8>().vida<= 0) {
				
				DestruirDefensa(MyTrigger.gameObject);
			}
			Destroy(this.gameObject);
		}


		//LINFOCITO B...........................................
		
		if (MyTrigger.gameObject.name.Equals ("LinfoncitoB(Clone)")) {
			

			MyTrigger.GetComponent<LinfocitoB>().vida-=ataque;
			
			if (MyTrigger.GetComponent<LinfocitoB>().vida<= 0) {
				
				DestruirDefensa(MyTrigger.gameObject);
			}
			Destroy(this.gameObject);
		}

		//LINFOCITO B MEJORADO.................................
		if (MyTrigger.gameObject.name.Equals ("linfocitoB(Clone)")) {
			
			Debug.Log("toca");
			MyTrigger.GetComponent<LinfocitoB2>().vida-=ataque;
			
			if (MyTrigger.GetComponent<LinfocitoB2>().vida<= 0) {
				
				DestruirDefensa(MyTrigger.gameObject);
			}
			Destroy(this.gameObject);
		}
		
		//LINFOCITO TCD4...........................................
		
		if (MyTrigger.gameObject.name.Equals ("LinfoncitoTCD4(Clone)")) {
			
			MyTrigger.GetComponent<TCD4>().vida-=ataque;
			if (MyTrigger.GetComponent<TCD4>().vida<= 0) {
				
				DestruirDefensa(MyTrigger.gameObject);
			}
			Destroy(this.gameObject);
		}
		
		//LINFOCITO TCD8...........................................
		
		if (MyTrigger.gameObject.name.Equals ("LinfoncitoTCD8(Clone)")) {
			
			MyTrigger.GetComponent<TCD8>().vida-=ataque;
			if (MyTrigger.GetComponent<TCD8>().vida<= 0) {
				
				DestruirDefensa(MyTrigger.gameObject);
			}
			Destroy(this.gameObject);
		}

	}

	public void DestruirDefensa(GameObject muerto){
		
		
		ControladorRecursos.defensas--;
		Destroy (muerto);
		
	}
}
