using UnityEngine;
using System.Collections;

public class ControladorInmuAdquirida : MonoBehaviour {


	public SeleccionarUnidad seleccionador;
	private GameObject[] arreglo;
	public Sprite LinfoB;
	public Sprite Killer;
	public Sprite TCD4Reducida;
	public Sprite TCD4;
	public Sprite TCD8;
	public Sprite TCD8Reducido;

	void Start(){

		arreglo = new SeleccionarUnidad [4];
		arreglo[0]=seleccionador.linfoB;
		arreglo[1]=seleccionador.killer;
		arreglo [2] = seleccionador.tcd4;
		arreglo[3]=seleccionador.tcd4;
	}


	public void cmhi () {


		SpriteRenderer s= STCD8.GetComponent<SpriteRenderer>(); 
		s.sprite = TCD8Reducido;	

	//	STCD8.nutrientes = 15;
	//	STCD8.oxigeno = 10;
		SpriteRenderer s1= STCD4.GetComponent<SpriteRenderer>(); 
		s1.sprite = TCD4;	

		desbloquear ();
	}

	public void cmhii(){

		SpriteRenderer s= STCD4.GetComponent<SpriteRenderer>(); 
		s.sprite = TCD4Reducida;	
		//STCD4.nutrientes = 15;
		//STCD4.oxigeno = 10;

		SpriteRenderer s1= STCD8.GetComponent<SpriteRenderer>(); 
		s1.sprite = TCD8;	

		desbloquear ();
	}

	void desbloquear(){

		SpriteRenderer s= SLinfoB.GetComponent<SpriteRenderer>(); 
		s.sprite = LinfoB;	
		SpriteRenderer s1= SKiller.GetComponent<SpriteRenderer>(); 
		s1.sprite = Killer;	


		foreach (SeleccionarUnidad co in arreglo) {

			co.GetComponent<Collider2D>().enabled=true;
		}

	}

}
