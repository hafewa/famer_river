using UnityEngine;
using System.Collections;

public class ChickenScript : Animal {

	public Vector3 myMarkInBoat;
	public Vector3 myMarkOnEastBank;
	public Vector3 myMarkOnWestBank; 
	public static bool inBoat;

  void OnEnable(){
    PlayerScript.OnPress_C += PlaceInBoat;
    PlayerScript.OnPress_RC += PlaceOnShore;
  }
  
  void OnDisable(){
    PlayerScript.OnPress_C -= PlaceInBoat;
    PlayerScript.OnPress_RC -= PlaceOnShore;
  }

  void Start(){
    myMarkOnEastBank = gameObject.transform.position;
    myMarkOnWestBank = GameObject.FindWithTag("chx_mark_west").transform.position;
    myMarkInBoat = GameObject.FindWithTag("chx_mark_in_boat").transform.position;
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
