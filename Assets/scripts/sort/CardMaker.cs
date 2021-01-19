using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//using System.IO;
using System.Linq; //OrderBy 

using System;

public class CardMaker : MonoBehaviour
{
	public  GameObject sortPanel;
	//public Sprite[] cardImgs ;
	public Material theMaterial ;

	public float unblurSpeed;
	
	public ScriptableObject[] cats ; 
	
	public GameObject cardPrefab;
	
public	persist_state state;
public GameObject aiCards;
	
	public (Cat_so,Sprite, AudioClip)[] all_cards_shuffled;// 
	public GameObject aiCardsParents;
    void Start()
    {
		state = GameObject.Find("persist_State").GetComponent<persist_state>();
		
        cardPrefab = Resources.Load("card_prefab", typeof(GameObject)) as GameObject;
		var sr = cardPrefab.GetComponent<SpriteRenderer>();
		var vr = cardPrefab.GetComponent<AudioSource>();
		sr.sortingOrder = 99;
			
		var r = new System.Random();
		all_cards_shuffled = cards().ToArray().OrderBy(x => r.Next()).ToArray();
		make(0);
		
		make(1);
		activate(0);
		savetheCardsforAi();
    }
	
	void make(int n)
	{
		Debug.Log("oof");
	
		var (cat, img, voice) = all_cards_shuffled[n];
		cardPrefab.GetComponent<category>().cat = cat;
		
		SpriteRenderer sr = cardPrefab.GetComponent<SpriteRenderer>();
		sr.sprite = img ;

		var vr = cardPrefab.GetComponent<AudioSource>();
		//Delay();
		//vr.playOnAwake = voice;
        
		vr.clip = voice;
		sr.sortingOrder -= 1;
		var card = Instantiate(cardPrefab, new Vector3(0, 0, 0), Quaternion.identity);
		card.transform.parent = sortPanel.transform;
		card.name = "card"+n;
		Debug.Log("ss");
		state.cards.Add(card);
	}
	
	public void nextActiveCard(){
		if (state.cards.Count < all_cards_shuffled.Length){
			make(state.cards.Count);
			activate(state.cards.Count-2);
		}else{
			activate(state.cards.Count-1);
		}
	}
	
	
	void activate (int n)
	{
		state.activeCard = state.cards[n];
		state.activeCard.GetComponent<AudioSource>().Play();
		theMaterial.SetFloat ("_R", 15);
		state.activeCard.GetComponent<SpriteRenderer>().material = theMaterial;
		
	}
	
	void Update () {
		
		theMaterial.SetFloat ("_R", Math.Max(theMaterial.GetFloat("_R")- unblurSpeed * Time.deltaTime,0.1f));
	}
	
	
	IEnumerable<(Cat_so,Sprite,AudioClip)> cards(){
		foreach(Cat_so cat in cats)
		{
			var im_vo = cat.cardImgs.Zip(cat.cardVoice, (x, y) => (x , y));
			
			foreach((Sprite, AudioClip) imvo in im_vo){
				
				(var im, var audio)  = imvo;
				yield return( cat,im,audio );
			}
		}
	}
	
	
	public void savetheCardsforAi()
	{
		for (int i = 1; i < 21; i++)
		{
			aiCards.transform.position=new Vector3(1,3,0);
			//makeCardforAi(i);
			
		}
	}
	/*public void makeCardforAi(int n)
	{
		
		
		var (cat, img, voice) = all_cards_shuffled[n];
		cardPrefab.GetComponent<category>().cat = cat;
		
		SpriteRenderer sr = cardPrefab.GetComponent<SpriteRenderer>();
		sr.sprite = img ;
		sr.sortingOrder -= 1;
		var card = Instantiate(cardPrefab, new Vector3(0, 0, 0), Quaternion.identity);
		card.transform.parent = aiCardsParents.transform;
		card.name = "Ai card"+n;
		card.SetActive(false);
		state.aiCards.Add(card);
	}*/
	
}
