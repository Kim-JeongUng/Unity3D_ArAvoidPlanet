using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SpaceShip : MonoBehaviour
{
    public GameObject ship;
    public GameObject oneTimeText;
    public AudioClip DeadSound;
    public Material DieMt;
    public Material LiveMt;


    public float cur_angle;
    float prev_angle;
    float delta_angle;
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
        cur_angle = ship.transform.eulerAngles.y;
        
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
            }
        }

        //Hp
        HpGauge.fillAmount = Hp/100;
        if (Hp <= 0)
        {
            PlayerPrefs.SetFloat("Score", timer);
            this.transform.GetChild(0).GetComponent<Renderer>().material = DieMt;
            StartCoroutine(Die());
        }

        timer += Time.deltaTime;
        
        if (ismessage == true)
            m_Timer += Time.deltaTime;
        if (m_Timer >= 3.0f)
        {
            ismessage = false;
            oneTimeText.GetComponent<Text>().text = "Message Area";
            m_Timer = 0;
        }
        SetPosChild1 = ItemSetPosition[0].transform.childCount-1;
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
        oneTimeMessage("GameOver!");
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene("EndScene");
    }
}