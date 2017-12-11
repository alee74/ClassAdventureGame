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

	public float distFromRoad = 1.16f;

    private WorldPersist persist;

	// tile information
	private Dictionary<Vector2,int> tileList;
	private List<Vector2> process;
	private GameObject[] grassTiles;
	private GameObject[] roadTiles;
	private GameObject[] buildingTiles;
	private int gtn = 14; // "grass tile num" -> how many grass tiles
	private int rtn = 13;
	private int btn = 1;

    void GenerateWorld() {
        persist = GetComponent<WorldPersist>();
		grassTiles = new GameObject[gtn];
		tileList = new Dictionary<Vector2,int>();
		process = new List<Vector2>();
		for (int i=0;i<gtn;i++) {
			grassTiles[i] = (GameObject)Resources.Load("Prefabs/Tiles/Grass/grass_"+i);
		}
		roadTiles = new GameObject[rtn];
		for (int i=0;i<rtn;i++) {
			roadTiles[i] = (GameObject)Resources.Load("Prefabs/Tiles/Roads/road_"+i);
		}
		buildingTiles = new GameObject[btn];
		for (int i=0;i<btn;i++) {
			buildingTiles[i] = (GameObject)Resources.Load("Prefabs/Tiles/Buildings/building_"+i);
		}
        GenerateRoads();
		GenerateGrass();
    }

    private void GenerateRoads() {
		var temp = new List<Vector2>();
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
		foreach (Vector2 vec in temp) PlaceTile(vec);
    }

	void GenerateGrass() {
		if (grassDist>0) {
			var temp = new List<Vector2>();
			foreach (Vector2 pos in process) temp.AddRange(cardinals(pos));
			process.Clear();
			foreach (Vector2 pos in temp) PlaceGrass(pos);
			grassDist--;
			GenerateGrass();
		}
	}

	private void PlaceTile(Vector2 pos) {
		if (!tileList.ContainsKey(pos)) {
			tileList.Add(pos,1);
			process.Add(pos);
			var road = Instantiate(roadTiles[TileCode(pos)], pos, Quaternion.identity);
			persist.PersistObject(road);
		}
    }

	private void PlaceBuilding(Vector2 pos){
		int temp = Random.Range(0,btn);
		bool buildingCollide = Physics2D.OverlapBox(pos, buildingTiles[temp].GetComponent<BoxCollider2D>().size, buildingMask);
		if (Random.Range (0f, 1f) > 0.85f && !buildingCollide) {
			var building = Instantiate (buildingTiles[temp], pos, Quaternion.identity);
            persist.PersistObject(building);
		}
	}

	private void PlaceGrass(Vector2 pos) {
		if (!tileList.ContainsKey(pos)) {
			tileList.Add(pos,0);
			process.Add(pos);
			var grass = Instantiate(grassTiles[BinCode(pos)], pos, Quaternion.identity);
			persist.PersistObject(grass);
		}
	}


	private List<Vector2> cardinals(Vector2 pos) {
		var temp = new List<Vector2>();
		temp.Add(new Vector2((pos.x-1),pos.y));
		temp.Add(new Vector2((pos.x+1),pos.y));
		temp.Add(new Vector2(pos.x,(pos.y-1)));
		temp.Add(new Vector2(pos.x,(pos.y+1)));
		return temp;
	}

	private int BinCode(Vector2 pos) {
		int code = 0;
		int output;
		var temp = cardinals(pos);
		foreach (Vector2 dir in temp) {
			code += code;
			if (tileList.TryGetValue(pos, out output)) {
				code += output;
			}
		}
		return code;
	}

	private int TileCode(Vector2 pos) {
		switch(BinCode(pos)) {
			case 3:
				return (Random.Range(0,2));
			case 12:
				return (Random.Range(2,4));
			case 6:
				return 4;
			case 5:
				return 5;
			case 10:
				return 6;
			case 9:
				return 7;
			case 11:
				return 8;
			case 13:
				return 9;
			case 7:
				return 10;
			case 14:
				return 11;
			default:
				return 12;
		}
	}
    private string IterateN(string prev, int n) {
        for (int i = 0; i < n; ++i) {
            prev = Iterate(prev);
        }
        return prev;
    }

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
