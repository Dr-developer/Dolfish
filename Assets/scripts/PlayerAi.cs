using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlayerAi : MonoBehaviour
{
    private  string[] _categoryName = new string [] {"ActionCards", "Clothe", "Food", "Jobs", "Money", "Places"};
    public int playerID;
    public GameObject[] players;
    public List<GameObject> playersCards;
    public string playerName;
    public GameObject recivedCard;
    public string recivedCardCat;
    public string sendCat;
    public string revicidCardName;
    private GameObject _choisedCard;   
    public  PlayerAi[] _ai=new PlayerAi[3];
    
    public Text[] ActionCardText=new Text[3];

    public Text[] FoodCardText=new Text[3];

    public Text[] placeCardText=new Text[3];
    
    public Text[] clothCardText=new Text[3];
    
    public Text[] jobCardText  =new Text[3];
    
    public Text[] MoneyText    =new Text[3];
    // Start is called before the first frame update
   

    public void AcceptTheCard()
    {
        int dection = Random.Range(-1, 1);
        if (dection == 0)
        {
            Debug.Log("True Word");

            AcceptThePLayerWords();
        }
        else
        {
            Debug.Log("WrongWord");
            SusTheCard();
        }
    }

    public void Start()
    {
        
    }

    public void SusTheCard()
    {
        if (sendCat != recivedCardCat)
        {   Debug.Log("put the card in front of player");
            GameManger.ins.GetLastPlayer().GetComponent<PutCardInPlace>().recivedCardCat = recivedCardCat;
            GameManger.ins.GetLastPlayer().GetComponent<PutCardInPlace>().PutItInFrontOFPlayer();
         
        }
        else
        {     Debug.Log("put the card in players card");
            PutItInFrontOFPlayer();
        }
    }

    public void AcceptThePLayerWords()
    {
        if (sendCat == recivedCardCat)
        {
            GameManger.ins.GetLastPlayer().GetComponent<PlayerAi>().PutItInFrontOFPlayer();
            Debug.Log("put the card in players card");
        }
        else
        {
            PutItInFrontOFPlayer();
            Debug.Log("put the card in front of player");
        }
    }

    public void  choiseACard()
    {
        _choisedCard = playersCards[Random.Range(0, playersCards.Count)];
        string category = _choisedCard.GetComponent<category>().cat.Name;
        Sprite cardImage = _choisedCard.GetComponent<SpriteRenderer>().sprite;
        //select What To to the Cards

    }

    public void CardReciver()//call when other players want to pass thhe Card
    {  Debug.Log("CardReciver");
       int choiseWhatHappend=Random.Range(-1, 1);
       if (choiseWhatHappend == 0)
       {
           Debug.Log("Card_Acepted");
           AcceptTheCard();
           
       }
       else
       {
           Debug.Log("Card_Sended");
           //AcceptTheCard();
          SendCard();

       }
    }

    public void SendCard()//Send the card to other Players 
    {Debug.Log("send");
        int counter=0;
        l2:
        int playerIndex = Random.Range(0, 2);
        if (!GameManger.ins.listOfPlayed.Contains(playerIndex))
        {
            _ai[playerIndex].recivedCard = recivedCard;
            _ai[playerIndex].recivedCardCat = recivedCardCat;
            _ai[playerIndex].sendCat = sendCat;
            _ai[playerIndex].revicidCardName = revicidCardName;
            _ai[playerIndex].CardReciver();
        }
        else
        {
            counter++;
            if (counter <= 2)
            {
                goto l2;
            }
            else
            {
                AcceptTheCard();
            }
        }
        
    }

    public void PutItInFrontOFPlayer()
    {
        switch (recivedCardCat)
        {
            case  "ActionCards":
              putTheCardsInEmpetyPlacese(ActionCardText,revicidCardName);
                break;
            
            case "Cloth":
                putTheCardsInEmpetyPlacese(clothCardText,revicidCardName);

                break;
            
            case "Food":
                putTheCardsInEmpetyPlacese(FoodCardText,revicidCardName);

                break;
            
            case "Job":
                putTheCardsInEmpetyPlacese(jobCardText,revicidCardName);

                break;
            
            case "Place":
                putTheCardsInEmpetyPlacese(placeCardText,recivedCardCat);
               break;
            
            case "Money":
                putTheCardsInEmpetyPlacese(MoneyText,revicidCardName);

                break;
            
            default:
                Debug.LogError("UnValid Card Category");
                break;
                
        }
    }

    public void putTheCardsInEmpetyPlacese(Text[] arry, string text)
    {
    

        for (int i = 0; i < arry.Length; i++)
        {
            if (arry[i] == null)
            {
                arry[i].text = text;
                break;
                
            }
        }
        
    }
    
        

}
