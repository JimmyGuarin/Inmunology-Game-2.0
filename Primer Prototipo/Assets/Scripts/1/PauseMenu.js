//*******************************************************************************
//*																							*
//*							Written by Grady Featherstone								*
//										© Copyright 2011										*
//*******************************************************************************
var mainMenuSceneName : String;
var pauseMenuFont : Font;
private var pauseEnabled = false;	
		var calidad:String;

function Start(){
	pauseEnabled = false;
	calidad =" Fantasticos";
	Time.timeScale = 1;
	AudioListener.volume = 1;
	
	Cursor.visible = true;
	
}



function Update(){

	//check if pause button (escape key) is pressed
	if(Input.GetKeyDown("escape")||Input.GetKeyDown("p")){
	
		
	
		//check if game is already paused		
		if(pauseEnabled == true){
			//unpause the game
			pauseEnabled = false;
			
			Time.timeScale = 1;
			AudioListener.volume = 1;
			Cursor.visible = true;			
		}
		
		//else if game isn't paused, then pause it
		else if(pauseEnabled == false){
			pauseEnabled = true;
			AudioListener.volume = 0;
			Time.timeScale = 0;
			Cursor.visible = true;
		}
	}
}

private var showGraphicsDropDown = false;

function OnGUI(){

GUI.skin.box.font = pauseMenuFont;
GUI.skin.button.font = pauseMenuFont;

	if(pauseEnabled == true){
		
		//Make a background box
		//GUI.Box(Rect(Screen.width /2 - 100,Screen.height /2 - 150,250,200), "");
			
		//Make Main Menu button
		if(GUI.Button(Rect(Screen.width /2 - 100,Screen.height /2 - 150,250,50), "Reanudar")){
			pauseEnabled = false;
			Time.timeScale = 1;
			AudioListener.volume = 1;
			showGraphicsDropDown = false;
		}	
		//Make Main Menu button
		if(GUI.Button(Rect(Screen.width /2 - 100,Screen.height /2 - 100,250,50), "Reiniciar")){
			Application.LoadLevel("1");
			pauseEnabled = false;
		}
		//Make Main Menu button
		
		if(GUI.Button(Rect(Screen.width /2 - 100,Screen.height /2 - 50,250,50), "Menu Principal")){
			
			//AudioListener.volume = 1;
			Application.LoadLevel("Inicio");
			
		}
		
		//Make Change Graphics Quality button
			if(GUI.Button(Rect(Screen.width /2 - 100,Screen.height /2 ,250,50),"Graficos :"+calidad)){
			
			if(showGraphicsDropDown == false){
				showGraphicsDropDown = true;
			}
			else{
				showGraphicsDropDown = false;
			}
		}
		
		//Create the Graphics settings buttons, these won't show automatically, they will be called when
		//the user clicks on the "Change Graphics Quality" Button, and then dissapear when they click
		//on it again....
		if(showGraphicsDropDown == true){
			
			Time.timeScale = 0;
			if(GUI.Button(Rect(Screen.width /2 + 150,Screen.height /2 ,250,50),"Muy Bajos")){
				QualitySettings.SetQualityLevel(QualityLevel.Fastest.value__);
				calidad="Muy Bajos";
				showGraphicsDropDown = false;
				 
			}
			if(GUI.Button(Rect(Screen.width /2 + 150,Screen.height /2 + 50,250,50), "Bajos")){
					QualitySettings.SetQualityLevel(QualityLevel.Simple.value__);
				calidad="Bajos";
				showGraphicsDropDown = false;
			}
			if(GUI.Button(Rect(Screen.width /2 + 150,Screen.height /2 + 100,250,50),"Buenos" )){
				
				QualitySettings.SetQualityLevel(QualityLevel.Good.value__);
				calidad="Buenos";
				showGraphicsDropDown = false;
			}
			if(GUI.Button(Rect(Screen.width /2 + 150,Screen.height /2 + 150,250,50), "Muy Buenos")){
					QualitySettings.SetQualityLevel(QualityLevel.Beautiful.value__);
				calidad="Muy Buenos";
				showGraphicsDropDown = false;
			}
			if(GUI.Button(Rect(Screen.width /2 + 150,Screen.height /2 + 200,250,50),"Fantasticos")){
					QualitySettings.SetQualityLevel(QualityLevel.Fantastic.value__);
				calidad="Fantasticos";
				showGraphicsDropDown = false;
			}
			
			if(Input.GetKeyDown("escape")){
				showGraphicsDropDown = false;
			}
		}
		
		//Make quit game button
		if (GUI.Button (Rect (Screen.width /2 - 100,Screen.height /2 + 50,250,50), "Salir")){
			Application.Quit();
		}
	}
}