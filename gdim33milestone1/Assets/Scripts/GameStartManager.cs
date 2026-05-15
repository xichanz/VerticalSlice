using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartManager : MonoBehaviour
{
    public GameObject rulePanel;

    public MonoBehaviour playerController;
    public MonoBehaviour mouseLook;

    private bool gameStarted = false;
    private static bool hasShownRules = false;

    private void Start()
    {
        if (!hasShownRules)
        {
            rulePanel.SetActive(true);

            if (playerController != null ) 
                playerController.enabled = false;
                mouseLook.enabled = false;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

        }

        else
        {
            rulePanel.SetActive (false);
            if (playerController != null )
                playerController.enabled = true;
                mouseLook.enabled = true;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            gameStarted = true;
        }
        
    }

    private void Update()
    {
        if(!gameStarted && (Input.GetKeyDown(KeyCode.Space)|| (Input.GetKeyDown(KeyCode.Return))))
        {
            StartGame();
        }
    }

    void StartGame()
    {
        gameStarted = true;

        hasShownRules = true;

        rulePanel.SetActive(false);
        if( playerController != null )
            playerController.enabled = true;
        mouseLook.enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
