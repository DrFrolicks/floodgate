using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class PhrasesDiscoveredDisplay : MonoBehaviour
{

    [SerializeField]
    string templateString; 


    private void Start()
    {
        templateString = GetComponent<TextMeshProUGUI>().text; 
        GameManager.Instance.OnPhraseDiscovered.AddListener(UpdateText); 
    }
    
    public void UpdateText()
    {
        TextMeshProUGUI TMPtext = GetComponent<TextMeshProUGUI>();
        TMPtext.text = templateString.Replace("%discovered", StringUtil.NumberToWords(GameManager.Instance.phrasesDiscovered)); 
    }
}
