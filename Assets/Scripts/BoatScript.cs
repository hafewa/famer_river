using UnityEngine;
using System.Collections;

public class BoatScript : MonoBehaviour {

  public float speed;
  public Transform target;
  public bool moving;  
  public static bool objectInBoat;  

  void OnEnable(){
    PlayerScript.OnBoatLaunch += BoatMovement;
  }

  void OnDisable(){
    PlayerScript.OnBoatLaunch -= BoatMovement;
  }


  public void BoatMovement(){
    moving = true;
    //movetowards 
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
