using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 
public class FallingChar : MonoBehaviour
{

    [SerializeField]
    float pushForce, maxVelocity;

    Rigidbody rb; 
    char character;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>(); 
        character = GetComponent<TextMeshPro>().text[0]; 
    }
    public bool moving;
    private void Start()
    {
        moving = true; 
    }

    private void Update()
    {
        if(moving)
        {
            rb.AddForce((GetClosestOpenPosition() - transform.position).normalized * pushForce * Time.deltaTime);
            rb.velocity = Vector3.ClampMagnitude(Vector3.zero, maxVelocity);
        }

    }

    Vector3 GetClosestOpenPosition()
    {
        List<Slot> openSlots = GameManager.Instance.activePhrase.GetComponent<HiddenPhrase>().openSlots;
        CharPosition cp = GameManager.Instance.activePhrase.GetComponent<CharPosition>();

        Vector3 nearestOpenPos = Vector3.positiveInfinity; 
        foreach(Slot s in openSlots)
        {
            if(s.character == character)
            {
                float sDistance = Vector3.Distance(transform.position, cp.GetWorldPosition(s.index));
                nearestOpenPos = sDistance < Vector3.Distance(transform.position, nearestOpenPos) ? cp.GetWorldPosition(s.index) : nearestOpenPos; 
            }
        }
        return nearestOpenPos == Vector3.positiveInfinity ? transform.position : nearestOpenPos; 
    }


}
