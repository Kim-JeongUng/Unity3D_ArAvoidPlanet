using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Items : MonoBehaviour
{
    bool LBtnUse = false;
    bool RBtnUse = false;

    public bool oneTimeCheck = true;
    public string thisItemName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!LBtnUse && !RBtnUse)
            transform.Translate(Vector3.forward * Time.deltaTime * 100, Space.Self);

        if (LBtnUse || RBtnUse)
        {
            transform.localPosition = Vector3.zero;
            transform.Rotate(0, 0, 0);
            transform.parent.GetChild(0).GetComponent<TextMesh>().text = thisItemName;
            if (LBtnUse && GameObject.Find("ImageTarget").GetComponent<vbButton>().LbPressed)
            {
                UseAbility();
            }
            else if (RBtnUse && GameObject.Find("ImageTarget").GetComponent<vbButton>().RbPressed)
            {
                UseAbility();
            }
        }

    }
    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.name == "Ship" && oneTimeCheck)
        {
            if (other.gameObject.GetComponent<SpaceShip>().SetPosChild1 == 0) //아이템이 없으면 부모를 ItemSetPosition로 잡음
            {
                this.transform.parent = other.gameObject.GetComponent<SpaceShip>().ItemSetPosition[0];
                LBtnUse =  true;
                oneTimeCheck = false;
            }
            else if(other.gameObject.GetComponent<SpaceShip>().SetPosChild2 == 0)
            {
                this.transform.parent = other.gameObject.GetComponent<SpaceShip>().ItemSetPosition[1];
                RBtnUse = true;
                oneTimeCheck = false;
            }
            else
            {
                other.gameObject.GetComponent<SpaceShip>().oneTimeMessage("NotUsed");
            }
        }
        if (other.gameObject.CompareTag("DestroyCollider"))
        {
            Destroy(this.gameObject);
        }
    }

    void UseAbility()
    {
        Debug.Log("used");
        GameObject.Find("Ship").GetComponent<AllSkill>().UseSkill(thisItemName);
        if(thisItemName == "Bomb")
        {
            this.gameObject.GetComponent<AudioSource>().Play();
        }
        //GameObject.Find("Ship").GetComponent<SpaceShip>().InvincibleObj.SetActive(true);
        if (LBtnUse)
            transform.parent.GetChild(0).GetComponent<TextMesh>().text = "Item1";
        else if(RBtnUse)
            transform.parent.GetChild(0).GetComponent<TextMesh>().text = "Item2";

        Destroy(this.gameObject);
    }
}
