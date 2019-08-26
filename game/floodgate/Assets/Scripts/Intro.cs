using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.RainMaker;
using UnityEngine.Playables; 
public class Intro : MonoBehaviour
{
    [SerializeField]
    PlayableDirector playableDirector;

    /// <summary>
    /// How long does the timeline play for each keystroke? 
    /// </summary>
    [SerializeField]
    float keystrokePlaytAddAmt;

    float timelinePlaytime;

    private void Start()
    {
        playableDirector.Pause(); 
    }
    private void Update()
    {
        if(Input.anyKeyDown)
        {
            timelinePlaytime += keystrokePlaytAddAmt; 
        }

        if (timelinePlaytime > 0)
        {
            if(playableDirector.state == PlayState.Paused)
                playableDirector.Resume();
            timelinePlaytime -= Time.deltaTime; 
        } else
        {
            playableDirector.Pause(); 
        }
    }

    private void OnDisable()
    {
        playableDirector.Resume(); 
    }




}
