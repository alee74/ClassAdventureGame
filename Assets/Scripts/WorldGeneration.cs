﻿using System.Collections;
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
	public int grassDist = 5; // How many iterations of grass generation

	/// <summary>
	/// Layer masks for the layers building and road are on.
	/// </summary>
	public LayerMask buildingMask;
	public LayerMask roadMask;

	public BoxCollider2D buildingBox;

	public float distFromRoad = 1.16f;

	private Dictionary<Vector2,int> tileList = new Dictionary<Vector2,int>();
	private List<Vector2> process = new List<Vector2>();

	//private GameObject[] buildingTiles;

    public int width = 200;
    public int height = 200;

    private int w = 2000;
    private int h = 2000;
	
	private int[,] arr = new int[2000,2000];

    private WorldPersist persist;


	// tile placement info *Reidlee*
	private bool up;	// true if there is a tile "above" whichever is being checked
	private bool down;	// etc.
	private bool left;
	private bool right;
	private int count;
	private string sprite;

	// tile orientation info *Reidlee*
	private Vector3 vinv = new Vector3(0,0,180);
    private Vector3 vr = new Vector3(0,0,270);
    private Vector3 vl = new Vector3(0,0,90);

	// tile information
	private enum roadNums { h1, h2, v1, v2, c1, c2, c3, c4, i3, i4 };
	private enum grassNums { tlc, ue, trc, ae, nb, le, ne, re, nt, blc, be, brc, nr, nl };
	private GameObject[] grassTiles;
	private GameObject[] roadTiles;
	private GameObject[] buildingTiles;
	private int gtn = 14; // "grass tile num" -> how many grass tiles
	private int rtn = 10;
	private int btn = 1;

	void Awake() {
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
			buildingTiles[i] = (GameObject)Resources.Load("Prefabs/Tiles/Buildings/building_"_i.toString());
		}
	}

    void GenerateWorld() {
        persist = GetComponent<WorldPersist>();
        for(int i = 0; i < w; i++)
        {
            for(int j = 0; j < h; j++)
            {
                arr[i, j] = 0;
            }
        }
        GenerateRoads();
		foreach (Vector2 pos in tileList) process.Add(pos);
		GenerateGrass();
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
						//PlaceBuilding (pos);
					}
                    pos += delta;
                }
            } else if (c == '+') {
                ang += 90;
            } else if (c == '-') {
                ang -= 90;
            }
        }
        for(int i = 0; i < w; i++)
        {
            for (int j = 0; j < h; j++)
            {
                if(arr[i, j] == 1)
                {
					resetChecks(); //resets vals that help pick right tile
					if (arr[i-1,j] == 1) { left = true; count++; }
					if (arr[i+1,j] == 1) { right = true; count ++; }
					if (arr [i,j-1] == 1) { down = true; count ++; }
					if (arr [i,j+1] == 1) { up = true; count ++; }

					//print("up = "+up+", down = "+down+", left = "+left+", right = "+right);

					switch(count) {
            			case 0:
							PlaceTile(new Vector2(i-w/2,j-h/2),h1,false);
                			break;
            			case 1:
                			if (up || down) PlaceTile(new Vector2(i-w/2,j-h/2),v1,false);
                			else if (left || right) PlaceTile(new Vector2(i-w/2,j-h/2),h2,false);
                			break;
            			case 2:
                			if (up && down) PlaceTile(new Vector2(i-w/2,j-h/2),v2,false);
                			else if (left && right) PlaceTile(new Vector2(i-w/2,j-h/2),h1,false);
                			else if (up && left) PlaceTile(new Vector2(i-w/2,j-h/2),c4,false);
                			else if (up && right) PlaceTile(new Vector2(i-w/2,j-h/2),c3,false);
                			else if (down && left) PlaceTile(new Vector2(i-w/2,j-h/2),c2,false);
                			else if (down && right) PlaceTile(new Vector2(i-w/2,j-h/2),c1,false);
                			break;
            			case 3:
                			PlaceTile(new Vector2(i-w/2,j-h/2),i3,true);
                			break;
            			case 4:
                			PlaceTile(new Vector2(i-w/2,j-h/2),i4,false);
                		break;
        			}
/*
                    PlaceTile(new Vector2(i - w / 2, j - h / 2));
*/
                }
            }
        }
    }

	void GenerateGrass() {
		if (grassDist >0) {
			foreach (Vector2 pos in process) {
				if (arr[pos.x-1,pos.y] == 0) { PlaceGrass(new Vector2((pos.x-1)-w/2,(pos.y)-h/2)); }
				if (arr[pos.x+1,pos.y] == 0) { PlaceGrass(new Vector2((pos.x+1)-w/2,(pos.y)-h/2)); }
				if (arr[pos.x,pos.y-1] == 0) { PlaceGrass(new Vector2((pos.x)-w/2,(pos.y-1)-h/2)); }
				if (arr[pos.x,pos.y+1] == 0) { PlaceGrass(new Vector2((pos.x)-w/2,(pos.y+1)-h/2)); }
			}
		}
	}

	private void PlaceGrass(Vector2 pos) {
		var grass = Instantiate(grassTile, pos, Quaternion.identity);
		persist.PersistObject(grass);
	}

	// alternative place tile to get sprites right *Reidlee*
	private void PlaceTile(Vector2 pos, GameObject tile, bool rotate) {
		process.Add(pos);
		//print("Place Tile Called");
		var road = Instantiate(tile, pos, Quaternion.identity);
		//Debug.Log(road);
		if (rotate && down) {
			if (!up) road.transform.Rotate(vinv);
			if (!left) road.transform.Rotate(vr);
			if (!right) road.transform.Rotate(vl);
		}
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

	private void resetChecks() {
		count = 0;
		up = false;
		down = false;
		left = false;
		right = false;
	}
}