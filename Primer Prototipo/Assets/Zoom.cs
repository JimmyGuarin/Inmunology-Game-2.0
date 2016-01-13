using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;

public class Zoom : MonoBehaviour {
	
	public Transform objeto;
	public GameObject panel;
	public GameObject minipanel;
	Camera mycam;
	public Vector3 posicion_inicial;
	public bool change;
	// Use this for initialization
	void Start () {

		change = false;
		mycam = GetComponent<Camera> ();
		posicion_inicial = new Vector3(0,0,-137.9f);

	}
	
	// Update is called once per frame
	void Update () {
	
		if (change == false) {
		
			desenfocar ();
			if(transform.position.z<-136){
				panel.SetActive(true);
				
			}
		} else {

			enfocar(objeto);
			if(transform.position.z>=-26){

				minipanel.SetActive(true);
			}
			                        	
		}

		}
	public void changeState(){
	
		if (change) {
			change = false;
			minipanel.SetActive(false);
		}
			
		else {
			panel.SetActive (false);
			change = true;
		}
	}


	void enfocar(Transform objecto){


		transform.position= Vector3.Lerp(transform.position,objeto.position+new Vector3(5,0,-20),0.05f);

	}

	void desenfocar(){

		transform.position= Vector3.Lerp(transform.position,posicion_inicial,0.05f);

	}
}
