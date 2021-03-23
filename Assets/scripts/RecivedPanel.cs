using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
public class RecivedPanel : MonoBehaviour
{
    //TODO:Button Colors Button Fixing 
    //Public Variables
    public static RecivedPanel ins; //Create a Instans from this class
    public const int PlayerOneIndex=1;
    
    public Sprite cardBack;
    public Sprite[] cardImages; //Image Of All Card Must Put In This Array So We Can Create Random Image Send To Players
    public Text[] foodCardText = new Text[3];
    public Text[] placeCardText = new Text[3];
    public Text[] clothCardText = new Text[3];
    public Text[] jobCardText = new Text[3];
    public Text[] moneyText = new Text[3];
    public GameObject cardReceiverPanel;
    public Button[] playerButtons;
    public Toggle[] cardCategoryButton;
    public Image card;
    public Image[] cardCategoryImages;
    public GameObject[] playerHollow;
    public GameObject passPanel;
    public GameObject acceptsPanel;
    public GameObject receiverPanel;
    //Private Variables 
    private int _imageIndex;
    private bool _playerSaidLie; //Changed 
    private Image _cardTemp;
    private int _lastPlayerIndex;
    private int _playerIndex;
    private string _cardName;
    private string _cardCategory;
    private string _senderCategory;
    
    // Start is called before the first frame update
    private void Awake()
    {
        ins = this;
    }
   //Initiate the Card Receiver Stution 
    public  void InitiateCardReceiver(int playerNumber,string category,string senderCategory, string cardName,bool bluff,int lastPlayerIndex)
    {
        
        if (PutCardInPlace.ins.hidePanel.activeSelf)
        {
            _cardName = cardName;
            _cardCategory = category;
            _senderCategory = senderCategory;
            _lastPlayerIndex = lastPlayerIndex;
            _playerSaidLie = bluff;
            _playerIndex = playerNumber;
            card.sprite = cardBack;
            MakeButtonWhite();
            cardReceiverPanel.SetActive(true);
            SelectButtons(senderCategory);
            PlayerButtonDeActive();
            GameManger.ins.DeActivePlayersPanel();
            acceptsPanel.SetActive(true);
            
        }

        //PutCardInPlace.ins.HideThePanel();
    }

    private void  PlayerButtonDeActive()
    {
        for (int i = 0; i < playerButtons.Length; i++)
        {
            if (RandomCardGenrtor.Ins.playersNamesIndex.Contains(i + 2))
            {
                playerButtons[i].gameObject.SetActive(false);
            }
        }
    }
    private void MakeButtonWhite()
    {
        for (var i = cardCategoryButton.Length - 1; i >= 0; i--)
        {
            cardCategoryButton[i].interactable = false;
            cardCategoryButton[i].GetComponent<Image>().color=Color.white;
        } 
        for ( int i=0;i<playerButtons.Length;i++)
        {
            if (RandomCardGenrtor.Ins.GetPlayerNamesIndex().Contains(i) == false)
            {
                playerButtons[i].interactable = false;
                playerButtons[i].GetComponent<Image>().color = Color.white;
            }
            else
            {
                playerButtons[i].gameObject.SetActive(false);
            }
        }
    }
    //Change The Color Of Button When Player Click On It
    
