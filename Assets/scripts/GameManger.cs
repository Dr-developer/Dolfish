using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManger : MonoBehaviour
{
    public static GameManger ins;
    public List<int> listOfPlayed = new List<int>(3);
    public GameObject humanPlayer;
    public GameObject gameOverPanel;
    public GameObject[] otherPlayersGameOverPanel = new GameObject[3];
    public GameObject winPanel;
    private int _playerWinCounter;
    public List<int> listOfGameOverPlayers = new List<int>(3);
    [FormerlySerializedAs("numberOFcardInForntPlayers")]
    public int[] numberOfCardInFrontPlayers;
    public Image[] player2Lights;
    public Image[] player3Lights;
    public Image[] player4Lights;
    public Image[] playerGreenLight;
    public GameObject[] categoryLightGameObjects;
    public GameObject[] playerSendButton;
    public GameObject[] playerReceiverPanelSendButton;


    public GameObject hidePanel;

    public void Awake()
    {
        if (ins == null) ins = this;
    }

    void Start()
    {
        numberOfCardInFrontPlayers = new int[3];
        for (int i = 0; i < numberOfCardInFrontPlayers.Length; i++)
        {
            numberOfCardInFrontPlayers[i] = 9;
        }

        _playerWinCounter = 0;
    }

  

    public void TurnOfTheCardLights(int index )
    {
        int i = numberOfCardInFrontPlayers[index - 2];
        if (i <= 0)
        {
            OtherPlayersGameOver(index - 2);
        }
        else
        {
            switch (index)
            {
                case 2:
                    for (i = numberOfCardInFrontPlayers[index - 2]; i < player2Lights.Length; i++)
                    {
                        player2Lights[i].gameObject.SetActive(false);
                    }

                    break;
                case 3:
                    for (i = numberOfCardInFrontPlayers[index - 2]; i < player3Lights.Length; i++)
                    {
                        player3Lights[i].gameObject.SetActive(false);
                    }

                    break;
                case 4:
                    for (i = numberOfCardInFrontPlayers[index - 2]; i < player4Lights.Length; i++)
                    {
                        player4Lights[i].gameObject.SetActive(false);
                    }

                    break;
                default:
                    Debug.LogError("Player Index Does Not Exsite ");
                    break;
                
            }
        }
       
        
    }
    public void GameOver()
    {
        gameOverPanel.SetActive(true);
    }

    public void OtherPlayersGameOver(int index)
    {
        playerSendButton[index].SetActive(false);
        playerReceiverPanelSendButton[index].SetActive(false);
        listOfGameOverPlayers.Add(index+2);
        otherPlayersGameOverPanel[index].SetActive(true);
        _playerWinCounter++;
        Debug.Log("nubmers of Lost players : "+_playerWinCounter);
        if (_playerWinCounter == 2)
        {
            GameWinnerAndLooserConditions(index);
        }
        StartCoroutine(RandomCardGenrtor.Ins.NoneHumanPlaying(index+1));
    }

    public void GameWinnerAndLooserConditions( int index)
    {
        
        if (PutCardInPlace.ins.GetNumberOfCardsOfPlayer() > numberOfCardInFrontPlayers[index])
        {
            StopAllCoroutines();
            Win();
        }
        else
        {
            StopAllCoroutines();
            GameOver();
        }
    }

    public void Win()
    {
        StopAllCoroutines();
        winPanel.SetActive(true);
    }

    public GameObject GetLastPlayer()
    {
        return humanPlayer;
    }

    public void PlayerTurner()
    {
    }

    public void GoHome()
    {
        SceneManager.LoadScene(1);
    }

    public  void FindingThePlayingCharacter(int playerNumber, string category, string senderCategory,
        string cardName, int lastPlayerNumber)
    {
        Debug.Log("FindingPlaying Charater ");
        
        if (RecivedPanel.ins.cardReceiverPanel.activeSelf == false|| PutCardInPlace.ins.hidePanel.activeSelf)
        {
            
          
            bool bluff = category != senderCategory; //BUG:This place may have bug 
            if (playerNumber == 2)
            {
                DisableGreenLight();
                playerGreenLight[playerNumber - 2].gameObject.SetActive(true);
                ActiveTheCategoryHollowed(senderCategory);
                StartCoroutine(RandomCardGenrtor.Ins.PlayerIsPlaying(playerNumber, bluff, senderCategory, category,
                    cardName));

            }
            else if (playerNumber == 3)
            {
                DisableGreenLight();
                playerGreenLight[playerNumber - 2].gameObject.SetActive(true);
                ActiveTheCategoryHollowed(senderCategory);
                StartCoroutine(RandomCardGenrtor.Ins.PlayerIsPlaying(playerNumber, bluff, senderCategory, category,
                    cardName));
            }
            else if (playerNumber == 4)
            {
                DisableGreenLight();
                playerGreenLight[playerNumber - 2].gameObject.SetActive(true);
                ActiveTheCategoryHollowed(senderCategory);
                StartCoroutine(RandomCardGenrtor.Ins.PlayerIsPlaying(playerNumber, bluff, senderCategory, category,
                    cardName));

            }
            else if (playerNumber == 1)
            {
                DisableGreenLight();
                ActiveTheCategoryHollowed(senderCategory);
                StopPlaying();
                RandomCardGenrtor.Ins.StopPlayings();
                RecivedPanel.ins.InitiateCardReceiver(playerNumber, category, senderCategory, cardName, bluff,
                    lastPlayerNumber);

            }
        }
    }

    public void DisableGreenLight()
    {
        for (int i = 0; i < playerGreenLight.Length; i++)
        {
            playerGreenLight[i].gameObject.SetActive(false);
        }
    }

    private void ActiveTheCategoryHollowed(string senderCategory)
    {
        foreach (var light in categoryLightGameObjects)
        {
            light.SetActive(false);
        }

        foreach (var light in categoryLightGameObjects)
        {
            if (light.name == senderCategory)
            {
                light.SetActive(true);
            }
        }
    }

    public void DeactivateTheCategoryHollowed()
    {
        foreach (var light in categoryLightGameObjects)
        {
            light.SetActive(false);
        }
    }

    public void DeActivePlayersPanel()
    {
        hidePanel.SetActive(true);
        PutCardInPlace.ins.passPanel.SetActive(false);
        PutCardInPlace.ins.playerTimer.SetActive(false);
    }

    private async Task WaitOneSecondAsync()
    {
        int randomWaite = Random.Range(3, 7);
        await Task.Delay(TimeSpan.FromSeconds(randomWaite));
    }

    public void StopPlaying()
    {
        StopAllCoroutines();
    }
    // BUG: When You pass The Card  A Problem Happening 

}
