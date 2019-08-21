using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LetterSpawner))]
public class AutoLetterSpawn : MonoBehaviour
{
    [SerializeField]
    string[] phrases;

    [SerializeField]
    float phraseSpawnInterval, letterSpawnInterval;

    public void Start()
    {
        StartCoroutine(SpawnLoop()); 
    }

    IEnumerator SpawnLoop()
    {
        while(GameManager.Instance.phraseQueue.Count > 0)
        {
            yield return StartCoroutine(SpawnPhrase(phrases[Random.Range(0, phrases.Length)]));
            yield return new WaitForSeconds(phraseSpawnInterval);
        }
    }

    IEnumerator SpawnPhrase(string phrase)
    {
        foreach (char c in phrase)
        {
            GetComponent<LetterSpawner>().SpawnLetter(c.ToString());
            yield return new WaitForSeconds(letterSpawnInterval); 
        }
        GetComponent<LetterSpawner>().ResetTC();
    }
    
    
}
