using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Items : MonoBehaviour
{
    public bool oneTimeCheck = true;
    public string thisItemName;
    public AudioClip equipSound;
    bool LBtnUse = false;
    bool RBtnUse = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!LBtnUse && !RBtnUse) //장착되지 않음
            transform.Translate(Vector3.forward * Time.deltaTime * 100, Space.Self);

        if (LBtnUse || RBtnUse) //장착 완료
        {
            transform.localPosition = Vector3.zero;
            transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f); 
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
            if (other.gameObject.GetComponent<SpaceShip>().SetPosChild1 == 0) //아이템칸이 비었으면 
            {
                this.gameObject.GetComponent<AudioSource>().PlayOneShot(equipSound);
                other.gameObject.GetComponent<SpaceShip>().oneTimeMessage("Get "+ thisItemName);
                this.transform.parent = other.gameObject.GetComponent<SpaceShip>().ItemSetPosition[0]; //부모를 ItemSetPosition로 잡음
                LBtnUse =  true;
                oneTimeCheck = false;
            }
            else if(other.gameObject.GetComponent<SpaceShip>().SetPosChild2 == 0)
            {
                this.gameObject.GetComponent<AudioSource>().PlayOneShot(equipSound);
                other.gameObject.GetComponent<SpaceShip>().oneTimeMessage("Get " + thisItemName);
                this.transform.parent = other.gameObject.GetComponent<SpaceShip>().ItemSetPosition[1];
                RBtnUse = true;
                oneTimeCheck = false;
            }
            else
            {
                other.gameObject.GetComponent<SpaceShip>().oneTimeMessage("Equip P Full");
            }
        }
        if (other.gameObject.CompareTag("DestroyCollider"))
        {
            Destroy(this.gameObject);
        }
    }

    void UseAbility()
    {
        GameObject.Find("Ship").GetComponent<AllSkill>().UseSkill(thisItemName);
        if (LBtnUse)
            transform.parent.GetChild(0).GetComponent<TextMesh>().text = "Item1";
        else if(RBtnUse)
            transform.parent.GetChild(0).GetComponent<TextMesh>().text = "Item2";

        Destroy(this.gameObject);
    }
}
