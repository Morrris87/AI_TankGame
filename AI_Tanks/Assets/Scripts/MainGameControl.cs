using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainGameControl : MonoBehaviour
{
    public Canvas pausedScreen;
    public bool paused = false;
    // Start is called before the first frame update
    void Start()
    {
        pausedScreen.enabled = false;
        paused = false;  
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            paused = true;
            pausedScreen.enabled = true;

        }
    }

    public void ResumeClicked()
    {
        pausedScreen.enabled = false;
    }

    public void QuitClicked()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
