using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAi : MonoBehaviour
{
    float elapsedTime = 0.0f;
    float intervalTime = 3.0f;
    public GameObject Bullet;
    public GameObject Bullet_Emitter;
    public float Bullet_Forward_Force;

    public GameObject Aim;



    // Start is called before the first frame update
    void Start()
    {
        GetComponentInParent<BoxCollider>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.spawned)
        {
            GetComponentInParent<BoxCollider>().enabled = true;
            elapsedTime += Time.deltaTime;

            Transform tank = gameObject.transform;
            Transform player = Aim.transform;


            Quaternion targetRotation = Quaternion.LookRotation(new Vector3(player.position.x, player.position.y + 1, player.position.z) - tank.position);

            tank.rotation = Quaternion.Slerp(tank.rotation,
                    targetRotation, 1);

            if (elapsedTime >= intervalTime)
            {
                if (GameManager.Instance.timeToShoot)
                {
                    GameObject Temporary_Bullet_Handler;
                    Temporary_Bullet_Handler = Instantiate(Bullet, Bullet_Emitter.transform.position, Bullet_Emitter.transform.rotation) as GameObject;

                    //Sometimes bullets may appear rotated incorrectly due to the way its pivot was set from the original modeling package.
                    //This is EASILY corrected here, you might have to rotate it from a different axis and or angle based on your particular mesh.
                    Temporary_Bullet_Handler.transform.Rotate(Vector3.left * 90);

                    //Retrieve the Rigidbody component from the instantiated Bullet and control it.
                    Rigidbody Temporary_RigidBody;
                    Temporary_RigidBody = Temporary_Bullet_Handler.GetComponent<Rigidbody>();

                    //Tell the bullet to be "pushed" forward by an amount set by Bullet_Forward_Force.
                    Temporary_RigidBody.AddForce(transform.forward * Bullet_Forward_Force);

                    //Basic Clean Up, set the Bullets to self destruct after 10 Seconds, I am being VERY generous here, normally 3 seconds is plenty.
                    Destroy(Temporary_Bullet_Handler, 10.0f);

                    elapsedTime = 0.0f;
                }
                GameManager.Instance.timeToShoot = true;                
            }
        }
    }   
}
