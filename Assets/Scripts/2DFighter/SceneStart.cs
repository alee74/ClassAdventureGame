using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// attatch to canvas.
/// instantiates enemy and sets names in UI.
/// </summary>
public class SceneStart : MonoBehaviour {

    public float xStart;
    public float yStart;

    private GameObject enemy;
    private string[] enemyType = new string[] { "Panda", "Bandit", "Mutant", "Rogue" };
    static private System.Random rando = new System.Random();

    /// <summary>
    /// called when scene is being loaded.
    /// instantiates enemy of random type at (xStart, yStart).
    /// sets enemy and player names in UI.
    /// </summary>
	void OnEnable () {

		Character character = CharInfo.getCurrentCharacter();
		GameObject.Find("PlayerName").GetComponent<Text>().text = character.name;

        string enemyName = enemyType[rando.Next(enemyType.Length)];
        enemy = Resources.Load<GameObject>("Prefabs/2DFighter/" + enemyName);
        Instantiate(enemy, new Vector3(xStart, yStart, 0f), Quaternion.identity);

        GameObject.Find("EnemyName").GetComponent<Text>().text = enemyName;
		
	}

}
