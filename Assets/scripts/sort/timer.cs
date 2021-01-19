using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timer : MonoBehaviour
{
    float totalTime ;
	persist_state state;
    
    

    // Start is called before the first frame update
    void Start()
    {
		state = GameObject.Find("persist_State").GetComponent<persist_state>();

        totalTime = state.remainedSortTime;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.localPosition  = new Vector2(-10f + state.remainedSortTime*10f/totalTime, -0.22f) ;
    }

   
    
}
