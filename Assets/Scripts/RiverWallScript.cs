using UnityEngine;
using System.Collections;

public class RiverWallScript : MonoBehaviour {


  void OnEnable(){
    PlayerScript.OnBoatLaunch += hideWall;
    PlayerScript.OnBoatLand += erectWall;
  }

	void OnDisable () {
	  PlayerScript.OnBoatLaunch -= hideWall;
    PlayerScript.OnBoatLand -= erectWall;
	}
  
  public void hideWall(){
    gameObject.SetActive(false);
    Debug.Log("Wall is hidden");
  }

  public void erectWall(){
    gameObject.SetActive(true);
    Debug.Log("Wall is UP");
  }
  	
	
}
