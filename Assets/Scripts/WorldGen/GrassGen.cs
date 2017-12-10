using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassGen : MonoBehaviour {

	public int distance = 5;
	private int _distance;
	public GameObject grassTile;

	private List<Vector2> grass = new List<Vector2>();
	private Dictionary<Vector2,int> check = new Dictionary<Vector2,int>();
	private List<Vector2> temp = new List<Vector2>();
	private GameObject tile;
	private TileCollider u;
    private TileCollider d;
    private TileCollider l;
    private TileCollider r;
	private int tileNum=0;

	public void GenerateGrass(Dictionary<Vector2,GameObject> map) {
		if (distance>0) {
			_distance = distance;
			foreach (KeyValuePair<Vector2,GameObject> tile in map) {
				u = tile.Value.transform.Find("CheckUp").GetComponent<TileCollider>();
        		d = tile.Value.transform.Find("CheckDown").GetComponent<TileCollider>();
        		l = tile.Value.transform.Find("CheckLeft").GetComponent<TileCollider>();
        		r = tile.Value.transform.Find("CheckRight").GetComponent<TileCollider>();
				if (!u.touching()) PlaceGrass(u.transform.position);
				if (!d.touching()) PlaceGrass(d.transform.position);
				if (!l.touching()) PlaceGrass(l.transform.position);
				if (!r.touching()) PlaceGrass(r.transform.position);
			}			
			InstantiateAll();
			_distance--;
			if (_distance>0) _GenerateGrass();
		} else return;
	}

	public void _GenerateGrass() {
		InstantiateAll();
		_distance--;
		if (_distance>0) _GenerateGrass();
		else return;
	}

	private void PlaceGrass(Vector2 pos) {
		if (!check.ContainsKey(pos)) {
			grass.Add(pos);
			check.Add(pos,1);
		}
	}

	private void InstantiateAll() {
		temp.Clear();

		foreach(Vector2 pos in grass) {
			temp.Add(pos);
		}
		grass.Clear();

		foreach (Vector2 pos in temp) {
			tile = Instantiate(grassTile,pos,Quaternion.identity);
			tile.name = "Grass Tile #"+tileNum++;
            u = tile.transform.Find("CheckUp").GetComponent<TileCollider>();
            d = tile.transform.Find("CheckDown").GetComponent<TileCollider>();
            l = tile.transform.Find("CheckLeft").GetComponent<TileCollider>();
            r = tile.transform.Find("CheckRight").GetComponent<TileCollider>();
            if (!u.touching()) PlaceGrass(u.transform.position);
            if (!d.touching()) PlaceGrass(d.transform.position);
            if (!l.touching()) PlaceGrass(l.transform.position);
            if (!r.touching()) PlaceGrass(r.transform.position);
		}
	}
}
