using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class passThecard : MonoBehaviour
{
    public static passThecard Pass;

    public string cat;
    
    public GameObject passPanel;
    public GameObject hidePanel;
    public  PlayerAi[] _ai=new PlayerAi[3];
    private void Awake()
    {
        if (Pass==null)
        {
            Pass = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
//        Debug.Log(cat);
    }

     public void ActiveThePassPanel()
    { 
      passPanel.SetActive(true);  
      //passPanel.GetComponent<Animation>().Stop();
    }

     public void PassTheCardToWho(int index)
     { Debug.Log(cat);
         _ai[index].recivedCard = PutCardInPlace.ins.selectedcard;
         _ai[index].CardReciver(); 
        // PutCardInPlace.ins.DeleteTheCard(this.GetComponent<Image>(),this.cat);
       PutCardInPlace.ins.Delete(this.GetComponent<Image>());
       passPanel.SetActive(false);
     }
}
