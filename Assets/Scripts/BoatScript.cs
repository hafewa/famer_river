using UnityEngine;
using System.Collections;

public class BoatScript : MonoBehaviour {


  public enum MyState{EastBank, WestBank, Traveling, Tipped};
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
	
	}
	
	// Update is called once per frame
	void Update () {
	  if(moving){
      float step = speed * Time.deltaTime;
      transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }
	}
}
