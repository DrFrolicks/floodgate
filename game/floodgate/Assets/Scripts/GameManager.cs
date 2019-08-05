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
    Queue<string> phraseQueue;

    [HideInInspector]
    public int phrasesDiscovered = 0;

    [HideInInspector]
    public UnityEvent OnPhraseDiscovered; 
    
    private new void Awake()
    {
        base.Awake();

        phraseQueue = new Queue<string>();

        //load phrase text
        var textFile = Resources.Load<TextAsset>("Phrases");
        foreach(string phrase in textFile.text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries))
        {
            phraseQueue.Enqueue(phrase); 
        }
        phraseQueue = new Queue<string>(phraseQueue.Shuffle<string>().ToList<string>());
       
    }

    public void NextPhrase()
    {
       if(phrasesDiscovered < phraseQueue.Count)
        {
            phrasesDiscovered++; 

            if (activePhrase != null)
                activePhrase.GetComponent<HiddenPhrase>().CloseAllOpenSlots(); //ensures that slots dont check for positions, to allow for multiple phrase position debugging

            phraseTemplate.GetComponent<HiddenPhrase>().completeText = phraseQueue.Dequeue();
            activePhrase = Instantiate(phraseTemplate, phraseSpawnZone.bounds.RandomPointInBounds(), phraseTemplate.transform.rotation, transform).gameObject;
            OnPhraseDiscovered.Invoke(); 
        }
    }


    private void Start()
    {
        NextPhrase();
    }



}
