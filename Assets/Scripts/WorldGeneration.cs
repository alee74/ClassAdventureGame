using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGeneration : MonoBehaviour {

    // World Generation Parameters
    public string seed = "ABC";
    public float fwdDist = 5f;
    public int its = 16;
    public int numTrees = 90;
    public int numLakes = 15;
    public int numGrass = 8; // How many its of grass generation
    public int scope = 100;
    public int lakeRandomness = 3;
    public int lakeSize = 5;
    public int treesAlpha = 0;
    public int treesOmega = 14;
    public int buildingsAlpha = 0;
    public int buildingsOmega = 0;

    private static GameObject world;
    private Transform persist;

    public LayerMask buildingMask;
    public LayerMask roadMask;

    public float distFromRoad = 1.16f;

    // tile information
    private Dictionary<Vector2,int> tiles;
    private List<Vector2> process;
    private List<string> binary;
    private GameObject[] grass;
    private GameObject[] roads;
    private GameObject[] trees;
    private GameObject[] buildings;
    public GameObject lakes;
    //private GameObject[] lakess; *make these tiles and add them in. Make code that will account for it.
    private List<Vector2> lakeStarters;
    private int boundaryWidth = 10; // boundary width only need be large enough to hide unprocessed world
    private int btn = 1; // number of buildings

    // use N E S W to refer to directions--modify arrangement of directional checks

    void Awake () {
        world = new GameObject ("World");
        DoNotDestroyOnLoad (world);
        persist = world.transform;

        tiles = new Dictionary<Vector2,int> ();
        lakeStarters = new List<Vector2> ();
        process = new List<Vector2> ();

        binary = new List<string> {
            "0000", "0001", "0010", "0011",
            "0100", "0101", "0110", "0111",
            "1000", "1001", "1010", "1011",
            "1100", "1101", "1110", "1111" };

        grass = Load ("Grass", 0, 16);
        roads = Load ("Roads", 0, 16);
        trees = Load ("Trees", treesAlpha, treesOmega);
        buildings = Load ("Buildings", buildingsAlpha, buildingsOmega);
        
    }

void GenerateWorld () {
       GenerateInfrastructure ();
        GenerateTrees ();
        GenerateLakes ();
        GenerateGrass ();
        GenerateBoundary ();
   }

   private void GenerateInfrastructure () {
       Vector2 pos = V  (0,0);
       int ang = 0;
       string lsys = IterateN (seed, its);
       foreach  (char c in lsys) {
           if  (c == 'F') {
            	Vector2 delta = D (ang); // lol
               for  (int i = 0; i < 5; ++i) {
                   if  (i == (int) 5/2) PlaceBuilding(ang,pos);
            		if (!tiles.ContainsKey(pos)) { process.Add(pos); tiles.Add(pos,1); }
                   pos += delta;
               }
           } else if  (c == '+') {
            	if (ang==270) ang = 0;
               else ang += 90;
           } else if  (c == '-') {
               if  (ang==0) ang = 270;
            	else ang -= 90;
           }
       }
        foreach (Vector2 pos in process) PlaceTile(roads[Code(pos)],pos);
        foreach (Vector2 pos in lakeStarters) {
            if (!tiles.ContainsKey(pos)) {
               tiles.Add (pos,1);
               PlaceTile (lakes, pos);
            }
       }
   }

    private void GenerateGrass () {
        if (numGrass>0) {
            var temp = new List<Vector2> ();
            foreach (Vector2 pos in process) {
            	foreach (Vector2 p in GetCardinals(pos)) {
            		if (!tiles.ContainsKey(p)) {
            			tiles.Add (p,0);
            			temp.Add (p);
            		}
            	}
            }
            process.Clear ();
            foreach (Vector2 pos in temp) PlaceGrass(pos);
            numGrass--;
            GenerateGrass ();
        }
    }

    private void GenerateBoundary () {
        if (boundaryWidth>0) {
            var temp = new List<Vector2> ();
            foreach (Vector2 pos in process) {
            	foreach (Vector2 p in GetCardinals(pos)) {
            		if (!tiles.ContainsKey(p)) {
            			tiles.Add (p,-1);
            			temp.Add (p);
            		}
            	}
            }
            process.Clear ();
            foreach (Vector2 pos in temp) PlaceTile(trees,pos);
            boundaryWidth--;
            GenerateBoundary ();
        }
    }

    private void GenerateTrees () {
        int random = 0;
        int temp;
        while (random<numTrees) {
            var elem = V (R(-scope,scope+1),Random.Range(-scope,scope+1));
            if (!tiles.ContainsKey(elem)) {
            	random++;
            	tiles.Add (elem,1);
            	process.Add (elem);
            	PlaceTile (trees[R(treesAlpha,treesOmega)], elem);
            }
        }
    }

    private void GenerateLakes () {
        if (lakeSize>0) {
            List<Vector2> temp = new List<Vector2> ();
            foreach (Vector2 pos in lakeStarters) {
            	foreach (Vector2 d in GetCardinals(pos)) {
            		if (!tiles.ContainsKey(d) && !(R(0,lakeRandomness)==0)) {
            			tiles.Add (d,1);
            			temp.Add (d);
            		}
            	}
            }
            lakeStarters.Clear ();
            lakeSize--;
            foreach (Vector2 pos in temp) PlaceLake(pos);
            GenerateLakes ();
        } else {
            process.AddRange (lakeStarters);
        }
    }

    private void PlaceLake (Vector2 pos) {
        lakeStarters.Add (pos);
        PlaceTile (lakes, pos);
    }

    private void PlaceRoad (Vector2 pos) {
        process.Add (pos);
        PlaceTile (roads[Code(pos)], pos);
   }

    private void PlaceBuilding (Vector2 pos) {
        int temp = R (0,btn);
        bool buildingCollide = Physics2D.OverlapBox (pos, buildings[temp].GetComponent<BoxCollider2D>().size, buildingMask);
        if (R (0f, 1f) > 0.85f && !buildingCollide) {
            PlaceTile (buildings[temp], pos);
        }
    }

    private void _PlaceLakeStarter (Vector2 pos) {
        if (!tiles.ContainsKey(pos)) {
            tiles.Add (pos,1);
            PlaceTile (lakes, pos);
        }
    }

    private void PlaceGrass (Vector2 pos) {
        process.Add (pos);
        PlaceTile (grass[Code(pos)], pos);
    }

    private void PlaceBuilding (int ang, Vector2 pos) {
        switch (ang) {
            case 90:
            	PlaceTile (building(), pos + V (buildingDist,0));
            	tiles.Add (pos + V (buildingDist,0));
            	break;
            case 0:
            	PlaceTile (building(), pos + V (0,buildingDist));
            	tiles.Add (pos + V (0,buildingDist));
            	break;
            case 270:
            	PlaceTile (building(), pos + V (-buildingDist,0));
            	tiles.Add (pos + V (-buildingDist,0));
            	break;
            case 180:
            	PlaceTile (building(), pos + V (0,-buildingDist));
            	tiles.Add (pos + V (0,-buildingDist));
            	break;
        }
    }

    private void PlaceTile (GameObject tile, Vector2 pos) {
        process.Add (pos);
        Instantiate (tile,pos,Quaternion.identity,persist);
    }
    // returns vectors with the coordinates N, E, S, and W of the given position
    private List<Vector2> GetCardinals (Vector2 pos) {
        return (new List<Vector2> {
            V (pos.x,(pos.y+1))
            V ((pos.x+1),pos.y),
            V (pos.x,(pos.y-1)),
            V ((pos.x-1),pos.y),
        });
    }

    // Code corresponds to N E S W coordinates of pos; 1 means touching
    // a tile in that direction with a value of 1 or greater, 0 means not touching
    private int Code (Vector2 pos) {
        int code = 0;
        foreach (Vector2 dir in GetCardinals(pos)) {
            code += code;
            if (tiles[dir]>=1) code++;
        }
        return code;
    }


   private string IterateN (string prev, int n) {
       for  (int i = 0; i < n; ++i) {
           prev = Iterate (prev);
       }
       return prev;
   }

   private string Iterate (string prev) {
       string next = "";
       foreach  (char c in prev) {
           if  (c == 'A') {
               float rand = R (0, 100);
               if  (rand < 50) {
                   next += "F+AC";
               } else {
                   next += "F++FAC";
               }
           } else if  (c == 'B') next += "+FB+F";
           else if  (c == 'C') next += "-FA+";
           else next += c;
       }
       return next;
   }

    // Useful functions to make life a little easier and code a little cleaner

    public GameObject tree () {
        return trees[R (treesAlpha,treesOmega)];
    }

    public Vector2 V (int x, int y) {
        return new Vector2 (x,y);
    }

    public int R (int x, int y) {
        return Random.Range (x,y+1);
    }

    public Vector2 D (int ang) {
        return V ((int)Mathf.Cos(ang*Mathf.Deg2Rad), (int)Mathf.Sin(ang * Mathf.Deg2Rad));
    }

    public List<Vector2> Load (string folder, int a, int b) {
        List<Vector2> l = new List<Vector2> ();
        for (int i=a;i<b;i++) {
            if (a==0 && b==16) l[i] = (GameObject)Resources.Load("Prefabs/Tiles/"+s+"/"+binary[i]);
            else l[i] = (GameObject)Resources.Load("Prefabs/Tiles/"+s+"/"+i);
        }
    }
}
