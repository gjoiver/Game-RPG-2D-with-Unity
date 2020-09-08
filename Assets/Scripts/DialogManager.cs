using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public GameObject dialogBox;
    public Text dialogText;
    public bool dialogActive;

    public string[] dialogLines;
    public int currentDialogLine;

    private PlayerController playerController;

    private void Start()
    {
        dialogActive = false;
        dialogBox.SetActive(false);
        playerController = FindObjectOfType<PlayerController>();
    }
    // Update is called once per frame
    void Update()
    {
        if (dialogActive && Input.GetKeyDown(KeyCode.Space))
        {
            currentDialogLine++;

            if (currentDialogLine > dialogLines.Length)
            {
                playerController.isTalking = false;
                dialogActive = false;
                dialogBox.SetActive(false);
            }
            else
            {
                dialogText.text = dialogLines[currentDialogLine-1];
            }

        }
        
    }
    public void ShowDialog(string[] lines)
    {
        currentDialogLine = 0;
        dialogLines = lines;
        dialogActive = true;
        dialogBox.SetActive(true);
        dialogText.text = dialogLines[currentDialogLine];
        playerController.isTalking = true;
    }
}
