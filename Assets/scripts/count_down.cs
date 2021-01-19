using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class count_down : MonoBehaviour
{
    // Start is called before the first frame update
	
	persist_state state;
	
	private TextMeshProUGUI TMP;
	
    void Start()
    {
        state = GameObject.Find("persist_State").GetComponent<persist_state>();
		TMP = GetComponent<TextMeshProUGUI>();

    }

    // Update is called once per frame
    void Update()
    {
		var tmp = state.remainedSortTime.ToString("00.0").Split('.');
		TMP.text = tmp[0]+":"+tmp[1];
    }
}
