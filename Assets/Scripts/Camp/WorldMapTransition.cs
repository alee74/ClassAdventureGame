using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldMapTransition : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D col)
    {
        PlayerPrefs.SetFloat("X", 0);
        PlayerPrefs.SetFloat("Y", 0);
        PlayerPrefs.SetFloat("Z", -1);
        SceneManager.LoadScene("WorldMapMainScene");
    }

}