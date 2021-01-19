using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

[ExecuteInEditMode]
public class ArrengeCats : MonoBehaviour
{
    // Start is called before the first frame update


	persist_state state;

	
    void Start()
    {
		state = GameObject.Find("persist_State").GetComponent<persist_state>();

		
        float r = 2.2f;
        for (int i = 0; i <= 5; i++) 
        {
			GameObject cat = this.transform.GetChild(i).gameObject; 
			//state.inCatCards.Add(cat.GetComponent<category>().cat, new List<GameObject>());
			
            Vector3 pos = cat.transform.position;
            pos.x = r * (float)Math.Sin(i* 2*Math.PI / 6);
            pos.y = r * (float)Math.Cos(i* 2*Math.PI / 6);
            cat.transform.position = pos;
           //Debug.Log(pos);
        }
        //Debug.Log("hello");

    }


}
