using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Random_Weapon : MonoBehaviour {

	public List<GameObject>		weapons = new List<GameObject>();

	// Use this for initialization
	void Start () {
		GameObject.Instantiate (weapons [Random.Range (0, weapons.Count)], transform.position, Quaternion.identity);
		GameObject.Destroy(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
