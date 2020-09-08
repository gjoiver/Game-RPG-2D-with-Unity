using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTrigger : MonoBehaviour
{
    private QuestManager questManager;
    public int questID;
    public bool startPoint, endPoint;
    private bool playerInZone;
    public bool automaticCatch;
    // Start is called before the first frame update
    void Start()
    {
        questManager = FindObjectOfType<QuestManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            playerInZone = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            playerInZone = false;
        }
    }
    private void Update()
    {
        if (playerInZone)
        {
            if (automaticCatch||!automaticCatch&&Input.GetMouseButtonDown(1))
            {
                Quest q = questManager.QuestWhitID(questID);
                if (q == null)
                {
                    Debug.LogErrorFormat("La misión con ID {0} no existe", questID);
                    return;
                }
                //si llego aquí, la misión existe
                if (!q.questCompleted)
                {
                    //No he completado la misión actual
                    if (startPoint)
                    {
                        Debug.Log("Todo bien");
                        //estoy en la zona que empieza la misión
                        if (!q.questStart)
                        {
                            Debug.Log("Todo bien 2");
                            q.gameObject.SetActive(true);
                            q.StartQuest();
                        }
                    }
                    if (endPoint)
                    {
                        Debug.Log("Fin");
                        //estoy en la zona donde termina la misión
                        if (q.questStart)
                        {
                            q.CompleteQuest();
                        }
                    }
                }
            }
        }
    }
}
