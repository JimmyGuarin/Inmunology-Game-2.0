using UnityEngine;
using System.Collections;
using System;
using System.IO;
using UnityEngine.UI;

public class prueba : MonoBehaviour {

	public UnityEngine.UI.InputField nombre;
	public UnityEngine.UI.InputField contraseña;
	public AudioSource sonido;
	private string ruta; 
	private String [] arreglo;
	private  float    timeLive;

	public  void miFuncion(){

	
		if (verificar () == true) {


			Application.LoadLevel("Inicio");

		} else {

		}
	
	}
	public void registro(){

		Application.LoadLevel("Registro");

	}

	public void Start () {
	
		ruta= Application.persistentDataPath + "/datos.txt";
		cargar ();
	}


	public  void cargar(){



		int numero = 0;
		StreamReader sr = new StreamReader (ruta);

		while (sr.ReadLine()!=null) {

			numero++;

		}
		sr.Close ();
		sr = new StreamReader (ruta);

		 arreglo = new string[numero];
		Debug.Log (arreglo.Length);


		for (int i=0; i<arreglo.Length; i++) {

			String linea=sr.ReadLine();
			arreglo[i]=linea;
		}
		sr.Close ();



	}

	public bool verificar(){

		for (int i=0; i<arreglo.Length; i++) {

			Debug.Log(arreglo[i]);
			String [] user_pass=arreglo[i].Split(',');
			if(user_pass[0].Equals(this.nombre.text)&&user_pass[1].Equals(this.contraseña.text)){

				return true;

			}
		}

		return false;
	}
}
