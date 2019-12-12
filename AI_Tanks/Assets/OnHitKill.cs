using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHitKill : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<TargetHit>().TakeDamage(100);
        }
    }
}
