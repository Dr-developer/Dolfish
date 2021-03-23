using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class PlayeTimer : MonoBehaviour
{
    public static PlayeTimer ins;
    public Image Timebar;
    public float maxTime = 15f;
    public float reminedTime;
    public float timeLeft;
    public bool stopTheTimer;
    public bool fixTheTimer;
    private void Awake()
    {
        ins = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        fixTheTimer = false;
        timeLeft = maxTime;
        stopTheTimer = false;
        Timebar.fillAmount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            Timebar.fillAmount = timeLeft / maxTime;
        }
        else
        {
            TimeEnded();

        }
    }

    private void TimeEnded()
    {
         if(timeLeft<0) 
         {
            //GameManger.ins.PlayerTurner();
            if (GameManger.ins.listOfGameOverPlayers.Contains(0) == false)
            {
                StartCoroutine(RandomCardGenrtor.Ins.NoneHumanPlaying(2));
            }
            else if (GameManger.ins.listOfGameOverPlayers.Contains(1) == false)
            {
                StartCoroutine(RandomCardGenrtor.Ins.NoneHumanPlaying(3));

            }

            
            stopTheTimer = true;
            PutCardInPlace.ins.HideThePanel();
            //gameObject.SetActive(false);
         }
       
    }
}
