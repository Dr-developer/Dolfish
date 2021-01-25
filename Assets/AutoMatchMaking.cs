﻿using System.Collections;
using System.Collections.Generic;
using FiroozehGameService.Core;
using FiroozehGameService.Core.GSLive;
using FiroozehGameService.Handlers;
using FiroozehGameService.Models.Enums.GSLive;
using FiroozehGameService.Models.GSLive.Command;
using UnityEngine;
using UnityEngine.UI;

public class AutoMatchMaking  : MonoBehaviour
{
    // Start is called before the first frame update
    public Text joinedPlayersName;
    void Start()
    {
        if (GameService.IsAuthenticated())//Check That Player IS connect To Internet and Game Service 
        {
            SetHandler();
        }
        else
        {
            Debug.LogError("You Don't Log in The Game Service'");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetHandler()
    {
        TurnBasedEventHandlers.AutoMatchUpdated += AutoMatchUpdate;
        TurnBasedEventHandlers.JoinedRoom += JoinedRoom;

    }

    private void JoinedRoom(object sender, JoinEvent joinEvent)
    {
         
    }

    private void AutoMatchUpdate(object sender, AutoMatchEvent matchEvent)
    {
        if (matchEvent.Status == AutoMatchStatus.OnWaiting)
        {
            foreach (var VARIABLE in matchEvent.Players)
            {
                joinedPlayersName.text = VARIABLE.Name+"\n";
            }
        }
    }
    public async void OnPlayOnline()
    {
      await  GameService.GSLive.TurnBased.AutoMatch(new GSLiveOption.AutoMatchOption(
            "player", 4, 4, false));
    }
}