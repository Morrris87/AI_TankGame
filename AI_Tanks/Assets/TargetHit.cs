
using UnityEngine;

public class TargetHit : MonoBehaviour
{

    public float health = 50.0f;

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }
    
    void Die()
    {
        Destroy(gameObject);
    }
}
