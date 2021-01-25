using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
public class RandomCardGenrtor : MonoBehaviour
{
    //TODO:find Solution for removing the card and It's error 
    //TODO:Color of Button Fixing  
    private const int NumberOfPlaceText=9;
    public static RandomCardGenrtor Ins;
    public string receivedCardName;//Name Of Card That Ai Received From Player
    public string receivedCardCat;//Category Of CardThat Ai Recived From Player
    public Text[] foodCardText=  new Text[NumberOfPlaceText];
    public Text[] placeCardText= new Text[NumberOfPlaceText];
    public Text[] clothCardText= new Text[NumberOfPlaceText];
    public Text[] jobCardText  = new Text[NumberOfPlaceText];
    public Text[] moneyText    = new Text[NumberOfPlaceText];
    private List<string> cloth =new List<string>() {"dress", "jacket", "jean", "scarf", "gloves"};//name of cloth cards
    private List<string> food =new List<string>() {"coffee","frenchFires","ice cream","pizza","sandewich"};//name of food cards
    private List<string> jobs = new List<string>(){"cashier","customer","janitor","porter","seller"};//name of job cards
    private List<string> money =new List<string>() {"cash", "cheque", "coin", "credit card", "money"};//name of money cards
    private List<string> places =new List<string>() {"changing room", "elevator", "escalator", "restroom","store"};//name of place cards
    private List<string> cats = new List<string>(){"Clothe", "Food", "Job", "Place", "Money"};//name of Game Used Cats 
    public GameObject cardReceiverPanel;
   
