using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Puntajes : MonoBehaviour {

	public static bool levelComplete=false; 
	public string highscorePos; 
	public int score; 
	public int temp; 
	public  Text p1;
	public  Text p2;
	public  Text p3;
	public  Text p4;
	public  Text p5;
	public static CanvasGroup c;

	public void Start () { 

		c = GetComponent<CanvasGroup> ();

		if (p1 != null) {

			p1.text = "" + PlayerPrefs.GetInt ("highscorePos1");
			p2.text = "" + PlayerPrefs.GetInt ("highscorePos2");
			p3.text = "" + PlayerPrefs.GetInt ("highscorePos3");
			p4.text = "" + PlayerPrefs.GetInt ("highscorePos4");
			p5.text = "" + PlayerPrefs.GetInt ("highscorePos5");
		}

	
	}


	public static void mostrar(){

		c.alpha = 1;

	}
	public   void cerrar(){

		Debug.Log (c.name);
		c.alpha = 0;
		
	}

	 
			
} 
