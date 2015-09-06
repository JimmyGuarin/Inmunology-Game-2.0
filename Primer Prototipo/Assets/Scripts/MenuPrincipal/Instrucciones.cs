using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Instrucciones : MonoBehaviour {

	public Sprite D1; 
	public Sprite D2; 
	public Sprite D3; 
	public Sprite H1;
	public Sprite H2; 
	public Sprite H3; 
	public Sprite H4; 
	public Sprite C1; 
	public Sprite C2; 
	public  CanvasGroup cc;
	public  static CanvasGroup c;
	public Sprite [] Asprite;
	public static SpriteRenderer spriteRenderer; 
	private int nSprite;
	public Button jugar;
	public Button instru;
	public Button salir;
	public Button puntuaciones;
	public static Button[] botones; 



	void Start () { 

		botones = new Button[4];
		botones [0] = jugar;
		botones [1] = instru;
		botones [2] = salir;
		botones [3] = puntuaciones;

		c = cc;
		Asprite = new Sprite[9];
		Asprite[0]=D1; 
		Asprite[1]=D2; 
		Asprite[2]=D3; 
		Asprite[3]=H1;
		Asprite[4]=H2;
		Asprite[5]=H3;
		Asprite[6]=H4;
		Asprite[7]=C1;
		Asprite[8]=C2; 
		spriteRenderer = GetComponent<SpriteRenderer>(); 
		nSprite = 0;

		if (spriteRenderer.sprite == null) 
			spriteRenderer.sprite = Asprite[nSprite]; 
		// set the sprite to sprite1 
		
	}



	public void D(){

		nSprite = 0;
		spriteRenderer.sprite = Asprite [0];

	}
	public void H(){

		nSprite = 3;
		spriteRenderer.sprite = Asprite [3];
		
	}
	public void C(){

		nSprite = 7;
		spriteRenderer.sprite = Asprite [7];

	}

	public void cerrar(){


		nSprite = 0;
		spriteRenderer.sprite = Asprite [0];
		c.alpha = 0;
		spriteRenderer.sortingOrder = -1;
		for(int i=0;i<botones.Length;i++){
			
			botones[i].interactable=true;
		}

	}
	public void Adelante(){

		if (nSprite != 2 && nSprite != 6&& nSprite != 8) {
			nSprite++;
			spriteRenderer.sprite = Asprite [nSprite];
		}

	}
	public void Atras(){

		if (nSprite != 0 && nSprite != 3 && nSprite != 7) {
			nSprite--;
			spriteRenderer.sprite = Asprite [nSprite];
		}
	}

	public static void mostrar(){


		
		c.alpha = 1;
		spriteRenderer.sortingOrder = 0;

		for(int i=0;i<botones.Length;i++){

			botones[i].interactable=false;
		}
		

	}
}
