using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PutCardInPlace : MonoBehaviour
{
    //TODO:When PlayerTimer Ends Card Go in Front Of Player
    public static PutCardInPlace ins;
    public List<GameObject> cards;
    public GameObject selectedcard;
     //list of food cards
    [HideInInspector] public List<GameObject> foodCards;
    // list of cloth Card
    [HideInInspector] public List<GameObject> clothCards ;
    //list of Money Card;
    [HideInInspector] public List<GameObject> moneyCard;
    [HideInInspector] public List<GameObject> actionCard;//List Of Action Card
    [HideInInspector] public List<GameObject> jobCards;//List of Job Cards
    [HideInInspector] public List<GameObject> placeCards;//List Of Place Cards 
    [SerializeField] public Button[] cardButton=new Button[5];
    public string cat;//card Category
    public GameObject passPanel;
    public GameObject hidePanel;
    public  PlayerAi[] _ai=new PlayerAi[3];
    public GameObject ClickedCard;
    public Text[] actionCardText=new Text[3];
    public Text[] foodCardText=new Text[3];
    public Text[] placeCardText=new Text[3];
    public Text[] clothCardText=new Text[3];
    public Text[] jobCardText  =new Text[3];
    public Text[] moneyText    =new Text[3];
    public string recivedCardCat;
    public string recivedCat;
    public string revicidCardName;
    public bool sendCardActionPanelWork;
    public Image cardImage;
    public Button[] cardCategoryButtons;
    public GameObject[] playerHollow;
    public GameObject playerTimer;
    public string[] foodCardNames=new string[6];
    public string[] placeCardNames=new string[6];
    public string[] clothCardName=new string[6];
    public string[] jobCardName=new string[6];
    public string[] moneyCardName=new string[6];
    
    public void Awake()
    {
        ins = this; //Creat instance From this Class 
    }

     public  void Start()
    {
        cards = GameSaveSysteam.Ins.usedCart; 
        for (int i = 1; i < cards.Count; i++)//Divide card to Card list 
        {
            if (cards[i].GetComponent<category>().cat.Name == "ActionCards")
            {
                actionCard.Add(cards[i]);
            }
            else if (cards[i].GetComponent<category>().cat.Name == "Clothe")
            {
                clothCards.Add(cards[i]);
                clothCardName[i] = cards[i].name;
            }
            else if (cards[i].GetComponent<category>().cat.Name == "Food")
            {
                foodCards.Add(cards[i]);
                foodCardNames[i] = cards[i].name;
            }
            else if (cards[i].GetComponent<category>().cat.Name == "Job")
            {
                jobCards.Add(cards[i]);
             jobCardName[i] = cards[i].name;

            } 
            else if (cards[i].GetComponent<category>().cat.Name == "Money")
            {
                moneyCard.Add(cards[i]);
                moneyCardName[i] = cards[i].name;

            }
            else if (cards[i].GetComponent<category>().cat.Name == "Place")
            {
                placeCards.Add(cards[i]);   
                placeCardNames[i] = cards[i].name;

            } 
            else if (cards[i].GetComponent<category>().cat.Name == "ActionCards")
            {
                actionCard.Add(cards[i]); 
            }
            RandomCardGenrtor.Ins.CallToRemoveSameCard();

        }
    }

    public void OnActionButtonClick()//when you click on action button
    {            makeCardtransparent();
        passThecard.Pass.cat = "ActionCards";
        for (int i = 0; i < actionCard.Count; i++)
        {
        
            cardButton[i].GetComponent<Image>().sprite = actionCard[i].GetComponent<SpriteRenderer>().sprite;
            cardButton[i].GetComponent<Image>().color=new Color(cardButton[i].GetComponent<Image>().color.r,cardButton[i].GetComponent<Image>().color.g,cardButton[i].GetComponent<Image>().color.b,256f);
        }
    }
    public void OnFoodButtonClick()//when you click on Food Card 
    {            makeCardtransparent();
        cat = "Food";
        for (int i = 0; i < foodCards.Count; i++)
        {
          
            cardButton[i].GetComponent<Image>().sprite = foodCards[i].GetComponent<SpriteRenderer>().sprite;
            cardButton[i].GetComponent<Image>().color=new Color(cardButton[i].GetComponent<Image>().color.r,cardButton[i].GetComponent<Image>().color.g,cardButton[i].GetComponent<Image>().color.b,256f);

        }
    }
    public void OnMoneyButtonClick()
    {            makeCardtransparent();
        cat = "Money";
        for (int i = 0; i < moneyCard.Count; i++)
        {
          

            cardButton[i].GetComponent<Image>().sprite = moneyCard[i].GetComponent<SpriteRenderer>().sprite;
            cardButton[i].GetComponent<Image>().color=new Color(cardButton[i].GetComponent<Image>().color.r,cardButton[i].GetComponent<Image>().color.g,cardButton[i].GetComponent<Image>().color.b,256f);

        }
    }
    public void OnJobButtonClick()
    {            makeCardtransparent();
        cat = "Job";
        for (int i = 0; i < jobCards.Count; i++)
        {        

            cardButton[i].GetComponent<Image>().sprite = jobCards[i].GetComponent<SpriteRenderer>().sprite;
            cardButton[i].GetComponent<Image>().color=new Color(cardButton[i].GetComponent<Image>().color.r,cardButton[i].GetComponent<Image>().color.g,cardButton[i].GetComponent<Image>().color.b,256f);

        }
    }
    public void OnPlaceButtonClick()
    {            makeCardtransparent();
        cat = "Place";

        for (int i = 0; i < placeCards.Count; i++)
        {    

            cardButton[i].GetComponent<Image>().sprite = placeCards[i].GetComponent<SpriteRenderer>().sprite;
            
            cardButton[i].GetComponent<Image>().color=new Color(cardButton[i].GetComponent<Image>().color.r,cardButton[i].GetComponent<Image>().color.g,cardButton[i].GetComponent<Image>().color.b,256f);

        }
    }
    public void OnClothCardButtonClick()
    {            makeCardtransparent();
        cat = "Clothe";

        for (int i = 0; i < clothCards.Count; i++)
        {    
                
            cardButton[i].GetComponent<Image>().sprite = clothCards[i].GetComponent<SpriteRenderer>().sprite;
            cardButton[i].GetComponent<Image>().color=new Color(cardButton[i].GetComponent<Image>().color.r,cardButton[i].GetComponent<Image>().color.g,cardButton[i].GetComponent<Image>().color.b,256f);

        }
    }

    public void makeCardtransparent()
    {
        for (int i = 0; i < cardButton.Length; i++)
        {
            cardButton[i].GetComponent<Image>().color=new Color(cardButton[i].GetComponent<Image>().color.r,cardButton[i].GetComponent<Image>().color.g,cardButton[i].GetComponent<Image>().color.b,0);

        }


    }

    public void DeleteTheCard(Image cardImage ,string catName)
    {
 
        switch (catName)
        {
            case  "ActionCards":
                for (int i = 0; i < actionCard.Count; i++)
                {
                    if (actionCard[i].GetComponent<SpriteRenderer>().sprite.name==cardImage.sprite.name)
                    {
                        selectedcard = actionCard[i];
                        actionCard.Remove(actionCard[i]);
                    }
                }
                break;
            
            case "Clothe":
                for (int i = 0; i < clothCards.Count; i++)
                {
                    if (clothCards[i].GetComponent<SpriteRenderer>().sprite.name==cardImage.sprite.name)
                    {
                        selectedcard = clothCards[i];
                        clothCards.Remove(clothCards[i]);
                    }
                }
                break;
            
            case "Food":
                for (int i = 0; i < foodCards.Count; i++)
                {
                    if (foodCards[i].GetComponent<SpriteRenderer>().sprite.name==cardImage.sprite.name)
                    {
                        selectedcard = foodCards[i];
                        foodCards.Remove(foodCards[i]);
                    }
                }
                break;
            
            case "Job":
                for (int i = 0; i < jobCards.Count; i++)
                {
                    if (jobCards[i].GetComponent<SpriteRenderer>().sprite.name==cardImage.sprite.name)
                    {
                        selectedcard = jobCards[i];
                        jobCards.Remove(jobCards[i]);
                    }
                }
                break;
            
            case "Place":
                for (int i = 0; i <placeCards.Count; i++)
                {
                    if (placeCards[i].GetComponent<SpriteRenderer>().sprite.name==cardImage.sprite.name)
                    {
                        selectedcard = placeCards[i];
                        placeCards.Remove(placeCards[i]);
                    }
                        
                }break;
            
            case "Money":
                for (int i = 0; i < moneyCard.Count; i++)
                {
                    if (moneyCard[i].GetComponent<SpriteRenderer>().sprite.name==cardImage.sprite.name)
                    {
                        selectedcard = moneyCard[i];
                        moneyCard.Remove(moneyCard[i]);
                    }
                }
                break;
            
            default:
                Debug.LogError("UnValid Card Category");
                break;
                
            
        }
        Gameover();
    }

    public void Delete(Image card)//Delect the card from category list 
    {
        
        for (int i = 0; i < cards.Count; i++)
        {
         
            if (card.sprite== cards[i].GetComponent<SpriteRenderer>().sprite)
            {
                Debug.Log("Deleted ");
                cards.Remove(cards[i]);
            }
        }
        cards.Remove(selectedcard);
    }
    public void ActiveThePassPanel()//active the pass Panel
    { 
       
        ClickedCard = EventSystem.current.currentSelectedGameObject;
        cardImage.sprite = ClickedCard.GetComponent<Image>().sprite;
        passPanel.SetActive(true);
        sendCardActionPanelWork = true;
        hidePanel.SetActive(true);
        SelectButtons();

    }

    public void SelectButtons()
    {
        if (cat == "Food")
        {
            cardCategoryButtons[0].Select();
            
        }else if (cat == "Money")
        {
            cardCategoryButtons[1].Select();
        }
        else if (cat == "Job")
        {
            cardCategoryButtons[2].Select();
        }
        else if (cat == "Place")
        {
            cardCategoryButtons[3].Select();    
        }
        else if (cat == "Clothe")
        {
            cardCategoryButtons[4].Select();
        }
    }

    public void PassTheCardToWho(bool timer=false)
    {
        RandomCardGenrtor.Ins.receivedCardName= ClickedCard.GetComponent<Image>().sprite.name;
        RandomCardGenrtor.Ins.receivedCardCat = cat;
        passPanel.SetActive(false);
        DeleteTheCard(ClickedCard.GetComponent<Image>(),cat);
        ClickedCard.GetComponent<Image>().sprite = null;
        sendCardActionPanelWork = false;
        playerHollow[0].SetActive(false);
        playerHollow[1].SetActive(false);
        playerHollow[2].SetActive(false);
        playerTimer.SetActive(false);
        if (ClickedCard.GetComponent<Image>().name != "dolphin")
        {
            RandomCardGenrtor.Ins.PutItInFrontOfPlayer();
            Debug.Log("not dol");
        }
        else if (ClickedCard.GetComponent<Image>().name == "dolphin")
        {
            RandomCardGenrtor.Ins.PutItInFrontOfPlayer(true);
            Debug.Log("Dol");
        }
        else if (timer )
        {
            RandomCardGenrtor.Ins.PutItInFrontOfPlayer(false,true);
            Debug.Log("normal");
        }
       // GameManger.ins.PlayerTurner();
    }

 
    public void PutItInFrontOFPlayer()
    {
        switch (recivedCardCat)
        {
            case  "ActionCards":
                putTheCardsInEmpetyPlacese(actionCardText,revicidCardName);
                break;
            
            case "Clothe":
                putTheCardsInEmpetyPlacese(clothCardText,revicidCardName);

                break;
            
            case "Food":
                putTheCardsInEmpetyPlacese(foodCardText,revicidCardName);

                break;
            
            case "Job":
                putTheCardsInEmpetyPlacese(jobCardText,revicidCardName);

                break;
            
            case "Place":
                putTheCardsInEmpetyPlacese(placeCardText,recivedCardCat);
                break;
            
            case "Money":
                putTheCardsInEmpetyPlacese(moneyText,revicidCardName);

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
            }
        }
        
    }
   
    public void RemoveThePanel()
    {
       
        hidePanel.SetActive(false);
        playerTimer.SetActive(true);
        PlayeTimer.ins.timeLeft = PlayeTimer.ins.maxTime;
       
    }

    public void HideThePanel()
    {
        hidePanel.SetActive(true);
        passPanel.SetActive(false);
        playerTimer.SetActive(false);


    }

    public void Gameover()
    {
        if ((clothCards != null) && (!clothCards.Any()))
        {
            if ((foodCards != null) && (!foodCards.Any()))
            {
                if ((placeCards != null) && (!placeCards.Any()))
                {
                    if ((jobCards != null) && (!jobCards.Any()))
                    {
                        if ((moneyCard != null) && (!moneyCard.Any()))
                        {
                            Debug.Log("GameOver");
                            GameManger.ins.GameOver();
                        }
                    }
                }
            }
        }
    }

    public void SendToWitchPlayer(int playernumber)
    {
        if (playernumber == 2)
        {
            playerHollow[0].SetActive(true);
            playerHollow[1].SetActive(false);
            playerHollow[2].SetActive(false);
            GameManger.ins.playerTurn = 0;
            GameManger.ins.PlayerTurner();
        }
        else if (playernumber == 3)
        {
            playerHollow[0].SetActive(false);
            playerHollow[1].SetActive(true);
            playerHollow[2].SetActive(false);
            GameManger.ins.playerTurn = 1;
            GameManger.ins.PlayerTurner();
        }
        else if (playernumber == 4)
        {
            playerHollow[0].SetActive(false);
            playerHollow[1].SetActive(false);
            playerHollow[2].SetActive(true);
            GameManger.ins.playerTurn = 1;
            GameManger.ins.PlayerTurner();

        }
    }

    public void DeActiveThePanel()
    {
        playerHollow[0].SetActive(false);
        playerHollow[1].SetActive(false);
        playerHollow[2].SetActive(false);
        GameManger.ins.playerTurn = 3;
        GameManger.ins.PlayerTurner();
        passPanel.SetActive(false);
        hidePanel.SetActive(false);
    }

}
