using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassAlignScript : MonoBehaviour {

	public LayerMask roadMask;
    public float delay = 0.01f;
    public Vector3 spriteScale = new Vector3 ( 0.05f, 0.05f, 0 );
    private SpriteRenderer sr;

    public Sprite leftCornerUp;
    public Sprite rightCornerUp;
    public Sprite leftCornerDown;
    public Sprite rightCornerDown;
    public Sprite allEdges;
    public Sprite center;
	//public Sprite vert;
	//public Sprite hor;
    public Sprite cr;
    public Sprite cl;
    public Sprite cu;
    public Sprite cd;
	public Sprite spu;
	public Sprite spd;
	public Sprite spl;
	public Sprite spr;

    private bool up;
    private bool down;
    private bool left;
    private bool right;
    private int count;

    private TileCollider u;
    private TileCollider d;
    private TileCollider l;
    private TileCollider r;

	// Use this for initialization
	void Start () {
		sr = GetComponent<SpriteRenderer>();
        u = transform.Find("CheckUp").GetComponent<TileCollider>();
        d = transform.Find("CheckDown").GetComponent<TileCollider>();
        l = transform.Find("CheckLeft").GetComponent<TileCollider>();
        r = transform.Find("CheckRight").GetComponent<TileCollider>();
        Invoke("Align",delay);
	}

	public void Align() {
		count = 0;

		up = u.touching();
        down = d.touching();
        left = l.touching();
        right = r.touching();

        if (up) count++;
        if (down) count++;
        if (left) count++;
        if (right) count++;

		switch(count) {
			case 0:
				sr.sprite = center;
				break;
			case 1:
				if (up) sr.sprite = spu;
				if (down) sr.sprite = spd;
				if (left) sr.sprite = spl;
				if (right) sr.sprite = spr;
				break;
			case 2:
				if (up && left) sr.sprite = leftCornerUp;
				if (up && right) sr.sprite = rightCornerUp;
				if (down && left) sr.sprite = leftCornerDown;
				if (down && right) sr.sprite = rightCornerDown;
				//if (up && down) sr.sprite = vert;
				//if (left && right) sr.sprite = hor;
				else sr.sprite = center; //get rid of this.
				break;
			case 3:
				if (!up) sr.sprite = cu;
				if (!down) sr.sprite = cd;
				if (!left) sr.sprite = cl;
				if (!right) sr.sprite = cr;
				break;
			case 4:
				sr.sprite = allEdges;
				break;
		}

		transform.localScale += spriteScale;
	}
}
