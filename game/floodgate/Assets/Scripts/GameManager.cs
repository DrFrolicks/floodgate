using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 
using System.Linq;
using System; 


public class GameManager : Singleton<GameManager>
{
    public GameObject phraseTemplate; 
    [HideInInspector]
    public GameObject activePhrase;

    public BoxCollider phraseSpawnZone; 
    public Queue<string> phraseQueue;

    public int phrasesDiscovered = 0;

    [HideInInspector]
    public UnityEvent OnPhraseDiscovered; 
    
    private new void Awake()
    {
        base.Awake();

        OnPhraseDiscovered.AddListener(ProcessPhraseDiscovered); 

        phraseQueue = new Queue<string>();

        //load phrase text
        var textFile = Resources.Load<TextAsset>("Phrases");
        foreach(string phrase in textFile.text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries))
        {
            phraseQueue.Enqueue(phrase); 
        }
        phraseQueue = new Queue<string>(phraseQueue.Shuffle<string>().ToList<string>());
        NextPhrase();
    }

    public void NextPhrase()
    {
       if(phrasesDiscovered < phraseQueue.Count)
        {
            phraseTemplate.GetComponent<HiddenPhrase>().completeText = phraseQueue.Dequeue();
            activePhrase = Instantiate(phraseTemplate, phraseSpawnZone.bounds.RandomPointInBounds(), phraseTemplate.transform.rotation, transform).gameObject;
        }
    }

    private void Start()
    {
        
    }

    void ProcessPhraseDiscovered()
    {
        if (activePhrase != null)
            activePhrase.GetComponent<HiddenPhrase>().CloseAllOpenSlots(); //ensures that slots dont check for positions, to allow for multiple phrase position debugging

        GameManager.Instance.NextPhrase();
        GameManager.Instance.phrasesDiscovered++;
    }

    
}
