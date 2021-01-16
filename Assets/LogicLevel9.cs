using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicLevel9 : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject finalPipe;
    [SerializeField] GameObject firstLeft;
    [SerializeField] GameObject firstRight;
    [SerializeField] GameObject secondLeft;
    [SerializeField] GameObject secondRight;
    [SerializeField] GameObject secondLeftAfterNot;

    bool firstValue = false;
    bool secondValue = false;
    bool secondAfterNotValue = false;
    bool finalPipeValue = false;
    bool initialPipe1 = true;
    bool initialPipe2 = false;
    public List<string> gatesList = new List<string>();
    /*    string[] gatesArray = new string[1]; */
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && gatesList.Count < 3)
        {
            Debug.Log("Adding And");
            gatesList.Add("And");
        }
        if (Input.GetKeyDown(KeyCode.O) && gatesList.Count < 3)
        {
            Debug.Log("Adding Or");
            gatesList.Add("Or");
        }
        if (Input.GetKeyDown(KeyCode.N) && gatesList.Count < 3)
        {
            Debug.Log("Adding Nand");
            gatesList.Add("Nand");
        }
        if (Input.GetKeyDown(KeyCode.R) && gatesList.Count < 3)
        {
            Debug.Log("Adding Nor");
            gatesList.Add("Nor");
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            gatesList.Clear();
            var pipeRenderer = finalPipe.GetComponent<Renderer>();
            pipeRenderer.material.SetColor("_Color", Color.black);
            pipeRenderer = firstLeft.GetComponent<Renderer>();
            pipeRenderer.material.SetColor("_Color", Color.black);
            pipeRenderer = firstRight.GetComponent<Renderer>();
            pipeRenderer.material.SetColor("_Color", Color.black);
            pipeRenderer = secondLeft.GetComponent<Renderer>();
            pipeRenderer.material.SetColor("_Color", Color.black);
            pipeRenderer = secondRight.GetComponent<Renderer>();
            pipeRenderer.material.SetColor("_Color", Color.black);
            pipeRenderer = secondLeftAfterNot.GetComponent<Renderer>();
            pipeRenderer.material.SetColor("_Color", Color.black);
        }
        if (Input.GetKeyDown(KeyCode.G) && gatesList.Count == 3)
        {

            if (gatesList[0] == "And")
            {
                firstValue = AndFunction(initialPipe1, initialPipe2);
            }
            if (gatesList[0] == "Or")
            {
                firstValue = OrFunction(initialPipe1, initialPipe2);
            }
            if (gatesList[0] == "Nand")
            {
                firstValue = NandFunction(initialPipe1, initialPipe2);
            }
            if (gatesList[0] == "Nor")
            {
                firstValue = NorFunction(initialPipe1, initialPipe2);
            }

            if (firstValue)
            {
                //Get the Renderer component from the new cube
                var pipeRenderer = firstLeft.GetComponent<Renderer>();

                //Call SetColor using the shader property name "_Color" and setting the color to red
                pipeRenderer.material.SetColor("_Color", Color.yellow);

                pipeRenderer = firstRight.GetComponent<Renderer>();

                //Call SetColor using the shader property name "_Color" and setting the color to red
                pipeRenderer.material.SetColor("_Color", Color.yellow);
            }

            if (gatesList[1] == "And")
            {
                secondValue = AndFunction(firstValue, firstValue);
            }
            if (gatesList[1] == "Or")
            {
                secondValue = OrFunction(firstValue, firstValue);
            }
            if (gatesList[1] == "Nand")
            {
                secondValue = NandFunction(firstValue, firstValue);
            }
            if (gatesList[1] == "Nor")
            {
                secondValue = NorFunction(firstValue, firstValue);
            }

            if (secondValue)
            {
                secondAfterNotValue = false;
                //Get the Renderer component from the new cube
                var pipeRenderer = secondLeft.GetComponent<Renderer>();

                //Call SetColor using the shader property name "_Color" and setting the color to red
                pipeRenderer.material.SetColor("_Color", Color.yellow);

                pipeRenderer = secondRight.GetComponent<Renderer>();

                //Call SetColor using the shader property name "_Color" and setting the color to red
                pipeRenderer.material.SetColor("_Color", Color.yellow);
            }
            else
            {
                var pipeRenderer = secondLeftAfterNot.GetComponent<Renderer>();

                //Call SetColor using the shader property name "_Color" and setting the color to red
                pipeRenderer.material.SetColor("_Color", Color.yellow);
                secondAfterNotValue = true;
            }

            if (gatesList[2] == "And")
            {
                finalPipeValue = AndFunction(secondValue, secondAfterNotValue);
            }
            if (gatesList[2] == "Or")
            {
                finalPipeValue = OrFunction(secondValue, secondAfterNotValue);
            }
            if (gatesList[2] == "Nand")
            {
                finalPipeValue = NandFunction(secondValue, secondAfterNotValue);
            }
            if (gatesList[2] == "Nor")
            {
                finalPipeValue = NorFunction(secondValue, secondAfterNotValue);
            }
            if (finalPipeValue)
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
    bool NotFunction(bool first)
    {
        return !first;
    }
}
