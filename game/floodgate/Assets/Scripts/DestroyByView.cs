using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByView : MonoBehaviour
{
    private void Update()
    {
        if (!GetComponent<Renderer>().isVisible)
        {
            Destroy(gameObject);
        }
    }
}