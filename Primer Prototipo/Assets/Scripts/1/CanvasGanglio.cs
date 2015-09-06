using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class CanvasGanglio : MonoBehaviour {

	public Slider slider;
	private float valor;
	public GameObject paso1;
	public  GameObject paso2;
	public  GameObject paso3;
	public  GameObject paso4;


	// Use this for initialization
	void Start () {
	


	}
	
	// Update is called once per frame
	void Update () {
	


		if (valor >=0.25&&valor<=0.3) {

			paso1.SetActive(true);
		}
		if (valor >=0.5&&valor<=0.65) {
			
			paso2.SetActive(true);
		}
		if (valor >=0.8&&valor<=0.85) {
			
			paso3.SetActive(true);
		}
		if (valor <= 1) {
			
			valor += 0.001f;
			slider.value = valor;

		} else {

			paso4.SetActive(true);
			Destroy(this);

		}


	}


}
