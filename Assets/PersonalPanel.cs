using System.Collections;
using System.Collections.Generic;
using FiroozehGameService.Core;
using UnityEngine;
using UnityEngine.UI;

public class PersonalPanel : MonoBehaviour
{
    public Text playerName;

    public Text playerRank;
    // Start is called before the first frame update
    async void  Start()
    {
        if (GameService.IsAuthenticated())
        {
            var user = await  GameService.GetCurrentPlayer();
            playerName.text ="User Name: " +user.Name;
            playerRank.text = "User Rand: 1";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
