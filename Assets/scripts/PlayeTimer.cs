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
    public float maxTime = 20f;
    public float reminedTime;
    public float timeLeft;
    public bool stopTheTimer;

    private void Awake()
    {
        ins = this;
    }

    // Start is called before the first frame update
    void Start()
    {
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
        else if(timeLeft<0 && stopTheTimer==false&&PutCardInPlace.ins.passPanel.activeSelf==false)
        {
            GameManger.ins.PlayerTurner();
            RandomCardGenrtor.Ins.PlayerTurns();
            PutCardInPlace.ins.HideThePanel();
            stopTheTimer = true;
        }
        else if (timeLeft<0 && stopTheTimer==false&& PutCardInPlace.ins.passPanel.activeSelf)
        {
            //TODO:This Where the Bug IS Happening =)))
            // PutCardInPlace.ins.PassTheCardToWho(true);   
        }
    }
}
