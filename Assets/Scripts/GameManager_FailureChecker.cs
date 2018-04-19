using UnityEngine;
using System.Collections;

public class GameManager_FailureChecker : MonoBehaviour {



  public delegate void FailureEvent(string failMessage);
	public static event FailureEvent OnFailMet;
	public delegate void SuccessEvent();
	public static event SuccessEvent OnSuccess;

	//We check for failure only when the boat is half way across the river.
//	void OnTriggerEnter(){
//		CheckFailure ();
//	}
	void OnTriggerStay(){
		CheckFailure ();

	}
	
	void CheckFailure(){

		//Debug.Log ("cabbage state = " + CabbageScript.my_state.ToString () + "wolf state " + WolfScript.my_state.ToString () + " chx state " + ChickenScript.my_state.ToString () + " player state " + PlayerScript.my_state.ToString());

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
			&& CabbageScript.my_state == CabbageScript.MyState.EastBank && PlayerScript.inBoat) {
			OnFailMet ("The wolf will eat the chicken. Or the chicken will eat the cabbage. No bueno.");
		} else if (ChickenScript.my_state == ChickenScript.MyState.WestBank && WolfScript.my_state == WolfScript.MyState.WestBank 
			&& CabbageScript.my_state == CabbageScript.MyState.WestBank && PlayerScript.inBoat) {
			OnFailMet("The wolf will eat the chicken. Or the chicken will eat the cabbage. No bueno.");
		}

	}

	//call this script when boat encounters WestBank. 
	public static void CheckSuccess(){
		if (ChickenScript.my_state == ChickenScript.MyState.WestBank && WolfScript.my_state == WolfScript.MyState.WestBank 
			&& CabbageScript.my_state == CabbageScript.MyState.WestBank && PlayerScript.my_state == PlayerScript.MyState.WestBank) {
			OnSuccess(); 
			//Debug.Log ("Success = true");
		}

	}


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//CheckSuccess ();
	}
}
