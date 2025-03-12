using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBoard : MonoBehaviour
{
    [SerializeField] private float slideForce = 5f;  // 미끄러지는 힘
    [SerializeField] private float slideDuration = 3f; // 미끄러지는 지속 시간

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody rigidbody = collision.gameObject.GetComponent<Rigidbody>();
        if (rigidbody != null)
        {
            StartCoroutine(ApplySlideEffect(rigidbody));
        }
    }

    private IEnumerator ApplySlideEffect(Rigidbody rigidbody)
    {
        Vector3 slideDirection = rigidbody.velocity.normalized * slideForce;
        float elapsedTime = 0f;

        while (elapsedTime < slideDuration)
        {
            if (rigidbody != null)
            {
                rigidbody.AddForce(slideDirection * Time.deltaTime, ForceMode.Force);
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
