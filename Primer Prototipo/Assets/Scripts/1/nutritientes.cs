using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class nutritientes : MonoBehaviour {

	public CanvasGroup c;
	public int tipo=0;
	public Sprite imagen1;
	public Sprite imagen2;
	private Image miImagen;

	// Use this for initialization
	public void Start () {

		//tipo = 0;
		c =(CanvasGroup)this.gameObject.GetComponent ("CanvasGroup");
		miImagen=(Image)this.gameObject.GetComponent ("Image");
		c.alpha = 0.0f;
		InvokeRepeating ("nutrir", 5.0f, 10.0f);
	}

	// Update is called once per frame
	void Update () {
	
		if (c.alpha >= 0) {

			c.alpha-=0.01f;
		}
	}

	void nutrir(){


		Debug.Log ("tipo:" + tipo);
		Debug.Log (miImagen.sprite);
		c.alpha = 2.0f;
		if (tipo == 0) {

			tipo = 1;
			miImagen.sprite = imagen2;
			Debug.Log("OXIGENO");
			ControladorRecursos.oxigeno+=10;
			return;
		} else {

			tipo = 0;
			miImagen.sprite = imagen1;
			Debug.Log("NUTRIENTES");
			ControladorRecursos.nutrientes+=5;
			return;
		}

	}

	public void empezar(){

		//Start ();


	}

}
