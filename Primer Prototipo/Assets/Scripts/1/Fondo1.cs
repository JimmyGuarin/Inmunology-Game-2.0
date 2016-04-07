using UnityEngine;
using System.Collections;

/// <summary>
/// Fondo1.Este script maneja todos los eventos relacionados con el Fondo del juego.
/// </summary>
public class Fondo1 : MonoBehaviour {

	/// <summary>
	/// The punto destino.
	/// Variable que almacena el punto donde se hace click en el fondo
	/// el cual puede ser utilizado como punto de destino para otros objetos
	/// que movemos en pantalla
	/// </summary>
	public static Vector2 puntoDestino;
	public static bool seleccionada;
	Ray pulsacion;
	RaycastHit hit ;


	public CursorMode cursorMode = CursorMode.Auto;
	public Texture2D cursorImage;
	
	private int cursorWidth = 24;
	private int cursorHeight = 24;





	// Use this for initialization
	void Start () {
	
		Cursor.visible = false;
		//Cursor.SetCursor(yourCursor, hotSpot, cursorMode);
		seleccionada = false;
		puntoDestino = new Vector3 (0, 0, -1);
	}
	
	void OnGUI()
	{
		if(seleccionada)
		GUI.DrawTexture(new Rect(Input.mousePosition.x-cursorWidth/2, Screen.height - Input.mousePosition.y-cursorHeight/2, cursorWidth, cursorHeight), cursorImage);
	}
	
	
	// Update is called once per frame
	void Update () {

		if (seleccionada == false && Cursor.visible == false)
			Cursor.visible = true;

		//Si el click es el clik derecho(0)
		if (Input.GetMouseButtonDown (0)) {

			
			Ray pulsacion;
			RaycastHit hit ;
			pulsacion=Camera.main.ScreenPointToRay(Input.mousePosition);
			


			
			if (Physics.Raycast(pulsacion,out hit)) {
				
				PosicionSeleccionada.posicionar=0;
				Vector3 posVec = Input.mousePosition; 
				posVec.z = -5 - Camera.main.transform.position.z; 
				posVec = Camera.main.ScreenToWorldPoint(posVec);
				puntoDestino=posVec;

				/*
				 * Condicion que pregunta si el collider
				 * con el que colisiona el rayo es el 
				 * collider que tiene el fondo
				 * hit.collider.name.Equals ("Fondo1")||hit.collider.name.Equals ("VirusFinal(Clone)")
				    ||hit.collider.name.Equals ("celula1")||hit.collider.name.Equals ("puntoEncuentro")
				 * */

				//if () {	
					
				if(seleccionada==true){

					NotificationCenter.DefaultCenter().PostNotification(this,"cambiarPosCelula");
					Cursor.visible=false;
				}
				else{
					GameObject objeto_seleccionado=hit.collider.gameObject;;


					if(hit.collider.name.Equals("Dentrica(Clone)")){


						if(objeto_seleccionado.GetComponent<CrearUnidadInnata>().llevarBase==false&&
						   objeto_seleccionado.GetComponent<CrearUnidadInnata>().isSeleted==false){
							objeto_seleccionado.GetComponent<CrearUnidadInnata>().isSeleted=true;
							objeto_seleccionado.transform.FindChild("seleccionada").gameObject.SetActive(true);
							seleccionada=true;
							NotificationCenter.DefaultCenter().PostNotification(this,"CambiarGuiaDendritica",1);

						}
					}
					if(hit.collider.name.Equals("Neutrofilo(Clone)")){
						
						
						if(objeto_seleccionado.GetComponent<ManejarNeutrofilo>().isSeleted==false&&
						   objeto_seleccionado.GetComponent<ManejarNeutrofilo>().mivirus==null&&
						   objeto_seleccionado.transform.FindChild("mira").gameObject.activeSelf==false){
							objeto_seleccionado.transform.FindChild("seleccionada").gameObject.SetActive(true);
							objeto_seleccionado.GetComponent<ManejarNeutrofilo>().isSeleted=true;
							seleccionada=true;
							NotificationCenter.DefaultCenter().PostNotification(this,"CambiarGuiaNeutrofilo",1);
							
						}
					}

					if(hit.collider.name.Equals("Macrofago(Clone)")){
						
						
						if(objeto_seleccionado.GetComponent<Macrofago>().isSeleted==false&&
						   objeto_seleccionado.GetComponent<Macrofago>().mivirus==null&&
						   objeto_seleccionado.transform.FindChild("mira").gameObject.activeSelf==false){
							objeto_seleccionado.GetComponent<Macrofago>().isSeleted=true;
							objeto_seleccionado.transform.FindChild("seleccionada").gameObject.SetActive(true);
							seleccionada=true;
							NotificationCenter.DefaultCenter().PostNotification(this,"CambiarGuiaMacrofago",1);
							
						}
					}

					if(hit.collider.name.Equals("NaturalK(Clone)")){
						
						
						if(objeto_seleccionado.GetComponent<TCD8>().isSeleted==false){
							
							objeto_seleccionado.GetComponent<TCD8>().isSeleted=true;
							objeto_seleccionado.transform.FindChild("seleccionada").gameObject.SetActive(true);
							seleccionada=true;
							
						}
					}

					if(hit.collider.name.Equals("LinfoncitoTCD8(Clone)")){
						
						
						if(objeto_seleccionado.GetComponent<TCD8>().isSeleted==false){
							
							objeto_seleccionado.GetComponent<TCD8>().isSeleted=true;
							objeto_seleccionado.transform.FindChild("seleccionada").gameObject.SetActive(true);
							seleccionada=true;
							
						}
					}

					if(hit.collider.name.Equals("LinfoncitoB(Clone)")){
						
						
						if(objeto_seleccionado.GetComponent<LinfocitoB>().isSeleted==false&&
						   objeto_seleccionado.transform.FindChild("mira").gameObject.activeSelf==false){
							objeto_seleccionado.transform.FindChild("seleccionada").gameObject.SetActive(true);
							objeto_seleccionado.GetComponent<LinfocitoB>().isSeleted=true;
							seleccionada=true;
							
						}
					}

					if(hit.collider.name.Equals("linfocitoB(Clone)")){
						
						
						if(objeto_seleccionado.GetComponent<LinfocitoB2>().isSeleted==false){
							objeto_seleccionado.transform.FindChild("seleccionada").gameObject.SetActive(true);
							objeto_seleccionado.GetComponent<LinfocitoB2>().isSeleted=true;
							seleccionada=true;
							
						}
					}




					if(hit.collider.name.Equals("LinfoncitoTCD4(Clone)")){
						
						
						if(objeto_seleccionado.GetComponent<TCD4>().isSeleted==false){
							objeto_seleccionado.transform.FindChild("seleccionada").gameObject.SetActive(true);
							objeto_seleccionado.GetComponent<TCD4>().isSeleted=true;
							objeto_seleccionado.GetComponent<TCD4>().ayudado=null;
							seleccionada=true;
							
						}
					}
					if (hit.collider.name.Equals ("puntoEncuentro")) {

						if(objeto_seleccionado.GetComponent<MoverPuntoEncuentro>().isSeleted==false){
							objeto_seleccionado.GetComponent<MoverPuntoEncuentro>().isSeleted=true;
							objeto_seleccionado.transform.FindChild("seleccionada").gameObject.SetActive(true);
							seleccionada=true;
						}

					}



				}

					
					if(Nacimiento.seleccionado==true){

						Nacimiento.seleccionado=false;
						NotificationCenter.DefaultCenter ().PostNotification (this, "crearCelula",posVec);
						
						
					}

			}
			
		}
		
	}
}