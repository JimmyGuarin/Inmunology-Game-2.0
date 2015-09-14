﻿using UnityEngine;
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
	public int nSprite;
	public float vidaVirus;

	void Start () { 

		Asprite = new Sprite[10];
		Asprite[0]=sprite10; 
		Asprite[1]=sprite9; 
		Asprite[2]=sprite8;
		Asprite[3]=sprite7;
		Asprite[4]=sprite6;
		Asprite[5]=sprite5;
		Asprite[6]=sprite4;
		Asprite[7]=sprite3; 
		Asprite[8]=sprite2; 
		Asprite[9]=sprite1; 


		
		spriteRenderer = GetComponent<SpriteRenderer>(); 
		nSprite = 9;

		
		if (spriteRenderer.sprite == null) 
			spriteRenderer.sprite = Asprite[nSprite]; 
		// set the sprite to sprite1 

	} 


	
		
	void Update(){


		vidaVirus = GetComponentInParent<InteligenciaVirus> ().vida;

		if (vidaVirus < 1000) {
			
			nSprite = (int)vidaVirus/ 100;
			spriteRenderer.sprite = Asprite[nSprite]; 
		}


	}


	// set the sprite to sprite1 
} 
