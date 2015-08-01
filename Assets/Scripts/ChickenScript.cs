using UnityEngine;
using System.Collections;

public class ChickenScript : Animal {

	public Vector3 myMarkInBoat;
	public Vector3 myMarkOnEastBank;
	public Vector3 myMarkOnWestBank; 
	public static bool inBoat;
	public enum MyState{EastBank, WestBank, InBoat, Dead};
	public static MyState my_state;

  void OnEnable(){
    PlayerScript.OnPress_C += PlaceInBoat;
    PlayerScript.OnPress_RC += PlaceOnShore;
		BoatScript.OnBoatLand += CheckBank;
  }
  
  void OnDisable(){
    PlayerScript.OnPress_C -= PlaceInBoat;
    PlayerScript.OnPress_RC -= PlaceOnShore;
		BoatScript.OnBoatLand -= CheckBank;
  }

  void Start(){
	my_state = MyState.EastBank;
    myMarkOnEastBank = gameObject.transform.position;
    myMarkOnWestBank = GameObject.FindWithTag("chx_mark_west").transform.position;
    myMarkInBoat = GameObject.FindWithTag("chx_mark_in_boat").transform.position;
    base.GetMyState();
  }

  void Update(){
    if (inBoat) {
      myMarkInBoat = GameObject.FindWithTag("chx_mark_in_boat").transform.position;
      //gameObject.transform.position = myMarkInBoat;
    }

  }

  public void PlaceInBoat(){
    gameObject.transform.position = myMarkInBoat;
    gameObject.transform.parent = GameObject.FindWithTag ("Boat").transform;
    inBoat = true;
		my_state = MyState.InBoat;
  }

  public void PlaceOnShore(){
    inBoat = false;
    gameObject.transform.parent = null;
    if (BoatScript.boat_state == BoatState.EastBank) {
      gameObject.transform.position = myMarkOnEastBank;
			my_state = MyState.EastBank;
    } else if (BoatScript.boat_state == BoatState.WestBank) {
      gameObject.transform.position = myMarkOnWestBank;
			my_state = MyState.WestBank;
    }
  }

	public void CheckBank(string bank){
		if (bank == "west") {
			my_state = MyState.WestBank;
		} else if( bank == "east") {
			my_state =MyState.EastBank;
		}
	}

}
