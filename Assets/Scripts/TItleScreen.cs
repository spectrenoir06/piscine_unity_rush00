using UnityEngine;
using System.Collections;

public class TItleScreen : MonoBehaviour {

	public GameObject	header;
	public GameObject	effelImage;

	private float		rotation = 0.5F;
	private float		rotateDest = 15;
	private float	 	scaleUP = 1.03F;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
/*		header.transform.rotation = Quaternion.Euler(0, 0, Mathf.Lerp(header.transform.eulerAngles.z, rotateDest + 360, 0.02F));
		if ((header.transform.eulerAngles.z > 14 && header.transform.eulerAngles.z < 180) || (header.transform.eulerAngles.z > 180 && header.transform.eulerAngles.z < 360 - 14))
			rotateDest = -rotateDest;*/
		effelImage.transform.localScale *= scaleUP;
		if (effelImage.transform.localScale.x > 16F)
			effelImage.transform.localScale = new Vector3(0.5F, 0.5F, 0);
	}

	public void onExitPressed() {
		Application.Quit();
	}

	public void onStartPressed() {
		Application.LoadLevel("Scene-adoussau");
	}
}
