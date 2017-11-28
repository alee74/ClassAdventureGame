﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This component is responsible for managing the world gen persistence between scenes.
/// </summary>
public class WorldPersist : MonoBehaviour {
    private const string PERSIST_PARENT_NAME = "PersistentWorld";

    private static GameObject persistParent;

    /// <summary>
    /// Causes the given GameObject to persist when the world map is loaded again later.
    /// </summary>
    public void PersistObject(GameObject obj) {
        Debug.Assert(persistParent != null);
        obj.transform.parent = persistParent.transform;
    }

    private void Awake() {
        if (persistParent == null) {
            // There is no persistent world, so create an empty one.
            persistParent = new GameObject(PERSIST_PARENT_NAME);
            persistParent.AddComponent<PersistentWorldObj>();
            DontDestroyOnLoad(persistParent);
        } else if (!persistParent.activeSelf) {
            // There is a persisted world, so re-activate it and disable world generation.
            persistParent.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}