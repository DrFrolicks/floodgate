using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 
public class FallingChar : MonoBehaviour
{

    [SerializeField]
    float pushForce, maxVelocity;

    
    /// <summary>
    /// The seconds it takes until the falling char starts being pushed towards an OpenSlot 
    /// </summary>
    [SerializeField]
    float forceDelay; 

    Rigidbody rb; 
    char character;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>(); 
    }

    public bool beingPushed;
    private void Start()
    {
        character = GetComponent<TextMeshPro>().text[0];
        Invoke("startBeingPushed", forceDelay); 
    }

    private void Update()
    {
        if (beingPushed)
        {
            Debug.Log((GetClosestOpenPosition() - transform.position).normalized * pushForce * Time.deltaTime);
            rb.AddForce((GetClosestOpenPosition() - transform.position).normalized * pushForce * Time.deltaTime);
            //rb.velocity = Vector3.ClampMagnitude(Vector3.zero, maxVelocity);
        }
    }
    
    void startBeingPushed()
    {
        print(GetClosestOpenPosition()); 
        if (GetClosestOpenPosition() != transform.position)
            beingPushed = true;
    }

    /// <summary>
    /// Returns the position of the closet open slot with a matching character.
    /// </summary>
    /// <returns></returns>
    Vector3 GetClosestOpenPosition()
    {
        List<Slot> openSlots = GameManager.Instance.activePhrase.GetComponent<HiddenPhrase>().openSlots;

        Vector3 nearestOpenPos = Vector3.positiveInfinity; 
        foreach(Slot s in openSlots)
        {
            if(s.character == character)
            {
                Vector3 worldPos = GameManager.Instance.activePhrase.GetComponent<CharPosition>().GetWorldPosition(s.index);
                float sDistance = Vector3.Distance(transform.position, worldPos);
                
                nearestOpenPos = sDistance < Vector3.Distance(transform.position, nearestOpenPos) ? new Vector3(worldPos.x, 0, worldPos.z) : nearestOpenPos; 
            }
        }
        return nearestOpenPos.x == Mathf.Infinity ? transform.position : nearestOpenPos; 
    }
    
    

}
