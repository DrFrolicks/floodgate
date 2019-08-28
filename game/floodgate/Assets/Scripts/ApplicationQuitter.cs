using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Quits the application on start.
/// </summary>
public class ApplicationQuitter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Application.Quit();
    }

}
