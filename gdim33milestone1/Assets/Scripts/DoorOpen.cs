using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public Transform doorPivot;
    public Vector3 closedRotation;
    public Vector3 openRotation;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doorPivot.localEulerAngles = openRotation;
        }
    }

   
}
