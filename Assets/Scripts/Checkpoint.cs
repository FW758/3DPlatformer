using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CheckpointSystem checkpointSystem = other.GetComponent<CheckpointSystem>();
            if (checkpointSystem != null)
            {
                checkpointSystem.SetCheckpoint(transform.position);
                Debug.Log("Checkpoint reached!");
            }
        }
    }
}

