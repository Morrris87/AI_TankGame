using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    TargetHit characterTargetHit;
    Slider healthSlider;

    // Start is called before the first frame update
    void Start()
    {
        characterTargetHit = GameObject.Find("Player").GetComponent<TargetHit>();
        healthSlider = GameObject.Find("HealthSlider").GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateUI()
    {
        healthSlider.value = characterTargetHit.health;
    }
}
