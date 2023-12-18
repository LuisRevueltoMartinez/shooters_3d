using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparo : MonoBehaviour
{
    //Objeto vacío que contiene la punta de la pistola
    public GameObject canon;
    //Referencia al efecto que va a hacer la bala cuando choque
    public GameObject particle;
    //Referencia al componente AudioSource del objeto
    private AudioSource compaudio;
    //Valor del delay del disparo
    public float delay;
    //Valor que guarda el tiempo entre disparos
    float timer;
    //Valor del daño que hace el disparo
    public float damage;
    
    
    // Start is called before the first frame update
    void Start()
    {
        //Se inicializa el AudioSource
        compaudio = this.GetComponent<AudioSource>();
        timer = delay;
    }

    // Update is called once per frame
    void Update()
    {
        //Comprobar si ha pasado el delay entre disparos
        if (timer <= 0) {
            //Comprobar cuando se activa el clic izquierdo
            if (Input.GetKeyDown(KeyCode.Mouse0)) {
                Shoot();
                timer = delay;
            }
        }
        else
        {
            //Reducir el timer para que avance el tiempo
            timer = timer - Time.deltaTime;
        }
    }

    //Función que realiza el disparo
    void Shoot()
    {
        //Definir el raycast
        RaycastHit resultado;
        //Comprobar si golpea y guardar el raycast
        if (Physics.Raycast(this.transform.position, this.transform.forward, out resultado, 100))
        {
            //Se crea un GameObject con el efecto
            GameObject chispa = Instantiate(particle, resultado.point, this.transform.rotation);
            GameObject chispa_canon = Instantiate(particle, canon.transform.position, canon.transform.rotation);
            //Se reproduce el sonido del disparo
            compaudio.Play();
            //Se destruye el objeto creado para ahorrar espacio
            Destroy(chispa, 0.3f);
            Destroy(chispa_canon, 0.3f);
            if (resultado.collider.gameObject.CompareTag("Enemigo"))
            {
                Enemigo script = resultado.collider.gameObject.GetComponent<Enemigo>();
                script.HP -= damage;
                Animator animation = script.GetComponent<Animator>();
                animation.SetTrigger("Golpea");
            }
        }
    }
}
