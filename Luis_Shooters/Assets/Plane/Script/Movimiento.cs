using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Android;
using static UnityEngine.GraphicsBuffer;

public class Movimiento : MonoBehaviour
{
    public float vel; //Velocidad con la que se mueve el avión
    public float rotvel; //Velocidad de rotación
    Vector3 distance; //Distancia entre la cámara y el target
    GameObject aspas; //Para guardar aspas

    // Start is called before the first frame update
    void Start()
    {
        aspas = GameObject.FindGameObjectWithTag("EditorOnly");//Se busca el objeto aspas. Podría haber creado un Tag nuevo pero como no voy a usar el EditorOnly pues para tener todo un poco más limpio
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(0,0, vel * Time.deltaTime); //Movimiento constante hacia el frente
        if (aspas != null)
        {
            aspas.transform.Rotate(0, 0, -1000 * vel * Time.deltaTime); //Se rotan las aspas del avión
        }
        
        if (Input.GetKey(KeyCode.W)) //Cuando se pulsa W el avión desciende
        {
            this.transform.Rotate(rotvel * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.S))//Cuando se pulsa S el avión asciende
        {
            this.transform.Rotate(-rotvel * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.A))//Cuando se pulsa A el avión vira a izquierda
        {
            this.transform.Rotate(0, -rotvel * Time.deltaTime, 0);

        }
        if (Input.GetKey(KeyCode.D))//Cuando se pulsa D el avión vira a derecha
        {
            this.transform.Rotate(0, rotvel * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.Q))//Cuando se pulsa Q el avión gira a la izquierda
        {
            this.transform.Rotate(0, 0, rotvel * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.E))//Cuando se pulsa E el avión gira a la derecha
        {
            this.transform.Rotate(0, 0, -rotvel * Time.deltaTime);
        }
    }
}
