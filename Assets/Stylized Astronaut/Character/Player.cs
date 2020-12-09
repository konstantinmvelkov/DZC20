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
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = gameObject.GetComponentInChildren<Animator>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && !isTranslating)
        {
            anim.SetInteger("AnimationPar", 1);
            isTranslating = true;
            StartCoroutine(Rotate(Vector3.up, -90, 0.2f));
        }
        if (Input.GetKeyDown(KeyCode.D) && !isTranslating)
        {   
            anim.SetInteger("AnimationPar", 1);
            isTranslating = true;
            StartCoroutine(Rotate(Vector3.up, 90, 0.2f));
        }

        if (Input.GetKeyDown(KeyCode.W) && !isTranslating)
        {
            anim.SetInteger("AnimationPar", 1);
            isTranslating = true;
            StartCoroutine(SmoothLerp(0.3f));
        }

        IEnumerator SmoothLerp(float time)
        {
            Vector3 startingPos = transform.position;
            Vector3 finalPos = transform.position + transform.forward;
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
}
