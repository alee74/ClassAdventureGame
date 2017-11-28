using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LakeGen : MonoBehaviour {

    public GameObject seafab;
    public int lakeZoneSize = 700;
    public float lakeChance = 0.99f;
    private WorldPersist persist;

	// Use this for initialization
	void Start () {
        persist = GetComponent<WorldPersist>();
        GenLake();
	}
	
        
    void GenLake()
    {
        for (int i = -lakeZoneSize; i <= lakeZoneSize; i++)
        {
            for ( int j = -lakeZoneSize; j <= lakeZoneSize; j++)
            {
                float sample = Mathf.PerlinNoise(map(i,-lakeZoneSize, lakeZoneSize,0,lakeZoneSize*lakeZoneSize)/100f, map(j,-lakeZoneSize, lakeZoneSize,0,lakeZoneSize*lakeZoneSize)/100f);
                if(sample > lakeChance)
                {
                    var lake = Instantiate(seafab, new Vector3(i + seafab.GetComponent<BoxCollider2D>().size.x, j + seafab.GetComponent<BoxCollider2D>().size.y, 3), Quaternion.identity);
                    persist.PersistObject(lake);
                }
            }
        } 
    }
    int map(int i, int in_min, int in_max, int out_min, int out_max)
    {
        int slope = (out_max - out_min) / (in_max - in_min);
        return (i -in_min) *slope + out_min;
    }
}
