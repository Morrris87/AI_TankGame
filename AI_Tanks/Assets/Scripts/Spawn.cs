using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;
    public GameObject Spawn1;
    public GameObject Spawn2;
    public GameObject Spawn3;
    public GameObject Spawn4;
    public GameObject Center;
    // Start is called before the first frame update
    void Start()
    {
        int spot = Random.Range(0, 4);
        if(spot == 0)
        {
            player.transform.position = Spawn1.transform.position;
            player.transform.rotation = Quaternion.identity;//make rotate towarde the center

            Instantiate(enemy, Spawn2.transform.position, Quaternion.identity);//make rotate towarde the center
            Instantiate(enemy, Spawn3.transform.position, Quaternion.identity);//make rotate towarde the center
            Instantiate(enemy, Spawn4.transform.position, Quaternion.identity);//make rotate towarde the center
        }
        else if(spot == 1)
        {
            player.transform.position = Spawn2.transform.position;
            player.transform.rotation = Quaternion.identity;//make rotate towarde the center

            Instantiate(enemy, Spawn1.transform.position, Quaternion.identity);//make rotate towarde the center
            Instantiate(enemy, Spawn3.transform.position, Quaternion.identity);//make rotate towarde the center
            Instantiate(enemy, Spawn4.transform.position, Quaternion.identity);//make rotate towarde the center
        }
        else if (spot == 2)
        {
            player.transform.position = Spawn3.transform.position;
            player.transform.rotation = Quaternion.identity;//make rotate towarde the center

            Instantiate(enemy, Spawn1.transform.position, Quaternion.identity);//make rotate towarde the center
            Instantiate(enemy, Spawn2.transform.position, Quaternion.identity);//make rotate towarde the center
            Instantiate(enemy, Spawn4.transform.position, Quaternion.identity);//make rotate towarde the center
        }
        else if (spot == 3)
        {
            player.transform.position = Spawn4.transform.position;
            player.transform.rotation = Quaternion.identity;//make rotate towarde the centerw

            Instantiate(enemy, Spawn1.transform.position, Quaternion.identity);//make rotate towarde the center
            Instantiate(enemy, Spawn2.transform.position, Quaternion.identity);//make rotate towarde the center
            Instantiate(enemy, Spawn3.transform.position, Quaternion.identity);//make rotate towarde the center
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
