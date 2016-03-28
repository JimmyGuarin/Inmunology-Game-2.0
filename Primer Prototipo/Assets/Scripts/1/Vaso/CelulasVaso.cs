using UnityEngine;
using System.Collections;

public class CelulasVaso : MonoBehaviour {

    public GameObject vaso;
    public static float tiempo_Oxigenar;
	public bool desafioDendritica;

    // Use this for initialization
    void Start () {

        tiempo_Oxigenar = 10f;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

   
    void OnTriggerEnter (Collider MyTrigger) {


        if (MyTrigger.name.Equals("Dentrica(Clone)"))
        {
			if(MyTrigger.GetComponent<CrearUnidadInnata>().llevarBase==true){
	            

				MyTrigger.gameObject.GetComponent<CrearUnidadInnata>().enabled = false;
				MyTrigger.gameObject.GetComponent<FuncionesDendritica>().enabled=false;
				int hijos=MyTrigger.gameObject.transform.childCount;
				int amenazas=hijos;

				while(hijos>4){
					if(MyTrigger.gameObject.transform.FindChild("Bacteria(Clone)")!=null){
						Destroy(MyTrigger.gameObject.transform.FindChild("Bacteria(Clone)").gameObject);
					}
					else{
						Destroy(MyTrigger.gameObject.transform.FindChild("virusFinalFracture(Clone)").gameObject);
					}
					hijos--;
				}
				ManejadorVirus.numeroVirus-=(amenazas-4);
				Debug.Log("Virus Muerto.. Virus:"+ManejadorVirus.numeroVirus);
				MyTrigger.gameObject.GetComponent<ParticleSystem>().enableEmission = true;

				if(desafioDendritica){

					NotificationCenter.DefaultCenter().PostNotification(this,"CambiarGuiaDendritica",4);
					NotificationCenter.DefaultCenter().PostNotification(this,"CambiarGuiaDendritica",5);
				}
					

				ControladorRecursos.puntaje+=300;
	            if (tiempo_Oxigenar > 3)
	            {
	                vaso.GetComponent<VasoGrande>().activarVaso(tiempo_Oxigenar--);
	            }
			}
        }      
    }
}
