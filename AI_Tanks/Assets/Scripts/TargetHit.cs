
using UnityEngine;

public class TargetHit : MonoBehaviour
{
    //Put this script on the enemy

    public float health = 50.0f;
    public ParticleSystem Blowup;
    public AudioClip BlowupSound;
    public AudioSource audioSource;
    public GameObject tank;

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
        Blowup.Play();
        audioSource.PlayOneShot(BlowupSound);
        gameObject.GetComponent<BoxCollider>().enabled = false;
        Destroy(tank, 0.2f);
    }
}
