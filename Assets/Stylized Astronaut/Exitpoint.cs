using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exitpoint : MonoBehaviour
{
    private Animator anim;
    private CharacterController controller;

    public float speed = 200.0f;
    public float turnSpeed = 400.0f;
    public float gravity = 20.0f;
    public int interpolationFramesCount = 45; // Number of frames to completely interpolate between the 2 positions

    bool hasentered = false;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = gameObject.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) // Placeholder statement
        {
            anim.SetInteger("AnimationPar", 2);
            StartCoroutine(JumpForward(0.3f));
        }
        IEnumerator JumpForward(float time)
        {
            Vector3 startingPos = transform.position;
            Vector3 finalPos = transform.position + transform.forward + transform.up * 0.5f;
            float elapsedTime = 0;

            while (elapsedTime < time)
            {
                transform.position = Vector3.Lerp(startingPos, finalPos, (elapsedTime / time));
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            hasentered = true;
            anim.SetInteger("AnimationPar", 0);
        }
    }
}