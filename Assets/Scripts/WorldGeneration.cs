using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGeneration : MonoBehaviour {
    public string initString = "ABC";
    public float fwdDist = 5f;
    public int iterations = 16;
	public int grassDist = 8; // How many iterations of grass generation
	public int treeDist = 10;
	public int randTrees = 90;
	public int scope = 100;
	public int lakeChance = 8;
	public int lakeRandomness = 3;
	public int lakeSize = 5;
	public int trl = 0;
	public int trh = 14;

	public GameObject lakeTile;

	public LayerMask buildingMask;
	public LayerMask roadMask;

	public float distFromRoad = 1.16f;

    private WorldPersist persist;

	// tile information
	private List<string> binary = new List<string> {
		"0000",
		"0001",
		"0010",
		"0011",
		"0100",
		"0101",
		"0110",
		"0111",
		"1000",
		"1001",
		"1010",
		"1011",
		"1100",
		"1101",
		"1110",
		"1111"
	};
	private Dictionary<Vector2,int> tileList;
	private List<Vector2> process;
	private GameObject[] grassTiles;
	private GameObject[] roadTiles;
	private GameObject[] buildingTiles;
	private GameObject[] treeTiles;
	// FIX
	//private GameObject[] lakeTiles;
	private List<Vector2> lakeStarters = new List<Vector2>();
	private int lakes;
	private int gtn = 16; // "grass tile num" -> how many grass tiles
	private int rtn = 16; // road tiles
	private int btn = 1; // number of buildings

	private int xmin = 0;
	private int xmax = 0;
	private int ymin = 0;
	private int ymax = 0;

    void GenerateWorld() {
        persist = GetComponent<WorldPersist>();
		lakes = iterations + grassDist;
		tileList = new Dictionary<Vector2,int>();
		process = new List<Vector2>();
		grassTiles = new GameObject[gtn];
		for (int i=0;i<gtn;i++) {
			grassTiles[i] = (GameObject)Resources.Load("Prefabs/Tiles/Grass/"+binary[i]);
		}
		roadTiles = new GameObject[rtn];
		for (int i=0;i<rtn;i++) {
			roadTiles[i] = (GameObject)Resources.Load("Prefabs/Tiles/Roads/"+binary[i]);
		}
		buildingTiles = new GameObject[btn];
		for (int i=0;i<btn;i++) {
			buildingTiles[i] = (GameObject)Resources.Load("Prefabs/Tiles/Buildings/building_"+i);
		}
		treeTiles = new GameObject[trh];
		for (int i=trl;i<trh;i++) {
			treeTiles[i] = (GameObject)Resources.Load("Prefabs/Tiles/Trees/tree_"+i);
		}
        GenerateRoads();
		GenerateRandom();
		GenerateLakes();
		GenerateGrass();
		GenerateTrees();
    }

    private void GenerateRoads() {
		int lakeDFR = grassDist + lakeSize;
		var temp = new List<Vector2>();
        Vector2 pos = new Vector2(0,0);
        int ang = 0;
        string lsys = IterateN(initString, iterations);
        foreach (char c in lsys) {
            if (c == 'F') {
                Vector2 delta = new Vector2((int)Mathf.Cos(ang * Mathf.Deg2Rad), (int)Mathf.Sin(ang * Mathf.Deg2Rad));
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
					if (Random.Range(0,lakeChance)==0) {
						switch(ang) {
							case 90:
								PlaceLakeStarter(pos+new Vector2(lakeDFR+Random.Range(lakeSize,grassDist),0));
								break;
							case 0:
								PlaceLakeStarter(pos+new Vector2(0,lakeDFR+Random.Range(lakeSize,grassDist)));
								break;
							case 270:
								PlaceLakeStarter(pos+new Vector2(Random.Range(-grassDist,-lakeSize)-lakeDFR,0));
								break;
							case 180:
								PlaceLakeStarter(pos+new Vector2(0,Random.Range(-grassDist,-lakeSize)-lakeDFR));
								break;
						}
					}
					if (!tileList.ContainsKey(pos)) { temp.Add(pos); tileList.Add(pos,1); }
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
		foreach (Vector2 vec in temp) PlaceRoad(vec);
		foreach (Vector2 vec in lakeStarters) _PlaceLakeStarter(vec);
    }

	private void GenerateGrass() {
		if (grassDist>0) {
			var temp = new List<Vector2>();
			foreach (Vector2 pos in process) {
				foreach (Vector2 p in cardinals(pos)) {
					if (!tileList.ContainsKey(p)) {
						tileList.Add(p,0);
						temp.Add(p);
					}
				}
			}
			process.Clear();
			foreach (Vector2 pos in temp) PlaceGrass(pos);
			grassDist--;
			GenerateGrass();
		}
	}

	private void GenerateTrees() {
		if (treeDist>0) {
			var temp = new List<Vector2>();
			foreach (Vector2 pos in process) {
				foreach (Vector2 p in cardinals(pos)) {
					if (!tileList.ContainsKey(p)) {
						tileList.Add(p,-1);
						temp.Add(p);
					}
				}
			}
			process.Clear();
			foreach (Vector2 pos in temp) PlaceTree(pos);
			treeDist--;
			GenerateTrees();
		}
	}

	private void GenerateRandom() {
		int random = 0;
		int temp;
		while (random<randTrees) {
			var elem = new Vector2(Random.Range(-scope,scope+1),Random.Range(-scope,scope+1));
			if (!tileList.ContainsKey(elem)) {
				random++;
				tileList.Add(elem,1);
				process.Add(elem);
				var tree = Instantiate(treeTiles[Random.Range(trl,trh)], elem, Quaternion.identity);
				persist.PersistObject(tree);
			}
		}
	}

	private void GenerateLakes() {
		if (lakeSize>0) {
			List<Vector2> temp = new List<Vector2>();
			foreach (Vector2 pos in lakeStarters) {
				foreach (Vector2 d in cardinals(pos)) {
					if (!tileList.ContainsKey(d) && !(Random.Range(0,lakeRandomness)==0)) {
						tileList.Add(d,1);
						temp.Add(d);
					}
				}
			}
			lakeStarters.Clear();
			lakeSize--;
			foreach (Vector2 pos in temp) PlaceLake(pos);
			GenerateLakes();
		} else {
			process.AddRange(lakeStarters);
		}
	}

	private void PlaceLake(Vector2 pos) {
		lakeStarters.Add(pos);
		var lake = Instantiate(lakeTile, pos, Quaternion.identity);
		persist.PersistObject(lake);
	}

	private void PlaceRoad(Vector2 pos) {
		process.Add(pos);
		int x = BinCode(pos);
		if (x==1 || x==2 || x==4 || x==8) tileList[pos] = x*2;
		var road = Instantiate(roadTiles[x], pos, Quaternion.identity);
		persist.PersistObject(road);
    }

	private void PlaceBuilding(Vector2 pos) {
		int temp = Random.Range(0,btn);
		bool buildingCollide = Physics2D.OverlapBox(pos, buildingTiles[temp].GetComponent<BoxCollider2D>().size, buildingMask);
		if (Random.Range (0f, 1f) > 0.85f && !buildingCollide) {
			var building = Instantiate (buildingTiles[temp], pos, Quaternion.identity);
            persist.PersistObject(building);
		}
	}

	private void PlaceLakeStarter(Vector2 pos) {
		lakeStarters.Add(pos);
	}

	private void _PlaceLakeStarter(Vector2 pos) {
		if (!tileList.ContainsKey(pos)) {
			tileList.Add(pos,1);
			//lakeStarters.Add(pos);
			var lake = Instantiate(lakeTile, pos, Quaternion.identity);
			persist.PersistObject(lake);
		}
	}

	private void PlaceGrass(Vector2 pos) {
		xmin = (int)Mathf.Min(pos.x,xmin);
		xmax = (int)Mathf.Max(pos.x,xmax);
		ymin = (int)Mathf.Min(pos.y,ymin);
		ymax = (int)Mathf.Max(pos.y,ymax);
		process.Add(pos);
		var grass = Instantiate(grassTiles[BinCode(pos)], pos, Quaternion.identity);
		persist.PersistObject(grass);
	}

	private void PlaceTree(Vector2 pos) {
		process.Add(pos);
		var tree = Instantiate(treeTiles[Random.Range(0,trh)], pos, Quaternion.identity);
		persist.PersistObject(tree);
		var grass = Instantiate(grassTiles[0],pos,Quaternion.identity);
		persist.PersistObject(grass);
	}

	private List<Vector2> cardinals(Vector2 pos) {
		var temp = new List<Vector2>();
		temp.Add(new Vector2((pos.x-1),pos.y));
		temp.Add(new Vector2((pos.x+1),pos.y));
		temp.Add(new Vector2(pos.x,(pos.y-1)));
		temp.Add(new Vector2(pos.x,(pos.y+1)));
		//print(pos+" ->  "+temp[0]+" , "+temp[1]+" , "+temp[2]+" , "+temp[3]);
		return temp;
	}

	// Code corresponds to LRDU of pos, 1 means touching, 0 means not
	private int BinCode(Vector2 pos) {
		int temp, code = 0, tagNum = -5;
		bool tag = false;
		//print("BinCode -> ("+pos.x+","+pos.y+")");
		foreach (Vector2 dir in cardinals(pos)) {
			code += code;
			if (tileList.TryGetValue(dir,out temp)) {
				if (temp>1) { tag = true; code++; tagNum = temp/2; }
				else code += tileList[dir];
			}
		}
		if (tag && code==tagNum) return 0;
		//print(binary[code]);
		return code;
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