    //TODO:Use Toggler for this
    private void SelectButtons(string cardCategory)
    {
        switch (cardCategory)
        {
            case "Food":
                cardReceiverPanel.SetActive(true);

                cardCategoryImages[0].color=Color.green;
                
                break;
            case "Clothe":
                cardReceiverPanel.SetActive(true);

                cardCategoryImages[1].color=Color.green;
                 break;
            case "Job" :
                cardReceiverPanel.SetActive(true);

                cardCategoryImages[2].color=Color.green;
                break;
            case "Place":
                cardReceiverPanel.SetActive(true);

                cardCategoryImages[3].color=Color.green;
                break;
            case "Money":
                cardReceiverPanel.SetActive(true);
                
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
        card.sprite = FindImage();
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
        acceptsPanel.SetActive(false);
        passPanel.SetActive(true);
       
        
    
    }
    //BeLive This Players Word 
    public async void  AcceptHisWord()
    {
        if (_playerSaidLie)
        {
            PutItInFrontOfYou(_cardCategory, _cardName);
            card.sprite = FindImage();
            await WaitTwoSecondAsync();
            StartCoroutine(RandomCardGenrtor.Ins.NoneHumanPlaying(0));
            PutCardInPlace.ins.HumanTurn();
            Disable();
        }
        else
        {
            card.sprite = FindImage();
            await WaitTwoSecondAsync();
            RandomCardGenrtor.Ins.PutItInFrontOfNoneHumanPlayer(_cardCategory, _cardName, _lastPlayerIndex);
            StartCoroutine(RandomCardGenrtor.Ins.NoneHumanPlaying(_lastPlayerIndex));
            Disable();
        }

    }
    //Don't Believe this Player Word 
    public async void DontAcceptPlayerWord()
    {
        if (_playerSaidLie)
        {
            card.sprite = FindImage();
            await WaitTwoSecondAsync();
            RandomCardGenrtor.Ins.PutItInFrontOfNoneHumanPlayer(_cardCategory, _cardName, _lastPlayerIndex);
            StartCoroutine(RandomCardGenrtor.Ins.NoneHumanPlaying(_lastPlayerIndex));
            Disable();
        }
        else
        {
            PutItInFrontOfYou(_cardCategory, _cardName);
            card.sprite = FindImage();
            await WaitTwoSecondAsync();
            StartCoroutine(RandomCardGenrtor.Ins.NoneHumanPlaying(0));

            PutCardInPlace.ins.HumanTurn();
            Disable();
        } 
    }

    private Sprite FindImage()
    {
        foreach (var image in cardImages)
        {
            if (image.name == _cardName)
            {
                return image;
            }
        }
        return null;
    }
    public void PutItInFrontOfYou(string cardCategory, string cardName)
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
    private   void PutTheCardsInEmptyPlaces(Text[] arr, string text)
    {
    

        for (int i = 0; i < arr.Length; i++)
        {
            if (String.IsNullOrEmpty(arr[i].text))
            {
                arr[i].text = text;
                GameOverChecker(arr);
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
    private async void Disable()
    {
         // PutCardInPlace.ins.hidePanel.SetActive(false);
         //  PutCardInPlace.ins.passPanel.SetActive(false);
         //PutCardInPlace.ins.playerTimer.SetActive(false);
         //  PlayeTimer.ins.timeLeft = PlayeTimer.ins.maxTime;
        foreach (var variable in cardCategoryImages)
        {
            variable.color = Color.white;
        }
        card.sprite = cardBack;
        RandomCardGenrtor.Ins.StopPlayings();
        GameManger.ins.StopPlaying();
        cardReceiverPanel.SetActive(false);

    }
    public void SendToWitchPlayer(int playerNumber)
    {
        _playerIndex = playerNumber;
        if (playerNumber == 2) 
        {
              
            playerHollow[0].SetActive(true);
            playerHollow[1].SetActive(false);
            playerHollow[2].SetActive(false);
           
        }else if (playerNumber == 3)
        {
              
            playerHollow[0].SetActive(false);
            playerHollow[1].SetActive(true);
            playerHollow[2].SetActive(false);
            
        }
        else if (playerNumber == 4) {
              
            playerHollow[0].SetActive(false);
            playerHollow[1].SetActive(false);
            playerHollow[2].SetActive(true);
         
        }


    }
    public void SendCategory(string category)
    {
        _senderCategory = category;
    }

    public void PassTheCard()
    {
        RandomCardGenrtor.Ins.playersNamesIndex.Add(1);
        GameManger.ins.FindingThePlayingCharacter(_playerIndex,_cardCategory,_senderCategory,_cardName,PlayerOneIndex);
        Disable();

    }
    
    private async Task WaitOneSecondAsync()
    {
        var randomWaiting = Random.Range(3, 5);
        await Task.Delay(TimeSpan.FromSeconds(randomWaiting));
    }

    private async Task WaitTwoSecondAsync()
    {
        await Task.Delay(TimeSpan.FromSeconds(2));
    }
//BUG:It's Have Bug 
}
