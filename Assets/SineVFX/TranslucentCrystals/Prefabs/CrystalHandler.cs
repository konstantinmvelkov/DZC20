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
    
    //private Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        text=GameObject.Find("CrystalScore").GetComponent<Text>();
        //slider=GameObject.Find("CrystalSlider").GetComponent<Slider>();
        CrystalCount = 0;
        CrystalList = GameObject.FindGameObjectsWithTag("Crystal"); //List of active crystal gameobjects in scene
        TotalCrystals = CrystalList.Length; //Amount of active crystals at start of scene
        
    }


    // Update is called once per frame
    void Update()
    {
        CrystalList = GameObject.FindGameObjectsWithTag("Crystal"); //Find the current amount of active crystals in scene
        CrystalCount = TotalCrystals - CrystalList.Length; //Total crystals in scene - current in scene
        //Debug.Log("Total crystals in scene: "+ TotalCrystals);
        //Debug.Log("Collected Crystals: "+ CrystalCount);
        text.text="Collected Crystals: "+CrystalCount.ToString()+" / "+ TotalCrystals.ToString();

        //slider.value = (CrystalCount/TotalCrystals);
        
    }
}
