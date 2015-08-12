using UnityEngine;
using System.Collections;

public class WolfScript : Animal {

	public GameObject myMarkInBoat;
	public static bool inBoat;
	public Vector3 myMarkOnEastBank;
	public Vector3 myMarkOnWestBank;
	public enum MyState{EastBank, WestBank, InBoat, Dead};
	public static MyState my_state;
	

  void OnEnable(){
    PlayerScript.OnPress_W += PlaceInBoat;
    PlayerScript.OnPress_RW += PlaceOnShore;
	BoatScript.OnBoatLand += CheckBank;
    //GameManager_FailureChecker.OnFailMet += Reset;
	//PlayerScript.ResetEvent += Reset;

  }

  void OnDisable(){
    PlayerScript.OnPress_W -= PlaceInBoat;
    PlayerScript.OnPress_RW -= PlaceOnShore;
		BoatScript.OnBoatLand -= CheckBank;
    //GameManager_FailureChecker.OnFailMet -= Reset;
		//PlayerScript.ResetEvent -= Reset;

  }

  void Start(){
    ///////myMarkInBoat = GameObject.FindWithTag("wolf_mark_in_boat").transform.position;
		myMarkOnEastBank = gameObject.transform.position;
		myMarkOnWestBank = GameObject.FindWithTag("wolf_mark_west").transform.position;
		my_state = MyState.EastBank;
    //base.GetMyState();
  }

  void Update(){
    if (inBoat) {
      /////////myMarkInBoat = GameObject.FindWithTag ("wolf_mark_in_boat").transform.position;
      //gameObject.transform.position = myMarkInBoat;
    }
  }

  public void PlaceInBoat(){
    gameObject.transform.position = myMarkInBoat.transform.position;
    gameObject.transform.parent = GameObject.FindWithTag ("Boat").transform;
    inBoat = true;
	my_state = MyState.InBoat;
  }
  
  public void PlaceOnShore(){
		if (inBoat) {
			inBoat = false;
			transform.parent = null;
			if (BoatScript.boat_state == BoatState.EastBank) {
				gameObject.transform.position = myMarkOnEastBank;
				my_state = MyState.EastBank;
				//Debug.Log ("cabbage state = " +  CabbageScript.my_state.ToString () + "wolf state " + WolfScript.my_state.ToString () + " chx state " + ChickenScript.my_state.ToString () + " player state " + PlayerScript.my_state.ToString() + "boat state" + BoatScript.boat_state.ToString());

			} else if (BoatScript.boat_state == BoatState.WestBank) {
				gameObject.transform.position = myMarkOnWestBank;
				my_state = MyState.WestBank;
				//Debug.Log ("cabbage state = " +  CabbageScript.my_state.ToString () + "wolf state " + WolfScript.my_state.ToString () + " chx state " + ChickenScript.my_state.ToString () + " player state " + PlayerScript.my_state.ToString() + "boat state" + BoatScript.boat_state.ToString());

			}
		}
  }

  	public void CheckBank(string bank){
		if (bank == "west") {

			if(inBoat){
				//my_state = MyState.WestBank;
				PlaceOnShore();

			}
		} else if( bank == "east") {
			if(inBoat){
				//my_state =MyState.EastBank;
				PlaceOnShore();

			}
		}
	}

  public void Reset(string whatev){
    my_state = MyState.EastBank;
    inBoat = false;
  }


}
