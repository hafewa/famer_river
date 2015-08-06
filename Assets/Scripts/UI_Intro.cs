using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_Intro : UI_Master {


	public Text pressAnyKey;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.anyKeyDown) {
			Application.LoadLevel("Main");
		}

		pressAnyKey.GetComponent<CanvasGroup> ().alpha = Mathf.PingPong (Time.time/5, 1);
	
	}
}
