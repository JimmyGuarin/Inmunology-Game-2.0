using UnityEngine;
using System.Collections;

public class EstadoBarraGanglio : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public float pegarTamañoBarra(float minValor,float maxValor){
		
		return minValor / maxValor;
	}
	
	public int pegarPorcentajeBarra(float minValor,float maxValor,int factor){
		
		return Mathf.RoundToInt(pegarTamañoBarra (minValor, maxValor) * factor);
	}
}
