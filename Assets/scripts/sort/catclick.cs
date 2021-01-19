using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class catclick : MonoBehaviour
{
     moveActiveCardTo moveScript; 
		
		
    void Start()
    {
        moveScript = GameObject.Find("sort").GetComponent<moveActiveCardTo>();   
    }
    void OnMouseDown()
    {
  
      moveScript.setAsDest(gameObject);
    }

}
