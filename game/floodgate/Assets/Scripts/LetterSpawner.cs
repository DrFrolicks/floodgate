using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System.Linq;

/// <summary>
/// Spawns typed letters from left to right with little variaton 
/// starting from the spawn region to the reset region.
/// last edited: ec
/// </summary>
public class LetterSpawner : MonoBehaviour
{
    [SerializeField]
    bool spawnsKeypresses; 


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
    BoxCollider tcSpawnRegion; 
    

    /// <summary>
    /// Hard triggers reset when letter reaches it; soft triggers reset when a new word is started at that location
    /// </summary>
    [SerializeField]
    BoxCollider tcResetRegionHard, tcResetRegionSoft;   

    /// <summary>
    /// The change in the perlin noise parameters that determine the Y position of the next letter. 
    /// </summary>
    [SerializeField]
    float pnDelta; 


    float pnX, pnY; 

    private void Awake()
    {
        ResetTC();
        pnX = Random.value * 100f;
        pnY = Random.value * 100f; 
    }
    public void SpawnLetter(string letter)
    {
        if(GameManager.Instance.activePhrase.GetComponent<HiddenPhrase>().RemainingLettersUpperCase())
            letter = letter.ToUpper();

        GameObject spawnedLetter = Instantiate(letterTemplate, textCursor.position, letterTemplate.transform.rotation, transform); 
        spawnedLetter.GetComponent<TextMeshPro>().text = letter;
        float v = Mathf.PerlinNoise(pnX, pnY);

        textCursor.position += (textCursor.transform.right * constantDeltaRight) + (textCursor.transform.up * (-maxDeltaVertical + maxDeltaVertical * v * 2));

        pnX += pnDelta;
        pnY += pnDelta; 

        onSpawnText.Invoke();
        //EditorApplication.isPaused = true; 
    }

    public void ResetTC()
    {
        textCursor.position = tcSpawnRegion.bounds.RandomPointInBounds();    
    }

    private void Update()
    {
        if(spawnsKeypresses && Input.anyKeyDown && Input.inputString.Length == 1)
        {
            if (tcResetRegionHard.bounds.Contains(textCursor.position) || Input.inputString == " " && tcResetRegionSoft.bounds.Contains(textCursor.position))
                ResetTC(); 
            else 
                SpawnLetter(Input.inputString);
        }
    }
}

