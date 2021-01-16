using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicLevel8 : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject finalPipe;
    bool firstPipe1 = true;
    bool firstPipe2 = true;
    public List<string> gatesList = new List<string>();
    /*    string[] gatesArray = new string[1]; */
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && gatesList.Count < 1)
        {
            Debug.Log("Adding And");
            gatesList.Add("And");
        }
        if (Input.GetKeyDown(KeyCode.O) && gatesList.Count < 1)
        {
            Debug.Log("Adding Or");
            gatesList.Add("Or");
        }
        if (Input.GetKeyDown(KeyCode.N) && gatesList.Count < 1)
        {
            Debug.Log("Adding Nand");
            gatesList.Add("Nand");
        }
        if (Input.GetKeyDown(KeyCode.R) && gatesList.Count < 1)
        {
            Debug.Log("Adding Nor");
            gatesList.Add("Nor");
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            gatesList.Clear();
            var pipeRenderer = finalPipe.GetComponent<Renderer>();
            Debug.Log("Clearing gates list and fixing pipes");
            //Call SetColor using the shader property name "_Color" and setting the color to red
            pipeRenderer.material.SetColor("_Color", Color.black);
        }
        if (Input.GetKeyDown(KeyCode.G) && gatesList.Count > 0)
        {
            bool finalValue = false;

            foreach (string gate in gatesList)
            {
                if (gate == "And")
                {
                    finalValue = AndFunction(firstPipe1, firstPipe2);
                }
                if (gate == "Or")
                {
                    finalValue = OrFunction(firstPipe1, firstPipe2);
                }
                if (gate == "Nand")
                {
                    finalValue = NandFunction(firstPipe1, firstPipe2);
                }
                if (gate == "Nor")
                {
                    finalValue = NorFunction(firstPipe1, firstPipe2);
                }
            }
            if (finalValue)
            {
                //Get the Renderer component from the new cube
                var pipeRenderer = finalPipe.GetComponent<Renderer>();

                //Call SetColor using the shader property name "_Color" and setting the color to red
                pipeRenderer.material.SetColor("_Color", Color.yellow);
            }
        }
    }
    bool AndFunction(bool first, bool second)
    {
        return (first && second);
    }
    bool NandFunction(bool first, bool second)
    {
        return !(first && second);
    }
    bool OrFunction(bool first, bool second)
    {
        return (first || second);
    }
    bool NorFunction(bool first, bool second)
    {
        return !(first || second);
    }
    /*    bool NotFunction(bool first)
        {
            return !first;
        }*/
}
