using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    public AudioSource asDesarrollo;

    public GameObject goCubos;
    public GameObject goComunidades;
    public GameObject goOficinas;
    public GameObject goCristales;
    public GameObject goGarajes;
    public GameObject goAbrillantamiento;  
 
    public Animator antrCharlyCubos;
    public Animator antrGothyComunidades;
    public Animator antrCharlyOficinas;
    public Animator antrGothyCristales;
    public Animator antrCharlyGarages;
    public Animator antrGothyAbrillantamiento;

    public GameObject[] gosObjDestruidos;


    public void PlayAudioDesarrollo()
    {
        asDesarrollo.Play();
    }

    public void ApareceCubos()
    {
        goCubos.SetActive(true);
    }

    public void ApareceComunidades()
    {
        goComunidades.SetActive(true);
    }

    public void ApareceOficinas()
    {
        goOficinas.SetActive(true);
    }

    public void ApareceCristales()
    {
        goCristales.SetActive(true);
    }

    public void ApareceGarajes()
    {
        goGarajes.SetActive(true);
    }

    public void ApareceAbrillantamiento()
    {
        goAbrillantamiento.SetActive(true);

        gosObjDestruidos = GameObject.FindGameObjectsWithTag("TagObjDestruidos");
    }

    public void Beña()
    {

        antrCharlyCubos.speed = 1;
        antrGothyComunidades.speed = 1;
      
        antrGothyCristales.speed = 1;
        antrCharlyGarages.speed = 1;
        antrGothyAbrillantamiento.speed = 1;


        antrCharlyCubos.SetLayerWeight(1, 1f);
        antrGothyComunidades.SetLayerWeight(1, 1f);
        antrCharlyOficinas.SetLayerWeight(1, 1f);

        antrCharlyOficinas.speed = 1;

        antrGothyCristales.SetLayerWeight(1, 1f);
        antrCharlyGarages.SetLayerWeight(1, 1f);
        antrGothyAbrillantamiento.SetLayerWeight(1, 1f);

    }

    public void Parada()
    {
        goCubos.transform.rotation = Quaternion.Euler(0, 0, 0);
        goComunidades.transform.rotation = Quaternion.Euler(0, 0, 0);
        goOficinas.transform.rotation = Quaternion.Euler(0, 0, 0);
        goCristales.transform.rotation = Quaternion.Euler(0, 0, 0);
        goGarajes.transform.rotation = Quaternion.Euler(0, 0, 0);
        goAbrillantamiento.transform.rotation = Quaternion.Euler(0, 0, 0);

        antrCharlyCubos.speed=0;
        antrGothyComunidades.speed = 0;
        antrCharlyOficinas.speed = 0;
        antrGothyCristales.speed = 0;
        antrCharlyGarages.speed = 0;
        antrGothyAbrillantamiento.speed = 0;

        goCubos.GetComponent<Movimiento>().enabled = false;
        goComunidades.GetComponent<Movimiento>().enabled = false;
        goOficinas.GetComponent<Movimiento>().enabled = false;
        goCristales.GetComponent<Movimiento>().enabled = false;
        goGarajes.GetComponent<Movimiento>().enabled = false;
        goAbrillantamiento.GetComponent<Movimiento>().enabled = false;


    }

    public void ObjetosDestruidos()
    {
        foreach (GameObject goObjDestruido in gosObjDestruidos)
        {
            Destroy(goObjDestruido);
        }
     
    }
}
