using UnityEngine;
using System.Collections;

public class WolfScript : Animal {

	public Vector3 myMarkInBoat;
	public static bool inBoat;
	public Vector3 myMarkOnEastBank;
	public Vector3 myMarkOnWestBank;

  void OnEnable(){
    PlayerScript.OnPress_W += PlaceInBoat;
    PlayerScript.OnPress_RW += PlaceOnShore;
  }

  void OnDisable(){
    PlayerScript.OnPress_W -= PlaceInBoat;
    PlayerScript.OnPress_RW -= PlaceOnShore;
  }

  void Start(){
    myMarkInBoat = GameObject.FindWithTag("wolf_mark_in_boat").transform.position;
		myMarkOnEastBank = gameObject.transform.position;
		myMarkOnWestBank = GameObject.FindWithTag("wolf_mark_west").transform.position;
    base.GetMyState();
  }

  public void PlaceInBoat(){
    gameObject.transform.position = myMarkInBoat;
    inBoat = true;
  }
  
  public void PlaceOnShore(){
    inBoat = false;
    if (BoatScript.boat_state == BoatState.EastBank) {
      gameObject.transform.position = myMarkOnEastBank;
    } else if (BoatScript.boat_state == BoatState.WestBank) {
      gameObject.transform.position = myMarkOnWestBank;
    }
  }
}
