using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBoard : MonoBehaviour
{
    private GameObject player;
    private bool playerOnBoard = false;
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerOnBoard = true;
            player = collision.gameObject;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerOnBoard = false;
            player = null;
        }
    }

    private void FixedUpdate()
    {
        if (playerOnBoard && player != null)
        {
            PlayerMove(player);
        }
    }

    private void PlayerMove(GameObject player)
    {
        Rigidbody rigidbody = player.GetComponent<Rigidbody>();
        if (rigidbody != null)
            
        {
            Vector3 slideDirection = rigidbody.velocity.normalized * CalculateSlideDistance(); // Get direction and apply force
            rigidbody.AddForce(slideDirection, ForceMode.Impulse);

            StartCoroutine(SlideEffect(rigidbody, slideDirection, 3f)); // Apply effect for 3 seconds
        }
    }

    private IEnumerator SlideEffect(Rigidbody rigidbody, Vector3 direction, float duration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            rigidbody.AddForce(direction * Time.deltaTime, ForceMode.Force); // Continuously apply force
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    private float CalculateSlideDistance() // Adjust this value to control slide distance
    {
        return 10f;
    }
}
