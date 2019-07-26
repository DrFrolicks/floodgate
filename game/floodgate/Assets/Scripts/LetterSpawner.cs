using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

/// <summary>
/// Spawns typed letters from left to right with little variaton 
/// starting from the spawn region to the reset region.
/// last edited: ec
/// </summary>
public class LetterSpawner : MonoBehaviour
{

    public UnityEvent onSpawnText;


    [SerializeField]
    GameObject letterTemplate;

    [SerializeField]
    Transform textCursor;

    [SerializeField]
    float maxLetterGap, minLetterGap;

    [SerializeField]
    BoxCollider tcSpawnRegion, tcResetRegion, pnScale;

    Vector2 pnOrg; 

    private void Start()
    {
        ResetTC();
        pnOrg = Random.insideUnitCircle * Random.Range(1, 10); 
        
        
    }
    public void SpawnLetter(string letter)
    {
        if (tcResetRegion.bounds.Contains(textCursor.position))
            ResetTC();

        Instantiate(letterTemplate, textCursor.position, letterTemplate.transform.rotation, transform).GetComponent<TextMeshPro>().text = letter; 
        textCursor.position += Vector3.ClampMagnitude(textCursor.transform.right * maxLetterGap 

        onSpawnText.Invoke();
    }

    private void ResetTC()
    {
        textCursor.position = tcSpawnRegion.bounds.RandomPointInBounds();    
    }
    private void Update()
    {

        if(Input.anyKeyDown && Input.inputString.Length == 1 && Input.inputString != " ")
        {
            SpawnLetter(Input.inputString);
        }
    }
}

