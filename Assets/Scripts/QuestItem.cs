using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CircleCollider2D))]
public class QuestItem : MonoBehaviour
{
    public int questID;
    private QuestManager manager;
    public string itemName;
    // Start is called before the first frame update
   
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            manager = FindObjectOfType<QuestManager>();
            Quest q = manager.QuestWhitID(questID);
            if (q == null)
            {
                Debug.LogErrorFormat("La misión con id {0} no exitse", questID);
            }
            if (q.gameObject.activeInHierarchy &&!q.questCompleted)
            {
                manager.itemCollected = this;
                gameObject.SetActive(false);
            }
        }
    }
}
