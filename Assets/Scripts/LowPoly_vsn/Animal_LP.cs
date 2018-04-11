using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal_LP : MonoBehaviour {


  //Responsibilities: 
  //Keep track of my state (ie which bank)
  //Create Transitions to go on and off of boat
  //Have a "Selectable" state

  [HideInInspector]
  public string myId; //ie "Wolf"
  public enum MyStatus { RedBank, YellowBank, Boat, Dead };
  public static MyStatus myStatus;

  public Vector3 mySpotOnRedBank;
  public Vector3 mySpotOnYellowBank;

  public Vector3 myRotRedBank;
  public Vector3 myRotYellowBank;

  public bool gazeIsOnMe;

	// Use this for initialization
	void Start () {
    myId = gameObject.name;
    myStatus = MyStatus.YellowBank;
    switch(myId){
      case "Wolf":
        mySpotOnYellowBank = transform.position;
        GameManager_LP.Instance.wolfSpotYellowBank = mySpotOnYellowBank;
        mySpotOnRedBank = GameManager_LP.Instance.wolfSpotRedBank;
        myRotYellowBank = transform.rotation.eulerAngles;
        myRotRedBank = GameManager_LP.Instance.wolfRotationRedBank;
        break;
      case "Chicken":
        mySpotOnYellowBank = transform.position;
        GameManager_LP.Instance.chickenSpotYellowBank = mySpotOnYellowBank;
        mySpotOnRedBank = GameManager_LP.Instance.chickenSpotRedBank;
        myRotYellowBank = transform.rotation.eulerAngles;
        myRotRedBank = GameManager_LP.Instance.chickenRotationRedBank;
        break;
      case "Cabbage":
        mySpotOnYellowBank = transform.position;
        GameManager_LP.Instance.cabbageSpotYellowBank = mySpotOnYellowBank;
        mySpotOnRedBank = GameManager_LP.Instance.cabbageSpotRedBank;
        myRotYellowBank = transform.rotation.eulerAngles;
        myRotRedBank = GameManager_LP.Instance.cabbageRotationRedBank;
        break;
    }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

  public void TransferToBank(string whichBank){
    //transition the animal to the bank
    switch(whichBank){
      case "redBank":
        myStatus = MyStatus.RedBank;

        break;
      case "yellowBank":
        myStatus = MyStatus.YellowBank;
        break;
    }
  }

  public void TransferToBoat(){
    myStatus = MyStatus.Boat;

  }

}
