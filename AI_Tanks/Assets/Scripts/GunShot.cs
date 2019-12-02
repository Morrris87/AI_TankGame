using UnityEngine;

public class GunShot : MonoBehaviour
{
    public float damage = 10.0f;
    public float range = 100f;
    public float impactForce = 10000000;

    public Camera fpsCam;
    public ParticleSystem MuzzelFlash;
    public GameObject ImpactEffect;
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        MuzzelFlash.Play();

        RaycastHit hit;
       if( Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            TargetHit target = hit.transform.GetComponent<TargetHit>();

            if(target != null)
            {
                target.TakeDamage(damage);
            }

            if(hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * 300);
            }

            GameObject ImpactGO = Instantiate(ImpactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(ImpactGO, 2f);
        }
    }
}
