using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GUIManager : MonoBehaviour
{

    private GameObject storeSelected;
    public Camera mainCamera;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        /*
		if (ES.currentSelectedGameObject != storeSelected)
        {
            if (ES.currentSelectedGameObject == null)
                ES.SetSelectedGameObject(storeSelected);
             else
                storeSelected = ES.currentSelectedGameObject;
        }
        */
    }

    public void Play()
    {
        SceneManager.LoadScene("PlayScene");
    }
    public void PlayAgain()
    {
        SceneManager.LoadScene("TestCamp");
    }

    public void Tutorial()
    {
        SceneManager.LoadScene("TutScene");
    }

    public void Quit()
    {
        // UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

}