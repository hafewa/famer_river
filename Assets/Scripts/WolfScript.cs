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

  void Update(){
    if (inBoat) {
      myMarkInBoat = GameObject.FindWithTag ("wolf_mark_in_boat").transform.position;
      //gameObject.transform.position = myMarkInBoat;
    }
  }

  public void PlaceInBoat(){
    gameObject.transform.position = myMarkInBoat;
    gameObject.transform.parent = GameObject.FindWithTag ("Boat").transform;
    inBoat = true;
  }
  
  public void PlaceOnShore(){
    gameObject.transform.parent = null;
    inBoat = false;
    if (BoatScript.boat_state == BoatState.EastBank) {
      gameObject.transform.position = myMarkOnEastBank;
    } else if (BoatScript.boat_state == BoatState.WestBank) {
      gameObject.transform.position = myMarkOnWestBank;
    }
  }
}
