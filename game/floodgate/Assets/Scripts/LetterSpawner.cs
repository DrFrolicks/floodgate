using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class LetterSpawner : MonoBehaviour
{

    public GameObject letterTemplate;
    public UnityEvent onSpawnText;
    public BoxCollider spawnRegion;

    private void Awake()
    {
        spawnRegion = GetComponent<BoxCollider>();
    }

    public void SpawnLetter(string letter)
    {
        Instantiate(letterTemplate, spawnRegion.bounds.RandomPointInBounds(), letterTemplate.transform.rotation, transform).GetComponent<TextMeshPro>().text = letter; 
        onSpawnText.Invoke();
    }


    private void Update()
    {

        if(Input.anyKeyDown && Input.inputString.Length == 1 && Input.inputString != " ")
        {
            SpawnLetter(Input.inputString);
        }
    }
}

