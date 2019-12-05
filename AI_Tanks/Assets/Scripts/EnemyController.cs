using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject Player;
    public GameObject turret;
   // public float movementSpeed = 1;
   // public float attackRange = 10;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //if ()
       // {
            //Vector3 target = Player.transform.position - turret.transform.position;

            //Quaternion targetRot = Quaternion.LookRotation(target);

            //turret.transform.rotation = Quaternion.Slerp(turret.transform.rotation, new Quaternion(targetRot.x, 1, targetRot.z, targetRot.w), 3 * Time.deltaTime);

            transform.LookAt(Player.transform);

            // transform.position += transform.forward * movementSpeed * Time.deltaTime;
       // }
        //else
        //{
        //    Attack();
        //}
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(turret.transform.position, transform.forward, Color.blue);
    }

    void Attack()
    {

    }
}

