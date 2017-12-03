using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileAlignScript : MonoBehaviour {
	
	public LayerMask roadMask;
	public float delay = 3;
	public Vector3 spriteScale = new Vector3 ( 0.05f, 0.05f, 0 );
	private SpriteRenderer sr;

	public Sprite h1;
	public Sprite h2;
	public Sprite v1;
	public Sprite v2;
	public Sprite c1;
	public Sprite c2;
	public Sprite c3;
	public Sprite c4;
	public Sprite i3;
	public Sprite i4;

	private bool up;
	private bool down;
	private bool left;
	private bool right;
	private int count;

	private TileCollider u;
	private TileCollider d;
	private TileCollider l;
	private TileCollider r;

	private Vector3 vinv = new Vector3(0,0,180);
	private Vector3 vr = new Vector3(0,0,270);
	private Vector3 vl = new Vector3(0,0,90);

	void Start() {

		sr = GetComponent<SpriteRenderer>();
		u = transform.Find("CheckUp").GetComponent<TileCollider>();
		d = transform.Find("CheckDown").GetComponent<TileCollider>();
		l = transform.Find("CheckLeft").GetComponent<TileCollider>();
		r = transform.Find("CheckRight").GetComponent<TileCollider>();
		Invoke("Align",delay);
	}

	public void Align() {

		print("Align()");

		count = 0;

		up = u.touching();
		down = d.touching();
		left = l.touching();
		right = r.touching();

		if (up) count++;
		if (down) count++;
		if (left) count++;	
		if (right) count++;

		//Debug.Log(count);

		switch(count) {
			case 0:
				sr.sprite = v1;
				break;
			case 1:
				if (up || down) sr.sprite = v1;
				else if (left || right) sr.sprite = h2;
				break;
			case 2:
				if (up && down) sr.sprite = v2;
				else if (left && right) sr.sprite = h1;
				else if (up && left) sr.sprite = c4;
				else if (up && right) sr.sprite = c3;
				else if (down && left) sr.sprite = c2;
				else if (down && right) sr.sprite = c1;
				break;
			case 3:
				sr.sprite = i3;
				if (!up) this.gameObject.transform.Rotate(vinv);
				if (!left) this.gameObject.transform.Rotate(vr);
				if (!right) this.gameObject.transform.Rotate(vl);
				break;
			case 4:
				sr.sprite = i4;
				break;
		}

		transform.localScale += spriteScale;

	}

}
