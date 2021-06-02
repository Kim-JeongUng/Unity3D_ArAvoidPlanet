using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vuforia;

public class StartBtn : MonoBehaviour, IVirtualButtonEventHandler
{
    public GameObject lBtnObj;

    public GameObject rBtnObj;

    private bool lbpressed;
    private bool rbpressed;


    // Use this for initialization
    void Start()
    {
        lBtnObj = GameObject.Find("leftVirtualButton");
        lBtnObj.GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);

        rBtnObj = GameObject.Find("rightVirtualButton");
        rBtnObj.GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);
    }
    void Update()
    {
        if (lbpressed && rbpressed)
            SceneManager.LoadScene("GameScene");
    }
    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        Debug.Log("Button pressed");
        Debug.Log(vb.name);
        if (vb.name == "leftVirtualButton")
        {
            lbpressed = true;
        }
        else if (vb.name == "rightVirtualButton")
        {
            rbpressed = true;
        }

    }

    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
        Debug.Log("Button released");
        if (vb.name == "leftVirtualButton")
        {
            lbpressed = false;
        }
        else if (vb.name == "rightVirtualButton")
        {
            rbpressed = false;
        }
    }
}
