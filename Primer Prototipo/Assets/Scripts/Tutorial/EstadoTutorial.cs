//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17929
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using UnityEngine;
using UnityEngine.UI;

	public class EstadoTutorial
	{

		public GameObject objeto;
		public GameObject flecha;
		public string titulo; 
		public String texto;


		public EstadoTutorial (GameObject flecha, GameObject objeto,String titul, String texto)
		{
			this.objeto = objeto;
			this.flecha = flecha;
			this.titulo = titul;
			this.texto = texto;
		}

		public void Activar(int numero){

				flecha.SetActive (false);
				
				switch (numero) {
				
					case 0:
						objeto.GetComponent<VasoGrande>().enabled=true;
						break;
				}
				objeto.GetComponent<Animator> ().enabled = true;
				objeto.GetComponent<Collider> ().enabled = false;
				
		}

		public string m_titulo
		{
			get { return titulo; }
			set { titulo = value; }
		}	

		public string m_texto
		{
			get { return texto; }
			set { texto = value; }
		}	

		public void Preparar(){
			
			flecha.SetActive (true);
			objeto.GetComponent<Collider> ().enabled = true;

		}

	}


