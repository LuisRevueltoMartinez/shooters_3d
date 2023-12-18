using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemigo : MonoBehaviour
{
    //Parámetro con la vida del enemigo
    public float HP = 100;
    //Parámetro con la distancia de visión del enemigo
    public float rango;
    //Parámetro con la distancia de ataque del enemigo
    public float atqrango;
    //Parámetro con el ángulo de visión del enemigo
    public float anguloVista;
    //Parámetro con el margen que hay desde el centro del objeto hasta el punto de destino
    public float margen;
    //Referencia del agente de navegación del GameObject
    public NavMeshAgent agent;
    //Referencia del jugador
    public GameObject player;
    //Se declaran todos los puntos de patrulla
    public GameObject p1;
    public GameObject p2;
    public GameObject p3;
    //Variable que guarda el objetivo al que se dirige
    int destinoActual;
    //Referencia al efecto que va a hacer la bala cuando choque
    public GameObject particle;
    //Punto donde se realiza el efecto
    public GameObject canon;
    //Referencia al componente AudioSource del objeto
    private AudioSource compaudio;
    //Probabilidad que tiene de acertar el enemigo
    public float precision;
    //Valor del delay del disparo
    public float delay;
    //Valor que guarda el tiempo entre disparos
    float timer;    
    //Valor del daño que hace el disparo
    public float damage;
    //Declaro un gameObject al firstpersoncharacter donde está la vida
    public GameObject firstpersoncharacter;
    //Variable booleana que indica si el enemigo es morado
    public bool morado;

    // Start is called before the first frame update
    void Start()
    {
        //Se carga el NavMeshAgent
        agent = this.GetComponent<NavMeshAgent>();
        //Comprueba si el enemigo es morado
        if (!morado)
        {
            //Se establece el primer destino del enemigo
            destinoActual = 1;
            agent.SetDestination(p1.transform.position);
        }
        else
        {
            rango = 10000;
        }
        //Se inicializa el AudioSource
        compaudio = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //Comprueba cuando el enemigo se ha quedado sin vida y lo elimina
        if (HP <= 0)
        {
            Destroy(this.gameObject);
        }
        else
        {
            //Si el disparo está el couldown para restar el tiempo que va pasando
            if (timer > 0)
            {
                timer = timer - Time.deltaTime;
            }
            //Comprueba si el enemigo es morado
            if (!morado)
            {
                Patrulla();
            }
            DetectarPlayer();

        }
    }

    //Función que detecta al player si está en su linea de visión
    void DetectarPlayer()
    {
        //Creamos un vector que nos servirá para ver la distancia a la que se encuentra el player
        Vector3 distPlayer = player.transform.position - this.transform.position;
        if(distPlayer.magnitude < rango || morado)
        {
            //Usamos un raycast para ver si el player está en linea de visión y no detrás de una cobertura
            RaycastHit resultadoRay;
            if(Physics.Raycast(this.transform.position, distPlayer, out resultadoRay, rango)) {
                //Comprobamos si el raycast golpea en el player
                if (resultadoRay.transform.tag == "Player" || morado)
                {
                    //Creamos un float que guardará el angulo en el que se encuentra el player
                    float angulo = Vector3.Angle(this.transform.forward, distPlayer);
                    if (angulo < anguloVista || morado)
                    {
                        //Se comprueba si el enemigo está en rango de disparar
                        if (distPlayer.magnitude > atqrango)
                        {
                            agent.SetDestination(player.transform.position);
                        }
                        else
                        {
                            if (!morado || resultadoRay.transform.tag == "Player")
                            {
                                agent.ResetPath();
                                this.transform.rotation = Quaternion.LookRotation(distPlayer, Vector3.up);
                                //Se comprueba si puede volver a disparar por el couldown
                                if (timer <= 0)
                                {
                                    Shoot();
                                    timer = delay;

                                }
                            }
                        }
                    }
                }
            }
        }
    }

    //Funcion para disparar al jugador
    void Shoot()
    {
        //Se crea un GameObject con el efecto
        GameObject chispa = Instantiate(particle, canon.transform.position, canon.transform.rotation);
        //Se reproduce el sonido del disparo
        compaudio.Play();
        //Se destruye el objeto creado para ahorrar espacio
        Destroy(chispa, 0.3f);

        //Se obtiene un unmero random del 0 al 99
        int randomNum = Random.Range(0, 100);
        //Se comprueba si el disparo golpea al jugador
        if(randomNum < precision)
        {
            Player script = firstpersoncharacter.GetComponent<Player>();
            script.HP -= damage;
        }

    }

    //Funcion para que el enemigo patrulle
    void Patrulla()
    {
        //Hay que comprobar cuanto le queda para llegar al punto de patrulla
        Vector3 dist = this.transform.position - agent.destination;
        if(dist.magnitude < margen)
        {
            //Cuando llega al destino hay que cambiar al siguiente punto
            if(destinoActual == 1)
            {
                destinoActual = 2;
                agent.SetDestination(p2.transform.position);
            }
            else
            {
                if(destinoActual == 2)
                {
                    destinoActual = 3;
                    agent.SetDestination(p3.transform.position);
                }
                else
                {
                    destinoActual = 1;
                    agent.SetDestination(p1.transform.position);
                }
            }
        }
    }
   
}
