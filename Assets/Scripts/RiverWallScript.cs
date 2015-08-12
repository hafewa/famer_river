using UnityEngine;
using System.Collections;

public class RiverWallScript : MonoBehaviour {


  void OnEnable(){
    PlayerScript.OnPlayerLaunchBoat += hideWall;
    BoatScript.OnBoatLand += erectWall;
  }

	void OnDisable () {
	  PlayerScript.OnPlayerLaunchBoat -= hideWall;
    BoatScript.OnBoatLand -= erectWall;
	}
  
  public void hideWall(){
		GetComponent<Collider> ().enabled = false;
    //Debug.Log("Wall is hidden");
  }

  public void erectWall(string bank){
		GetComponent<Collider> ().enabled = true;
		//Debug.Log("Wall is UP");
  }
  	
	
}
