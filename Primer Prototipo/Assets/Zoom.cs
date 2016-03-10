using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;

public class Zoom : MonoBehaviour {
	


	private Transform objeto;
	public Vector3 posicion_inicial;
	public bool change;
	public bool enfocador;
	public bool desenfocador;
	public GameObject panel1;
	public GameObject minipanel;
	private int x;
	private int y;
	private int z;
	// Use this for initialization
	void Start () {

		z = 0;
		change = false;
		posicion_inicial = new Vector3(0,0,-137.2f);

	}
	
	// Update is called once per frame
	void Update () {
	


			                        	
	
		if (enfocador == true) {

			panel1.SetActive(false);
			transform.position= Vector3.Lerp(transform.position,this.objeto.position+new Vector3(x,y,-50),0.02f);
			if(transform.position.z>=-z){
				
				minipanel.SetActive(true);
				enfocador=false;
			}
		}
		if (desenfocador == true) {
		
			transform.position= Vector3.Lerp(transform.position,posicion_inicial,0.05f);
			if(transform.position.z<=-136&&change==true){
				panel1.SetActive(true);
			}		
		}



	}




	public void enfocar(Transform obj,int axisx,int axisy,int profundidad){
	
		this.objeto = obj;
		desenfocador = false;
		enfocador = true;
		x = axisx;
		y = axisy;
		z = profundidad;
	}

	public void desenfocar(bool activarPanel){

		enfocador = false;
		desenfocador = true;
		minipanel.SetActive (false);
		change = activarPanel;

	}
}
