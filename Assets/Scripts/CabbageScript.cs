using UnityEngine;
using System.Collections;

public class CabbageScript : Animal {

  public Vector3 myMarkInBoat;
  public Vector3 myMarkOnWestBank;
  public Vector3 myMarkOnEastBank;
  public static bool inBoat;
	
  void OnEnable(){
    PlayerScript.OnPress_G += PlaceInBoat;
  }

  void OnDisable(){
    PlayerScript.OnPress_G -= PlaceInBoat;
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
  }

  public void PlaceOnEast(){
    gameObject.transform.position = myMarkOnEastBank;
  }
  
  public void PlaceOnWest(){
    gameObject.transform.position = myMarkOnWestBank;
  }
}
