using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterStats : MonoBehaviour
{
    public const int MAX_STAT_VALUE= 100;
    public const int MAX_HEALTH = 9999;

    public int level;
    public int exp;
    public int[] expToLevelUp;

    public int[] hpLevels;
    public int[] strengthLevels;
    public int[] defenseLevels;
    public int[] speedLevels;
    public int[] luckLevels;
    public int[] accuaricyLevels;

    private HealthManager healthManager;
    private PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        healthManager = GetComponent<HealthManager>();
        playerController = GetComponent<PlayerController>();

        healthManager.UpdateMaxHealth(hpLevels[level]);

        if (gameObject.tag.Equals("Enemy"))
        {
            EnemyController controller = GetComponent<EnemyController>();
            controller.speed += speedLevels[level]/MAX_STAT_VALUE;
        }
    }

   
    public void AddExperience(int exp)
    {
        this.exp += exp;
        if (level >= expToLevelUp.Length)
        {
            return;
        }
        if (this.exp > expToLevelUp[level])
        {
            level++;
            healthManager.UpdateMaxHealth(hpLevels[level]);
            playerController.attackTime -= speedLevels[level]/MAX_STAT_VALUE;
        }
    }
}
