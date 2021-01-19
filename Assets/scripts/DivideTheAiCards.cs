using System;

using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DivideTheAiCards : MonoBehaviour
{
    public List<GameObject> aiCards=new List<GameObject>();

    public List<GameObject> playerTwoCards;

    public List<GameObject> playerThreeCards;

    public List<GameObject> playerFourCards;
    public PlayerAi[] ai=new PlayerAi[3];
    // Start is called before the first frame update
    void Start()
    {
        aiCards = GameSaveSysteam.Ins.aiCart;
            GiveTheCardToAi();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DivideCard()
    { double[]a=new double[3];
        for (int i = 0; i < a.Length; i++)
        {
            a[i]=Math.Floor(20f / Random.Range(3 , 10));
        }
        GiveTheCardToAi();
    }

    private void GiveTheCardToAi()
    {
        Debug.Log("Do SomeThing ");
        int i, j, k;
        var rand1 = Random.Range(5, 10);
        for (i = 1; i <rand1 ; i++)
        { Debug.Log("1");
            playerTwoCards.Add( aiCards[i]);
        }

        ai[0].playersCards = playerTwoCards;
        var rand2= Random.Range(i+5, i+10);
        for (j = i; i <rand2; i++)
        { Debug.Log("2");
            playerThreeCards.Add( aiCards[j]);

        }

        ai[1].playersCards = playerThreeCards;
        var rand3 = Random.Range(j+5, j+10);
        for (k = j; i < rand3; i++)
        { Debug.Log("3");
            playerFourCards.Add( aiCards[k]);

        }
        ai[2].playersCards=playerFourCards;
    }
}
