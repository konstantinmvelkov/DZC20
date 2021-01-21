using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            savelevel();
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

    void savelevel()
    {
        Scene currentScene = SceneManager.GetActiveScene ();
        string sceneName = currentScene.name;
        if (sceneName == "Level 1") 
        {
            PlayerPrefs.SetInt("level1", 1);
        }
        else if (sceneName == "Level 2")
        {
            PlayerPrefs.SetInt("level2", 1);
        }
        else if (sceneName == "Level 3")
        {
            PlayerPrefs.SetInt("level3", 1);
        }
        else if (sceneName == "Level 4")
        {
            PlayerPrefs.SetInt("level4", 4);
        }
        else if (sceneName == "Level 5")
        {
            PlayerPrefs.SetInt("level5", 3);
        }
        else if (sceneName == "Level 6")
        {
            PlayerPrefs.SetInt("level6", 2);
        }
        else if (sceneName == "Level 7")
        {
            PlayerPrefs.SetInt("level7", 1);
        }
        else if (sceneName == "Level 8")
        {
            PlayerPrefs.SetInt("level8", 1);
        }
        else if (sceneName == "Level 9")
        {
            PlayerPrefs.SetInt("level9", 1);
        }
        else if (sceneName == "Level 10")
        {
            PlayerPrefs.SetInt("level10", 1);
        }
        PlayerPrefs.Save();
    }
}