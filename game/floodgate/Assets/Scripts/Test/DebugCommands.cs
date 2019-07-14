using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugCommands : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetKey(KeyCode.D)) //debug hotkey
        {

            if(Input.GetKeyDown(KeyCode.Space))
            {
                GameManager.Instance.NextPhrase(); 
            }
        }
    }
}
