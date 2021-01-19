using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSaveSysteam : MonoBehaviour
{
  public static GameSaveSysteam Ins;
  public List<GameObject> usedCart=new List<GameObject>();
  public List<GameObject> aiCart=new List<GameObject>();
    
  private void Awake()
  {
      if (Ins != null)
      {
          Destroy(gameObject);
      }
      else
      {
          Ins = this;
          DontDestroyOnLoad(gameObject);
      }
  }

  public void EndOfTheGame()
  {
      
    //  CardSaveSysteam.ins.SaveData(this);
  }

  public void LoadCard()
  {
   // usedCart=  CardSaveSysteam.ins.LoadData().usedCart;

  }
}
