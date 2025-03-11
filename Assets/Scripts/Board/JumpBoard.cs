using UnityEngine;

public class JumpBoard : MonoBehaviour
{
    [SerializeField] private float jumpForce = 10f; 
    private bool playerOnBoard = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerOnBoard = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerOnBoard = false;
        }
    }

    private void FixedUpdate()
    {
        if (playerOnBoard)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                Rigidbody rigidbody = player.GetComponent<Rigidbody>();
                if (rigidbody != null)
                {
                    rigidbody.velocity = new Vector3(rigidbody.velocity.x, jumpForce, rigidbody.velocity.z);
                }
            }
        }
    }
}