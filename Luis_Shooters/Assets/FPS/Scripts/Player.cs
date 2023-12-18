using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.FirstPerson;

public class Player : MonoBehaviour
{
    //Vida del jugador
    public float HP = 100;
    float vida;
    //Llamamos a las referencias del resto de objetos
    //Para parar al jugador
    public GameObject movimiento;
    //Para quitar la mira
    public GameObject mira;
    //Para quitar la vida de pantalla
    public GameObject mensajevida;
    //Para poner el mensaje de muerte
    public GameObject texto_perder;
    //Para poner el mensaje de victoria
    public GameObject texto_ganar;
    //Para parar los enemigos
    public GameObject enemigo1;
    public GameObject enemigo2;
    public GameObject enemigo3;
    //Comprobar si el juego se ha parado ya
    bool finish = false;

    // Start is called before the first frame update
    void Start()
    {
        vida = HP;
    }

    // Update is called once per frame
    void Update()
    {
        if (!finish)
        {
            mensajevida.GetComponent<TextMeshProUGUI>().text = HP + "/" + vida;
            if (HP <= 0)
            {
                Stop();
                texto_perder.GetComponent<TextMeshProUGUI>().gameObject.SetActive(true);
                if (enemigo1 != null)
                {
                    enemigo1.GetComponent<NavMeshAgent>().ResetPath();
                    enemigo1.GetComponent<Enemigo>().enabled = false;
                }
                if (enemigo2 != null)
                {
                    enemigo2.GetComponent<NavMeshAgent>().ResetPath();
                    enemigo2.GetComponent<Enemigo>().enabled = false;
                }
                if (enemigo3 != null)
                {
                    enemigo3.GetComponent<NavMeshAgent>().ResetPath();
                    enemigo3.GetComponent<Enemigo>().enabled = false;
                }
                finish = true;
            }
            else
            {
                if(enemigo1 == null && enemigo2 == null && enemigo3 == null)
                {
                    Stop();
                    texto_ganar.GetComponent<TextMeshProUGUI>().gameObject.SetActive(true);
                    finish = true;
                }
            }
        }
    }

    //Finaliza las cosas comunes tanto si ganas como si pierdes
    void Stop()
    {
        this.GetComponent<Disparo>().enabled = false; //Para el disparo
        movimiento.GetComponent<FirstPersonController>().enabled = false;   //Para el movimiento
        mira.gameObject.SetActive(false); //Quita la mira
        mensajevida.gameObject.SetActive(false); //Quita la vida
    }
}
