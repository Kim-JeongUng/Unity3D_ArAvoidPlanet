using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndSceneGE : MonoBehaviour
{
    public Text HighScore;
    public Text Score;
    public float btnPressTimer = 0;
    public Text ResetText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            if (PlayerPrefs.GetFloat("Score") > PlayerPrefs.GetFloat("HighScore"))
                PlayerPrefs.SetFloat("HighScore", PlayerPrefs.GetFloat("Score"));
        }
        catch
        {
            PlayerPrefs.SetFloat("HighScore", PlayerPrefs.GetFloat("Score"));
        }
        HighScore.text = PlayerPrefs.GetFloat("HighScore").ToString("F2");
        Score.text = PlayerPrefs.GetFloat("Score").ToString("F2");

        if (Input.GetMouseButton(0))
        {
            btnPressTimer += Time.deltaTime;
            if (btnPressTimer >= 2) //2초이상 누르면 초기화
            {
                PlayerPrefs.SetFloat("HighScore", 0);
                PlayerPrefs.SetFloat("Score", 0);
                HighScore.text = PlayerPrefs.GetFloat("HighScore").ToString("F2");
                Score.text = PlayerPrefs.GetFloat("Score").ToString("F2");
                btnPressTimer = 0;
                ResetText.text = "Reset Compelete";
            }

        }
        if (Input.GetMouseButtonUp(0))
        {
            ResetText.text = "Reset score : long click";
            SceneManager.LoadScene("StartScene");
        }
    }
}
