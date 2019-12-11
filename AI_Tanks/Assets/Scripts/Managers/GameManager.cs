using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    public List<GameObject> Enemies;
    public GameObject boss;
    public bool spawned = false;
    public bool timeToShoot = false;

    // Start is called before the first frame update
    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Enemies.Count == 0 && !spawned)
        {
            boss.GetComponent<Animator>().Play("BossEntryAnim");
            spawned = true;
        }
    }
}
