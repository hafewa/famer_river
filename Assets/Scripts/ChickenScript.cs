using UnityEngine;
using System.Collections;

public class ChickenScript : Animal {

	public Vector3 myMarkInBoat;
	public Vector3 myMarkOnEastBank;
	public Vector3 myMarkOnWestBank; 
	public static bool inBoat;

  void OnEnable(){
    PlayerScript.OnPress_C += PlaceInBoat;
  }
  
  void OnDisable(){
    PlayerScript.OnPress_C -= PlaceInBoat;
  }

  void Start(){
    myMarkOnEastBank = gameObject.transform.position;
    myMarkOnWestBank = GameObject.FindWithTag("chx_mark_west").transform.position;
    myMarkInBoat = GameObject.FindWithTag("chx_mark_in_boat").transform.position;
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
