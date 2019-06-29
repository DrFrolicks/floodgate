
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharRays : MonoBehaviour
{
    CharPosition cp; 
    private void Start()
    {
        cp = GetComponent<CharPosition>(); 
    }
    // Update is called once per frame
    void Update()
    {
        for(int i = 1; i <= cp.textComp.text.Length; i++)
        {
            cp.GetWorldPosition(i); 
        }
        
    }
}
