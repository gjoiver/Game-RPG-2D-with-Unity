using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public List<Quest> quest;
    private DialogManager dialogManager;
    public QuestItem itemCollected;
    public QuestEnemy enemyKilled;
    // Start is called before the first frame update
    void Start()
    {
        dialogManager = FindObjectOfType<DialogManager>();
        foreach(Transform t in transform)
        {
            quest.Add(t.gameObject.GetComponent<Quest>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ShowQuestText(string questText)
    {
        Debug.Log("Mostra linea");
        dialogManager.ShowDialog(new string[] { questText });
    }
    public Quest QuestWhitID(int questID)
    {
        Quest q = null;
        foreach(Quest temp in quest){
            if (temp.questID == questID)
            {
                q = temp;
            }
        }
        return q;
    }
}
