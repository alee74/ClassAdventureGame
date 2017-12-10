using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TMinusDaysScript : MonoBehaviour {

    public int daysToSurvive;

	public Text TMinusText;
    private int daysLeft;

	// Use this for initialization
	void Start () {
        TMinusText = gameObject.GetComponent<Text>();
        daysLeft = daysToSurvive - CampController.day;
	}
	
	// Update is called once per frame
	void Update () {
        if (daysLeft != -1)
        {
            TMinusText.text = "T-" + daysLeft + " days left until salvation";
            Invoke("ChangeScene", 3);
        }
        else
        {
            TMinusText.text = "The Avatar has returned and restored balance to the world. You and your party have been saved.";
        }

		if (daysLeft == 0) 
		{
			SceneManager.LoadScene ("WinScene"); 
		}
    }

    private void ChangeScene()
    {
        SceneManager.LoadScene("TestCamp");
    }
}
