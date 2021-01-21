using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogicLevel7 : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject firstFinal;
    [SerializeField] GameObject secondFinal;
    [SerializeField] GameObject finalPipe;

    [SerializeField] Button btnAnd;
    [SerializeField] Button btnOr;
    [SerializeField] Button btnNand;
    [SerializeField] Button btnNor;

    [SerializeField] Button btnDelete;
    [SerializeField] Button btnGo;

    bool firstPipe1 = false;
    bool firstPipe2 = true;
    bool secondPipe1 = false;
    bool secondPipe2 = false;
    bool firstF = false;
    bool secondF = false;
    bool final = false;

    public List<string> gatesList = new List<string>();

    // Start is called before the first frame update
    private void Start()
    {
        btnAnd.onClick.AddListener(BtnAndClicked);
        btnOr.onClick.AddListener(BtnOrClicked);
        btnNand.onClick.AddListener(BtnNandClicked);
        btnNor.onClick.AddListener(BtnNorClicked);
        btnDelete.onClick.AddListener(BtnDeleteClicked);
        btnGo.onClick.AddListener(BtnGoClicked);
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

    public void BtnAndClicked()
    {
        if (gatesList.Count < 2)
        {
            Debug.Log("Adding And");
            gatesList.Add("And");
        }
    }

    public void BtnOrClicked()
    {
        if (gatesList.Count < 2)
        {
            Debug.Log("Adding Or");
            gatesList.Add("Or");
        }
    }

    public void BtnNandClicked()
    {
        if (gatesList.Count < 2)
        {
            Debug.Log("Adding Nand");
            gatesList.Add("Nand");
        }
    }

    public void BtnNorClicked()
    {
        if (gatesList.Count < 2)
        {
            Debug.Log("Adding Nor");
            gatesList.Add("Nor");
        }
    }

    public void BtnDeleteClicked()
    {
        gatesList.Clear();
        var pipeRenderer = finalPipe.GetComponent<Renderer>();
        Debug.Log("Clearing gates list and fixing pipes");
        //Call SetColor using the shader property name "_Color" and setting the color to red
        pipeRenderer.material.SetColor("_Color", Color.black);

        pipeRenderer = firstFinal.GetComponent<Renderer>();
        Debug.Log("Clearing gates list and fixing pipes");
        //Call SetColor using the shader property name "_Color" and setting the color to red
        pipeRenderer.material.SetColor("_Color", Color.black);

        pipeRenderer = secondFinal.GetComponent<Renderer>();
        Debug.Log("Clearing gates list and fixing pipes");
        //Call SetColor using the shader property name "_Color" and setting the color to red
        pipeRenderer.material.SetColor("_Color", Color.black);
    }

    public void BtnGoClicked()
    {
        if (gatesList.Count > 0)
        {
            if (gatesList[0] == "And")
            {
                firstF = AndFunction(firstPipe1, firstPipe2);
            }
            if (gatesList[0] == "Or")
            {
                firstF = OrFunction(firstPipe1, firstPipe2);
            }
            if (gatesList[0] == "Nand")
            {
                firstF = NandFunction(firstPipe1, firstPipe2);
            }
            if (gatesList[0] == "Nor")
            {
                firstF = NorFunction(firstPipe1, firstPipe2);
            }

            if (firstF)
            {
                var pipeRenderer = firstFinal.GetComponent<Renderer>();

                //Call SetColor using the shader property name "_Color" and setting the color to red
                pipeRenderer.material.SetColor("_Color", Color.yellow);
            }

            if (gatesList[1] == "And")
            {
                secondF = AndFunction(secondPipe1, secondPipe2);
            }
            if (gatesList[1] == "Or")
            {
                secondF = OrFunction(secondPipe1, secondPipe2);
            }
            if (gatesList[1] == "Nand")
            {
                secondF = NandFunction(secondPipe1, secondPipe2);
            }
            if (gatesList[1] == "Nor")
            {
                secondF = NorFunction(secondPipe1, secondPipe2);
            }

            if (secondF)
            {
                var pipeRenderer = secondFinal.GetComponent<Renderer>();

                //Call SetColor using the shader property name "_Color" and setting the color to red
                pipeRenderer.material.SetColor("_Color", Color.yellow);
            }

            final = AndFunction(firstF, secondF);
            if (final)
            {
                //Get the Renderer component from the new cube
                var pipeRenderer = finalPipe.GetComponent<Renderer>();

                //Call SetColor using the shader property name "_Color" and setting the color to red
                pipeRenderer.material.SetColor("_Color", Color.yellow);
                GameObject.Find("Sphere").SetActive(false);
                GameObject.Find("Crystalsv05").SetActive(false);
            }
        }
    }

    /*// Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && gatesList.Count < 2)
        {
            Debug.Log("Adding And");
            gatesList.Add("And");
        }
        if (Input.GetKeyDown(KeyCode.O) && gatesList.Count < 2)
        {
            Debug.Log("Adding Or");
            gatesList.Add("Or");
        }
        if (Input.GetKeyDown(KeyCode.N) && gatesList.Count < 2)
        {
            Debug.Log("Adding Nand");
            gatesList.Add("Nand");
        }
        if (Input.GetKeyDown(KeyCode.R) && gatesList.Count < 2)
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

            pipeRenderer = firstFinal.GetComponent<Renderer>();
            Debug.Log("Clearing gates list and fixing pipes");
            //Call SetColor using the shader property name "_Color" and setting the color to red
            pipeRenderer.material.SetColor("_Color", Color.black);

            pipeRenderer = secondFinal.GetComponent<Renderer>();
            Debug.Log("Clearing gates list and fixing pipes");
            //Call SetColor using the shader property name "_Color" and setting the color to red
            pipeRenderer.material.SetColor("_Color", Color.black);
        }
        if (Input.GetKeyDown(KeyCode.G) && gatesList.Count > 0)
        {

            if (gatesList[0] == "And")
            {
                firstF = AndFunction(firstPipe1, firstPipe2);
            }
            if (gatesList[0] == "Or")
            {
                firstF = OrFunction(firstPipe1, firstPipe2);
            }
            if (gatesList[0] == "Nand")
            {
                firstF = NandFunction(firstPipe1, firstPipe2);
            }
            if (gatesList[0] == "Nor")
            {
                firstF = NorFunction(firstPipe1, firstPipe2);
            }

            if (firstF)
            {
                var pipeRenderer = firstFinal.GetComponent<Renderer>();

                //Call SetColor using the shader property name "_Color" and setting the color to red
                pipeRenderer.material.SetColor("_Color", Color.yellow);
            }

            if (gatesList[1] == "And")
            {
                secondF = AndFunction(secondPipe1, secondPipe2);
            }
            if (gatesList[1] == "Or")
            {
                secondF = OrFunction(secondPipe1, secondPipe2);
            }
            if (gatesList[1] == "Nand")
            {
                secondF = NandFunction(secondPipe1, secondPipe2);
            }
            if (gatesList[1] == "Nor")
            {
                secondF = NorFunction(secondPipe1, secondPipe2);
            }

            if (secondF)
            {
                var pipeRenderer = secondFinal.GetComponent<Renderer>();

                //Call SetColor using the shader property name "_Color" and setting the color to red
                pipeRenderer.material.SetColor("_Color", Color.yellow);
            }

            final = AndFunction(firstF, secondF);
            if (final)
            {
                //Get the Renderer component from the new cube
                var pipeRenderer = finalPipe.GetComponent<Renderer>();

                //Call SetColor using the shader property name "_Color" and setting the color to red
                pipeRenderer.material.SetColor("_Color", Color.yellow);
            }
        }
    }*/
}
