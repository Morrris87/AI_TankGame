using UnityEngine;

public class GunShot : MonoBehaviour
{
    public float damage = 10.0f;
    public float range = 100f;

    public Camera fpsCam;
    public ParticleSystem MuzzelFlash;

    public ParticleSystem MachineGun1;
    public ParticleSystem MachineGun2;

    public GameObject ImpactEffect;
    bool muzzleOn;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

    }

    void Shoot()
    {
        if (MuzzelFlash != null)
        {
            ParticleSystem.EmissionModule emission = MuzzelFlash.emission;
            MuzzelFlash.Play();
            emission.enabled = muzzleOn;
        }
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            TargetHit target = hit.transform.GetComponent<TargetHit>();

            if (target != null)
            {
                target.TakeDamage(damage);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * 300);
            }

            GameObject ImpactGO = Instantiate(ImpactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(ImpactGO, 2f);
        }
    }


}
