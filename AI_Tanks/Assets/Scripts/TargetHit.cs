
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

    private void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.tag == "Enemy")
        {
            Vector3 dir = c.contacts[0].point - transform.position;

            dir = -dir.normalized;

            GetComponent<Rigidbody>().AddForce(dir * 50000);
        }
    }
}
