using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalCrystalsCollected : MonoBehaviour
{
    // Start is called before the first frame update
    public int CrystalSum; //The collective number of all crystals in all levels collected
    public Text Text;
    void Start()
    {
        CrystalSum = PlayerPrefs.GetInt("level1")+PlayerPrefs.GetInt("level2")+PlayerPrefs.GetInt("level3")+PlayerPrefs.GetInt("level4")+PlayerPrefs.GetInt("level5")+PlayerPrefs.GetInt("level7")+PlayerPrefs.GetInt("level8")+PlayerPrefs.GetInt("level9")+PlayerPrefs.GetInt("level10");
		Text = GameObject.Find("TotalCrystalsCollected").GetComponent<UnityEngine.UI.Text>();
    }

    // Update is called once per frame
    void Update()
    {
        //Here we need to count all crystals from all completed levels together.
        Text.text = "You have collected " + CrystalSum.ToString() + "/14 crystals.";
        
    }
}
