using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeGen : MonoBehaviour {
    public GameObject treefab;

    /// <summary>
    /// Size of the square area around 0, 0 where trees will be spawned.
    /// </summary>
    public float treeZoneSize = 100f;

    /// <summary>
    /// Number of forests to generate.
    /// </summary>
    public int numForests = 20;

    public float treeRad = 5f;
    public float treeChance = 0.4f;
    public int treeIters = 10;

    public int maxTrees = 500;

    void Start() {
        for (int i = 0; i < numForests; ++i) {
            var pos = new Vector2(Random.Range(-treeZoneSize, treeZoneSize),
                                  Random.Range(-treeZoneSize, treeZoneSize));
            GenForest(pos, treeRad, treeChance, treeIters);
        }
    }

    void GenForest(Vector2 start, float radius, float chance, int iters) {
        List<Vector2> forest = new List<Vector2>();
        forest.Add(start);

        for (int i = 0; i < iters; ++i) {
            IterateForest(ref forest, radius, chance);
        }

        foreach (var pos in forest) {
            var tree = Instantiate(treefab, pos, Quaternion.identity);
            tree.transform.parent = transform;
        }
    }

    /// <summary>
    /// Runs one iteration of tree growth for the given forest.
    /// </summary>
    /// <param name="forest">reference to the forest to grow</param>
    /// <param name="radius">max distance between a tree and its parent</param>
    /// <param name="chance">chance between 1 and 0 for each tree to spawn another</param>
    void IterateForest(ref List<Vector2> forest, float radius, float chance) {
        for (int i = 0; i < forest.Count && forest.Count < 500; ++i) {
            if (Random.Range(0.0f, 1.0f) > chance) continue;
            float r = Random.Range(0.0f, radius);
            float ang = Random.Range(0.0f, 360f);
            Vector2 pos = forest[i] + new Vector2(Mathf.Cos(ang * Mathf.Deg2Rad), -Mathf.Sin(ang * Mathf.Deg2Rad)) * r;
            forest.Add(pos);
        }
    }
}
