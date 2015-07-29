using UnityEngine;
using System.Collections;

public class CabbageScript : Animal {

  public Vector3 myMarkInBoat;
  public Vector3 myMarkOnWestBank;
  public Vector3 myMarkOnEastBank;
  public static bool inBoat;
	
  void OnEnable(){
    PlayerScript.OnPress_G += PlaceInBoat;
    PlayerScript.OnPress_RG += PlaceOnShore;
  }

  void OnDisable(){
    PlayerScript.OnPress_G -= PlaceInBoat;
    PlayerScript.OnPress_RG -= PlaceOnShore;
  }

	// Use this for initialization
	void Start () {
    myMarkOnWestBank = GameObject.FindWithTag("cab_mark_west" ).transform.position;
    myMarkOnEastBank = gameObject.transform.position;
    myMarkInBoat = GameObject.FindWithTag("cab_mark_in_boat").transform.position;
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
