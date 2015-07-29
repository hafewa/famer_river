using UnityEngine;
using System.Collections;

public enum BoatState{EastBank, WestBank, Traveling, Tipped};

public class BoatScript : MonoBehaviour {



  public static BoatState boat_state;
  public float speed;
  public Transform target;
  public bool moving;  
  public static bool objectInBoat;  

  void OnEnable(){
    PlayerScript.OnBoatLaunch += BoatMovement;
   // PlayerScript.OnInBoat += PlayerInBoat;
  }

  void OnDisable(){
    PlayerScript.OnBoatLaunch -= BoatMovement;
   // PlayerScript.OnInBoat -= PlayerInBoat;   
  }


  public void BoatMovement(){
    moving = true;
    //movetowards other shore 
  }
  
  public void PlayerInBoat(){
    Debug.Log("Boat responds to player in boat event,as does river");
  }

	// Use this for initialization
	void Start () {
    boat_state = BoatState.EastBank;
	}
	
	// Update is called once per frame
	void Update () {
	  if(moving){
      float step = speed * Time.deltaTime;
      transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }
	}
}
