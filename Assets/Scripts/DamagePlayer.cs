using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    //public float timeToRevivePlayer;
    //private float timeRevivalCounter;
    //private bool playerReviving;

    public int damage;

    public GameObject canvasDamage;

    private CharacterStats playerStats;
    private CharacterStats _stats;

    private void Start()
    {
        playerStats=GameObject.Find("Player").GetComponent<CharacterStats>();
        _stats = GetComponent<CharacterStats>();
    }

    //private GameObject thePlayer;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //factorde fuerza del enemigo
            float strFac = 1 + _stats.strengthLevels[_stats.level]/CharacterStats.MAX_STAT_VALUE;
            //factor de defensa del enemigo
            float plaFac = 1 - playerStats.defenseLevels[playerStats.level] / CharacterStats.MAX_STAT_VALUE;
            //se multiplican para obtener el daño total
            float totalDamage = damage*strFac*plaFac;
            //se hace un clamp para limitar los valores de 1 a 100
            //se redondea en un numero entero
            totalDamage = Mathf.Clamp((int)totalDamage, 1, CharacterStats.MAX_HEALTH);
            //probabilidad que el enemigo falle
            float missProb = playerStats.luckLevels[playerStats.level];
            //se genera un numero entre 0 y 100, y si es menor que la suerte del jugador, el ataque dele enemigo es 0
            if (Random.Range(0, CharacterStats.MAX_STAT_VALUE) < missProb)
            {
                //pero si un numero generado entre 0 y 100 es mayor que la precision del enemigo,
                //tambien fallara, en caso contrario se efectuara el daño
                if (Random.Range(0, CharacterStats.MAX_STAT_VALUE) > _stats.accuaricyLevels[_stats.level])
                {
                    totalDamage = 0;
                }
            }

            var clone = (GameObject)Instantiate(canvasDamage, collision.gameObject.transform.position, Quaternion.Euler(Vector3.zero));

            collision.gameObject.GetComponent<HealthManager>().DamageCharacter(damage);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (playerReviving)
        //{
        //    timeRevivalCounter -= Time.deltaTime;
        //    if (timeRevivalCounter < 0)
        //    {
        //        playerReviving = false;
        //        thePlayer.SetActive(true);
        //    }
        //}
    }
}
