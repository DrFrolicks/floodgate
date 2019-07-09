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

    float minStopDist = 0.01f;
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
            rb.useGravity = false;
            rb.AddForce((GetClosestOpenSlot().position - transform.position).normalized * pushForce * Time.deltaTime);
            rb.velocity = Vector3.ClampMagnitude((GetClosestOpenSlot().position - transform.position).normalized * rb.velocity.magnitude, maxVelocity); 
            if (Vector3.Distance(transform.position, GetClosestOpenSlot().position) < minStopDist)
            {
                beingPushed = false;
                GameManager.Instance.activePhrase.GetComponent<HiddenPhrase>().closeSlot(GetClosestOpenSlot().index);
                Destroy(rb);
            }
        }
    }
    
    /// <summary>
    /// Starts pushing the letter towards an open slot, if available.
    /// If not, checks again in forceDelay seconds.
    /// </summary>
    void startBeingPushed()
    {
        if (GetClosestOpenSlot().position.x != Mathf.Infinity)
        {
            beingPushed = true;
            Destroy(GetComponent<SphereCollider>()); 
        } else
        {
            Invoke("startBeingPushed", forceDelay);
        }

    }

    /// <summary>
    /// Returns the closet open slot that matches the character. 
    /// </summary>
    /// <returns></returns>
    Slot GetClosestOpenSlot()
    {
        List<Slot> openSlots = GameManager.Instance.activePhrase.GetComponent<HiddenPhrase>().openSlots;
        Slot nearestOpenSlot = new Slot(); 
        foreach (Slot s in openSlots)
        {
            if (s.character == character)
            {
                float sDistance = Vector3.Distance(transform.position, s.position);

                nearestOpenSlot = sDistance < Vector3.Distance(transform.position, nearestOpenSlot.position) ? s : nearestOpenSlot;
            }
        }
        return nearestOpenSlot; 
    }
    

}
