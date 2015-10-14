using UnityEngine;
using System.Collections;

public class CelulasVaso : MonoBehaviour {

    public GameObject vaso;
    public static float tiempo_Oxigenar;

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
            MyTrigger.gameObject.GetComponent<CrearUnidadInnata>().enabled = false;
            MyTrigger.gameObject.GetComponent<ParticleSystem>().enableEmission = true;
            if (tiempo_Oxigenar > 1)
            {
                vaso.GetComponent<VasoGrande>().activarVaso(tiempo_Oxigenar);
            }
            
        }

            
        }
}
