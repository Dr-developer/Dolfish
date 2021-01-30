using System.Collections;
using System.Collections.Generic;
using FiroozehGameService.Core;
using FiroozehGameService.Core.GSLive;
using FiroozehGameService.Handlers;
using FiroozehGameService.Models.Enums.GSLive;
using FiroozehGameService.Models.GSLive.Command;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AutoMatchMaking  : MonoBehaviour
{
    // Start is called before the first frame update
    private string _joinedPlayersName;
    public GameObject panel;
    public Text stute;
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
        stute.text = "Game Service Not Connected ";
        panel.SetActive(false);
        SceneManager.LoadScene(0);
    }

    private void AutoMatchUpdate(object sender, AutoMatchEvent matchEvent)
    {
        
        if (matchEvent.Status == AutoMatchStatus.OnWaiting)
        {
            foreach (var VARIABLE in matchEvent.Players)
            {
                _joinedPlayersName= VARIABLE.Name+"\n";
            }
        }
        else
        {
            stute.text = "Game Service Not Connected ";
            SceneManager.LoadScene(0);
        }
    }
    public async void OnPlayOnline()
    {
        panel.SetActive(true);
        if (GameService.GSLive.IsCommandAvailable())
        {
            await GameService.GSLive.TurnBased.AutoMatch(new GSLiveOption.AutoMatchOption(
                "player", 2, 2, false));
            stute.text = "Please Waite We Are Looking For A Player "+"PlayerThatJoined The Game "+_joinedPlayersName;

        }
        else
        {
            stute.text = "Game Service Not Connected ";
            SceneManager.LoadScene(0);
        }
        
    }
}
