using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Random_sprite : MonoBehaviour {

	public List<Sprite>		heads = new List<Sprite>();
	public List<Sprite>		bodys = new List<Sprite>();

	// Use this for initialization
	void Start () {
		transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = heads[Random.Range (0, heads.Count)];
		transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().sprite = bodys[Random.Range (0, bodys.Count)];
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
