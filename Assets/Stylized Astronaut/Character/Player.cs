  
using UnityEngine;
using System.Collections;
using UnityEditor;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private Animator anim;
    private CharacterController controller;

    public float speed = 200.0f;
    public float turnSpeed = 400.0f;
    public float gravity = 20.0f;
    public int interpolationFramesCount = 45; // Number of frames to completely interpolate between the 2 positions

    bool isTranslating = false;

    public int stepCount;

    public bool UpLastTick = true;
    public bool DownLastTick = false;

    [SerializeField] Button btnUp;
    [SerializeField] Button btnLeft;
    [SerializeField] Button btnRight;
    [SerializeField] Button btnGo;
    [SerializeField] Button btnDelete;

    public bool executeSequence = false;

    List<string> alternative;
   /* List<string> movementList = new List<string>();*/
    void Start()
    {
        alternative = new List<string>();
        controller = GetComponent<CharacterController>();
        anim = gameObject.GetComponentInChildren<Animator>();
        stepCount = 0;  // Counts the amount of steps the player has taken so far. (is not used yet)

        //Button listeners
        btnUp.onClick.AddListener(BtnUpPressed);
        btnLeft.onClick.AddListener(BtnLeftPressed);
        btnRight.onClick.AddListener(BtnRightPressed);
        btnGo.onClick.AddListener(BtnGoPressed);
        btnDelete.onClick.AddListener(BtnDeletePressed);
    }

    public IEnumerator ExecuteSequence()
    {
        //PrintSequence();
        /*Debug.Log("Adding an extra up move");
        yield return StartCoroutine(SmoothLerp(0.3f));*/
        if (alternative.Count == 0)
        {
            Debug.Log("No movements in sequence!");
        }
        else
        {
            foreach (string element in alternative)
            {
                if (element.Equals("w"))
                {
                    Debug.Log("Starting W");
                    yield return StartCoroutine(SmoothLerp(0.3f));
                }
                else if (element.Equals("a"))
                {
                    Debug.Log("Starting A");
                    yield return StartCoroutine(Rotate(Vector3.up, -90, 0.2f));
                }
                else if (element.Equals("d"))// && !isTranslating)
                {
                    Debug.Log("Starting D");
                    yield return StartCoroutine(Rotate(Vector3.up, 90, 0.2f));
                }
            }
        }
        isTranslating = false;
    }
    void Update()
    {
        if (executeSequence == true && !isTranslating)
        {
            StartCoroutine(ExecuteSequence());
            executeSequence = false;
        }
        else
        {
            Debug.Log("Inside Update but translating and/or executeSequence == false");
        }

        //StartCoroutine(ExecuteSequence());
        /*//When G is pressed sequence will start
        if (Input.GetKeyDown(KeyCode.G) && !isTranslating)
        {
            Debug.Log("Starting the sequence");
            StartCoroutine(ExecuteSequence());
        }
        //When H is pressed sequence is deleted
        if (Input.GetKeyDown(KeyCode.H) && !isTranslating)
        {
            Debug.Log("Sequence deleted");
            movementList.Clear();
        }
        //When R is pressed last is removed
        if (Input.GetKeyDown(KeyCode.R) && !isTranslating)
        {
            Debug.Log("Last element removed");
            movementList.RemoveAt(movementList.Count - 1);
        }
        if (Input.GetKeyDown(KeyCode.A) && !isTranslating)
        {
            movementList.Add("a");
            Debug.Log("Added element a");
        }
        if (Input.GetKeyDown(KeyCode.D) && !isTranslating)
        {
            movementList.Add("d");
            Debug.Log("Added element d");
        }
        if (Input.GetKeyDown(KeyCode.W) && !isTranslating)
        {
            movementList.Add("w");
            Debug.Log("Added element w");
        }*/
    }
    public void PrintSequence()
    {
        if(alternative.Count == 0)
        {
            Debug.LogError("EMPTY MOVEMENTLIST!");
            return;
        }
        foreach (string movement in alternative)
        {
            Debug.Log(movement);
        }
    }

    public void BtnGoPressed()
    {
        PrintSequence();
        Debug.Log("Starting the sequence");
        executeSequence = true;
        StartCoroutine(ExecuteSequence());
    }

    public void BtnDeletePressed()
    {
        if (!isTranslating)
        {
            alternative.Clear();
            Debug.Log("Btn Delete was pressed");
        }
        else
        {
            Debug.Log("Translating");
        }
    }

    public void BtnLastPressed()
    {
        if (!isTranslating)
        {
            alternative.RemoveAt(alternative.Count - 1);
            Debug.Log("Last element removed");
        }
        else
        {
            Debug.Log("Translating");
        }
    }

    public void BtnLeftPressed()
    {
        if (!isTranslating)
        {
            alternative.Add("a");
            Debug.Log("Btn Left was pressed");
        }
        else
        {
            Debug.Log("Translating");
        }
    }

    public void BtnUpPressed()
    {
        if (!isTranslating)
        {
            alternative.Add("w");
            Debug.Log("Btn Up was pressed");
        }
        else
        {
            Debug.Log("Translating");
        }
        PrintSequence();
    }

    public void BtnRightPressed()
    {
        if (!isTranslating)
        {
            alternative.Add("d");
            Debug.Log("Btn right was pressed");
        }
        else
        {
            Debug.Log("Translating");
        }
        PrintSequence();
    }

    public IEnumerator RunSquence()
    {
        Debug.Log("Inside RunSequence");

        yield return StartCoroutine(SmoothLerp(0.3f));

        yield return StartCoroutine(Rotate(Vector3.up, -90, 0.2f));

        yield return StartCoroutine(SmoothLerp(0.3f));
    }
    public IEnumerator SmoothLerp(float time)
    {
        isTranslating = true;
        stepCount++;
        /*anim.SetInteger("AnimationPar", 1);*/
        Vector3 startingPos = transform.position;
        Vector3 finalPos = transform.position + transform.forward;

        Collider[] Intersecting = Physics.OverlapSphere(startingPos, 0.1f); // This detects intersecting objects including the plate that indicates stairs
                                                                            // Might have to change the value if we use a different model for the robot...

        if (Intersecting.Length != 0) //This is true if there are intersecting objects nearby, triggers, crystals etc..
        {
            for (var i = 0; i < Intersecting.Length; i++)
            {
                Debug.Log(Intersecting[0].name);
                if (Intersecting[i].tag == "StairsUp" && transform.eulerAngles == Intersecting[i].transform.eulerAngles) //Stairs up condition
                {
                    finalPos = transform.position + transform.forward + 0.25f * transform.up; //Modify final pos to include height difference
                    Debug.Log(Intersecting[i].name);
                }
                if (Intersecting[i].tag == "StairsDown" && transform.eulerAngles == Intersecting[i].transform.eulerAngles) //Stairs down condition
                {
                    finalPos = transform.position + transform.forward - 0.25f * transform.up; //Modify final pos to include height difference
                    Debug.Log(Intersecting[i].name);
                }
            }

        }

        float elapsedTime = 0;

        while (elapsedTime < time)
        {
            transform.position = Vector3.Lerp(startingPos, finalPos, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        isTranslating = false;
        /*anim.SetInteger("AnimationPar", 0);*/
    }
    public IEnumerator Rotate(Vector3 axis, float angle, float duration = 1.0f)
    {
        isTranslating = true;
        stepCount++;
        /*anim.SetInteger("AnimationPar", 1);*/
        Quaternion from = transform.rotation;
        Quaternion to = transform.rotation;
        to *= Quaternion.Euler(axis * angle);

        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            transform.rotation = Quaternion.Slerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        isTranslating = false;
       /* anim.SetInteger("AnimationPar", 0);*/
        transform.rotation = to;
    }
    private void HeightCheck() //not used right now
    {
        Debug.Log("Raycast script running");
        Vector3 origin = transform.position + 0.5f*transform.up;
        Vector3 direction = -transform.up;
        Debug.Log(origin);
        Debug.Log(direction);
        Debug.DrawRay(origin, direction * 10f, Color.red);
        Ray ray = new Ray(origin, direction);
        if (Physics.Raycast(origin, direction, 0.6f) && !UpLastTick)
        {
            transform.position = transform.position + 0.01f * transform.up;
            UpLastTick = true;
            DownLastTick = false;
            }
        else{
            transform.position = transform.position - 0.01f * transform.up;
            UpLastTick = false;
            DownLastTick = true;
            }

    }
}
