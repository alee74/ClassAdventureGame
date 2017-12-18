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
    public float lakeRandomness = .3f;
    public int lakeSize = 1;
    public int treesAlpha = 0;
    public int treesOmega = 15;
    public int buildingsAlpha = 0;
    public int buildingsOmega = 1;
    public float treeDensity = 0.5f;
    public int treeDist = 6;
    public float lakeDensity = 0.01f;
    public int lakeDist = 12;

    public BoxCollider2D buildingBox;

    private static GameObject world;
    private Transform persist;
    private Vector2 xVec = new Vector2(1.16f,0);
    private Vector2 yVec = new Vector2(0,1.16f);

    public LayerMask buildingMask;
    public LayerMask roadMask;

    private string lsys;

    // tile information
    private Dictionary<Vector2,int> tiles;
    private List<Vector2> process;
    private List<string> binary;
    private List<GameObject> grass;
    private List<GameObject> roads;
    private List<GameObject> trees;
    private List<GameObject> buildings;
    private float buildingDist = 1.16f;
    public GameObject lakes;
    //private GameObject[] lakess; *make these tiles and add them in. Make code that will account for it.
    private List<Vector2> lakeStarters;
    private int boundaryWidth = 10; // boundary width only need be large enough to hide unprocessed world
    private int btn = 1; // number of buildings

    // use N E S W to refer to directions--modify arrangement of directional checks

    void Awake () {
        print("creating world...");
        world = new GameObject ("World");
        DontDestroyOnLoad (world);
        persist = world.transform;

        tiles = new Dictionary<Vector2,int> ();
        lakeStarters = new List<Vector2> ();
        process = new List<Vector2> ();

        binary = new List<string> {
            "0000", "0001", "0010", "0011",
            "0100", "0101", "0110", "0111",
            "1000", "1001", "1010", "1011",
            "1100", "1101", "1110", "1111" };

        print("printing binary...");
        foreach (string s in binary) {
            print(s);
        }

        print("loading grass...");
        grass = Load ("Grass", 0, 16);
        print("loading roads...");
        roads = Load ("Roads", 0, 16);
        print("loading trees...");
        trees = Load ("Trees", treesAlpha, treesOmega);
        print("loading buildings...");
        buildings = Load ("Buildings", buildingsAlpha, buildingsOmega);
        
    }

    void Start() {
    //void GenerateWorld () {
        print("generating world...");
        print("generating l-system...");
        print("l-system = "+lsys);
        lsys = IterateN (seed, its);
        print("generating infrastructure...");
        GenerateInfrastructure ();
        print("generating lakes...");
        GenerateLakes ();
        print("generating trees...");
        GenerateTrees ();
        print("generating grass...");
        GenerateGrass ();
        print("generating boundary...");
        GenerateBoundary ();
    }

   private void GenerateInfrastructure () {
       Vector2 pos = V (0,0);
       int ang = 0;
       foreach (char c in lsys) {
           if (c == 'F') {
               for (int i = 0; i < 5; ++i) {
            		if (!tiles.ContainsKey(pos)) { process.Add(pos); tiles.Add(pos,1); }
                    if (i == 5/2) PlaceBuilding(ang,pos);
                    pos += D(ang); //lol dang -- it's the "delta" of the angle
               }
           } else if (c == '+') {
            	if (ang==270) ang = 0;
                else ang += 90;
           } else if (c == '-') {
                if (ang==0) ang = 270;
            	else ang -= 90;
           }
       }
        print("printing process");
        foreach (Vector2 p in process) {
            print(p);
            PlaceTile(roads[Code(p)],p);
            print("done");
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
            foreach (Vector2 pos in temp) {
                process.Add(pos);
                PlaceGrass(pos);
            }
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
            foreach (Vector2 pos in temp) {
                process.Add(pos);
                PlaceTile(tree(),pos);
                PlaceTile(grass[0],pos);
            }
            boundaryWidth--;
            GenerateBoundary ();
        }
    }

    private void GenerateTrees () {
        foreach (Vector2 v in process) {
            foreach(Vector2 v1 in GetCardinals(v)) {
                print("generating tree...");
                var temp = v1+V(R(-treeDist,treeDist),R(-treeDist,treeDist));
                if (Random.Range(0f,1f)<=treeDensity && !tiles.ContainsKey(temp)) {
                    tiles.Add(temp,0);
                    PlaceTile(tree(),temp);
                    PlaceTile(grass[0],temp);
                }
            }
        }
    }

    private void GenerateLakes () {
        List<Vector2> lake = new List<Vector2>();
        List<Vector2> lake2 = new List<Vector2>();
        foreach (Vector2 v in process) {
            lake.Add(v);
        }
        foreach (Vector2 pos in lake) {
            if (Random.Range(0f,1f)<=lakeDensity) {
                var temp = pos+V(R(-lakeDist,lakeDist),R(-lakeDist,lakeDist));
                if (!tiles.ContainsKey(temp)) {
                    tiles.Add(temp,4);
                    lake2.Add(temp);
                    process.Add(temp);
                    PlaceTile(lakes,temp);
                }
            }
        }
        _GenerateLakes (lake2,lakeSize);
    }

    private void _GenerateLakes (List<Vector2> pos, int size) {
        if (size>0) {
            List<Vector2> temp = new List<Vector2>();
            foreach (Vector2 p in pos) {
                foreach (Vector2 dir in GetCardinals(p)) {
                print(dir);
                    if (!tiles.ContainsKey(dir) && Random.Range(0f,1f)<=lakeRandomness) {
                        tiles.Add(dir,4);
                        temp.Add(dir);
                        process.Add(dir);
                        PlaceTile(lakes,dir);
                    }
                }
            }
            _GenerateLakes(temp,--size);
        }
    }

    private void PlaceRoad (Vector2 pos) {
        process.Add (pos);
        PlaceTile (roads[Code(pos)], pos);
    }

    private void PlaceGrass (Vector2 pos) {
        process.Add (pos);
        PlaceTile (grass[Code(pos)], pos);
    }

    private void PlaceBuilding (int ang, Vector2 pos) {
        bool buildingCollide = Physics2D.OverlapBox(pos, buildingBox.size, buildingMask);
        if (Random.Range (0f, 1f) > 0.85f && !buildingCollide) {
            print("placing building at "+pos+", ang = "+ang+"...");
            var temp = V(0,0);
            switch (ang) {
                case 90:
                    temp = pos + xVec;
            	    tiles.Add (temp, 3);
                    PlaceTile (building(),temp);
            	    break;
                case 0:
                    temp = pos + yVec;
            	    tiles.Add (temp, 3);
                    PlaceTile (building(),temp);
            	    break;
                case 270:
                    temp = pos - xVec;
            	    tiles.Add (temp, 3);
                    PlaceTile (building(),temp);
            	    break;
                case 180:
                    temp = pos - yVec;
            	    tiles.Add (temp, 3);
                    PlaceTile (building(),temp);
             	    break;
            }
        }
    }

    private void PlaceTile (GameObject tile, Vector2 pos) {
        print("placing tile...");
        Instantiate (tile,pos,Quaternion.identity,persist);
    }
    // returns vectors with the coordinates N, E, S, and W of the given position
    private List<Vector2> GetCardinals (Vector2 pos) {
        return (new List<Vector2> {
            V (pos.x,(pos.y+1)),
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
            code *= 2;
            int temp;
            if (tiles.TryGetValue(dir, out temp)) {
                if (temp >= 1) code++;
            }
        }
        return code;
    }


   private string IterateN (string prev, int n) {
       for (int i = 0; i < n; ++i) {
           prev = Iterate (prev);
       }
       return prev;
   }

   private string Iterate (string prev) {
       string next = "";
       foreach (char c in prev) {
           if (c == 'A') {
               float rand = R (0, 100);
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

    // Useful functions to make life a little easier and code a little cleaner

    public GameObject tree () {
        return trees[R (treesAlpha,treesOmega)];
    }

    public GameObject building () {
        print("getting building...");
        return buildings[R (buildingsAlpha,buildingsOmega)];
    }

    public Vector2 V (float x, float y) {
        return new Vector2 (x,y);
    }

    public int R (int x, int y) {
        print("(int)R = " + x + "," + y + "...");
        return Random.Range (x,y);
    }

   public float R (float x, float y) {
        print("(float)R = " + x + "," + y + "...");
        return (int)Random.Range (x,y);
    }

    public Vector2 D (int ang) {
        return V ((int)Mathf.Cos(ang*Mathf.Deg2Rad), (int)Mathf.Sin(ang * Mathf.Deg2Rad));
    }

    public List<GameObject> Load (string folder, int a, int b) {
        List<GameObject> l = new List<GameObject> ();
        if (a==0 && b==16) {
            print("loading binary from folder "+folder+"...");
            for (int i=a;i<b;i++) {
                print("loading "+binary[i]+"...");
                l.Add((GameObject)Resources.Load("Prefabs/Tiles/"+folder+"/"+binary[i]));
            }
        } else {
                print("loading sprites from folder "+folder+"...");
            for (int i=a;i<b;i++) {
                print("loading "+i+"...");
                l.Add((GameObject)Resources.Load("Prefabs/Tiles/"+folder+"/"+i));
            }
        }
        return l;
    }
}
