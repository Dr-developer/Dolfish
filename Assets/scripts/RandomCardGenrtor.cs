using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DeadMosquito.AndroidGoodies;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using Random = UnityEngine.Random;
public class RandomCardGenrtor : MonoBehaviour
{
    //TODO:find Solution for removing the card and It's error 
    //TODO:Color of Button Fixing  
    private const int NumberOfPlaceText=9;
    public static RandomCardGenrtor Ins;
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
    public List<int> playersNamesIndex;
    
    public GameObject[] hollowes;
    
    private IEnumerator _noneHumanPlaying;

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
  
    public void PutItInFrontOfNoneHumanPlayer(string cardCategory, string cardName,int localPlayersIndex ,bool dolphin=false,bool timout=false)
    {
      //  const int acceptTheCard=1;
        
       // var playerDecision = Random.Range(0,2);
       // if (dolphin)
      //  {
        //    playerDecision = acceptTheCard;
      //      receivedCardCat = cats[Random.Range(0, cats.Count)];
       // }

      //  if (timout)
      //  {
        //    playerDecision = 2;
        //}
     
      //  if (playerDecision==acceptTheCard)
       // {
            switch (cardCategory)
            {
                case "Clothe":
                    PutTheCardsInEmptyPlaces(clothCardText, cardName,localPlayersIndex);
                    break;
                case "Food":
                    PutTheCardsInEmptyPlaces(foodCardText, cardName,localPlayersIndex);
                    break;
                case "Job":
                    PutTheCardsInEmptyPlaces(jobCardText, cardName,localPlayersIndex);
                    break;
                case "Place":
                    PutTheCardsInEmptyPlaces(placeCardText, cardName,localPlayersIndex);
                    break;
                case "Money":
                    PutTheCardsInEmptyPlaces(moneyText, cardName,localPlayersIndex);
                    break;
                default:
                    Debug.LogError("UnValid Card Category");
                    break;
            }
      //  }
      //  else//Pas The Card To OtherPlayer
       // {
            //Note : may be it happened to work 
            //Debug.Log("PlayerDontAccept This Card ");
            // RecivedPanel.ins.cardName = receivedCardName;//BUG : look for here
            //  RecivedPanel.ins.cardCategory = receivedCardCat;//BUG: look for here 
           //  RecivedPanel.ins.PutItInFrontOfYou();
         //  PlayerTurns();
       // }
    }
    private void PutTheCardsInEmptyPlaces(Text[] array, string text, int playerIndex)
    {
        if (playerIndex == 2)
           FindEmptyPlace(array,text,playerIndex);
        else if (playerIndex == 3)
           FindEmptyPlace(array,text,playerIndex);
        else if (playerIndex == 4) FindEmptyPlace(array,text,playerIndex);
    }

    private void FindEmptyPlace(Text[] array, string text, int index)
    {
        if (index == 2)
        {
            for (int i = 0; i < 3; i++)
                if (string.IsNullOrEmpty(array[i].text))
                {
                    GameOverChecker(array);
                    PutIt(array, text, i, index);
                    break;

                }
        }
        else if (index == 3)
        {
            for (var i = 3; i < 6; i++)
                if (string.IsNullOrEmpty(array[i].text))
                {
                    GameOverChecker(array);
                    PutIt(array, text, i, index);
                    break;

                }
        }
        else if (index == 4)
        {
            for (var i = 6; i < 9; i++)
                if (string.IsNullOrEmpty(array[i].text))
                {
                    GameOverChecker(array);
                    PutIt(array, text, i, index);
                    break;
                }
        }
    }
    private void  PutIt(Text[]arry,string text,int index, int playerIndex)
    {
        arry[index].text = text;
        StartCoroutine(NoneHumanPlaying(playerIndex));

    }
    private void GameOverChecker(Text[]arry)//
    {
        int playerindex;
        int counter1=0;
        int counter2=0;
        int counter3=0;
        
        for (int i = 0; i < 3; i++)
        {
            if (!String.IsNullOrEmpty(arry[i].text))
            {
                counter1++;
                if (counter1 == 3)
                {
                    playerindex = 0;
                    Debug.Log("Player " + playerindex + 2 + "is Game overed ");
                    GameManger.ins.OtherPlayersGameOver(playerindex);
                }
             
            }
        }

        for (int i = 3; i < 6; i++)
        {
            if (!String.IsNullOrEmpty(arry[i].text))
            {
                counter2++;
                if (counter2 == 3)
                {
                    playerindex = 1;
                    Debug.Log("Player " + playerindex + 2 + "is Game overed ");
                    GameManger.ins.OtherPlayersGameOver(playerindex);
                }
             
            }
        }

        for (int i = 6; i < 9; i++)
        {
            if (!String.IsNullOrEmpty(arry[i].text) )
            {
                counter3++;
                if (counter3 == 3)
                {
                    playerindex = 2;
                    Debug.Log("Player " + playerindex + 2 + "is Game overed ");
                    GameManger.ins.OtherPlayersGameOver(playerindex);
                }
               
            } 
        }
        
    }
    private async void  ActiveCardReceiverPanel()
    {
        await WaitOneSecondAsync();
        if (PutCardInPlace.ins.sendCardActionPanelWork == false)
         cardReceiverPanel.SetActive(true);
        // RecivedPanel.ins.InitiateCardReceiver();//BUG:look for here 

    }
    private async Task WaitOneSecondAsync()
    {
       int  randomWaite= Random.Range(3,8);
        await Task.Delay(TimeSpan.FromSeconds(randomWaite));
    }

