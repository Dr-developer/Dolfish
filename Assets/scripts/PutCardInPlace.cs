using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PutCardInPlace : MonoBehaviour
{
    //TODO:When PlayerTimer Ends Card Go in Front Of Player
    private const int PlayerOneIndex = 1;
    public static PutCardInPlace ins;
    public List<GameObject> cards;
    public GameObject selectedcard;

    //list of food cards
    [HideInInspector] public List<GameObject> foodCards;

    // list of cloth Card
    [HideInInspector] public List<GameObject> clothCards;

    //list of Money Card;
    [HideInInspector] public List<GameObject> moneyCard;
    [HideInInspector] public List<GameObject> actionCard; //List Of Action Card
    [HideInInspector] public List<GameObject> jobCards; //List of Job Cards
    [HideInInspector] public List<GameObject> placeCards; //List Of Place Cards 
    [SerializeField] public Button[] cardButton = new Button[5];
    public string cat; //card Category
    public GameObject passPanel;
    public GameObject hidePanel;
    public PlayerAi[] _ai = new PlayerAi[3];
    public GameObject ClickedCard;
    public Text[] actionCardText = new Text[3];
    public Text[] foodCardText = new Text[3];
    public Text[] placeCardText = new Text[3];
    public Text[] clothCardText = new Text[3];
    public Text[] jobCardText = new Text[3];
    public Text[] moneyText = new Text[3];
    public string recivedCardCat;
    public string revicidCardName;
    private string _senderCategory;
    public bool sendCardActionPanelWork;
    public Image cardImage;
    public Button[] cardCategoryButtons;
    public GameObject[] playerHollow;
    public GameObject[] playerOneCardsLights;
    private int _numberOfCardsOfPlayer;
    public GameObject playerTimer;
    public string[] foodCardNames = new string[6];
    public string[] placeCardNames = new string[6];
    public string[] clothCardName = new string[6];
    public string[] jobCardName = new string[6];
    public string[] moneyCardName = new string[6];
    public string cardName;
    public GameObject playerOneHollow;
    public int playerIndex;
    
    public void Awake()
    {
        ins = this; //Creat instance From this Class 
    }

    public void Start()
    {
        cards = GameSaveSysteam.Ins.usedCart;
        _numberOfCardsOfPlayer = 8;
        Debug.Log(_numberOfCardsOfPlayer);
        for (var i = 1; i < cards.Count; i++) //Divide card to Card list 
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

    public int GetNumberOfCardsOfPlayer()
    {
        return _numberOfCardsOfPlayer;
    }

    public void OnActionButtonClick() //when you click on action button
    {
        MakeCardtransparent();
        passThecard.Pass.cat = "ActionCards";
        for (var i = 0; i < actionCard.Count; i++)
        {
            cardButton[i].GetComponent<Image>().sprite = actionCard[i].GetComponent<SpriteRenderer>().sprite;
            cardButton[i].GetComponent<Image>().color = new Color(cardButton[i].GetComponent<Image>().color.r,
                cardButton[i].GetComponent<Image>().color.g, cardButton[i].GetComponent<Image>().color.b, 256f);
        }
    }

    public void OnFoodButtonClick() //when you click on Food Card 
    {
        MakeCardtransparent();
        cat = "Food";
        for (var i = 0; i < foodCards.Count; i++)
        {
            cardButton[i].GetComponent<Image>().sprite = foodCards[i].GetComponent<SpriteRenderer>().sprite;
            cardButton[i].GetComponent<Image>().color = new Color(cardButton[i].GetComponent<Image>().color.r,
                cardButton[i].GetComponent<Image>().color.g, cardButton[i].GetComponent<Image>().color.b, 256f);
        }
    }

    public void OnMoneyButtonClick()
    {
        MakeCardtransparent();
        cat = "Money";
        for (var i = 0; i < moneyCard.Count; i++)
        {
            cardButton[i].GetComponent<Image>().sprite = moneyCard[i].GetComponent<SpriteRenderer>().sprite;
            cardButton[i].GetComponent<Image>().color = new Color(cardButton[i].GetComponent<Image>().color.r,
                cardButton[i].GetComponent<Image>().color.g, cardButton[i].GetComponent<Image>().color.b, 256f);
        }
    }

    public void OnJobButtonClick()
    {
        MakeCardtransparent();
        cat = "Job";
        for (var i = 0; i < jobCards.Count; i++)
        {
            cardButton[i].GetComponent<Image>().sprite = jobCards[i].GetComponent<SpriteRenderer>().sprite;
            cardButton[i].GetComponent<Image>().color = new Color(cardButton[i].GetComponent<Image>().color.r,
                cardButton[i].GetComponent<Image>().color.g, cardButton[i].GetComponent<Image>().color.b, 256f);
        }
    }

    public void OnPlaceButtonClick()
    {
        MakeCardtransparent();
        cat = "Place";
        for (var i = 0; i < placeCards.Count; i++)
        {
            cardButton[i].GetComponent<Image>().sprite = placeCards[i].GetComponent<SpriteRenderer>().sprite;
            cardButton[i].GetComponent<Image>().color = new Color(cardButton[i].GetComponent<Image>().color.r,
                cardButton[i].GetComponent<Image>().color.g, cardButton[i].GetComponent<Image>().color.b, 256f);
        }
    }

    public void OnClothCardButtonClick()
    {
        MakeCardtransparent();
        cat = "Clothe";
        for (var i = 0; i < clothCards.Count; i++)
        {
            cardButton[i].GetComponent<Image>().sprite = clothCards[i].GetComponent<SpriteRenderer>().sprite;
            cardButton[i].GetComponent<Image>().color = new Color(cardButton[i].GetComponent<Image>().color.r,
                cardButton[i].GetComponent<Image>().color.g, cardButton[i].GetComponent<Image>().color.b, 256f);
        }
    }

    public void MakeCardtransparent()
    {
        for (var i = 0; i < cardButton.Length; i++)
            cardButton[i].GetComponent<Image>().color = new Color(cardButton[i].GetComponent<Image>().color.r,
                cardButton[i].GetComponent<Image>().color.g, cardButton[i].GetComponent<Image>().color.b, 0);
    }

    public void DeleteTheCard(Image cardImage, string catName)
    {
        switch (catName)
        {
            case "ActionCards":
                for (var i = 0; i < actionCard.Count; i++)
                    if (actionCard[i].GetComponent<SpriteRenderer>().sprite.name == cardImage.sprite.name)
                    {
                        selectedcard = actionCard[i];
                        actionCard.Remove(actionCard[i]);
                    }

                break;
            case "Clothe":
                for (var i = 0; i < clothCards.Count; i++)
                    if (clothCards[i].GetComponent<SpriteRenderer>().sprite.name == cardImage.sprite.name)
                    {
                        selectedcard = clothCards[i];
                        clothCards.Remove(clothCards[i]);
                    }

                break;
            case "Food":
                for (var i = 0; i < foodCards.Count; i++)
                    if (foodCards[i].GetComponent<SpriteRenderer>().sprite.name == cardImage.sprite.name)
                    {
                        selectedcard = foodCards[i];
                        foodCards.Remove(foodCards[i]);
                    }

                break;
            case "Job":
                for (var i = 0; i < jobCards.Count; i++)
                    if (jobCards[i].GetComponent<SpriteRenderer>().sprite.name == cardImage.sprite.name)
                    {
                        selectedcard = jobCards[i];
                        jobCards.Remove(jobCards[i]);
                    }

                break;
            case "Place":
                for (var i = 0; i < placeCards.Count; i++)
                    if (placeCards[i].GetComponent<SpriteRenderer>().sprite.name == cardImage.sprite.name)
                    {
                        selectedcard = placeCards[i];
                        placeCards.Remove(placeCards[i]);
                    }

                break;
            case "Money":
                for (var i = 0; i < moneyCard.Count; i++)
                    if (moneyCard[i].GetComponent<SpriteRenderer>().sprite.name == cardImage.sprite.name)
                    {
                        selectedcard = moneyCard[i];
                        moneyCard.Remove(moneyCard[i]);
                    }

                break;
            default:
                Debug.LogError("UnValid Card Category");
                break;
        }

        GameOver();
    }

    public void Delete(Image card) //Delect the card from category list 
    {
        for (var i = 0; i < cards.Count; i++)
            if (card.sprite == cards[i].GetComponent<SpriteRenderer>().sprite)
            {
                Debug.Log("Deleted ");
                cards.Remove(cards[i]);
            }

        cards.Remove(selectedcard);
    }

    public void ActiveThePassPanel() //active the pass Panel
    {
        ClickedCard = EventSystem.current.currentSelectedGameObject;
        cardImage.sprite = ClickedCard.GetComponent<Image>().sprite;
        cardName = ClickedCard.GetComponent<Image>().sprite.name;
        passPanel.SetActive(true);
        sendCardActionPanelWork = true;
        hidePanel.SetActive(true);
        SelectButtons();
    }

    public void SelectButtons()
    {
        if (cat == "Food") cardCategoryButtons[0].Select();
        else if (cat == "Money") cardCategoryButtons[1].Select();
        else if (cat == "Job") cardCategoryButtons[2].Select();
        else if (cat == "Place") cardCategoryButtons[3].Select();
        else if (cat == "Clothe") cardCategoryButtons[4].Select();
    }

    public void PassTheCardToWho(bool timer = false)
    {
        passPanel.SetActive(false);
        DeleteTheCard(ClickedCard.GetComponent<Image>(), cat);
        ClickedCard.GetComponent<Image>().sprite = null;
        sendCardActionPanelWork = false;
        playerTimer.SetActive(false);
        if (ClickedCard.GetComponent<Image>().name != "dolphin")
        {
            //TODO:Thing For a Solution For Dol 
            RandomCardGenrtor.Ins.playersNamesIndex.Add(PlayerOneIndex);
            GameManger.ins.FindingThePlayingCharacter(playerIndex, cat, _senderCategory, cardName, PlayerOneIndex);
        }
        else if (ClickedCard.GetComponent<Image>().name == "dolphin")
        {
            //TODO:Thing For a Solution For Dol
            // RandomCardGenrtor.Ins.PutItInFrontOfPlayer(true);
            Debug.Log("Dol");
        }
        else if (timer)
        {
            // RandomCardGenrtor.Ins.PutItInFrontOfPlayer(false,true);
            //  Debug.Log("normal");
        }

        _numberOfCardsOfPlayer--;
        TurnOfPlayerCardsLights();
        // GameManger.ins.PlayerTurner();
    }

    private void TurnOfPlayerCardsLights()
    {
        for (int i = _numberOfCardsOfPlayer; i <= playerOneCardsLights.Length; i++)
        {
            playerOneCardsLights[i].SetActive(false);
        }
    }

    public void PutItInFrontOfPlayer()
    {
        switch (recivedCardCat)
        {
            case "ActionCards":
                PutTheCardsInEmpetyPlacese(actionCardText, revicidCardName);
                break;
            case "Clothe":
                PutTheCardsInEmpetyPlacese(clothCardText, revicidCardName);
                break;
            case "Food":
                PutTheCardsInEmpetyPlacese(foodCardText, revicidCardName);
                break;
            case "Job":
                PutTheCardsInEmpetyPlacese(jobCardText, revicidCardName);
                break;
            case "Place":
                PutTheCardsInEmpetyPlacese(placeCardText, recivedCardCat);
                break;
            case "Money":
                PutTheCardsInEmpetyPlacese(moneyText, revicidCardName);
                break;
            default:
                Debug.LogError("UnValid Card Category");
                break;
        }
    }

    private void PutTheCardsInEmpetyPlacese(Text[] arry, string text)
    {
        for (var i = 0; i < arry.Length; i++)
            if (arry[i] == null)
                arry[i].text = text;
    }

    public void HumanTurn()
    {
        hidePanel.SetActive(false);
        playerTimer.SetActive(true);
        PlayeTimer.ins.timeLeft = PlayeTimer.ins.maxTime;
        playerOneHollow.gameObject.SetActive(true);
    }

    public void HideThePanel()
    {
        hidePanel.SetActive(true);
        passPanel.SetActive(false);
        playerTimer.SetActive(false);
    }

    private void GameOver()
    {
        if (_numberOfCardsOfPlayer==0)
        {
            GameManger.ins.GameOver();

        }
        if (clothCards != null && !clothCards.Any())
            if (foodCards != null && !foodCards.Any())
                if (placeCards != null && !placeCards.Any())
                    if (jobCards != null && !jobCards.Any())
                        if (moneyCard != null && !moneyCard.Any())
                        {
                            Debug.Log("GameOver");
                            GameManger.ins.GameOver();
                        }
    }

    public void SendCategory(string category)
    {
        _senderCategory = category;
    }

    private string GetSendCategory(string category)
    {
        return category;
    }

    public void SendToWitchPlayer(int playerNumber)
    {
        playerIndex = playerNumber;
        if (playerNumber == 2)
        {
            playerHollow[0].SetActive(true);
            playerHollow[1].SetActive(false);
            playerHollow[2].SetActive(false);
        }
        else if (playerNumber == 3)
        {
            playerHollow[0].SetActive(false);
            playerHollow[1].SetActive(true);
            playerHollow[2].SetActive(false);
        }
        else if (playerNumber == 4)
        {
            playerHollow[0].SetActive(false);
            playerHollow[1].SetActive(false);
            playerHollow[2].SetActive(true);
        }
    }

    public void DeActiveThePanel()
    {
        //  GameManger.ins.PlayerTurner();
        passPanel.SetActive(false);
        hidePanel.SetActive(false);
    }
    
}
