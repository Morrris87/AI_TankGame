
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
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        transform.rotation = Quaternion.LookRotation(transform.forward ,Vector3.up);
        Blowup.Play();
        audioSource.PlayOneShot(BlowupSound);
        //PlayEffectOnce(Blowup, this.transform.position);
        Destroy(tank, 0.2f);
        Destroy(gameObject, 4.2f);
    }

    private void OnCollisionEnter(Collision c)
    {
        if(c.gameObject.tag == "Bullet")
        {
            TakeDamage(10f);
            Vector3 dir = c.GetContact(0).point - transform.position;

            dir = -dir.normalized;

            GetComponent<Rigidbody>().AddForce(dir * 250);
        }
    }
    protected void PlayEffectOnce(ParticleSystem prefab, Vector3 position)
    {
        if (prefab == null)
        {
        }
        ParticleSystem ps = Instantiate(prefab, position, Quaternion.identity) as ParticleSystem;
        Destroy(ps.gameObject, ps.time);
    }
}
