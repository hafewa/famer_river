using UnityEngine;
using System.Collections;

public enum BoatState{EastBank, WestBank, Traveling, Tipped};

public class BoatScript : MonoBehaviour {

  
  public GameObject EastBankLandingSpot;
  public GameObject WestBankLandingSpot;
  public static BoatState boat_state;
  public static BoatState otherBank;
  private float speed = 4.0f;
  //public Transform target;
  public static bool moving;  
  public static bool objectInBoat;  
  public delegate void BoatLandEvent(string bank);
  public static event BoatLandEvent OnBoatLand;
		

	void OnTriggerEnter(Collider other){
    switch (other.gameObject.tag) {
    case "EastBankBoatSpot":
			boat_state = BoatState.EastBank;
	//		Debug.Log ("Arrived on EastBank");
 	  moving = false;
      
	  PlayerScript.beStill = false;	  
	  otherBank = BoatState.WestBank;	  
	  
	  OnBoatLand("east");
	  //GameManager_FailureChecker.CheckSuccess();
      break;
    case "WestBankBoatSpot":
	  boat_state = BoatState.WestBank;
	//  Debug.Log ("Arrived on WestBank");
	  moving = false;
	  PlayerScript.beStill = false;
    	otherBank = BoatState.EastBank;
	  //all of the animals subscribe to this event
	  
	  OnBoatLand("west");
	  GameManager_FailureChecker.CheckSuccess();

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
   // Debug.Log("Boat responds to player in boat event,as does river");
  }

	// Use this for initialization
	void Start () {

#if UNITY_EDITOR
		speed = 10;
		#endif


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
