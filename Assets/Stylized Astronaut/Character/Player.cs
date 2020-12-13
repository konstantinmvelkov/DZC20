  
using UnityEngine;
using System.Collections;
using UnityEditor;
using System;

public class Player : MonoBehaviour
{

    private Animator anim;
    private CharacterController controller;

    public float speed = 200.0f;
    public float turnSpeed = 400.0f;
    public float gravity = 20.0f;
    public int interpolationFramesCount = 45; // Number of frames to completely interpolate between the 2 positions

    bool isTranslating = false;

    int stepCount;

    bool UpLastTick = true;
    bool DownLastTick = false;



    
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = gameObject.GetComponentInChildren<Animator>();
        stepCount = 0;  // Counts the amount of steps the player has taken so far. (is not used yet)
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && !isTranslating)
        {
            anim.SetInteger("AnimationPar", 1);
            isTranslating = true;
            stepCount++;
            StartCoroutine(Rotate(Vector3.up, -90, 0.2f));
        }
        if (Input.GetKeyDown(KeyCode.D) && !isTranslating)
        {   
            anim.SetInteger("AnimationPar", 1);
            isTranslating = true;
            stepCount++;
            StartCoroutine(Rotate(Vector3.up, 90, 0.2f));
        }

        if (Input.GetKeyDown(KeyCode.W) && !isTranslating)
        {
            anim.SetInteger("AnimationPar", 1);
            isTranslating = true;
            stepCount++;
            StartCoroutine(SmoothLerp(0.3f));
        }
        IEnumerator SmoothLerp(float time)
        {
            Vector3 startingPos = transform.position;
            Vector3 finalPos = transform.position + transform.forward;
            
            Collider[] Intersecting = Physics.OverlapSphere(startingPos, 0.02f); // This detects intersecting objects including the plate that indicates stairs(>0.02f detects ground)
            // Might have to change the value of 0.02f if we use a different model for the robot...
            
            if (Intersecting.Length != 0) //This is true if there are intersecting objects nearby, triggers, crystals etc..
            {
                for (var i = 0; i < Intersecting.Length; i++) 
                {
                if (Intersecting[i].tag == "StairsUp" && transform.eulerAngles == Intersecting[i].transform.eulerAngles) //Stairs up condition
                    {
                        finalPos = transform.position + transform.forward + 0.25f*transform.up; //Modify final pos to include height difference
                        //Debug.Log(Intersecting[i].name);
                    }
                if (Intersecting[i].tag == "StairsDown" && transform.eulerAngles == Intersecting[i].transform.eulerAngles) //Stairs down condition
                    {
                        finalPos = transform.position + transform.forward - 0.25f*transform.up; //Modify final pos to include height difference
                        // Debug.Log(Intersecting[i].name);
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

        IEnumerator Rotate(Vector3 axis, float angle, float duration = 1.0f)
        {
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


    }
    private void HeightCheck()
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
