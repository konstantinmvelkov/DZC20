using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CrystalHandler : MonoBehaviour
{
    public Animator anim;
    public int CrystalCount;
    public GameObject[] CrystalList;

    [SerializeField] GameObject inLevelMenu;
    [SerializeField] GameObject cancelButton;
    [SerializeField] GameObject cogratsText;

    public int TotalCrystals;

    Text text;

    private float sliderprogress;
    private float slidersize;
    private float slidervalue;
    public float FillSpeed;
    private Slider slider;
    //private ParticleSystem particleSys;

    // Start is called before the first frame update
    void Start()
    {
        text = GameObject.Find("CrystalScore").GetComponent<Text>();
        slider = GameObject.Find("CrystalSlider").GetComponent<Slider>();
        //particleSys = GameObject.Find("CrystalSliderParticles").GetComponent<ParticleSystem>();
        CrystalCount = 0;
        CrystalList = GameObject.FindGameObjectsWithTag("Crystal"); //List of active crystal gameobjects in scene
        TotalCrystals = CrystalList.Length; //Amount of active crystals at start of scene
        FillSpeed = 0.3f;
        slider.value = 0f;

    }


    // Update is called once per frame
    void Update()
    {
        CrystalList = GameObject.FindGameObjectsWithTag("Crystal"); //Find the current amount of active crystals in scene
        CrystalCount = TotalCrystals - CrystalList.Length; //Total crystals in scene - current in scene
        //Debug.Log("Total crystals in scene: "+ TotalCrystals);
        //Debug.Log("Collected Crystals: "+ CrystalCount);
        text.text = "Collected Crystals: " + CrystalCount.ToString() + " / " + TotalCrystals.ToString();
        sliderprogress = (float)(CrystalCount);
        slidersize = (float)(TotalCrystals);
        slidervalue = (sliderprogress / slidersize);
        //Debug.Log(particleSys.isPlaying);
        //particleSys.Play();
        if (slider.value < slidervalue)
        {
            slider.value += FillSpeed * Time.deltaTime;
            anim.updateMode = AnimatorUpdateMode.UnscaledTime;
            anim.SetInteger("AnimationPar", 2);
            StartCoroutine(Pause(0.0000015f));


            //if (!particleSys.isPlaying)
            //{
            //    particleSys.Play();
            //}
            
            //StartCoroutine(Wait(0.3f));
        }
        if (CrystalList.Length == 0)
        {
            victory();
            GameObject.Find("InLevelMenu").SetActive(true);
            Debug.Log("level completed");
        }


        IEnumerator Pause(float p)
        {
            Time.timeScale = 0.000001f;
            yield return new WaitForSeconds(p);
            Time.timeScale = 1;
            anim.SetInteger("AnimationPar", 1);
            anim.updateMode = AnimatorUpdateMode.Normal;
        }
        //IEnumerator Wait(float time)
        //{
            //anim.updateMode = AnimatorUpdateMode.UnscaledTime;
            //anim.SetInteger("AnimationPar", 2);
            //Time.timeScale = 0;
            //anim.Play("Base Layer.Grab", 0, 0f);
            //anim.updateMode = AnimatorUpdateMode.UnscaledTime;
            //anim.SetInteger("AnimationPar", 2);
            //Vector3 startingPos = transform.position;
            //Vector3 finalPos = transform.position;
            //float elapsedTime = 0;

            //while (elapsedTime < time)
            //{
            //    transform.position = Vector3.Lerp(startingPos, finalPos, (elapsedTime / time));
            //    elapsedTime += Time.deltaTime;
            //    yield return null;
            //}
            //anim.SetInteger("AnimationPar", 0);
        //}
        
        //else
        //{
        //    particleSys.Stop();
        //}
        //Debug.Log(sliderprogress/slidersize);
    }
    public void victory()
    {
        //Put here what needs to happen when the player completes the level
        inLevelMenu.SetActive(true);
        cancelButton.SetActive(false);
        cogratsText.SetActive(true);
        savelevel();
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