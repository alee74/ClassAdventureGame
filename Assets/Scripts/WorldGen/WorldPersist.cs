using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This component is responsible for managing the world gen persistence between scenes.
/// </summary>
public class WorldPersist : MonoBehaviour {
    private const string PERSIST_PARENT_NAME = "PersistentWorld";
    private GameObject persistParent;
    private static GameObject world;

    /// <summary>
    /// Causes the given GameObject to persist when the world map is loaded again later.
    /// </summary>
    public void PersistObject(GameObject obj) {
        Debug.Assert(persistParent != null);
        obj.transform.parent = persistParent.transform;
    }

    private void Start() {
        if (persistParent == null) {
            // There is no persistent world, so create an empty one and then generate the world.
            persistParent = new GameObject(PERSIST_PARENT_NAME);
            persistParent.AddComponent<PersistentWorldObj>();
            DontDestroyOnLoad(persistParent);
            SendMessage("GenerateWorld");
        }
        // If there is a persisted world, it will re-activate itself.
    }
}
