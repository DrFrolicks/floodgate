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
        //foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
        //{
        //    if (Input.GetKeyDown(vKey) && vKey.ToString().Length == 1)
        //    {
        //        SpawnLetter(vKey.ToString());
        //    }
        //}

        if(Input.inputString.Length == 1)
        {
            SpawnLetter(Input.inputString);
        }
    }
}

