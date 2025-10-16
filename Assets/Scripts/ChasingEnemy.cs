using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingEnemy: MonoBehaviour
{
    public Transform player;
    public float speed = 4f;
    public float detectionRange = 10f;

    private bool isChasing = false;

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer < detectionRange)
        {
            isChasing = true;
        }
        else
        {
            isChasing = false;
        }

        if (isChasing)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Add logic for what happens when the enemy catches the player
            Debug.Log("Player caught by the enemy!");
            // Example: Restart level
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }
    }
}
