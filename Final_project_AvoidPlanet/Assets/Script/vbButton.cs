using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vuforia;

public class vbButton : MonoBehaviour, IVirtualButtonEventHandler
{
    public SpaceShip ship;
    public GameObject lBtnObj;
    public GameObject rBtnObj;

    public bool LbPressed;
    public bool RbPressed;

    public bool TempLbPressed;
    public bool TempRbPressed;

    private float tempLbTimer;
    private float tempRbTimer;
    //public Animator cubeAni;

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
        if (TempLbPressed && !LbPressed)
        {
            tempLbTimer += Time.deltaTime;
            if (tempLbTimer > 0.3f)
            {
                LbPressed = true;
                Debug.Log("lbbtnPressed");
            }
        }
        if (TempRbPressed && !RbPressed)
        {
            tempRbTimer += Time.deltaTime;
            if (tempRbTimer > 0.3f)
            {
                RbPressed = true;
                Debug.Log("rbbtnPressed");
            }
        }
    }
    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        if(vb.name == "leftVirtualButton")
        {
            TempLbPressed = true;
        }
        if(vb.name == "rightVirtualButton"){
            TempRbPressed = true;
        }

    }

    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
        Debug.Log("Button released"); 
        if (vb.name == "leftVirtualButton")
        {
            LbPressed = false;
            TempLbPressed = false;
            tempLbTimer = 0;
        }
        else if (vb.name == "rightVirtualButton")
        {
            RbPressed = false;
            TempRbPressed = false;
            tempRbTimer = 0;
        }
    }
}