    public void Awake()
    {
        Ins = this;
     
    }
    private void Start()
    {
      CallToRemoveSameCard();
    }
    public void CallToRemoveSameCard()
    {
        RemoveSameCard(cloth, PutCardInPlace.ins.clothCardName);
        RemoveSameCard(food, PutCardInPlace.ins.foodCardNames);
        RemoveSameCard(jobs, PutCardInPlace.ins.jobCardName);
        RemoveSameCard(money, PutCardInPlace.ins.moneyCardName);
        RemoveSameCard(places, PutCardInPlace.ins.placeCardNames);
        
    }
    private void RemoveSameCard(List<string> cardName,string[] usedCardName)
    {
        for (int i = 0; i < cardName.Count; i++)
        {
            for (int j = 0; j < usedCardName.Length; j++)
            {
                if (cardName[i].Equals(usedCardName[j]))
                {
                    cardName.RemoveAt(i);
                }
            }  
        }
    }
    public void PlayerTurns()
    {
        const int sendItToPlayer = 1;
        var random = Random.Range(0,4);
        
        if (GameManger.ins.playerTurn == 0)
        {
            random = 3;
            
        }
        if (random != sendItToPlayer)
        {   
            var chosenCategory= Random.Range(0, 5);
            receivedCardCat = cats[chosenCategory];
         
            Debug.Log("chosen category  index : " + chosenCategory);
            if (chosenCategory == 0)
            {
                if (cloth.Count ==0)
                {
                    Debug.Log("Clothe is Null");
                    PlayerTurns();
                }
                var chosenCategoryIndex = Random.Range(0, cloth.Count);
                Debug.Log(chosenCategoryIndex);
                Debug.Log(cloth.Count);
                Debug.Log(cloth[chosenCategoryIndex]);
                receivedCardName = cloth[chosenCategoryIndex];
                Debug.Log(receivedCardName);
                PutItInFrontOfPlayer();
                GameManger.ins.PlayerTurner();
                cloth.RemoveAt(chosenCategoryIndex);

            }
            else if (chosenCategory == 1)
            {
                if (food.Count==0)
                {
                    Debug.Log("Food is Null");
                    PlayerTurns();
                }
                var chosenCategoryIndex = Random.Range(0, food.Count); 
                Debug.Log(chosenCategoryIndex);
                Debug.Log(food.Count);
                Debug.Log(food[chosenCategoryIndex]);
                receivedCardName = food[chosenCategoryIndex];
             
                PutItInFrontOfPlayer();
                GameManger.ins.PlayerTurner();
                food.RemoveAt(chosenCategoryIndex);

            }
            else if (chosenCategory == 2)
            {
                if (jobs.Count == 0)
                {
                    Debug.Log("Job is Null");
                    PlayerTurns();
                }
                var chosenCategoryIndex = Random.Range(0, jobs.Count);   Debug.Log(chosenCategoryIndex);
                Debug.Log(jobs.Count);
                Debug.Log(jobs[chosenCategoryIndex]);
                receivedCardName = jobs[chosenCategoryIndex];
             
                PutItInFrontOfPlayer();
                GameManger.ins.PlayerTurner();
                jobs.RemoveAt(chosenCategoryIndex);

            }
            else if (chosenCategory == 3)
            {
                if (places.Count == 0)
                {
                    Debug.Log("Places IS null");
                    PlayerTurns();
                }
                var chosenCategoryIndex = Random.Range(0, places.Count);
                receivedCardName = places[chosenCategoryIndex];
            
                PutItInFrontOfPlayer();
                GameManger.ins.PlayerTurner();
                places.RemoveAt(chosenCategoryIndex);

            }
            else if (chosenCategory == 4)
            {
                if (money.Count == 0)
                {
                    PlayerTurns();
                }
                var chosenCategoryIndex = Random.Range(0, money.Count);
                receivedCardName = money[chosenCategoryIndex];
                PutItInFrontOfPlayer();
                GameManger.ins.PlayerTurner();
                money.RemoveAt(chosenCategoryIndex);

            }
            else
            {
                Debug.LogError("Invalid Index");
            }
           
        }
        else if (random==sendItToPlayer)
        {
            StartCoroutine(ActiveCardReceiverPanel());
        }
        if (GameManger.ins.playerTurn == 1)
        {
            GameManger.ins.numberOfCardInFrontPlayers[0]--;
        }
        else if  (GameManger.ins.playerTurn == 2)
        {
            GameManger.ins.numberOfCardInFrontPlayers[1]--;
        }
        else if (GameManger.ins.playerTurn == 3)
        {
            GameManger.ins.numberOfCardInFrontPlayers[2]--;
        }         
    }
    public void PutItInFrontOfPlayer(bool dolphin=false,bool timout=false)
    {
        const int acceptTheCard=1;
        
        var playerDecision = Random.Range(0,2);
        if (dolphin)
        {
            playerDecision = acceptTheCard;
            receivedCardCat = cats[Random.Range(0, cats.Count)];
        }

        if (timout)
        {
            playerDecision = 2;
        }
     
        if (playerDecision==acceptTheCard)
        {
            switch (receivedCardCat)
            {
                case "Clothe":
                    PutTheCardsInEmptyPlaces(clothCardText, receivedCardName);
                    break;
                case "Food":
                    PutTheCardsInEmptyPlaces(foodCardText, receivedCardName);
                    break;
                case "Job":
                    PutTheCardsInEmptyPlaces(jobCardText, receivedCardName);
                    break;
                case "Place":
                    PutTheCardsInEmptyPlaces(placeCardText, receivedCardName);
                    break;
                case "Money":
                    PutTheCardsInEmptyPlaces(moneyText, receivedCardName);
                    break;
                default:
                    Debug.LogError("UnValid Card Category");
                    break;
            }
        }
        else//Pas The Card To OtherPlayer
        {
            //Note : may be it happened to work 
            Debug.Log("PlayerDontAccept This Card ");
           // RecivedPanel.ins.cardName = receivedCardName;
           // RecivedPanel.ins.cardCategory = receivedCardCat;
           // RecivedPanel.ins.PutItInFrontOfYou();
           PlayerTurns();
        }
    }
    private void PutTheCardsInEmptyPlaces(Text[] arry, string text)
    {
        l1:
        var random = Random.Range(1, 9);
        if (String.IsNullOrEmpty(arry[random].text))
        {
            GameOverChecker(arry);
            StartCoroutine(PutIt(arry, text, random));
        }
        else
        {
            goto l1;
        }
            
    }
    private IEnumerator  PutIt(Text[]arry,string text,int index)
    {
        yield return  new WaitForSeconds(1.2f);
        arry[index].text = text;
        PlayerTurns();
    }
    private void GameOverChecker(Text[]arry)//
    {
        int playerindex;
        int counter1=0;
        int counter2=0;
        int counter3=0;
        
        for (int i = 0; i < 3; i++)
        {
            
            if (String.IsNullOrEmpty(arry[i].text)==false)
            {
                counter1++;
                Debug.Log(counter1);
                if (counter1 == 3)
                {
                    playerindex = 0;
                    GameManger.ins.OtherPlayersGameOver(playerindex);
                }
            }
           
        }

        for (int i = 3; i < 6; i++)
        {
            if (String.IsNullOrEmpty(arry[i].text)==false)
            {
                counter2++;
                Debug.Log(counter2);

                if (counter2 == 3)
                {
                   

                    playerindex = 1;
                    GameManger.ins.OtherPlayersGameOver(playerindex);
                }
            }  
        }
        for (int i = 6; i < 9; i++)
        {
            
            if (String.IsNullOrEmpty(arry[i].text)==false)
            {
                counter3++;
                Debug.Log(counter3);

                if (counter3 == 3)
                {   playerindex = 2;
                    GameManger.ins.OtherPlayersGameOver(playerindex);
                }

            } 
        }
        
    }
    private IEnumerator ActiveCardReceiverPanel()
    {
        yield return new WaitForSeconds(1.2f);
        if (PutCardInPlace.ins.sendCardActionPanelWork == false)
        {
            cardReceiverPanel.SetActive(true);
            RecivedPanel.ins.InitiateCardReceiver();
        }

    }
   
    

 
}
