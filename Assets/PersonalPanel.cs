using System.Collections;
using System.Collections.Generic;
using FiroozehGameService.Core;
using UnityEngine;
using UnityEngine.UI;

public class PersonalPanel : MonoBehaviour
{
    public Text playerName;
    public Text playerRank;
    public Image[] category=new Image[5];
    // Start is called before the first frame update
    async void  Start()
    {
        if (GameService.IsAuthenticated())
        {
            var user = await  GameService.GetCurrentPlayer();
            playerName.text ="User Name: " +user.Name;
            playerRank.text = "User Rand: 1";
        }

        var data = GameSaveSystem.Instans.LoadData();
        Debug.Log(data.CardsCatName[0]);
        for (int i = 0; i < data.CardsCatName.Count; i++)
        {
            category[i].gameObject.SetActive(true);
            category[i].transform.GetChild(0).GetComponent<Text>().text = data.CardsCatName[i];
        }
        
    }
    
    // Update is called once per frame
  
}
