using System.Collections;
using System.Collections.Generic;
using System.Text; 
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq; 

[System.Serializable]
public class Slot
{
    public Slot(char c, int i)
    {
        character = c;
        index = i; 
    }

    public char character;
    public int index; 
}

[RequireComponent(typeof(Text))]
public class HiddenPhrase : MonoBehaviour
{
    public string completeText;
    public Text textComp;
    public List<Slot> openSlots; 

    
    private void Awake()
    {
        GetComponentInChildren<Text>().text = completeText;
        InitializeOpenSlots(); 
    }

    void InitializeOpenSlots()
    {
        openSlots = new List<Slot>(completeText.Replace(" ", "").Length);
        for (int i = 0; i < completeText.Length; i++)
        {
            if (completeText[i] != ' ')
            {
                openSlots.Add(new Slot(completeText[i], i)); 
            }
        }
    }
}
