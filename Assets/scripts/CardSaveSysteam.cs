using  System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using  System.IO;

public  class CardSaveSysteam : MonoBehaviour
{
    public static CardSaveSysteam ins;

    private void Awake()
    {
        if (ins != null)
        {
            Destroy(gameObject);
        }
        else
        {
            ins = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public  void SaveData(GameSaveSysteam cards)
    {
        
        BinaryFormatter binaryFormatter =new BinaryFormatter();
        FileStream stream =new FileStream(Application.persistentDataPath+" USedCard.dolfish",FileMode.Create);
        UsedCards card=new UsedCards(cards.usedCart);
        binaryFormatter.Serialize(stream,card);//change this;
        stream.Close();
        Debug.Log("file is Close");
        
    }

    public  UsedCards LoadData()
    {
        if (File.Exists(Application.persistentDataPath + "USedCard.dolfish"))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + " USedCard.dolfish", FileMode.Open);
            UsedCards cards = binaryFormatter.Deserialize(stream) as UsedCards;
            stream.Close(); //chnage this;
            return cards;
        }
        else
        {
            Debug.LogError("DontFindthePatch");
            return null;
        }
    }

    [System.Serializable]
    public class UsedCards
    {
        public List<GameObject> usedCart;

       public UsedCards(List<GameObject> carts)
        {
            usedCart = carts;
            
        }

    }


}