    public IEnumerator   PlayerIsPlaying(int playerName , bool bluff,string senderCategory, string category ,string cardName )
    {
        playersNamesIndex.Add(playerName);

        const int pass = 1;
        const int playingTheGame = 0;
        var randomDecision = Random.Range(0, 2);
        
        if (randomDecision == pass)
        {
            if (PassToWho() != -1)
            {
                yield return new WaitForSeconds(2);
                var senderCategoryRandom = cats[Random.Range(0, cats.Count)];
                GameManger.ins.FindingThePlayingCharacter(PassToWho(), category, senderCategoryRandom, cardName,
                    playerName);
            }
            else
            {
                yield return new WaitForSeconds(2);
                StartCoroutine(PlayingTheGame(bluff, category, cardName, playerName));
            }
        }
        else if (randomDecision == playingTheGame)
        {
         
            StartCoroutine(PlayingTheGame(bluff, category, cardName, playerName));
        }
        
      
    }

    private int PassToWho()
    {
        
        var playerName = Random.Range(1, 5);
        if (playersNamesIndex.Contains(playerName)|| GameManger.ins.listOfGameOverPlayers.Contains(playerName+1))
        {
            if (playersNamesIndex.Count == 4) return -1;
          
            if (GameManger.ins.listOfGameOverPlayers.Count ==1)
            {
                if (GameManger.ins.listOfGameOverPlayers[0] == 2)
                {
                    return 3;
                }

                if (GameManger.ins.listOfGameOverPlayers[0] == 3)
                {
                    return 4;
                }

                if (GameManger.ins.listOfGameOverPlayers[0] == 4)
                {
                    return 3; 
                }
            }
            else   if (GameManger.ins.listOfGameOverPlayers.Count == 2)
            {
                Debug.Log("Fuck YOU");
                GameManger.ins.Win();
            }
            else
            {
                return PassToWho();
            }
            
           
        }
      
      
        return playerName;
    }

