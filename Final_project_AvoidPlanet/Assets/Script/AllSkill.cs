using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllSkill : MonoBehaviour
{
    public AudioClip[] audioClips;
    public GameObject InviObj;
    public bool bomb = false;

    private bool HpFixed = false;
    private float pastHp;

    private float timer;
    private float fixTimerMax = 4.0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (HpFixed) //invincibleW
        {
            timer += Time.deltaTime;
            SpaceShip.Hp = pastHp;
            GetComponent<SpaceShip>().oneTimeMessage("무적");
            if (timer > fixTimerMax)
            {
                HpFixed = false;
                timer = 0; 
                GetComponent<SpaceShip>().oneTimeMessage("");
                InviObj.SetActive(false);
            }
        }
        if (bomb)
        {
            float bombtimer = 0.0f;
            bombtimer += Time.deltaTime;
            if (bombtimer > 0.5f)
            {
                bomb = false;
            }
        }
        
    }
    public void UseSkill(string SkillName) //SkilName Set : Invincible , Smaller, Heal, Bomb
    {
        GetComponent<SpaceShip>().oneTimeMessage(SkillName);
        if (SkillName == "Invincible")
        {
            if (HpFixed)
            {
                fixTimerMax += 4;
                this.gameObject.GetComponent<AudioSource>().PlayOneShot(audioClips[0]);
                InviObj.SetActive(true);
            }
            else
            {
                HpFixed = true;
                pastHp = SpaceShip.Hp;
                this.gameObject.GetComponent<AudioSource>().PlayOneShot(audioClips[0]);
                InviObj.SetActive(true);
            }
        } 
        if (SkillName == "Smaller") //10초간 크기가 0.8 줄어듬
        {
            transform.localScale = transform.localScale * 0.8f;//new Vector3(0.9f, 0.9f, 0.9f); 
            StartCoroutine(Bigger());
            this.gameObject.GetComponent<AudioSource>().PlayOneShot(audioClips[1]);
        }
        if (SkillName == "Heal")
        {
            SpaceShip.Hp += 10;
            this.gameObject.GetComponent<AudioSource>().PlayOneShot(audioClips[2]);
        }
        if (SkillName == "Bomb")
        {
            bomb = true;
            this.gameObject.GetComponent<AudioSource>().PlayOneShot(audioClips[3]);
        }
    }
    IEnumerator Bigger()
    {
        yield return new WaitForSeconds(10.0f);
        transform.localScale  = transform.localScale * 1.25f;
        GetComponent<SpaceShip>().oneTimeMessage("크기 원위치");
        StopCoroutine(Bigger());
    }
}



