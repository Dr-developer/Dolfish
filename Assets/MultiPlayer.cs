using System;
using System.Collections;
using System.Collections.Generic;
using FiroozehGameService.Handlers;
using FiroozehGameService.Models.Enums.GSLive;
using FiroozehGameService.Models.GSLive;
using FiroozehGameService.Models.GSLive.Command;
using FiroozehGameService.Models.GSLive.TB;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;

public class MultiPlayer : MonoBehaviour
{
    public Text Status;
    private int _whoTurn;

    private Member _me, _currentTurn, _whoIsX;
    private Member []_opponent=new Member[3];

    private List<Member> _members;
    // Start is called before the first frame update
    async void  Start()
    {
        try
        {
            SetEventHandler();
        }
        catch (Exception e)
        {
            Status.text = "Start Err : " + e.Message;
            Debug.LogError("Start Err : " + e.Message);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetEventHandler()
    {
        TurnBasedEventHandlers.SuccessfullyLogined += OnSuccessfullyLogined;
        TurnBasedEventHandlers.Error += OnError;
        TurnBasedEventHandlers.Reconnected += Reconnected;
        TurnBasedEventHandlers.JoinedRoom += OnJoinRoom;
        TurnBasedEventHandlers.Completed += OnCompleted;
        TurnBasedEventHandlers.AutoMatchUpdated += AutoMatchUpdated;
        TurnBasedEventHandlers.ChoosedNext += OnChooseNext;
        TurnBasedEventHandlers.TakeTurn += OnTakeTurn;
        TurnBasedEventHandlers.LeftRoom += OnLeaveRoom;
        TurnBasedEventHandlers.RoomMembersDetailReceived += OnRoomMembersDetailReceived;
        TurnBasedEventHandlers.CurrentTurnMemberReceived += OnCurrentTurnMember;
    }

    private void GameInit()
    {
        _whoTurn = 0;
        
    }

    public void ToWho()
    {
        //Select The PLayer That you Want To send The Card To Him 
    }
    

    private void OnCurrentTurnMember(object sender, Member e)
    {
    }

    private void OnRoomMembersDetailReceived(object sender, List<Member> e)
    {
    }

    private void OnLeaveRoom(object sender, Member e)
    {
    }

    private void OnTakeTurn(object sender, Turn e)
    {
    }

    private void OnChooseNext(object sender, Member e)
    {
    }

    private void AutoMatchUpdated(object sender, AutoMatchEvent e)
    {
    }

    private void OnCompleted(object sender, Complete e)
    {
    }

    private void OnJoinRoom(object sender, JoinEvent e)
    {
       Debug.Log(e.JoinData.JoinedMemberOrder);
    }

    private void Reconnected(object sender, ReconnectStatus e)
    {
    }

    private void OnError(object sender, ErrorEvent e)
    {
    }

    private void OnSuccessfullyLogined(object sender, EventArgs e)
    {
    }
}
