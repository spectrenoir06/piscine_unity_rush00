using UnityEngine;
using System.Collections;

public class gnomeGunBullet : MonoBehaviour {

	private Rigidbody2D		rbody;
	private float			f;

	// Use this for initialization
	void Start () {
		f = 0;
		rbody = GetComponent< Rigidbody2D >();
	}
	
	// Update is called once per frame
	void Update () {
		rbody.gravityScale = Mathf.Sin(f);
		f += 0.07F;
	}
}
