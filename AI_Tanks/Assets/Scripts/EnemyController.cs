using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject Player;
    public GameObject turret;

    public GameObject Bullet_Emitter;

    public GameObject Bullet;

    public float Bullet_Forward_Force;

    public bool attack;

    void Start()
    {
        attack = false;
    }

    void Update()
    {
        transform.LookAt(Player.transform);

        if(attack)
        {
            Attack();
        }
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(turret.transform.position, transform.forward, Color.blue);
    }

    void Attack()
    {

        GameObject Temporary_Bullet_Handler;

        Temporary_Bullet_Handler = Instantiate(Bullet, Bullet_Emitter.transform.position, Bullet_Emitter.transform.rotation) as GameObject;

        Temporary_Bullet_Handler.transform.Rotate(Vector3.left * 90);

        Rigidbody Temporary_RigidBody;

        Temporary_RigidBody = Temporary_Bullet_Handler.GetComponent<Rigidbody>();

        Temporary_RigidBody.AddForce(Bullet_Emitter.transform.forward * Bullet_Forward_Force);

        Destroy(Temporary_Bullet_Handler, 10.0f);

        attack = false;
    }

    private bool IsInCurrentRange(Transform trans, Vector3 pos, int range)
    {
        bool inRange = false;
        float dist = Vector3.Distance(trans.position, pos);
        if (dist <= range)
        {
            inRange = true;
        }
        return inRange;
    }
}

