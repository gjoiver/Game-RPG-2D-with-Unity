using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public int questID;
    public bool questCompleted;
    public bool questStart;
    private QuestManager questManager;

    public string title;
    public string startText;
    public string completeText;

    public bool needsItem;
    public List<QuestItem> itemNeeded;

    public bool killsEnemy;
    public List<QuestEnemy> enemies;
    public List<int> numberOfEnemies;

    public Quest nextQuest;
    // Start is called before the first frame update
    void Start()
    {
        questManager = FindObjectOfType<QuestManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (needsItem && questManager.itemCollected != null)
        {
            for(int i = 0; i < itemNeeded.Count; i++)
            {
                if (itemNeeded[i].itemName == questManager.itemCollected.itemName)
                {
                    itemNeeded.RemoveAt(i);
                    questManager.itemCollected = null;
                    break;
                }
            }
            if (itemNeeded.Count == 0)
            {           
                CompleteQuest();
            }
        }
        if (killsEnemy && questManager.enemyKilled != null)
        {
            Debug.Log("Tenemos a un enemigo matado");
            for(int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i].enemyName== questManager.enemyKilled.enemyName)
                {
                    numberOfEnemies[i]--;
                    questManager.enemyKilled= null;
                    if (numberOfEnemies[i] <= 0)
                    {
                        enemies.RemoveAt(i);
                        numberOfEnemies.RemoveAt(i);
                    }
                    break;
                }

            }
            if (enemies.Count == 0)
            {
                CompleteQuest();
            }
        }
    }
    //metodo que indica que empezo una mision
    public void StartQuest()
    {
        questStart = true;
        questManager.ShowQuestText(title + "\n" + startText);

        if (needsItem)
        {
            ActivateItems();
        }
        if (killsEnemy)
        {
            ActivateEnemies();
        }
        
    }
    private void OnLevelWasLoaded(int level)
    {
        if (needsItem)
        {
            ActivateItems();
        }
        if (killsEnemy)
        {
            ActivateEnemies();
        }
    }
    //activa los items de una mision en especifico
    void ActivateItems()
    {
        Object[] items = Resources.FindObjectsOfTypeAll<QuestItem>();
        foreach (QuestItem item in items)
        {
            if (item.questID == questID)
            {
                item.gameObject.SetActive(true);
            }
        }
    }
    //activa los enemigos
    void ActivateEnemies()
    {
        Object[] qenemies = Resources.FindObjectsOfTypeAll<QuestEnemy>();
        Debug.Log("Numero de enemigos: "+ qenemies.Length);
        foreach (QuestEnemy enemy in qenemies)
        {
            if (enemy.questID == questID)
            {
                enemy.gameObject.SetActive(true);
            }
        }
    }
    void ActivateNextQuest()
    {
        nextQuest.gameObject.SetActive(true);
        nextQuest.StartQuest();
    }
    //metodo que indica que finalizó una misión
    public void CompleteQuest()
    {
        questManager.ShowQuestText(title + "\n" + completeText);
        questCompleted = true;
        questStart = false;
        if (nextQuest != null)
        {
            Invoke("ActivateNextQuest", 5.0f);
        }
        gameObject.SetActive(false);
    }
}
