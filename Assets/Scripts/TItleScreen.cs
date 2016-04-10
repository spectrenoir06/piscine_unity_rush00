using UnityEngine;
using System.Collections;

public class TItleScreen : MonoBehaviour {

	public GameObject	header;
	public GameObject	effelImage;

	private float		rotation = 0.5F;
	private float		rotateDest = 15;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
/*		header.transform.rotation = Quaternion.Euler(0, 0, Mathf.Lerp(header.transform.eulerAngles.z, rotateDest + 360, 0.02F));
		if ((header.transform.eulerAngles.z > 14 && header.transform.eulerAngles.z < 180) || (header.transform.eulerAngles.z > 180 && header.transform.eulerAngles.z < 360 - 14))
			rotateDest = -rotateDest;*/
	}

	public void onExitPressed() {
		Application.Quit();
	}

	public void onStartPressed() {
		Application.LoadLevel("level1");
	}
}
