
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class TargetHit : MonoBehaviour
{
    //Put this script on the enemy

    public float health = 50.0f;
    public ParticleSystem Blowup;
    public AudioClip BlowupSound;
    public AudioSource audioSource;
    public GameObject tank;
    public GameObject UI;

    float elapsedTime;

    public void TakeDamage(float amount)
    {
        health -= amount;

        //if this is the player update the ui
        if (this.name == "Player")
        {
            UI.GetComponent<UIController>().UpdateUI();
        }

        if (health <= 0f)
        {
            if (tag != "Player")
            {
                Die();
            }
            else
            {
                if (GetComponent<Lives>().lives <= 0)
                {
                    GetComponent<Lives>().Respawn();
                    health = 50.0f;
                }
                else
                {
                    Die();
                }
            }
        }
    }

    private void Awake()
    {
        elapsedTime = 0;        
    }

    void Die()
    {
        GetComponent<BoxCollider>().enabled = false;

        if (tag == "Enemy" || tag == "Player")
        {
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }        

        Blowup.Play();
        audioSource.PlayOneShot(BlowupSound);

        transform.rotation = Quaternion.LookRotation(transform.forward, Vector3.up);

        if (tag == "Enemy")
        {
            GameManager.Instance.Enemies.Remove(gameObject);
        }
        
        Destroy(tank, 0.2f);
        Destroy(gameObject, 4.2f);
    }

    private void OnCollisionEnter(Collision c)
    {

        Vector3 dir = c.GetContact(0).point - transform.position;

        dir = -dir.normalized;

        if (c.gameObject.tag == "Bullet")
        {
            AI aIScript = GetComponent<AI>();
            TakeDamage(10f);


            KnockBack(dir, 150);                       

            if (aIScript)
            {
              //  aIScript.navAgent.
            }
        }

        else if(c.gameObject.tag == "Player")
        {
            KnockBack(dir, 100);
        }

        else if (c.gameObject.tag == "Enemy")
        {
           KnockBack(dir, 100);
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

    void FixedUpdate()
    {
        //uiController.UpdateUI();
    }
    void KnockBack(Vector3 dir, int force)
    {
        Rigidbody r = GetComponent<Rigidbody>();

        if(this.tag == "Enemy")
        {
            GetComponent<NavMeshAgent>().enabled = false;
        }        

        r.isKinematic = false;
        r.AddForce(dir * force);

        while (elapsedTime < 2.5f)
        {
            elapsedTime += Time.deltaTime;
        }

        elapsedTime = 0;

        if (this.tag == "Enemy")
        {
            r.isKinematic = true;
            GetComponent<NavMeshAgent>().enabled = true;
        }
    }
}