    private IEnumerator  PlayingTheGame(bool bluff,string category, string cardName,int playerIndex)
    {
        if (PutCardInPlace.ins.hidePanel.activeSelf || playerIndex != 0)
        {
            const int no = 0;
            const int yes = 1;

            var randomDecision = Random.Range(0, 2);

            if (bluff && randomDecision == yes)
            {
                yield return new WaitForSeconds(3);
                PutItInFrontOfNoneHumanPlayer(category, cardName, playerIndex);
                StartCoroutine(NoneHumanPlaying(playerIndex));
                playersNamesIndex = new List<int>();
            }
            else if (bluff && randomDecision == no)
            {
                if (playerIndex == 1)
                {
                    yield return new WaitForSeconds(3);
                    RecivedPanel.ins.PutItInFrontOfYou(category, cardName);
                    PutCardInPlace.ins.HumanTurn();
                    GameManger.ins.DisableGreenLight();
                    GameManger.ins.DeactivateTheCategoryHollowed();
                    playersNamesIndex = new List<int>();
                }
                else
                {
                    PutItInFrontOfNoneHumanPlayer(category, cardName, playerIndex);
                    StartCoroutine(NoneHumanPlaying(playerIndex));
                    playersNamesIndex = new List<int>();
                }

            }
            else if (bluff == false && randomDecision == yes)
            {
                if (playerIndex == 1)
                {
                    yield return new WaitForSeconds(3);
                    RecivedPanel.ins.PutItInFrontOfYou(category, cardName);
                    PutCardInPlace.ins.HumanTurn();
                    GameManger.ins.DisableGreenLight();
                    GameManger.ins.DeactivateTheCategoryHollowed();
                    playersNamesIndex = new List<int>();
                }
                else
                {
                    PutItInFrontOfNoneHumanPlayer(category, cardName, playerIndex);
                    StartCoroutine(NoneHumanPlaying(playerIndex));
                    playersNamesIndex = new List<int>();
                }



            }
            else if (bluff == false && randomDecision == no)
            {
                yield return new WaitForSeconds(3);
                PutItInFrontOfNoneHumanPlayer(category, cardName, playerIndex);
                StartCoroutine(NoneHumanPlaying(playerIndex));
                playersNamesIndex = new List<int>();


            }
        }
    }
//Witch None Human PlayTheGame 
    public IEnumerator  NoneHumanPlaying(int playerIndex)
    {
        if (PutCardInPlace.ins.hidePanel.activeSelf|| playerIndex!=0 )
        {
            Debug.Log(playerIndex+"Is Playing ");
        GameManger.ins.DisableGreenLight();
        GameManger.ins.DeactivateTheCategoryHollowed();
        TurnOfCardsLights(playerIndex);
        TurnOnTheHollow(playerIndex);
        List<int> localPlayersIndex=new List<int>(){1,2,3,4};
        localPlayersIndex.Remove(playerIndex);
        var chosenCategory = Random.Range(0, 5); 
        string cardCategory = cats[chosenCategory];
        var choiceSenderCategory = Random.Range(0, 5);
        string senderCategory=cats[choiceSenderCategory];
        int receiverPlayerIndex = localPlayersIndex[Random.Range(0, localPlayersIndex.Count)]; 
        string cardName;
        /*if (cardCategory == cats[0])
        {
            if (cloth.Count == 0)  StartCoroutine(NoneHumanPlaying(playerIndex));
            var chosenCategoryIndex = Random.Range(0, cloth.Count);
            cardName = cloth[chosenCategoryIndex];
            cloth.RemoveAt(chosenCategoryIndex);
            playersNamesIndex.Add(playerIndex);
            yield return new WaitForSeconds(Random.Range(5,15));
            GameManger.ins.FindingThePlayingCharacter(receiverPlayerIndex, cardCategory, senderCategory, cardName,playerIndex);


        }
        else if (cardCategory == cats[1])
        {
            if (food.Count == 0) StartCoroutine(NoneHumanPlaying(playerIndex));
            var chosenCategoryIndex = Random.Range(0, food.Count);
            cardName = food[chosenCategoryIndex];
            food.RemoveAt(chosenCategoryIndex);
            playersNamesIndex.Add(playerIndex);
            yield return new WaitForSeconds(Random.Range(5,15));
            GameManger.ins.FindingThePlayingCharacter(receiverPlayerIndex, cardCategory, senderCategory, cardName,playerIndex);

            
        }
        else if (cardCategory == cats[2])
        {
            if (jobs.Count == 0)  StartCoroutine(NoneHumanPlaying(playerIndex));
            var chosenCategoryIndex = Random.Range(0, jobs.Count);
            cardName = jobs[chosenCategoryIndex];
            jobs.RemoveAt(chosenCategoryIndex);
            playersNamesIndex.Add(playerIndex);
            yield return new WaitForSeconds(Random.Range(5,15));

            GameManger.ins.FindingThePlayingCharacter(receiverPlayerIndex, cardCategory, senderCategory, cardName,playerIndex);

        }
        else if (cardCategory == cats[3])
        {
            if (places.Count == 0) StartCoroutine(NoneHumanPlaying(playerIndex));
            var chosenCategoryIndex = Random.Range(0, places.Count);
            cardName = places[chosenCategoryIndex];
            places.RemoveAt(chosenCategoryIndex);
            playersNamesIndex.Add(playerIndex);
            yield return new WaitForSeconds(Random.Range(5,15));

            GameManger.ins.FindingThePlayingCharacter(receiverPlayerIndex, cardCategory, senderCategory, cardName,playerIndex);

        }
        else if (cardCategory == cats[4])
        {
            if (money.Count == 0)  StartCoroutine(NoneHumanPlaying(playerIndex));
            var chosenCategoryIndex = Random.Range(0, money.Count);
            cardName = money[chosenCategoryIndex];
            money.RemoveAt(chosenCategoryIndex);
            playersNamesIndex.Add(playerIndex);
            yield return new WaitForSeconds(Random.Range(5,15));
            GameManger.ins.FindingThePlayingCharacter(receiverPlayerIndex, cardCategory, senderCategory, cardName,playerIndex);

        }*/
          cardName=PreparationForNoneHumanPlaying(cardCategory);
          if (cardName == "0")
          {
              Debug.LogError("Fuck You ");
          }
          playersNamesIndex.Add(playerIndex);
          yield return new WaitForSeconds(Random.Range(5,15));
          GameManger.ins.FindingThePlayingCharacter(receiverPlayerIndex, cardCategory, senderCategory, cardName,playerIndex);


        }
      
    }

