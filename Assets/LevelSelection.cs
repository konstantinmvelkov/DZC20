using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LevelSelection : MonoBehaviour
{
	
	public Transform cameraTarget1;
	public Transform cameraTarget2;
	public Transform cameraTarget3;
	public Transform cameraTarget4;
	public Transform cameraTarget5;
	public Transform cameraTarget6;
	public Transform cameraTarget7;
	public Transform cameraTarget8;
	public Transform cameraTarget9;
	public Transform cameraTarget10;
	public Transform cameraTarget11;
    public float sSpeed = 10.0f;
    public Vector3 dist;
    public Transform lookTarget;

	public Transform lookTarget1;
	public Transform lookTarget2;
	public Transform lookTarget3;
	public Transform lookTarget4;
	public Transform lookTarget5;
	public Transform lookTarget6;
	public Transform lookTarget7;
	public Transform lookTarget8;
	public Transform lookTarget9;
	public Transform lookTarget10;
	public Transform lookTarget11;
 	public Text Text;
	public Text Button;
	
	private int currenttarget;
	private Transform cameraTarget;
	private AudioSource Audio;
	private GameObject Camera;

	
    // Start is called before the first frame update
    void Start()
    {
        currenttarget = 1;
		SetCameraTarget(currenttarget);
		Text = GameObject.Find("Text").GetComponent<UnityEngine.UI.Text>();
		Camera = GameObject.Find("Main Camera");
		Audio = Camera.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("w"))
		{
			UpCamera();
		}
        if ((Input.GetKeyDown("s")) && currenttarget != 8)
		{
			DownCamera();
		}
        if ((Input.GetKeyDown("s")) && currenttarget == 8)
		{
			SkipCamera();
		}
		if ((Input.GetKeyDown("d")) && currenttarget == 3)
		{
			DCamera();
		}
		if ((Input.GetKeyDown("g")) && currenttarget != 1) //Play selected level
		{
			LoadLevel();
		}

		if (Input.anyKeyDown)
		{
			//Debug.Log(currenttarget);
			//Debug.Log(cameraTarget.name);
			//Debug.Log(lookTarget.name);

		}
		if (currenttarget == 1)
		{
			Text.text = "Martian Base: Select which level you want to play.";
		}
		if (currenttarget == 2)
		{
			Text.text = "Level 1: Introduction Crystals";
		}
		if (currenttarget == 3)
		{
			Text.text = "Level 2: Introduction Logical Circuits";
		}
		if (currenttarget == 4)
		{
			Text.text = "Level 3: ...";
		}
		if (currenttarget == 5)
		{
			Text.text = "Level 4: ";
		}
		if (currenttarget == 6)
		{
			Text.text = "Level 5: ...";
		}
		if (currenttarget == 7)
		{
			Text.text = "Level 6: Not made yet";
		}
		if (currenttarget == 8)
		{
			Text.text = "Level 7: Continuation on Circuits";
		}
		if (currenttarget == 9)
		{
			Text.text = "Level 8: ...";
		}
		if (currenttarget == 10)
		{
			Text.text = "Level 9: ...";
		}
		if (currenttarget == 11)
		{
			Text.text = "Level 10: ...";
		}

    }
	
	void FixedUpdate() {
        Vector3 dPos = cameraTarget.position + dist;
        Vector3 sPos = Vector3.Lerp(transform.position, dPos, sSpeed * Time.deltaTime);
        transform.position = sPos;
        transform.LookAt(lookTarget.position);
    }
	
	public void SetCameraTarget(int num){
		switch(num){
			case 1 :
				cameraTarget = cameraTarget1.transform;
				lookTarget = lookTarget1.transform;
				break;
			case 2 :
				cameraTarget = cameraTarget2.transform;
				lookTarget = lookTarget2.transform;
				break;
			case 3 :
				cameraTarget = cameraTarget3.transform;
				lookTarget = lookTarget3.transform;
				break;
			case 4 :
				cameraTarget = cameraTarget4.transform;
				lookTarget = lookTarget4.transform;
				break;
			case 5 :
				cameraTarget = cameraTarget5.transform;
				lookTarget = lookTarget5.transform;
				break;
			case 6 :
				cameraTarget = cameraTarget6.transform;
				lookTarget = lookTarget6.transform;
				break;
			case 7 :
				cameraTarget = cameraTarget7.transform;
				lookTarget = lookTarget7.transform;
				break;
			case 8 :
				cameraTarget = cameraTarget8.transform;
				lookTarget = lookTarget8.transform;
				break;
			case 9 :
				cameraTarget = cameraTarget9.transform;
				lookTarget = lookTarget9.transform;
				break;
			case 10 :
				cameraTarget = cameraTarget10.transform;
				lookTarget = lookTarget10.transform;
				break;
			case 11 :
				cameraTarget = cameraTarget11.transform;
				lookTarget = lookTarget11.transform;
				break;
		}
	}
	
	public void UpCamera(){
		if(currenttarget < 11)
			currenttarget++;
		else 
			currenttarget = 11;
		SetCameraTarget(currenttarget);
		PlaySound();

	}
	public void DownCamera(){
		if(currenttarget > 1)
			currenttarget--;
		else 
			currenttarget = 1;
		SetCameraTarget(currenttarget);
		PlaySound();
	}
	public void DCamera(){
		if(currenttarget == 3)
			currenttarget = 8;
		SetCameraTarget(currenttarget);
		PlaySound();
	}
	public void SkipCamera(){
		if(currenttarget == 8)
			currenttarget = 3;
		SetCameraTarget(currenttarget);
		PlaySound();
	}

	public void PlaySound()
	{
		Audio.Play();
	}
	public void LoadLevel()
	{
		if (currenttarget != 1)
		{
			SceneManager.LoadScene(currenttarget);
		}

	}
}
