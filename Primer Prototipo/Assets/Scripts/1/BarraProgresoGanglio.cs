using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BarraProgresoGanglio : MonoBehaviour {


	public ControladorInmuAdquirida activarAdquirida;
	public GameObject barraProgreso;
	public EstadoBarraGanglio estadoBarra;
	public Text textoProgreso;
	public float maxProgreso;
	public float progresoActual;
	private float valor;
	public GameObject paso1;
	public  GameObject paso2;
	public  GameObject paso3;
	public  GameObject paso4;
	public  GameObject paso5;

	// Use this for initialization
	void Start () {
	
		estadoBarra = this.gameObject.GetComponent<EstadoBarraGanglio> ();

	}
	
	// Update is called once per frame
	void Update () {

		barraProgreso.transform.localScale=new Vector3(estadoBarra.pegarTamañoBarra(progresoActual,maxProgreso),barraProgreso.transform.localScale.y,barraProgreso.transform.localScale.z);
		textoProgreso.text=estadoBarra.pegarPorcentajeBarra(progresoActual,maxProgreso,100)+"%";


		if (progresoActual < maxProgreso) {
			progresoActual += Time.deltaTime * 5;
		} else {

			textoProgreso.text="100% Completo";
		}
		if (progresoActual >=20) {
			
			paso1.SetActive(true);
		}
		if (progresoActual >=40) {
			
			paso2.SetActive(true);
		}
		if (progresoActual >=60) {
			
			paso3.SetActive(true);
		}
		if (progresoActual >=80) {
			
			paso4.SetActive(true);
		}
		if (progresoActual== 100) {
		
			paso5.SetActive(true);
			activarAdquirida.cmhi();
			VasoGrande.activado=true;
				
		} 

	}
}
