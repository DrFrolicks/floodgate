using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class CharPosition : MonoBehaviour
{
    public Text textComp;
    public int charIndex;
    public Canvas canvas;


    public Vector3 GetWorldPosition(int charIndex)
    {
        charIndex++; 
        string text = textComp.text;

        if (charIndex > text.Length)
        {
            Debug.Log("Error: char index too high");
        }

            

        TextGenerator textGen = new TextGenerator(text.Length);
        Vector2 extents = textComp.gameObject.GetComponent<RectTransform>().rect.size;
        textGen.Populate(text, textComp.GetGenerationSettings(extents));

        int newLine = text.Substring(0, charIndex).Split('\n').Length - 1;
        int whiteSpace = text.Substring(0, charIndex).Split(' ').Length - 1;
        int indexOfTextQuad = (charIndex * 4) + (newLine * 4) - 4;
        if (indexOfTextQuad > textGen.vertexCount)
        {
            Debug.LogError("Errpr: Out of text bound");

        }

        Vector3 avgPos = (textGen.verts[indexOfTextQuad].position +
            textGen.verts[indexOfTextQuad + 1].position +
            textGen.verts[indexOfTextQuad + 2].position +
            textGen.verts[indexOfTextQuad + 3].position) / 4f;

        //print(avgPos);
        Vector3 worldPos = textComp.transform.TransformPoint(avgPos);
        Debug.DrawRay(worldPos, Vector3.up, Color.red);
        return worldPos; 

    }

    //void PrintWorldPos(Vector3 testPoint)
    //{
    //    //Vector3 worldPos = textComp.transform.TransformPoint(testPoint);
    //    //print(worldPos);
    //    //new GameObject("point").transform.position = worldPos;
    //    //Debug.DrawRay(worldPos, Vector3.up, Color.red, 50f);
    //}

    //void OnGUI()
    //{
    //    if (GUI.Button(new Rect(10, 10, 100, 80), "Test"))
    //    {
    //        PrintPos();
    //    }
    //}
}