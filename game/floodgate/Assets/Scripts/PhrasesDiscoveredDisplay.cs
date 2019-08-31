using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class PhrasesDiscoveredDisplay : MonoBehaviour
{

    [SerializeField]
    string templateString; 

    
    private void Awake()
    {
        templateString = GetComponent<TextMeshProUGUI>().text; 
    }

    private void OnEnable()
    {
        UpdateTextOnce();
        
    }

    public void UpdateTextWithTemplate()
    {
        TextMeshProUGUI TMPtext = GetComponent<TextMeshProUGUI>();
        TMPtext.text = templateString.Replace("%discovered", StringUtil.NumberToWords(GameManager.Instance.phrasesDiscovered));
    }

    /// <summary>
    /// Updates the text with the current text as the template text. Can only be called once while the template is displayed.
    /// </summary>
    void UpdateTextOnce()
    {
        templateString = GetComponent<TextMeshProUGUI>().text;
        UpdateTextWithTemplate(); 
    }
       
}
