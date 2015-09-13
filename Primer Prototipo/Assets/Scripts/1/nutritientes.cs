using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class nutritientes : MonoBehaviour {
	
	public float factor;
	public float darktotal;


	// Use this for initialization
	public void Start () {
		
		
		factor = 0.05f;
		float aux = Random.Range (10,25);
		float aux2 = Random.Range (20,25);
		InvokeRepeating ("nutrir",aux ,aux2);
	}

	// Update is called once per frame
	void Update () {
	

		if (darktotal > 0) {

			GetComponent<SpriteRenderer> ().color=new Color (1f, 1f, 1f, darktotal);
			darktotal -= factor;
	
		}


	}

	void nutrir(){


		darktotal = 2f;
		ControladorRecursos.nutrientes += 5;
	}
	
}
