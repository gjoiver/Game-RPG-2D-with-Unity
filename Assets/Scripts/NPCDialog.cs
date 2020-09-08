using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CircleCollider2D))]
public class NPCDialog : MonoBehaviour
{
    public string npcName;
    public string[] npcDialogLines;

    private DialogManager dialogManager;
    private bool playerInTheZone;
    // Start is called before the first frame update
    void Start()
    {
        dialogManager = FindObjectOfType<DialogManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            playerInTheZone = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            playerInTheZone = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

        string[] finalDialog = new string[npcDialogLines.Length];
        int i = 0;
        foreach(string line in npcDialogLines)
        {
            finalDialog[i++]=(npcName != null ? npcName + "\n" : "") + line;
        }
            
        if (playerInTheZone && Input.GetMouseButtonDown(1))
        {
            dialogManager.ShowDialog(finalDialog);
        }
        if (gameObject.GetComponentInParent<NPCMovement>() != null)
        {
            gameObject.GetComponentInParent<NPCMovement>().isTalking = true;
        }
    }
}
