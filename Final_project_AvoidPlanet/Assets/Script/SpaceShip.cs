using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SpaceShip : MonoBehaviour
{
    public GameObject ship;
    public GameObject oneTimeText;
    public GameObject explosion;
    public AudioClip DeadSound;
    public AudioClip detach;


    public Material DieMt;      //죽을 경우 나오는 머테리얼
    public Material LiveMt;     //일반 상황


    public static float timer;
    public static float Hp = 100;

    public Image HpGauge;
    public GameObject[] GameItems;
    public Transform[] ItemSetPosition;

    public int SetPosChild1; 
    public int SetPosChild2;

    public GameObject InvincibleObj; // 무적 구
    public bool invincible = false; // 무적아이템 사용중

    private float m_Timer; //메세지 타이머
    private bool ismessage = false; //메세지
    private float attachTimer;
    // Start is called before the first frame update
    void Start()
    {
        this.transform.GetChild(0).GetComponent<Renderer>().material = LiveMt;
        Hp = 100;
        timer = 0.0f;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameObject.Find("ImageTarget").GetComponent<MyDefaultTrackableEventHandler>().isAttach) {
            attachTimer += Time.deltaTime;
            oneTimeMessage("Check Mark"); 
            if(attachTimer > 1) // 안보여지면 초당 3씩 체력 낮아짐
            {
                Hp-= 3;
                attachTimer = 0;
                this.gameObject.GetComponent<AudioSource>().PlayOneShot(detach);
            }
        }

        //Hp
        HpGauge.fillAmount = Hp/100;

        //죽을 경우 시간 기록
        if (Hp <= 0)
        {
            StartCoroutine(Die());
        }
        timer += Time.deltaTime;
        
        //메세지 타이머(3초)
        if (ismessage == true)
            m_Timer += Time.deltaTime;
        if (m_Timer >= 3.0f)
        {
            ismessage = false;
            oneTimeText.GetComponent<Text>().text = "Message Area";
            m_Timer = 0;
        }

        //위치에 아이템이 있는지 판별, 1을 빼는 이유 : 기본으로 들어있는 ItemText(Canvas)를 제함
        SetPosChild1 = ItemSetPosition[0].transform.childCount-1; //childCount가 2면 아이템이 있는 것
        SetPosChild2 = ItemSetPosition[1].transform.childCount-1;
    }
    

    public void oneTimeMessage(string message)
    {
        Text tmp = oneTimeText.GetComponent<Text>();
        tmp.text = message;
        ismessage = true;
    }
    IEnumerator Die()
    {
        PlayerPrefs.SetFloat("Score", timer);
        explosion.SetActive(true);
        this.gameObject.GetComponent<AudioSource>().PlayOneShot(DeadSound);
        this.transform.GetChild(0).GetComponent<Renderer>().material = DieMt;
        oneTimeMessage("GameOver!");
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene("EndScene");
    }
}