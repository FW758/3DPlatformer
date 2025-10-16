using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointSystem : MonoBehaviour
{
    private Vector3 lastCheckpointPosition;

    void Start()
    {
        // Set initial spawn point to the player's starting position
        lastCheckpointPosition = transform.position;
    }

    public void SetCheckpoint(Vector3 newCheckpoint)
    {
        lastCheckpointPosition = newCheckpoint;
    }

    public void Respawn()
    {
        transform.position = lastCheckpointPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DeadlyObstacle"))
        {
            Respawn();
        }
    }
}