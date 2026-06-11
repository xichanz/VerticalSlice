using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteInteraction : MonoBehaviour
{
    public GameObject readInteractionPrompt;
    public GameObject notePanel;

    public UISoundManager uiSoundManager;

    private GameObject player;
    private MonoBehaviour playerController;
    private MonoBehaviour mouseLook;

    private bool playerInRange = false;
    private bool noteOpen = false;

    void Start()
    {
        readInteractionPrompt.SetActive(false);
        notePanel.SetActive(false);
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (!noteOpen)
                OpenNote();
            else
                CloseNote();
        }

        if (noteOpen && Input.GetKeyDown(KeyCode.C))
        {
            CloseNote();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            player = other.gameObject;

            FindPlayerScripts();

            if (!noteOpen)
                readInteractionPrompt.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            readInteractionPrompt.SetActive(false);

            if (noteOpen)
                CloseNote();

            player = null;
        }
    }

    void FindPlayerScripts()
    {
        if (player == null)
            return;

        MonoBehaviour[] scripts = player.GetComponentsInChildren<MonoBehaviour>();

        foreach (MonoBehaviour script in scripts)
        {
            if (script == null)
                continue;

            string scriptName = script.GetType().Name;

            if (scriptName == "PlayerController")
            {
                playerController = script;
            }

            if (scriptName == "MouseLook")
            {
                mouseLook = script;
            }
        }
    }

    void OpenNote()
    {
        if (uiSoundManager != null)
            uiSoundManager.PlayClickSound();

        noteOpen = true;

        readInteractionPrompt.SetActive(false);
        notePanel.SetActive(true);

        if (playerController != null)
            playerController.enabled = false;

        if (mouseLook != null)
            mouseLook.enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void CloseNote()
    {
        if (uiSoundManager != null)
            uiSoundManager.PlayClickSound();

        noteOpen = false;

        notePanel.SetActive(false);

        if (playerInRange)
            readInteractionPrompt.SetActive(true);

        if (playerController != null)
            playerController.enabled = true;

        if (mouseLook != null)
            mouseLook.enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}