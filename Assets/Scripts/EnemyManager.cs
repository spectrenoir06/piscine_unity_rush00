using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour {

	public delegate	void			playerFireEvent();
	public event playerFireEvent	OnPlayerFire;
	public List< GameObject >		enemies = new List<GameObject>();
	public static int enemyCount	= 1;
	
	// Use this for initialization
	void Start () {
		GameObject[] es = GameObject.FindGameObjectsWithTag("Enemy");
		for (int i = 0; i < es.Length; i++)
			if (i % 2 > 0)
				enemies.Add(es[i]);
		enemyCount = es.Length;
		Debug.Log (enemies.Count);
	}

	public void	playerFire() {
		if (enemies.Count != 0)
			OnPlayerFire();
	}

	// Update is called once per frame
	void Update () {
		enemyCount = enemies.Count;
	}
}
