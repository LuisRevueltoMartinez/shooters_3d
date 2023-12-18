using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparos : MonoBehaviour
{
    //Las referancias a los dos cañones que tiene el avion (Nos lo imaginamos en las alas)
    public GameObject canon_1;
    public GameObject canon_2;
    public GameObject bala; //Referencia a las balas que se van a disparar
    public float delay; //Tiempo entre disparo y disparo
    float time;
    int canon;
    AudioSource sound;

    // Start is called before the first frame update
    void Start()
    {
        time = delay; //Se añade el tiempo de delay
        canon = 1; //Se indica el primer cañón que disparará
        sound = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //Si está el delay el tiempo se va descontando
        if (time > 0)
        {
            time -= Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Space)) //Está pueto mientras que se mantiene el disparo para que pueda dejar pulsado y que vaya disparando
        {
            //Se comprueba si está en couldown
            if(time <= 0)
            {   
                //Este if elige el cañón por el que se dispara
                if (canon == 1)
                {
                    GameObject shoot = Instantiate(bala, canon_1.transform.position, canon_1.transform.rotation); //Se crea la bala
                    shoot.GetComponent<Bala>().avion = this.gameObject; //Se le asigna el avión del que ha salido
                    Destroy(shoot, 5); //Se destruye la bala
                    canon = 2; //Se cambia de cañón
                }
                else
                {
                    GameObject shoot = Instantiate(bala, canon_2.transform.position, canon_2.transform.rotation); //Se crea la bala
                    shoot.GetComponent<Bala>().avion = this.gameObject; //Se le asigna el avión del que ha salido
                    Destroy(shoot, 5); //Se destruye la bala
                    canon = 1; //Se cambia de cañón
                }
                sound.Play();
                time = delay; //Se añadae el delay
            }
        }
        
    }
}
