using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotataPropellers : MonoBehaviour
{
    public GameObject drone;

    // Start is called before the first frame update
    void Start()
    {
        //drone.GetComponent<Animation>().Play("hover");
    }
    private void Update()
    {
        if(!drone.GetComponent<Animation>().isPlaying)
        {
            StartCoroutine(PlayHover());
        }
    }

    IEnumerator PlayHover()
    {
        drone.GetComponent<Animation>().Play("hover");
        do
        {
            yield return null;
        } while (drone.GetComponent<Animation>().isPlaying);
    }
}
