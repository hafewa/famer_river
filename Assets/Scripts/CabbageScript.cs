using UnityEngine;
using System.Collections;

public class CabbageScript : Animal {

  public GameObject myMarkInBoat;
  public Vector3 myMarkOnWestBank;
  public Vector3 myMarkOnEastBank;
  public static bool inBoat;
  public enum MyState{EastBank, WestBank, InBoat, Dead};
  public static MyState my_state;
	
  void OnEnable(){
    PlayerScript.OnPress_G += PlaceInBoat;
    PlayerScript.OnPress_RG += PlaceOnShore;
    BoatScript.OnBoatLand += CheckBank;
    //GameManager_FailureChecker.OnFailMet += Reset;
  }

  void OnDisable(){
    PlayerScript.OnPress_G -= PlaceInBoat;
    PlayerScript.OnPress_RG -= PlaceOnShore;
	BoatScript.OnBoatLand -= CheckBank;
    //GameManager_FailureChecker.OnFailMet -= Reset;
  }

	// Use this for initialization
	void Start () {
    myMarkOnWestBank = GameObject.FindWithTag("cab_mark_west" ).transform.position;
    myMarkOnEastBank = gameObject.transform.position;
    //myMarkInBoat = GameObject.FindWithTag("cab_mark_in_boat").transform.position;
	my_state = MyState.EastBank;
    //base.GetMyState();	
	}

  void Update(){
    if (inBoat) {
      ////////myMarkInBoat = GameObject.FindWithTag ("cab_mark_in_boat").transform.position;
      //gameObject.transform.position = myMarkInBoat;
    }
  }

  public void PlaceInBoat(){
	Debug.Log ("PLACE CABBAGE IN BOAT");
    gameObject.transform.position = myMarkInBoat.transform.position;
    gameObject.transform.parent = GameObject.FindWithTag ("Boat").transform;
    inBoat = true;
		//transform.parent = GameObject.FindWithTag ("Boat").transform;
	my_state = MyState.InBoat;
		Debug.Log ("Cabbage state = "+my_state.ToString());
  }

  public void PlaceOnShore(){
		if (inBoat) {
			inBoat = false;
			transform.parent = null;
			if (BoatScript.boat_state == BoatState.EastBank) {
				gameObject.transform.position = myMarkOnEastBank;
				my_state = MyState.EastBank;
			} else if (BoatScript.boat_state == BoatState.WestBank) {
				gameObject.transform.position = myMarkOnWestBank;
				my_state = MyState.WestBank;
			}
		}
  }

	public void CheckBank(string bank){
		if (bank == "west") {
			if(inBoat){
				my_state = MyState.WestBank;
				PlaceOnShore();
			}
		} else if( bank == "east") {
			if(inBoat){
				my_state = MyState.EastBank;
				PlaceOnShore();
			}
		}
	}

  public void Reset(string whatev){
    my_state = MyState.EastBank;
    inBoat = false;
  }
	
}
