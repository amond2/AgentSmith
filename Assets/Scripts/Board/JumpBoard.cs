using UnityEngine;

public class JumpBoard : MonoBehaviour
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
            PlayerJump(player);
        }
    }

    private void PlayerJump(GameObject player)
    {
        Rigidbody rigidbody = player.GetComponent<Rigidbody>();
        if (rigidbody != null)
        {
            Vector3 jumpVelocity = new Vector3(rigidbody.velocity.x, CalculateJumpHeight(), rigidbody.velocity.z);
            rigidbody.velocity = jumpVelocity;
        }
    }

    private float CalculateJumpHeight()
    {
        return 10f;
    }
}