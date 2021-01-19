using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Musicplayer : MonoBehaviour
{
    public static Musicplayer musicplayer;
    // Start is called before the first frame update
    void Awake()
    {
        if (musicplayer!=null)
        {
          
            Destroy(gameObject);
        }
        else
        {
            musicplayer = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void SetUpSingelton()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
