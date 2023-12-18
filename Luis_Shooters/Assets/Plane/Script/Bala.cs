using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    public float vel; //Velocidad de la bala
    public GameObject avion; //Referencia del avión
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(0, 0, vel * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Esfera")
        {
            avion.GetComponent<Puntuacion>().objetivos++;
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
