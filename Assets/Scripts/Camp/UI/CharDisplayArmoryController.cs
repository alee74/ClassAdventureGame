using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharDisplayArmoryController : MonoBehaviour {

    public Transform prefab;

    // Use this for initialization
    void Start()
    {


        for (int i = 0; i < CharInfo.characters.Count; i++)
        {
            Transform newItem = Instantiate(prefab, gameObject.transform, false) as Transform;
            newItem.GetComponent<CharDisplayArmory>().character = CharInfo.characters[i];
        }
    }

}
