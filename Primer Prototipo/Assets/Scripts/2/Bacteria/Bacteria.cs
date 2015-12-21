﻿using UnityEngine;
using System.Collections;

public class Bacteria : MonoBehaviour {

	public float speed=2f;
	public Vector3 destino;
	public bool capturado;
	public float vida;
	//ultima mutacion
	public bool ultima;
	public GameObject fracturado;
	public bool comiendo;
	public int  celulaObjetivo;
	public float x;
	public float aux_x;
	public float y;
	// Use this for initialization 
	public void Start () {
		
		aux_x = 5f;
		speed = 2f;
		comiendo = false;
		ManejadorVirus.numeroVirus++;
		Debug.Log ("virus:"+ManejadorVirus.numeroVirus);
		NotificationCenter.DefaultCenter ().AddObserver (this, "atrapado");
		ultima = false;
		capturado=false;
		vida = 1000;
		
		if (this.gameObject.name.Equals ("VirusFinal(Clone)")) {
			destino = new Vector3 (Random.Range (this.transform.position.x - 6, this.transform.position.x + 10), Random.Range (-22f, 28f), -5f);
		} else {
			destino = new Vector3 (Random.Range (this.transform.position.x - 20, this.transform.position.x + 20), Random.Range (-22f, 28f), -5f);
		}
		
		
		
		
	}
	
	// Update is called once per frame
	void Update () {
		
		
		// Su vida se agoto ?
		if (vida <= 0) {
			
			ControladorRecursos.puntaje+=40;
			ManejadorVirus.numeroVirus--;
			Debug.Log("virus: "+ManejadorVirus.numeroVirus);
			Destroy(this.gameObject);
			
		}
		x=this.transform.position.x;
		y=this.transform.position.y;
		if (x < -52 || x > 42 || y > 29 || y < -23)
			destino = this.transform.position;
		if (this.transform.position.Equals (destino)) { //llego a su destino ?
			
		
			if(x<=-52){

				this.transform.position=new Vector3(-52,y,-5f);
				aux_x=5f;
			}
			if(x>=42){

				aux_x=-5f;
				this.transform.position=new Vector3(42,y,-5f);
			}
				
			if(y>=29){

				this.transform.position=new Vector3(x,29,-5f);
				destino = new Vector3 (x +aux_x,y -5f, -5f);
			}
				
			if(y<=-23){
				this.transform.position=new Vector3(x,-23,-5f);
				destino = new Vector3 (x +aux_x,y +5f, -5f);
			}
				
			else if(y>-23&&y<29)
					destino = new Vector3 (x +aux_x,Random.Range (y -5f,y +5f), -5f);


		} else { // No ha llegado a su destino 
			
			float step = speed * Time.deltaTime;
			transform.position = Vector3.MoveTowards (transform.position, destino, step);
			

			
		}
		
	}
	
	// El virus ha sido atrapado
	public void atrapado(Notification notification){
		
		
		if (capturado==true) {
			Destroy(this.GetComponent<Collider>());
			speed = 1f;
			destino = (Vector3)notification.data;
		} else {
			
			
		}
	}
	

}
