using UnityEngine;
using System.Collections;

public class BarraVida : MonoBehaviour {

	public Sprite sprite1; 
	public Sprite sprite2; 
	public Sprite sprite3;
	public Sprite sprite4; 
	public Sprite sprite5; 
	public Sprite sprite6; 
	public Sprite sprite7; 
	public Sprite sprite8; 
	public Sprite sprite9; 
	public Sprite sprite10; 
	public Sprite [] Asprite;
	SpriteRenderer spriteRenderer; 
	private int nSprite;

void Start () { 

		Asprite = new Sprite[10];
		Asprite[0]=sprite1; 
		Asprite[1]=sprite2; 
		Asprite[2]=sprite3;
		Asprite[3]=sprite4;
		Asprite[4]=sprite5;
		Asprite[5]=sprite6;
		Asprite[6]=sprite7;
		Asprite[7]=sprite8; 
		Asprite[8]=sprite9; 
		Asprite[9]=sprite10; 
		
		spriteRenderer = GetComponent<SpriteRenderer>(); 
		nSprite = 0;

		
		if (spriteRenderer.sprite == null) 
			spriteRenderer.sprite = Asprite[nSprite]; 
		// set the sprite to sprite1 

	} 

	void ChangeTheDamnSprite () { 
		
		nSprite++;
		if (nSprite <= 9)
			spriteRenderer.sprite = Asprite [nSprite]; // if the spriteRenderer sprite = sprite1 then change to sprite2 

	}

	// set the sprite to sprite1 
} 