    private string PreparationForNoneHumanPlaying(string cardCategory)
    {
        int t=  cats.FindIndex(x => x.StartsWith(cardCategory));
        
        string cardName = "0";
        if (cardCategory == cats[0])
        {
            if (cloth.Count == 0)
            {
                cats.Remove("Clothe");
                PreparationForNoneHumanPlaying(cats[1]);
            }

            var chosenCategoryIndex = Random.Range(0, cloth.Count);
            cardName = cloth[chosenCategoryIndex];
            cloth.RemoveAt(chosenCategoryIndex);
        }
        else if (cardCategory == cats[1])
        {
            if (food.Count == 0) PreparationForNoneHumanPlaying(cats[2]);
            var chosenCategoryIndex = Random.Range(0, food.Count);
            cardName = food[chosenCategoryIndex];
            food.RemoveAt(chosenCategoryIndex);
        }
        else if (cardCategory == cats[2])
        {
            if (jobs.Count == 0) PreparationForNoneHumanPlaying(cats[3]);
            var chosenCategoryIndex = Random.Range(0, jobs.Count);
            cardName = jobs[chosenCategoryIndex];
            jobs.RemoveAt(chosenCategoryIndex);
        }
        else if (cardCategory == cats[3])
        {
            if (places.Count == 0) PreparationForNoneHumanPlaying(cats[4]);
            var chosenCategoryIndex = Random.Range(0, places.Count);
            cardName = places[chosenCategoryIndex];
            places.RemoveAt(chosenCategoryIndex);
        }
        else if (cardCategory == cats[4])
        {
            if (money.Count == 0) PreparationForNoneHumanPlaying(cats[1]);
            var chosenCategoryIndex = Random.Range(0, money.Count);
            cardName = money[chosenCategoryIndex];
            money.RemoveAt(chosenCategoryIndex);
        }

        return cardName;
    }

    public List<int> GetPlayerNamesIndex()
    {
        return playersNamesIndex;
    }

    public void SetPlayerNameIndex(int index)
    {
        if (playersNamesIndex.Contains(index) == false)
        {
            playersNamesIndex.Add(index);
        }
    }
    private void TurnOfCardsLights(int playerIndex)
    {
        if(playerIndex==2)GameManger.ins.numberOfCardInFrontPlayers[0]--;
        if(playerIndex==3)GameManger.ins.numberOfCardInFrontPlayers[1]--;
        if(playerIndex==4)GameManger.ins.numberOfCardInFrontPlayers[2]--;
        GameManger.ins.TurnOfTheCardLights(playerIndex);
    }

    private void  TurnOnTheHollow(int playerIndex)
    {
        for (int i = 0; i < hollowes.Length; i++)
        {
            hollowes[i].gameObject.SetActive(false);
        }

        PutCardInPlace.ins.playerOneHollow.SetActive(false);

        hollowes[playerIndex - 2].SetActive(true);

    }

    public void StopPlayings()
    {
        StopAllCoroutines();
    }
    //TODO:
}
