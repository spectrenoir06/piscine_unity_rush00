using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {


	public delegate	void			playerFireEvent();

	public event playerFireEvent	OnPlayerFire;
	
	// Use this for initialization
	void Start () {
	}

	public void	playerFire() {
		OnPlayerFire();
	}

	// Update is called once per frame
	void Update () {
		
	}
}
