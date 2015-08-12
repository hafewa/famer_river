using UnityEngine;
using System.Collections;

public class ChickenScript : Animal {

	public GameObject myMarkInBoat;
	public Vector3 myMarkOnEastBank;
	public Vector3 myMarkOnWestBank; 
	public static bool inBoat;
	public enum MyState{EastBank, WestBank, InBoat, Dead};
	public static MyState my_state;

  void OnEnable(){
    PlayerScript.OnPress_C += PlaceInBoat;
    PlayerScript.OnPress_RC += PlaceOnShore;
		BoatScript.OnBoatLand += CheckBank;
    //GameManager_FailureChecker.OnFailMet += Reset;
  }
  
  void OnDisable(){
    PlayerScript.OnPress_C -= PlaceInBoat;
    PlayerScript.OnPress_RC -= PlaceOnShore;
		BoatScript.OnBoatLand -= CheckBank;
    //GameManager_FailureChecker.OnFailMet -= Reset;
  }

  void Start(){
	my_state = MyState.EastBank;
    myMarkOnEastBank = gameObject.transform.position;
    myMarkOnWestBank = GameObject.FindWithTag("chx_mark_west").transform.position;
    //myMarkInBoat = GameObject.FindWithTag("chx_mark_in_boat").transform.position;
    //base.GetMyState();
  }

  void Update(){
    if (inBoat) {
      //////myMarkInBoat = GameObject.FindWithTag("chx_mark_in_boat").transform.position;
      //gameObject.transform.position = myMarkInBoat;
    }

  }

  public void PlaceInBoat(){
    gameObject.transform.position = myMarkInBoat.transform.position;
    gameObject.transform.parent = GameObject.FindWithTag ("Boat").transform;
	//transform.parent = GameObject.FindWithTag ("Boat").transform;
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
