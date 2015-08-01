using UnityEngine;
using System.Collections;

public class GameManager_FailureChecker : MonoBehaviour {



	public delegate void FailureEvent(string failStringy);
	public static event FailureEvent OnFailMet;

	//We check for failure only when the boat is half way across the river.
//	void OnTriggerEnter(){
//		CheckFailure ();
//	}
	void OnTriggerStay(){
		CheckFailure ();
	}
	
	void CheckFailure(){
		if (ChickenScript.my_state == ChickenScript.MyState.EastBank && WolfScript.my_state == WolfScript.MyState.EastBank && PlayerScript.inBoat) {
			OnFailMet ("You left the chicken alone with the wolf");
		} else if (ChickenScript.my_state == ChickenScript.MyState.WestBank && WolfScript.my_state == WolfScript.MyState.WestBank && PlayerScript.inBoat) {
			OnFailMet ("You left the chicken alone with the wolf");
		} else if (ChickenScript.my_state == ChickenScript.MyState.WestBank && CabbageScript.my_state == CabbageScript.MyState.WestBank && PlayerScript.inBoat) {
			OnFailMet ("You left the cabbage alone with the chicken");
		} else if (ChickenScript.my_state == ChickenScript.MyState.EastBank && CabbageScript.my_state == CabbageScript.MyState.EastBank && PlayerScript.inBoat) {
			OnFailMet("You left the cabbage alone with the chicken");
		}

		if (ChickenScript.my_state == ChickenScript.MyState.EastBank && WolfScript.my_state == WolfScript.MyState.EastBank 
			&& ChickenScript.my_state == ChickenScript.MyState.EastBank && CabbageScript.my_state == CabbageScript.MyState.EastBank && PlayerScript.inBoat) {
			OnFailMet ("The wolf will eat the chicken in your absence.\nOr the chicken will eat the cabbage.");
		} else if (ChickenScript.my_state == ChickenScript.MyState.WestBank && WolfScript.my_state == WolfScript.MyState.WestBank 
			&& ChickenScript.my_state == ChickenScript.MyState.WestBank && CabbageScript.my_state == CabbageScript.MyState.WestBank && PlayerScript.inBoat) {
			OnFailMet("The wolf will eat the chicken in your absence.\nOr the chicken will eat the cabbage.");
		}

	}

	//call this script when boat encounters WestBank. 
	public void CheckSuccess(){


	}


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
