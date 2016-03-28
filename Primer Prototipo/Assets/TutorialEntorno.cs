using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TutorialEntorno : MonoBehaviour {

	//ZOOM
	public Zoom zoom;
	Ray pulsacion;
	RaycastHit hit ;
	

	//Estados
	public EstadoTutorial[] Estados;

	// Paneles Primarios de navegacion
	public GameObject panelPrincipal_1;
	public GameObject panelPrincipal_11;
	public GameObject panelPrincipal_12;
	public GameObject panelPrincipal_13;
	public GameObject panelPrincipal_14;

	//Panel Volver
	public GameObject panel_volver;


	//Panel info
	public GameObject panel_info;

	//Sonido al Hacer zoom
	public AudioSource zoom_sound;

	//Panel Matriz
	public GameObject panel_matriz; 

	//Primer Texto
	public Text textoJefe;

	//Panel para salir del tutorial
	public GameObject panelSalida;

	//Panel de Zoom
	public GameObject panelZoom;
	public Text	tituloZoom;
	public Text textoZoom;

	//Flechas apuntando a objetos de Objetos
	public GameObject flechaVaso;
	public GameObject flechaGanglio;
	public GameObject flechaCelula;
	public GameObject flechaZAfectada;

	//ObjetosInteractivos
	public GameObject vaso;
	public GameObject ganglio;
	public GameObject celula;
	public GameObject ZAfectada;
	public ManejadorVirus activarVirus;
	public GameObject primerVirus;
	//Manejador de Estados
	public int estadoActual;

	//ContadorVirus
	public int contadorVirus;

	//Tiempo
	public Text tiempo_text;
	private int tiempo;

	//Estrellas
	public RawImage Entorno;




	// Use this for initialization
	void Start () {
	
		tiempo = 0;

		textoJefe.text = "Bienvenido " + PlayerPrefs.GetString("name") + textoJefe.text;
		estadoActual = 0;
		contadorVirus=10;
		Estados= new EstadoTutorial[4];
		Estados [0] = new EstadoTutorial (flechaVaso, vaso,"VASO SANGUINEO" ,"Un vaso sanguíneo es una estructura hueca y tubular que " +
			"conduce la sangre impulsada por la acción del corazón, que recogen la sangre de todos los rincones " +
			"del cuerpo. ");
		Estados [1] = new EstadoTutorial (flechaGanglio,ganglio," GANGLIO LINFÁTICO" ,"Los nódulos linfáticos o ganglios linfáticos son unas estructuras nodulares que forman parte del sistema linfático y " +
			"forman agrupaciones en forma de racimos. Los nódulos linfáticos actúan como filtros de la linfa, al poseer una estructura interna de tejido conectivo fino, en forma de red, rellena de linfocitos " +
			"que recogen y destruyen bacterias y virus, por lo que estos nódulos también forman parte del sistema inmunitario, ayudando al cuerpo a reconocer y combatir gérmenes, infecciones y otras sustancias extrañas.");

		Estados [2] = new EstadoTutorial (flechaCelula, celula, "CÉLULAS", "Estas son las células que se encuentran en el epitelio respiratorio, producen nutrientes y ademas pueden ser destruidas por los virus atacantes  o por el contacto de los neutrófilos," +
			"adicionalmente recuerda que son el objetivo fundamental del virus para reproducirse, evita que se infecten o se destruyan las menos posibles");

		Estados [3] = new EstadoTutorial (flechaZAfectada, ZAfectada, "ZONA AFECTADA","¡SORPRESA! haz encontrado el lugar donde se rompió la matriz extracelular, de ahí llegarán los diferentes patógenos, no permitas que te invadan, destrúyelos con un CLICK.");
		zoom = this.GetComponent<Zoom> ();	


		NotificationCenter.DefaultCenter ().AddObserver (this, "celulaMuerta");
		NotificationCenter.DefaultCenter ().AddObserver (this, "TerminarTutorial");

	}
	
	// Update is called once per frame
	void Update () {
	
		//Si el click es el clik derecho(0)
		if (Input.GetMouseButtonDown (0)) {

			pulsacion = Camera.main.ScreenPointToRay (Input.mousePosition);

			if (Physics.Raycast (pulsacion, out hit)) {


				/*
				 * Condicion que pregunta si el collider
				 * con el que colisiona el rayo es el 
				 * collider de algun elemento interactivo
				 * */
				
				if (hit.collider.name.Equals ("VasoFinal")) {	

					Estados[estadoActual].Activar(estadoActual);
					tituloZoom.text=Estados[estadoActual].titulo;
					textoZoom.text=Estados[estadoActual].texto;
					zoom.enfocar(vaso.transform,-4,3,70);
					panel_matriz.SetActive(false);
					flechaVaso.SetActive(false);
					zoom_sound.Play();
					estadoActual++;


				}
				if (hit.collider.name.Equals ("Ganglio")) {	
					
					Estados[estadoActual].Activar(estadoActual);
					tituloZoom.text=Estados[estadoActual].titulo;
					textoZoom.text=Estados[estadoActual].texto;
					zoom.enfocar(ganglio.transform,1,-5,70);
					panel_matriz.SetActive(false);
					flechaGanglio.SetActive(false);
					zoom_sound.Play();
					estadoActual++;
				}

				if (hit.collider.name.Equals ("celula2")) {	
					
					Estados[estadoActual].Activar(estadoActual);
					tituloZoom.text=Estados[estadoActual].titulo;
					textoZoom.text=Estados[estadoActual].texto;
					panelZoom.GetComponent<RectTransform>().anchoredPosition=new Vector2(100,4);
					zoom.enfocar(celula.transform,5,0,50);
					panel_matriz.SetActive(false);
					flechaCelula.SetActive(false);
					zoom_sound.Play();
					estadoActual++;
				}

				if (hit.collider.name.Equals ("Zona Afectada")) {	
					
					Estados[estadoActual].Activar(estadoActual);
					tituloZoom.text=Estados[estadoActual].titulo;
					textoZoom.text=Estados[estadoActual].texto;
					panelZoom.GetComponent<RectTransform>().anchoredPosition=new Vector2(100,4);
					zoom.enfocar(ZAfectada.transform,2,6,60);
					panel_matriz.SetActive(false);
					zoom_sound.Play();
					flechaZAfectada.SetActive(false);
					estadoActual++;
				}

				if (hit.collider.name.Equals ("VirusFinal(Clone)")) {	
					
					hit.collider.gameObject.GetComponent<InteligenciaVirus>().vida-=100;
					hit.collider.gameObject.GetComponentInChildren<BarraVida>().modificarSprite();
				

						
				
				}
				
			}
		}
	}
	

  public void cambiarPanelPrincipal(int num){

		switch (num) 
		{
		
			case 1:
				panelPrincipal_11.SetActive(false);
				panelPrincipal_13.SetActive(false);
				panelPrincipal_12.SetActive(true);
				break;
			case 2:
				panelPrincipal_11.SetActive(true);
				panelPrincipal_12.SetActive(false);
				panelPrincipal_13.SetActive(false);
				break;
			case 3:
				panelPrincipal_11.SetActive(false);
				panelPrincipal_13.SetActive(true);
				panelPrincipal_12.SetActive(false);
				break;

			case 4:
				panelPrincipal_1.SetActive(false);
				panelPrincipal_11.SetActive(true);
				panelPrincipal_12.SetActive(false);
				panel_matriz.SetActive(true);
				Invoke("empezarVisualizar",5f);
				break;
			//llamado por panel zoom
			case 5:
				panelZoom.SetActive(false);
				if(estadoActual<4){
					zoom.desenfocar(false);
					panel_matriz.SetActive(true);
					Estados[estadoActual].Preparar();
				}
				else{
					
					zoom.desenfocar(false);
					activarVirus.GetComponent<ManejadorVirus>().enabled=true;
					primerVirus.SetActive(true);
					panel_matriz.SetActive(false);
					InvokeRepeating("ManejarTiempo",1f,1f);
					celula.GetComponent<Collider>().enabled=true;
					tiempo_text.transform.parent.gameObject.SetActive(true);
					panel_info.SetActive(true);
				}
				break;
				
			case 6:
				ControladorMenu.in_tutorial=true;
				Application.LoadLevel(0);
				break;
				
			case 7:
				Destroy(GameObject.Find("Canvas"));
				Destroy(GameObject.Find("Creador"));
				Application.LoadLevel(4);	
				break;


		}
	}

	public void empezarVisualizar(){

		flechaVaso.SetActive (true);
		vaso.GetComponent<Collider> ().enabled = true;
	}

	void ManejarTiempo(){
		
		tiempo++;
		tiempo_text.gameObject.GetComponent<Text>().text=""+tiempo+"s.";
	
	}

	void TerminarTutorial(Notification notification)
	{	
		if (tiempo <=40) {
			
			PlayerPrefs.SetString("Entorno","3");
		}
		if (tiempo >40&&tiempo<=50) {
			
			PlayerPrefs.SetString("Entorno","2");
		}
		if (tiempo >50) {
			
			PlayerPrefs.SetString("Entorno","1");
		}
		
		Entorno.texture = Resources.Load (PlayerPrefs.GetString("Entorno")) as Texture;
		QuitarSonidos ();
		panel_info.SetActive(false);
		panel_volver.SetActive(true);

	}


	void celulaMuerta(Notification notification)
	{	
		
		StartCoroutine(Wait());
		
	}
	
	
	IEnumerator Wait(){
		
		yield return new WaitForSeconds(2);
		panel_info.SetActive(false);
		panelPrincipal_13.SetActive (false);
		panelPrincipal_14.SetActive (true);
		panelPrincipal_1.SetActive (true);
		QuitarSonidos ();
		Time.timeScale = 0;
	}

	public void QuitarSonidos(){

		GameObject [] celulas=GameObject.FindGameObjectsWithTag ("celula");
		Camera.main.transform.FindChild("Audio Source").gameObject.SetActive(false);
		foreach(GameObject celu in celulas){
			
			celu.GetComponent<AudioSource>().enabled=false;
			
		}
	}
}
