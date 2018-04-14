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

  public BankStatus myStatus;

  public Vector3 mySpotOnRedBank;
  public Vector3 mySpotOnYellowBank;

  public Vector3 myRotRedBank;
  public Vector3 myRotYellowBank;

  public bool gazeIsOnMe;

	// Use this for initialization
	public virtual void Start () {
    myId = gameObject.name;
    myStatus = BankStatus.YellowBank;

	}
	
	// Update is called once per frame
	void Update () {
    if(Input.GetKeyDown(KeyCode.Space) ){
      if(PlayerGaze.Instance.objectOfMyGaze.Equals(gameObject)){
        if(!myStatus.Equals(BankStatus.Boat)){
          TransferToBoat();
        }
      }
    }
	}

  public virtual void TransferToBank(string whichBank){

  }

  public virtual void TransferToBoat(){
    myStatus = BankStatus.Boat;
    transform.position = Boat_LP.Instance.cargoPosition.position;
    transform.SetParent(Boat_LP.Instance.cargoPosition);
    Boat_LP.Instance.cargo = gameObject;
  }

}
