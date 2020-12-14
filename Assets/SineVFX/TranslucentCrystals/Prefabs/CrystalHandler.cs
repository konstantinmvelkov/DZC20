using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrystalHandler : MonoBehaviour
{
    public int CrystalCount;
    GameObject[] CrystalList;

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
            //if (!particleSys.isPlaying)
            //{
            //    particleSys.Play();
            //}
        }
        //else
        //{
        //    particleSys.Stop();
        //}
        //Debug.Log(sliderprogress/slidersize);
    }
}