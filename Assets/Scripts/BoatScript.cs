using UnityEngine;
using System.Collections;

public enum BoatState{EastBank, WestBank, Traveling, Tipped};

public class BoatScript : MonoBehaviour {


  public GameObject EastBankLandingSpot;
  public GameObject WestBankLandingSpot;
  public static BoatState boat_state;
  public static BoatState otherBank;
  public float speed =5.0f;
  public Transform target;
  public static bool moving;  
  public static bool objectInBoat;  
  public delegate void BoatLandEvent(string bank);
  public static event BoatLandEvent OnBoatLand;
		

	void OnTriggerEnter(Collider other){
    switch (other.gameObject.tag) {
    case "EastBankBoatSpot":
	  Debug.Log ("Arrived on EastBank");
      boat_state = BoatState.EastBank;
      otherBank = BoatState.WestBank;
	  PlayerScript.beStill = false;
	  moving = false;
			//all of the animals subscribe to this event
	  OnBoatLand("east");
      break;
    case "WestBankBoatSpot":
	  Debug.Log ("Arrived on WestBank");
	  moving = false;
	  //all of the animals subscribe to this event
	  OnBoatLand("east");
      boat_state = BoatState.WestBank;
      otherBank = BoatState.EastBank;
      break;

    default: 
      break;
    }
  }

  void OnEnable(){
    PlayerScript.OnPlayerLaunchBoat += BoatMovement;
  }

  void OnDisable(){
    PlayerScript.OnPlayerLaunchBoat -= BoatMovement;
  }


  public void BoatMovement(){
    moving = true;
  }
  
  public void PlayerInBoat(){
    Debug.Log("Boat responds to player in boat event,as does river");
  }

	// Use this for initialization
	void Start () {
    boat_state = BoatState.EastBank;
    otherBank = BoatState.WestBank;
	}
	
	// Update is called once per frame
	void Update () {
	  if(moving){
      float step = speed * Time.deltaTime;
      if((int)otherBank == (int)BoatState.WestBank){
        transform.position = Vector3.MoveTowards(transform.position, WestBankLandingSpot.transform.position, step);
      } else {
        transform.position = Vector3.MoveTowards(transform.position, EastBankLandingSpot.transform.position, step);
      }
    }
	}
}
