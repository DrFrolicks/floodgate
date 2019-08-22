using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 
public class FallingChar : MonoBehaviour
{

    [SerializeField]
    float acceleration, maxVelocity, lifespanSec; 

    float velocity = 0f; 
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
        Invoke("StartBeingPushed", forceDelay);
        Invoke("KillMyself", lifespanSec);  
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Tent"))
        {
            Destroy(rb);
            Destroy(GetComponent<Collider>());
        }
    }
    private void Update()
    {
        

        if (beingPushed)
        {
            if (GetClosestOpenSlot().position.x == Mathf.Infinity)  //if no slot exists
                return;

            velocity += acceleration * Time.deltaTime;  
            transform.position += Vector3.ClampMagnitude((GetClosestOpenSlot().position - transform.position).normalized * (velocity * Time.deltaTime), maxVelocity); 

            if (Vector3.Distance(transform.position, GetClosestOpenSlot().position) < minStopDist) //character is in place
            {
                CancelInvoke("KillMyself"); 
                beingPushed = false;
                transform.SetParent(GameManager.Instance.activePhrase.transform, true);  
                GameManager.Instance.activePhrase.GetComponent<HiddenPhrase>().closeSlot(GetClosestOpenSlot().index);
                GetComponent<Animator>().SetBool("Frozen", true); 
            }
        }
    }
    
    
    /// <summary>
    /// Starts pushing the letter towards an open slot, if available.
    /// If not, checks again in forceDelay seconds.
    /// </summary>
    void StartBeingPushed()
    {
        if (GetClosestOpenSlot().position.x != Mathf.Infinity)
        {
            beingPushed = true;
            Destroy(GetComponent<SphereCollider>()); 
        } else
        {
            Invoke("StartBeingPushed", forceDelay);
        }

    }

    /// <summary>
    /// Returns the closet open slot that matches the character. 
    /// Returns a default slot with positive infinity position if no slot exists. 
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
    
    void KillMyself()
    {
        Destroy(gameObject);  
    }

}
