using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManger : MonoBehaviour
{  
    public static GameManger ins;
    public List<int> listOfPlayed=new List<int>(3);
    public GameObject humanPlayer;
    public int playerTurn=1;
    public GameObject gameOverPanel;
    public GameObject [] otherPlayersGameOverPanel=new GameObject[3];
    public GameObject winPanel;
    public int playerWinCounter;
    [FormerlySerializedAs("numberOFcardInForntPlayers")] public int[] numberOfCardInFrontPlayers;
    public Image[] playerOneLights;
    public Image[] playerTwoLights;
    public Image[] playerThreeLights;
    public Image[] playersHollows;
    public void Awake()
    {
        if(ins==null)
            ins = this;
    }
    void Start()
    {
        numberOfCardInFrontPlayers=new int[3];
        for (int i = 0; i < numberOfCardInFrontPlayers.Length; i++)
        {
            numberOfCardInFrontPlayers[i] = 16;
        }
        /* this code for turning lights off by times
         
        TurnOfTheLight(numberOFcardInForntPlayers[0],playerOneLights);
        TurnOfTheLight(numberOFcardInForntPlayers[1],playerTwoLights);
        TurnOfTheLight(numberOFcardInForntPlayers[2],playerThreeLights);
        
        */


    }
    public void GameOver()
    {
          gameOverPanel.SetActive(true);
        
    }
    public void OtherPlayersGameOver(int index)
    {
        otherPlayersGameOverPanel[index].SetActive(true);
        playerWinCounter++;
        if (playerWinCounter == 3)
        {
            Win();
        }
    }
    private void Win()
    {
        winPanel.SetActive(true);
    }
    public GameObject GetLastPlayer()
    {
        return humanPlayer;
    }
    public void PlayerTurner()
    {
        playerTurn++;
           Debug.Log("Player"+playerTurn+"Is Playing");
        if (playerTurn == 1&&otherPlayersGameOverPanel[0].activeSelf==false)
        {   
            playersHollows[0].gameObject.SetActive(true);
            playersHollows[1].gameObject.SetActive(false);
            playersHollows[2].gameObject.SetActive(false);
            TurnOfTheLight(numberOfCardInFrontPlayers[0],playerOneLights);

           //TODO:Turn On TheHollow 
        }
        else if (playerTurn == 2&&otherPlayersGameOverPanel[1].activeSelf==false)
        {
            playersHollows[0].gameObject.SetActive(false);
            playersHollows[1].gameObject.SetActive(true);
            playersHollows[2].gameObject.SetActive(false);
            TurnOfTheLight(numberOfCardInFrontPlayers[1],playerTwoLights);

        }
        else if (playerTurn == 3&&otherPlayersGameOverPanel[2].activeSelf==false)
        {   
            playersHollows[0].gameObject.SetActive(false);
            playersHollows[1].gameObject.SetActive(false);
            playersHollows[2].gameObject.SetActive(true);
            TurnOfTheLight(numberOfCardInFrontPlayers[2],playerThreeLights);
 
        }
        else if (playerTurn == 4)
        {   playersHollows[0].gameObject.SetActive(false);
            playersHollows[1].gameObject.SetActive(false);
            playersHollows[2].gameObject.SetActive(false);
            playerTurn = 0;
            Debug.Log("Now The Panel will be Removed ");
            //PutCardInPlace.ins.removeThePanel();//How To go To other player 
        }
        else
        {
            PlayerTurner();
        }
        
    }
    public void GoHome()
    {
        SceneManager.LoadScene(1);
    }
    private void TurnOfTheLight(int numberOfCard,Image[] light)
    {

        for (int i = 0; i < numberOfCard/2; i++)
        {
         light[i].gameObject.SetActive(true);   
        }

        for (int j = numberOfCard/2; j < light.Length; j++)
        {
            light[j].gameObject.SetActive(false);   

        }
        
    }
}
