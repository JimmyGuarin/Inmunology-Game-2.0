using UnityEngine;
using System.Collections;
using System;
using System.IO;
using UnityEngine.UI;
public class Manejador : MonoBehaviour {


	String ruta; 
	public InputField nombre;
	public InputField apellido;
	public InputField codigo;
	public InputField semestre;
	public InputField userName;
	public InputField password;
	public InputField password2;
	public Text error;


	public void atras(){

		Application.LoadLevel ("Login");
	} 	

  public void avanzar(){



		if (!nombre.text.Equals("") && !apellido.text.Equals("")&& !codigo.text.Equals("") && 
		    !semestre.text.Equals("") &&
		    !userName.text.Equals("") && !password.text.Equals("") && !password2.text.Equals("")) {

			if(password.text.Equals(password2.text)){

				guardar();
				Debug.Log("Gurdado");
				Application.LoadLevel ("Login");
			}
			else{

				error.text="Las contraseñas no coiciden";


			}
		} else {

			error.text="Faltan Campos por llenar";

		}







	}

	public void Start () {


		ruta=Application.persistentDataPath + "/datos.txt";
		Debug.Log (ruta);


		 new FileStream (ruta, FileMode.OpenOrCreate);



	}

	public void guardar(){


		String cadena = userName.text + "," + password.text+","+ nombre.text + "," + apellido.text + "," + codigo.text + "," +
			semestre.text;

		Debug.Log ("ruta:" + ruta);
		StreamWriter sw = new StreamWriter (ruta,true);
		
		//Write a line of text
		sw.WriteLine(cadena);
		

		
		//Close the file
		sw.Close();
	

	
}

}
