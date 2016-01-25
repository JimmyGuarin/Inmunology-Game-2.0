using UnityEngine;
using System.Collections;

public class Mutaciones : MonoBehaviour {


	private int mutacion;
	public Material mutacion1;
	public Material mutacion2;
	public Material mutacion3;
	public Material mutacion4;


	

	// Use this for initialization
	void Start () {

		mutacion = 1;
		GetComponent<Renderer>().enabled = true;
		if (ManejadorVirus.mutando)
			InvokeRepeating ("mutar",Random.Range(15f,25f),Random.Range(10f,25f));
		else
			Destroy (this);

	}
	
	// Update is called once per frame
	void Update () {

	}

	void mutar()  {



		if (mutacion < 4) {
			GetComponentInParent<ColisionesVirus> ().mutacion = mutacion;
			switch (mutacion) {
			case 0:
			
				mutacion++;
				GetComponent<Renderer>().materials [0].color = mutacion1.color;
				break;
			
			case 1:

				GetComponent<Renderer>().materials [0].color = mutacion2.color;
				mutacion++;
				break;
			case 2:
				GetComponent<Renderer>().materials [0].color = mutacion3.color;
				mutacion++;
				break;
			case 3:
				GetComponent<Renderer>().materials [0].color = mutacion4.color;
				mutacion++;
				SendMessageUpwards("ultimaMutacion");
				break;
			}


		
		} else {
			CancelInvoke();

		}
	}





}
