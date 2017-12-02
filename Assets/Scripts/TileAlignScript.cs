using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileAlignScript : MonoBehaviour {
	
	public LayerMask roadMask;
	public float radius = 0.1f;
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

	private Transform u;
	private Transform d;
	private Transform l;
	private Transform r;

	void Start() {

		sr = GetComponent<SpriteRenderer>();
		u = transform.Find("CheckUp");
		d = transform.Find("CheckDown");
		l = transform.Find("CheckLeft");
		r = transform.Find("CheckRight");
		Invoke("Align",delay);
	}

	public void Align() {

		up = false;
		down = false;
		left = false;
		right = false;
		count = 0;

		up = Physics2D.OverlapCircle(u.position,radius,roadMask);
		down = Physics2D.OverlapCircle(d.position,radius,roadMask);
		left = Physics2D.OverlapCircle(l.position,radius,roadMask);
		right = Physics2D.OverlapCircle(r.position,radius,roadMask);

		if (up) count++;
		if (down) count++;
		if (left) count++;	
		if (right) count++;

		Debug.Log(count);

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
				if (!up) {};//rotate transforms!!!
				break;
			case 4:
				sr.sprite = i4;
				break;
		}

		transform.localScale += spriteScale;

	}

}
