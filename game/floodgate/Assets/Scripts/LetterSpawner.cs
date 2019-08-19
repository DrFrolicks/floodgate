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

    /// <summary>
    /// The maximum change in vertical distance in world units between the last letter and the new. 
    /// Actual change can be between +/- maxDeltaY and is determined by the perlin noise scale factor. 
    /// </summary>
    [SerializeField]
    float maxDeltaVertical;

    /// <summary>
    /// The horizontal distance gap between each letter. 
    /// </summary>
    [SerializeField]
    float constantDeltaRight; 

    [SerializeField]
    BoxCollider tcSpawnRegion, tcResetRegion;

    /// <summary>
    /// The change in the perlin noise parameters that determine the Y position of the next letter. 
    /// </summary>
    [SerializeField]
    float pnDelta; 


    float pnX, pnY; 

    private void Start()
    {
        ResetTC();
        pnX = Random.value * 100f;
        pnY = Random.value * 100f; 
    }
    public void SpawnLetter(string letter)
    {
        Instantiate(letterTemplate, textCursor.position, letterTemplate.transform.rotation, transform).GetComponent<TextMeshPro>().text = letter;
        float v = Mathf.PerlinNoise(pnX, pnY);

        textCursor.position += (textCursor.transform.right * constantDeltaRight) + (textCursor.transform.up * (-maxDeltaVertical + maxDeltaVertical * v * 2));

        pnX += pnDelta;
        pnY += pnDelta; 

        onSpawnText.Invoke();
    }

    private void ResetTC()
    {
        textCursor.position = tcSpawnRegion.bounds.RandomPointInBounds();    
    }

    private void Update()
    {
        if(Input.anyKeyDown && Input.inputString.Length == 1)
        {
            if (Input.inputString == " " && tcResetRegion.bounds.Contains(textCursor.position)) // if the start of the new word is in the reset zone
                ResetTC(); 
            else 
                SpawnLetter(Input.inputString);
        }
    }
}

