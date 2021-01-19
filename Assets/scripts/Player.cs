using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{ 
    private  string[] _categoryName = new string [] {"ActionCards", "Clothe", "Food", "Jobs", "Money", "Places"};
    public int playerID;
    public GameObject[] players;
    public List<GameObject> playersCards;
    public string playerName;
    public GameObject recivedCard;
    private GameObject _choisedCard;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
