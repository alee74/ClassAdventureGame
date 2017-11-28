using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This component is attached to the persistent world object, and simply disables
/// the gameobject when a new scene loads.
/// </summary>
public class PersistentWorldObj : MonoBehaviour {
    void Start() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene s, LoadSceneMode m) {
        gameObject.SetActive(false);
    }
}
