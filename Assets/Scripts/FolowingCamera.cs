using UnityEngine;
using System.Collections;

public class FolowingCamera : MonoBehaviour {
		
	private Player		player;

	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player").GetComponent< Player >();
	}
	
	// Update is called once per frame
	void Update () {
		if (player)
			transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
	}
}
