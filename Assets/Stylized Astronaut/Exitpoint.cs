using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exitpoint : MonoBehaviour
{
    Player s1;

    private Animator anim;
    private CharacterController controller;

    public float speed = 200.0f;
    public float turnSpeed = 400.0f;
    public float gravity = 20.0f;
    public int interpolationFramesCount = 45; // Number of frames to completely interpolate between the 2 positions

    bool hasentered = false;

    float time = 0;

    GameObject[] CrystalList;

    // Start is called before the first frame update
    void Start()
    {
        s1 = GetComponent<Player>();
        controller = GetComponent<CharacterController>();
        anim = gameObject.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CrystalList = GameObject.FindGameObjectsWithTag("Crystal");
        if (CrystalList.Length == 0 && transform.position.x > -0.25 && transform.position.x < 0.25 && transform.position.y > -0.25 && transform.position.y < 0.25 && transform.position.z > -0.25 && transform.position.z < 0.25 && ((transform.rotation.eulerAngles.y < -135 && transform.rotation.eulerAngles.y > -185) || (transform.rotation.eulerAngles.y > 135 && transform.rotation.eulerAngles.y < 185))) 
        {
            //s1.movementList.Clear();
            s1.alternative.Clear();
            anim.SetInteger("AnimationPar", 1);
            StartCoroutine(JumpForward(0.3f));
            Debug.Log("Exitpoint executed");
            time += time + Time.deltaTime;
            Debug.Log(time);
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