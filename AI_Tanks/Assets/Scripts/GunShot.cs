using UnityEngine;

public class GunShot : MonoBehaviour
{
    public Camera fpsCam;
    public ParticleSystem MuzzelFlash;

    public GameObject ImpactEffect;
    bool muzzleOn;


    //Drag in the Bullet Emitter from the Component Inspector.
    public GameObject Bullet_Emitter;

    //Drag in the Bullet Prefab from the Component Inspector.
    public GameObject Bullet;

    //Enter the Speed of the Bullet from the Component Inspector.
    public float Bullet_Forward_Force;

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


        //    GameObject ImpactGO = Instantiate(ImpactEffect, hit.point, Quaternion.LookRotation(hit.normal));
        //    Destroy(ImpactGO, 2f);

        if (Input.GetButtonDown("Fire1"))
        {

            GameObject Temporary_Bullet_Handler;

            Temporary_Bullet_Handler = Instantiate(Bullet, Bullet_Emitter.transform.position, Bullet_Emitter.transform.rotation) as GameObject;

            Temporary_Bullet_Handler.transform.Rotate(Vector3.left * 90);

            Rigidbody Temporary_RigidBody;
            Temporary_RigidBody = Temporary_Bullet_Handler.GetComponent<Rigidbody>();

            Temporary_RigidBody.AddForce(transform.forward * Bullet_Forward_Force);


            Destroy(Temporary_Bullet_Handler, 3.0f);
        }
    }


}
