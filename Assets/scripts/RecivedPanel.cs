using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
public class RecivedPanel : MonoBehaviour
{  
    //TODO:Button Colors Button Fixing 
    //Public Variables
    public static RecivedPanel ins; //Create a Instans from this class
    public string cardName;
    public string cardCategory;
    public Sprite cardBack;
    public Sprite[] cardImages;//Image Of All Card Must Put In This Array So We Can Create Random Image Send To Players
    public string[] cardImageCat;
    public Text[] foodCardText=new Text[3];
    public Text[] placeCardText=new Text[3];
    public Text[] clothCardText=new Text[3];
    public Text[] jobCardText  =new Text[3];
    public Text[] moneyText    =new Text[3];
    public GameObject cardReceiverPanel;
    public Button[] playerButtons;
    public Button[] cardCategoryButton;
    public Image card;
    public Image[] cardCategoryImages;
    public GameObject[] playerHollow;
    public GameObject hideThePanel;
    public GameObject no ;
    //Private Variables 
    private int _imageIndex;
    private bool _playerSaidLie;//Changed 
    private Image _cardTemp;

    
    // Start is called before the first frame update
    private void Awake()
    {
        ins = this;
    }
   //Initiate the Card Receiver Stution 
    public void InitiateCardReceiver()
    {  
        hideThePanel.SetActive(true);
        PutCardInPlace.ins.passPanel.SetActive(false);
        PutCardInPlace.ins.playerTimer.SetActive(false);
        no.SetActive(true);
        card.sprite = cardBack;
        var random = Random.Range(0, 25);
        _imageIndex = random;
        cardName = cardImages[random].name;
        cardCategory = cardImageCat[random];
        for (int i = 0; i < cardCategoryButton.Length; i++)
        {
            cardCategoryButton[i].interactable = false;
        } 
        for (int i = 0; i < playerButtons.Length; i++)
        {
            playerButtons[i].interactable = false;
        }
        SelectButtons(); 
    }
    //Change The Color Of Button When Player Click On It
    private void SelectButtons()
    {
        Debug.Log(cardCategory);
        switch (cardCategory)
        {
            case "Food":
                cardCategoryImages[0].color=Color.green;
                break;
            case "Clothe":
                cardCategoryImages[1].color=Color.green;
                 break;
            case "Job" :
                cardCategoryImages[2].color=Color.green;
                break;
            case "Place":
                cardCategoryImages[3].color=Color.green;
                break;
            case "Money":
                cardCategoryImages[4].color=Color.green;
                break;
            default:
                Debug.LogError("Some Thing Wrong ");
                break;
        }
     
    }
    //Pass This Card To Other Players 
    public void PassToOtherPlayers()
    {
        card.sprite = cardImages[_imageIndex];
        for (int i = 0; i <cardCategoryButton.Length; i++)
        {
            cardCategoryButton[i].interactable = true;
        }

        for (int i = 0; i < playerButtons.Length; i++)
        {
            playerButtons[i].interactable = true;
        }

        for (int i = 0; i < cardCategoryImages.Length; i++)
        {
            cardCategoryImages[i].color = Color.white;
        }
        no.SetActive(false);
        
    
    }
    //BeLive This Players Word 
    public void  AcceptHisWord()
    {
        if (_playerSaidLie)
        { 
         PutItInFrontOfYou();
         card.sprite = cardImages[_imageIndex];
        }
        else
        {   
            RandomCardGenrtor.Ins.receivedCardCat = cardCategory;
            RandomCardGenrtor.Ins.receivedCardName = cardName;
            RandomCardGenrtor.Ins.PutItInFrontOfPlayer();
            card.sprite = cardImages[_imageIndex];
        }

        StartCoroutine(Disable());
    }
    //Don't Belive this Player Word 
    public void DontAcceptPlayerWord()
    {
        StartCoroutine(Disable());
        var random = Random.Range(0, 2);
        if (_playerSaidLie)
        {
            Debug.Log("lie");
            RandomCardGenrtor.Ins.receivedCardCat = cardCategory;
            RandomCardGenrtor.Ins.receivedCardName = cardName;
            RandomCardGenrtor.Ins.PutItInFrontOfPlayer();
            card.sprite = cardImages[_imageIndex];
            
        }
        else
        {
         PutItInFrontOfYou();
         card.sprite = cardImages[_imageIndex];
        }
      


    }
    public void PutItInFrontOfYou()
    {
        Debug.Log(cardCategory);
        switch (cardCategory)
        {
           
            case "Clothe":
                PutTheCardsInEmptyPlaces(clothCardText,cardName);

                break;
            
            case "Food":
                PutTheCardsInEmptyPlaces(foodCardText,cardName);

                break;
            
            case "Job":
                PutTheCardsInEmptyPlaces(jobCardText,cardName);

                break;
            
            case "Place":
                PutTheCardsInEmptyPlaces(placeCardText,cardName);
                break;
            
            case "Money":
                PutTheCardsInEmptyPlaces(moneyText,cardName);

                break;
            
            default:
                Debug.LogError("UnValid Card Category");
                break;
                
        }
    }
    private static void PutTheCardsInEmptyPlaces(Text[] arr, string text)
    {
    

        for (int i = 0; i < arr.Length; i++)
        {
            if (String.IsNullOrEmpty(arr[i].text))
            {
                arr[i].text = text;
                GameOverChecker(arr);
                GameManger.ins.PlayerTurner();
                RandomCardGenrtor.Ins.PlayerTurns();
                break;
            }
        }
        
    }
    private static void  GameOverChecker(Text[] arr) 
    {
        int counter = 0;
        for (int i = 0; i < 3; i++)
        {
            if (!String.IsNullOrEmpty(arr[i].text))
            {
                counter++;
                if (counter == 3)
                {
                    GameManger.ins.GameOver();
                }
            }
           
        }
    }
    private IEnumerator Disable()
    {
        yield return  new WaitForSeconds(1.2f);
        if (GameManger.ins.playerTurn==0)
        {
            PutCardInPlace.ins.hidePanel.SetActive(false);
            PutCardInPlace.ins.passPanel.SetActive(false);
            PutCardInPlace.ins.playerTimer.SetActive(false);
            PlayeTimer.ins.timeLeft = PlayeTimer.ins.maxTime;

        }

        foreach (var VARIABLE in cardCategoryImages)
        {
            VARIABLE.color=Color.white;
        }
        cardReceiverPanel.SetActive(false);
        card.sprite = cardBack;

    }
    public void SendToWitchPlayer(int playerNumber)
    {
        if (playerNumber == 2)
        {
            playerHollow[0].SetActive(true);
            playerHollow[1].SetActive(false);
            playerHollow[2].SetActive(false);
            GameManger.ins.playerTurn = 0;
            GameManger.ins.PlayerTurner();
        }
        else if (playerNumber == 3)
        {
            playerHollow[0].SetActive(false);
            playerHollow[1].SetActive(false);
            playerHollow[2].SetActive(true);
            GameManger.ins.playerTurn = 1;
            GameManger.ins.PlayerTurner();
        }
        else if (playerNumber == 4)
        {
            playerHollow[0].SetActive(false);
            playerHollow[1].SetActive(true);
            playerHollow[2].SetActive(false);
            GameManger.ins.playerTurn = 2;
            GameManger.ins.PlayerTurner();

        }
    }


}
