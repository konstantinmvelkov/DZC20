  
using UnityEngine;
using System.Collections;
using UnityEditor;
using System;
using System.Collections.Generic;

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

    List<string> movementList = new List<string>();
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = gameObject.GetComponentInChildren<Animator>();
        stepCount = 0;  // Counts the amount of steps the player has taken so far. (is not used yet)
    }

    private IEnumerator ExecuteSequence()
    {
        if(movementList.Count == 0)
        {
            Debug.Log("No movements in sequence!");
        } 
        else
        {
            foreach (string element in movementList)
            {
                if (element.Equals("w") && !isTranslating)
                {
                    yield return StartCoroutine(SmoothLerp(0.3f));
                }
                else if (element.Equals("a") && !isTranslating)
                {
                    yield return StartCoroutine(Rotate(Vector3.up, -90, 0.2f));
                }
                else if (element.Equals("d") && !isTranslating)
                {
                    yield return StartCoroutine(Rotate(Vector3.up, 90, 0.2f));
                }
            }
        }

    }
    void Update()
    {
        //When G is pressed sequence will start
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
            movementList.RemoveAt(movementList.Count-1);
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
        }
    }
    public IEnumerator RunSquence()
    {
        yield return StartCoroutine(SmoothLerp(0.3f));

        yield return StartCoroutine(Rotate(Vector3.up, -90, 0.2f));

        yield return StartCoroutine(SmoothLerp(0.3f));
    }
    public IEnumerator SmoothLerp(float time)
    {
        isTranslating = true;
        stepCount++;
        anim.SetInteger("AnimationPar", 1);
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
        anim.SetInteger("AnimationPar", 0);
    }
    public IEnumerator Rotate(Vector3 axis, float angle, float duration = 1.0f)
    {
        isTranslating = true;
        stepCount++;
        anim.SetInteger("AnimationPar", 1);
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
        anim.SetInteger("AnimationPar", 0);
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
