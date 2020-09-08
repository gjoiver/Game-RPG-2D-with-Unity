using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{

    public int maxHealth;

    [SerializeField]
    private int currentHealth;
    // Start is called before the first frame update

        public int Health
    {
        get
        {
            return currentHealth;
        }
    }

    public bool flashActive;
    public float flashLength;
    public float flashCounter;

    private SpriteRenderer _characterRenderer;

    public int expWhenDefeated;

    private QuestEnemy quest;
    private QuestManager questManager;
    void Start()
    {
        _characterRenderer = GetComponent<SpriteRenderer>();
        UpdateMaxHealth(maxHealth);
        quest = GetComponent<QuestEnemy>();
        questManager = FindObjectOfType<QuestManager>();
    }
    //el daño que recibe un personaje
    public void DamageCharacter(int damage)
    {
        //a la vida actual se le resta dependiendo de la cantidad de daño
        currentHealth -= damage;
        //si la vida es menor o igual a cero, se desactiva el objeto
        if (currentHealth <= 0)
        {
            if (gameObject.tag.Equals("Enemy"))
            {
                GameObject.Find("Player").GetComponent<CharacterStats>().AddExperience(expWhenDefeated);
                questManager.enemyKilled = quest;
            }
            gameObject.SetActive(false);
        }
        if (flashLength > 0)
        {
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<PlayerController>().canMove = false;
            flashActive = true;
            flashCounter = flashLength;
        }
    }
    //metodo para volver a poner vida a un personaje
    public void UpdateMaxHealth(int newMaxHealth)
    {
        maxHealth = newMaxHealth;
        currentHealth = maxHealth;
    }

    void ToggleColor(bool visible)
    {
        _characterRenderer.color = new Color(_characterRenderer.color.r,_characterRenderer.color.g,_characterRenderer.color.b,(visible?1:0));
    }
    private void Update()
    {
        if (flashActive)
        {
            flashCounter -= Time.deltaTime;
            if (flashCounter > flashLength * 0.66f)
            {
                ToggleColor(false);
            }else if (flashCounter > flashLength * 0.33f)
            {
                ToggleColor(true);
            }else if (flashCounter > 0)
            {
                ToggleColor(false);
            }
            else
            {
                ToggleColor(true);
                flashActive = false;
                GetComponent<BoxCollider2D>().enabled = true;
                GetComponent<PlayerController>().canMove = true;
            }
        }
    }
}
