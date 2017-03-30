using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Esc123 : MonoBehaviour {

    public GameObject continueButton;
    public Button quitButton;
    public Canvas quitCanvas;
    public GameObject mainCamera;

    // Use this for initialization
    void Start()
    {
        quitCanvas.enabled = false;
        Time.timeScale = 1;
    }

    public void ContinuePress()
    {
        //quitCanvas.enabled = false;
        ////this.gameObject.active = false;
        //GameObject.SetActive();
        //mainCamera.active = false;
        quitCanvas.enabled = !quitCanvas.enabled;
    }
    public void QuitButton()
    {
        Application.Quit();
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            quitCanvas.enabled = !quitCanvas.enabled;
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
            }
            if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
            }
        }
    }
}
