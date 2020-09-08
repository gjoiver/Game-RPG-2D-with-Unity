using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageNumber : MonoBehaviour
{
    //Velocidad a la que se muevel el texto
    public float damageSpeed;
    //Puntos de daños
    public float damagePoints;
    //Variable que se usa para mostrar el texto en el juego
    public Text damageText;
 

    public Vector2 direction = new Vector2(1, 0);
    public float timeToChangeDirection = 1;
    public float timeToChangeDirectionCounter = 1;


    // Update is called once per frame
    void Update()
    {
        timeToChangeDirectionCounter -= Time.deltaTime;
        if (timeToChangeDirectionCounter < timeToChangeDirection/2)
        {
            direction *= -1;
            timeToChangeDirectionCounter += timeToChangeDirection;
        }
        //hacemos que damagePoints que es un float, sea pasado a un string, al concatenar con comillas
        damageText.text = "" + damagePoints;
        //Cambiamos la posición del texto en las coordenadas y, para que se mueva hacia arriba
        this.transform.position = new Vector3(this.transform.position.x+direction.x*damageSpeed*Time.deltaTime, this.transform.position.y + damageSpeed * Time.deltaTime, this.transform.position.z);
    }
}
