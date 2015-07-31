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
    //GetComponent<MeshCollider> ().isTrigger = true;
	gameObject.SetActive (false);
    Debug.Log("Wall is hidden");
  }

  public void erectWall(){
    //GetComponent<MeshCollider> ().isTrigger = false;
	gameObject.SetActive (false);
    Debug.Log("Wall is UP");
  }
  	
	
}
