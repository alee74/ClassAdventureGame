using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGen : MonoBehaviour {
    /// <summary>
    /// Initial string for the L-system.
    /// </summary>
    public string initString = "ABC";

    /// <summary>
    /// Distance to move forward when the L-system generates an F.
    /// </summary>
    public float fwdDist = 5f;

    /// <summary>
    /// Number of iterations of the L-system used to generate roads.
    /// </summary>
    public int iterations = 13;

	/// <summary>
	/// Layer masks for the layers building and road are on.
	/// </summary>
	public LayerMask buildingMask;
	public LayerMask roadMask;

	public BoxCollider2D buildingBox;

	public float distFromRoad = 1.16f;

	private Dictionary<Vector2,GameObject> map = new Dictionary<Vector2,GameObject>();
	private int tileNum = 0;
	public GameObject lastTile;
	public GameObject roadTile;
	public GameObject buildingTile;

    public int width = 200;
    public int height = 200;

    private int w = 2000;
    private int h = 2000;

    private int[,] arr = new int[2000,2000];

    private WorldPersist persist;

    void GenerateWorld() {
        persist = GetComponent<WorldPersist>();
        for(int i = 0; i < w; i++)
        {
            for(int j = 0; j < h; j++)
            {
                arr[i, j] = 0;
            }
        }
        print(arr[0, 0]);
        GenerateRoads();
		GetComponent<GrassGen>().GenerateGrass(map);
    }

    /// <summary>
    /// Generates a road system
    /// </summary>
    private void GenerateRoads() {
        Vector2 pos = new Vector2(0, 0);
        float ang = 0;
        string lsys = IterateN(initString, iterations);
        foreach (char c in lsys) {
            if (c == 'F') {
                Vector2 delta = new Vector2(Mathf.Cos(ang * Mathf.Deg2Rad), Mathf.Sin(ang * Mathf.Deg2Rad));
                for (int i = 0; i < fwdDist; ++i) {
                    arr[(int) Mathf.Round(pos.x) + w/2, (int) Mathf.Round(pos.y) + h/2] = 1;
                    if (i == (int)fwdDist / 2) {
						switch ((int)Mathf.Abs(ang)%360) {
							case 90:
								PlaceBuilding (pos + new Vector2 (distFromRoad, 0));
                                break;
							case 0:
							    PlaceBuilding (pos + new Vector2 (0, distFromRoad));
                                break;
							case 270:
							    PlaceBuilding (pos + new Vector2 (-distFromRoad, 0));
                                break;
							case 180:
							    PlaceBuilding (pos + new Vector2 (0, -distFromRoad));
                                break;
						}
					}
                    pos += delta;
                }
            } else if (c == '+') {
				if (ang==360) ang = 90;
                else ang += 90;
            } else if (c == '-') {
				if (ang==0) ang = 270;
                else ang -= 90;
            }
        }
        for(int i = 0; i < w; i++)
        {
            for (int j = 0; j < h; j++)
            {
                if(arr[i, j] == 1)
                {
                    PlaceTile(new Vector2(i - w / 2, j - h / 2));
                }
            }
        }
    }

    /// <summary>
    /// Places a road tile at the given position.
    /// </summary>
    private void PlaceTile(Vector2 pos) {
        var road = Instantiate(roadTile, pos, Quaternion.identity);
        persist.PersistObject(road);
    }

	private void PlaceBuilding(Vector2 pos){
		bool buildingCollide = Physics2D.OverlapBox(pos, buildingBox.size, buildingMask);
		if (Random.Range (0f, 1f) > 0.85f && !buildingCollide) {
			var building = Instantiate (buildingTile, pos, Quaternion.identity);
            persist.PersistObject(building);
		}
	}

    /// <summary>
    /// Runs n iterations of the L-system.
    /// </summary>
    private string IterateN(string prev, int n) {
        for (int i = 0; i < n; ++i) {
            prev = Iterate(prev);
        }
        return prev;
    }

    /// <summary>
    /// Takes a generated string and performs one iteration of the L system.
    /// </summary>
    /// <param name="prev">string to iterate on</param>
    /// <returns>the next iteration of prev</returns>
    private string Iterate(string prev) {
        string next = "";
        foreach (char c in prev) {
            if (c == 'A') {
                float rand = Random.Range(0, 100);
                if (rand < 50) {
                    next += "F+AC";
                } else {
                    next += "F++FAC";
                }
            } else if (c == 'B') next += "+FB+F";
            else if (c == 'C') next += "-FA+";
            else next += c;
        }
        return next;
    }
}
