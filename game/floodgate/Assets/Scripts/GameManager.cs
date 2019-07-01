using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System; 


public class GameManager : Singleton<GameManager>
{
    public GameObject phraseTemplate; 
    public GameObject activePhrase;
    Queue<string> phraseQueue; 
    
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
        phraseTemplate.GetComponent<HiddenPhrase>().completeText = phraseQueue.Dequeue();
        activePhrase = Instantiate(phraseTemplate, transform).gameObject;
    }


    private void Start()
    {
        NextPhrase();
    }



}
