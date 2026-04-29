using UnityEngine;
using TMPro;

public class HidingSpot : MonoBehaviour
{
    public GameObject player;
    public Transform hidePosition;
    public Transform exitPosition;
    public TextMeshProUGUI interactText;

    public bool isHiding = false;

    private bool playerNearby = false;

    void Start()
    {
        interactText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            if (isHiding)
                ExitHide();
            else
                EnterHide();
        }
    }

    void EnterHide()
    {
        isHiding = true;
        MovePlayerTo(hidePosition);
        interactText.text = "Press E to leave hiding spot";
    }

    void ExitHide()
    {
        isHiding = false;
        MovePlayerTo(exitPosition);
        interactText.text = "Press E to enter hiding spot";
    }

    void MovePlayerTo(Transform target)
    {
        CharacterController cc = player.GetComponent<CharacterController>();

        if (cc != null)
        {
            cc.enabled = false;
        }

        player.transform.position = target.position;
        player.transform.rotation = target.rotation;

        if (cc != null)
        {
            cc.enabled = true;
        }
    }

    public bool PlayerIsHiding()
    {
        return isHiding;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            interactText.gameObject.SetActive(true);

            if (isHiding)
                interactText.text = "Press E to leave hiding spot";
            else
                interactText.text = "Press E to enter hiding spot";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !isHiding)
        {
            playerNearby = false;
            interactText.gameObject.SetActive(false);
        }
    }
}



