using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointSystem : MonoBehaviour
{
    public Vector3 lastCheckpointPosition;
    public CharacterController controller;

    void Start()
    {
        // Set initial spawn point to the player's starting position
        lastCheckpointPosition = transform.position;
        controller = GetComponent<CharacterController>();
    }

    public void SetCheckpoint(Vector3 newCheckpoint)
    {
        lastCheckpointPosition = newCheckpoint + new Vector3(0, 10 , 0);
        Debug.Log(newCheckpoint);
    }

    public void Respawn()
    {
        controller.enabled = false;
        transform.position = lastCheckpointPosition;
        // Enable it back
        controller.enabled = true;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DeadlyObstacle"))
        {
           
            Respawn();
        }
    }
}