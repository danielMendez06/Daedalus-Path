using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreEasy : MonoBehaviour
{   
    public static ScoreEasy instance;

    public Text scoreText;
    public Text highScoreText;
    private float startTime;
    public static float m;
    public static float s;
    public static float mp;
    public static float sp;
    public static float HSm;
    public static float HSs;
    
    private void Awake()
    {
        if(PlayerPrefs.HasKey("EasyHSm"))
        {
            HSm = PlayerPrefs.GetFloat("EasyHSm");
            HSm = PlayerPrefs.GetFloat("EasyHSm");
        }
        else
        {
            HSm = -1;
        }
        if(PlayerPrefs.HasKey("EasyHSs"))
        {
            HSs = PlayerPrefs.GetFloat("EasyHSs");
        }
        else
        {
            HSm = -1;
        }  
    }
    
    void Start()
    {
        startTime = Time.time;
        //HIGHSCORE RESET//
        //PlayerPrefs.SetFloat("EasyHSm",-1);
        //PlayerPrefs.SetFloat("EasyHSs",-1);
    }
    
    void Update()
    {
        float t = Time.time - startTime;
        m = ((int) t / 60);
        s = (t % 60);
        string minutes = mp.ToString();
        string seconds = sp.ToString("0");

        if (s < 10)
        {
            scoreText.text = minutes + ":0" + seconds;
        }
        else
        {
            scoreText.text = minutes + ":" + seconds;
        }

        if (HSs < 10)
        {
            highScoreText.text = HSm + ":0" + (int)HSs;
        }
        else
        {
            highScoreText.text = HSm + ":" + (int)HSs;
        }
    }
    
    public static void UpdateHighscore()
    {   
        mp = m;
        sp = s;
        if(mp < HSm || HSm == -1)
        {
            HSm = mp;
            HSs = sp;
            PlayerPrefs.SetFloat("EasyHSm",HSm);
            PlayerPrefs.SetFloat("EasyHSs",HSs);
        }
        else if(mp == HSm)
        {
            if(sp < HSs) 
            {
                HSm = mp;
                HSs = sp;
                PlayerPrefs.SetFloat("EasyHSm",HSm);
                PlayerPrefs.SetFloat("EasyHSs",HSs);
            }
        }
    }
}
