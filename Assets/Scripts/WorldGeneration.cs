using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGeneration : MonoBehaviour {
    public string initString = "ABC";
    public float fwdDist = 5f;
    public int iterations = 13;
	public int grassDist = 5; // How many iterations of grass generation

	public LayerMask buildingMask;
	public LayerMask roadMask;

	public BoxCollider2D buildingBox;

	public float distFromRoad = 1.16f;

	private Dictionary<Vector2,int> tileList = new Dictionary<Vector2,int>(); //0=grass,1=obstacle
	private List<Vector2> process = new List<Vector2>();

    private int w = 2000;
    private int h = 2000;
	
	//private int[,] arr = new int[2000,2000];
    private WorldPersist persist;

	// tile orientation info *Reidlee*
	private Vector3 vinv = new Vector3(0,0,180);
    private Vector3 vr = new Vector3(0,0,270);
    private Vector3 vl = new Vector3(0,0,90);

	// tile information
	private GameObject[] grassTiles;
	private GameObject[] roadTiles;
	private GameObject[] buildingTiles;
	private int gtn = 14; // "grass tile num" -> how many grass tiles
	private int rtn = 10;
	private int btn = 1;

	void Awake() {
		directions = new bool[4];
		grassTiles = new GameObject[gtn];
		for (int i=0;i<gtn;i++) {
			grassTiles[i] = (GameObject)Resources.Load("Prefabs/Tiles/Grass/grass_"+i.toString());
		}
		roadTiles = new GameObject[rtn];
		for (int i=0;i<rtn;i++) {
			roadTiles[i] = (GameObject)Resources.Load("Prefabs/Tiles/Roads/road_"+i.toString());
		}
		buildingTiles = new GameObject[btn];
		for (int i=0;i<btn;i++) {
			buildingTiles[i] = (GameObject)Resources.Load("Prefabs/Tiles/Buildings/building_"+i.toString());
		}
	}

    void GenerateWorld() {
        persist = GetComponent<WorldPersist>();
        GenerateRoads();
		//GenerateTrees();
		GenerateGrass();
    }

    private void GenerateRoads() {
		var temp = List<Vector2>();
        Vector2 pos = new Vector2(0,0);
        int ang = 0;
        string lsys = IterateN(initString, iterations);
        foreach (char c in lsys) {
            if (c == 'F') {
                Vector2 delta = new Vector2(Mathf.Cos(ang * Mathf.Deg2Rad), Mathf.Sin(ang * Mathf.Deg2Rad));
                for (int i = 0; i < fwdDist; ++i) {
                    if (i == (int)fwdDist / 2) {
						switch(ang) {
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
					temp.Add(pos);
                    pos += delta;
                }
            } else if (c == '+') {
				if (ang==270) ang = 0;
                else ang += 90;
            } else if (c == '-') {
                if (ang==0) ang = 270;
				else ang -= 90;
            }
        }
		foreach (Vector2 pos in temp) PlaceTile(
    }

	void GenerateGrass() {
		var temp = new List<Vector2>();
		if (grassDist>0) {
			foreach (Vector2 pos in process) temp.AddRange(cardinals(pos));
			process.Clear();
			foreach (Vector2 pos in temp) PlaceGrass(pos);
			grassDist--;
		}
		GenerateGrass();
	}

	private List<Vector2> cardinals(Vector2 pos) {
		var temp = new List<Vector2>();
		temp.Add(new Vector2((pos.x-1,pos.y)));
		temp.Add(new Vector2((pos.x+1,pos.y)));
		temp.Add(new Vector2((pos.x,pos.y-1)));
		temp.Add(new Vector2((pos.x,pos.y+1)));
		return temp;
	}

	private void PlaceGrass(Vector2 pos) {
		if (!tileList.Contains(pos)) {
			tileList.Add(pos,0);
			process.Add(pos);
			var grass = Instantiate(grassTiles[BinCode(pos)], pos, Quaternion.identity);
			persist.PersistObject(grass);
		}
	}

	private int BinCode(Vector2 pos) {
	int code = 0;
		for (Vector2 dir in cardinals(pos)) {
			code <<= 1;
			var output;
			if (tileList.tryGetValue(pos, out output)) {
				code += output;
			}
		}
		return code;
	}

	//private enum roadNums { h1, h2, v1, v2, c1, c2, c3, c4, i3, i4 };
	//						   0   1   2   3   4   5   6  7   8   9
	private int TileCode(Vector2 pos) {
		switch(BinCode(pos)) {
			case 3:
				return (Random.Range(0,2));
			case 12:
				return (Random.Range(2,4));
			case 6:
				return 4;
			case 
		}
	}

	private void PlaceTile(Vector2 pos) {
		if (!tileList.Contains(pos)) {
			tileList.Add(pos,1);
			process.Add(pos);
			var road = Instantiate(roadTiles[TileCode(pos)], pos, Quaternion.identity);
			persist.PersistObject(road);
		}
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
