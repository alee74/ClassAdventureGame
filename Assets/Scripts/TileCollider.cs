using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TileCollider : MonoBehaviour {

	private bool _touching = false;
	public float radius=0.2f;
	public LayerMask mask;

	public bool touching() {
		_touching = Physics2D.OverlapCircle (transform.position,radius,mask);
		return _touching;
	}

}
