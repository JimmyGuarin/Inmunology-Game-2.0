using UnityEngine;
using System.Collections;

public class ColisionesVirus : MonoBehaviour {

	void Start(){
	
	

	
	}




	void OnCollisionEnter(Collision colision) {
		
		
		if(colision.collider.name.Equals("balaLifoncitoB(Clone)"))
		{
			this.gameObject.GetComponent<InteligenciaVirus> ().vida-=200;
			colision.gameObject.GetComponent<Rigidbody>().isKinematic=true;
			colision.gameObject.GetComponent<Collider>().enabled=false;
			colision.gameObject.GetComponent<Bala>().CancelInvoke();
			colision.gameObject.GetComponent<Bala>().enabled=false;
			colision.gameObject.transform.parent=this.transform;
			
			
		}
		
		
	}
	
	void OnTriggerEnter (Collider MyTrigger) {
		
		if (MyTrigger.gameObject.name.Equals ("Net")) {
			
			
			
		}
		
		if (MyTrigger.gameObject.name.Equals ("Dentrica(Clone)")) {
			
			
			if (this.gameObject.GetComponent<InteligenciaVirus> ().capturado == false) {
				
				
				this.gameObject.GetComponent<InteligenciaVirus> ().capturado = true;
				
				Instantiate(this.gameObject.GetComponent<InteligenciaVirus> ().fracturado,this.transform.position,
				            this.gameObject.GetComponent<InteligenciaVirus> ().fracturado.transform.rotation);
				Destroy(this.gameObject);
				NotificationCenter.DefaultCenter ().PostNotification (this, "llevarABase", this.transform.position);
			}
		}
		
		// Colisionado con celula denditrica ?
		if (MyTrigger.gameObject.name.Equals ("Neutrofilo(Clone)")||
		    MyTrigger.gameObject.name.Equals ("Macrofago(Clone)")) {
			
			if(this.gameObject.GetComponent<InteligenciaVirus> ().capturado==false){
				
				transform.position=MyTrigger.gameObject.transform.position;
			}
			
			
		}
		
		if (MyTrigger.gameObject.tag.Equals ("celula")) {
			
			this.gameObject.GetComponent<InteligenciaVirus> ().destino=transform.position;
			ManejarCelula mc=MyTrigger.GetComponent<ManejarCelula>();
			
			foreach (Celula i in ManejadorVirus.celulas_objetivos) {
				
				if(mc.c.m_identificador==i.m_identificador){
					this.gameObject.GetComponent<InteligenciaVirus> ().celulaObjetivo=i.m_identificador;
					break;
				}
				
			}	
			
		}
	}
	
	void OnTriggerStay (Collider MyTrigger) {
		
		
		if (MyTrigger.gameObject.name.Equals ("Net(Clone)")) {
			
			
			
			float xAux=MyTrigger.gameObject.transform.position.x;
			float yAux=MyTrigger.gameObject.transform.position.y;
			float zAux=MyTrigger.gameObject.transform.position.z;
			
			this.gameObject.GetComponent<InteligenciaVirus> ().destino= new Vector3(Random.Range(xAux-2,xAux+2),Random.Range(yAux-2,yAux+2),zAux);
			
			this.gameObject.GetComponent<InteligenciaVirus> ().vida-=MyTrigger.GetComponent<Net>().daño;
		}
		
		if (MyTrigger.gameObject.tag.Equals("celula")) {
			
			this.gameObject.GetComponent<InteligenciaVirus> ().comiendo=true;
			//Estoy comiendo
			
		}
		if (MyTrigger.gameObject.tag.Equals("muerta")&&this.gameObject.GetComponent<InteligenciaVirus> ().comiendo==true) {
			
			this.gameObject.GetComponent<InteligenciaVirus> ().comiendo=false;
			this.gameObject.GetComponent<InteligenciaVirus> ().destino=transform.position;
			//Llegue y no estoy comiendo 
			
			
		}
		
		
		// Colisionado con celula denditrica ?
		if (MyTrigger.gameObject.name.Equals ("Dentrica(Clone)")) {
			
			// Si no esta capturado
			if (this.gameObject.GetComponent<InteligenciaVirus> ().capturado == false) {
				
				this.gameObject.GetComponent<InteligenciaVirus> ().capturado = true;
				Destroy(this.GetComponent<Collider>());
				
				NotificationCenter.DefaultCenter ().PostNotification (this, "llevarABase", this.transform.position);
			}
		}
		
		
	}
	
	// Ya no esta comiendo
	void OnTriggerExit (Collider MyTrigger) {
		
		
		
		this.gameObject.GetComponent<InteligenciaVirus> ().comiendo = false;
		//destino=transform.position;
	}








}
