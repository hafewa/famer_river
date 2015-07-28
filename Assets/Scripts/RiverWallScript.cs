using UnityEngine;
using System.Collections;

public class RiverWallScript : MonoBehaviour {


  void OnEnable(){
    PlayerScript.OnBoatLaunch += hideWall;
  }

	void OnDisable () {
	  PlayerScript.OnBoatLaunch -= hideWall;
	}
  
  public void hideWall(){
    gameObject.SetActive(false);
    Debug.Log("Wall is hidden");
  }
  	
	
}
