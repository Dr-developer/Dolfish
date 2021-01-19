using System;
using System.Collections;
using System.Collections.Generic;
using FiroozehGameService.Models.Consts;
using FiroozehGameService.Models.GSLive;
using Org.BouncyCastle.Bcpg;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class persist_state : MonoBehaviour
{
	
	[HideInInspector] public List<GameObject> cards = new List<GameObject>();
	[HideInInspector] public List<GameObject> aiCards = new List<GameObject>();
	public GameObject activeCard = null;
	public float remainedSortTime = 25f; 
	public Dictionary<Cat_so, List<GameObject>> inCatCards = new Dictionary<Cat_so, List<GameObject>>()  ;
	public GameObject losePanel;
	public GameObject winPanel;
	private bool stoptheloop=true;
	public bool MultiPlayer;
	public TextMeshProUGUI scoreTextMeshPro;
	public PutCardInPlace InPlace;
	public GameObject sortPanel;	
	public GameObject playPanel;
	public Image[] O2Capsul=new Image[4];
	public GameObject o2;

	public float victoryTime;
	// Start is called before the first frame update
    void Update()
    {
	    if (moveActiveCardTo.Ins.numberOfCorrectAnswers == 8&& MultiPlayer)
	    {
		 
		    winPanel.SetActive(true);
		    victoryTime = remainedSortTime;
		    remainedSortTime = 0;

		    StartCoroutine(GotoStart());


	    }
	    if (remainedSortTime <= 0)
	    {


		     StartCoroutine(GotoStart());
			 SaveCardThatTheyUsed();
		    
	    }
	    
	    else
		{
			
				remainedSortTime -= Time.deltaTime;
		}


		
    }

    public void SaveCardThatTheyUsed()
    {
	    //InPlace.cards = cards;
	    GameSaveSysteam.Ins.usedCart = cards;
	    GameSaveSysteam.Ins.aiCart = aiCards;
	   // CardSaveSysteam.ins.SaveData(GameSaveSysteam.Ins);
	  
	}

    /*public void NotExitedInTheList(GameObject card)
    {
	    foreach (var VARIABLE in GameSaveSysteam.Ins.usedCart)
	    {
		    if (VARIABLE != card)
		    {
			    GameSaveSysteam.Ins.usedCart.Add(card);

		    }
					    
	    } 
    }*/
		
    IEnumerator GotoStart()
    {
	    o2.SetActive(false);
	     NumberOfCapsul();
	    yield return  new WaitForSeconds(5);
	    sortPanel.SetActive(false);
	    playPanel.SetActive(true);
	    SaveCardThatTheyUsed();
    }

    private void NumberOfCapsul()
    {
	    int numberofCapsul = 0;
	    if (victoryTime >= 20)
	    {
		    numberofCapsul = 4;
		    FillTheCapul(numberofCapsul);
	    }
	    else if (victoryTime<20&& victoryTime>=15)
	    {
		    numberofCapsul = 3;
		    FillTheCapul(numberofCapsul);
	    }
	    else if (victoryTime<15&& victoryTime>=10)
	    {
		    numberofCapsul = 2;
		    FillTheCapul(numberofCapsul);
	    }
	    else if (victoryTime<10)
	    {
		    numberofCapsul = 1;
		    FillTheCapul(numberofCapsul);
	    }
	   
    }

    private void FillTheCapul(int lenght)
    {
	    for (int i = 0; i < lenght; i++)
	    {
		    
		    O2Capsul[i].fillAmount+=1;
	    }
    }

}
