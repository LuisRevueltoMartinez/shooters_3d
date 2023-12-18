using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Puntuacion : MonoBehaviour
{
    public GameObject contador; //Mensaje con el contador de puntuacion
    int cantidad; //Cantidad de objetivos hay que total
    public int objetivos; //Cantidad de objetivos que lleva
    GameObject[] esferas; //Lista de GameObject para contar cuantas esferas hay en la escena
    public GameObject victoria; //Mensaje con la victoria
    public GameObject derrota; //Mensaje con la derrota
    public GameObject explosion; //Particula con la explosion
    AudioSource sound; //Sonido de la explosion
    public GameObject cam; //La camara para crear una nueva cuando esta se destruya

    // Start is called before the first frame update
    void Start()
    {
        esferas = GameObject.FindGameObjectsWithTag("Esfera"); //Se buscan cuantas esferas hay en la escena
        cantidad = esferas.Length; //Se guarda en la cantidad total
        objetivos = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (cantidad > objetivos)
        {
            contador.GetComponent<TextMeshProUGUI>().SetText(objetivos + "/" + cantidad); //Se muestra por pantalla el mensaje
        }
        else
        {
            Stop();
            victoria.gameObject.SetActive(true); //Se muestra el mensaje de vitoria
        }
    }

    //Comprueba si toca algún trigger que sea una esfera
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Esfera")
        {
            Destroy(other.gameObject); //Se destruye la esfera
            objetivos++; //Se suma al contador
        }
    }

    //Detiene el avión
    void Stop()
    {
        contador.gameObject.SetActive(false); //Se quita el contador
        this.GetComponent<Movimiento>().enabled = false;//Se para el movimiento
        this.GetComponent<Disparos>().enabled = false; //Se paran los disparos
    }

    //Comprueba si entra en contacto con el suelo
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Finish")
        {
            Stop();
            GameObject effect = Instantiate(explosion, this.transform.position, this.transform.rotation); //Crea el efecto de explosion
            GameObject newcam = Instantiate(cam, cam.transform.position, cam.transform.rotation); //Crea una nueva camara en la posion de la antigua
            sound = newcam.GetComponent<AudioSource>(); //Coge el sonido de explosion DE LA CAMARA NUEVA (Porque la otra se va a borrar)
            sound.Play(); //Se reproduce el sonido en la nueva camara
            derrota.gameObject.SetActive(true); //Se muestra el mensaje de derrota
            Destroy(this.gameObject); //Se borra el avion
        }
    }


}
