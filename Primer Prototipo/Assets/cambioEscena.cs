using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class cambioEscena : MonoBehaviour {
	
	public Slider slider_carga;
	private AsyncOperation asyng;
	// Use this for initialization
	void Start () {
	

		Invoke ("startCorrutina", 4f);



	}


	void startCorrutina(){
	
		if (Application.loadedLevelName.Equals ("1")) 	
			StartCoroutine (LoadLevelslider("3"));
		if (Application.loadedLevelName.Equals ("3")) 	
			StartCoroutine (LoadLevelslider("2"));



	
	}

	IEnumerator LoadLevelslider(string name){
		
		asyng = Application.LoadLevelAsync (name);
		
		while (!asyng.isDone) {
			slider_carga.value=asyng.progress;
			yield return null;
			
		}
		
		
	}
}
