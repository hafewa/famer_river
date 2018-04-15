using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Animal_LP : MonoBehaviour {


  //Responsibilities: 
  //Keep track of my state (ie which bank)
  //Create Transitions to go on and off of boat
  //Have a "Selectable" state

  [HideInInspector]
  public string myId; //ie "Wolf"

  public BankStatus animalStatus;

  public Vector3 mySpotOnRedBank;
  public Vector3 mySpotOnYellowBank;

  public Vector3 myRotRedBank;
  public Vector3 myRotYellowBank;

  public bool gazeIsOnMe;


	// Use this for initialization
	public virtual void Start () {
    myId = gameObject.name;
    animalStatus = BankStatus.YellowBank;

	}
	
	// Update is called once per frame
	void Update () {
    if (Player_LP.Instance.playerStatus != PlayerStatus.DraggingBoat)
    {
      if (Input.GetKeyDown(KeyCode.Space))
      {
        if (PlayerGaze.Instance.objectOfMyGaze.Equals(gameObject))
        {
          if (!animalStatus.Equals(BankStatus.Boat))
          {
            TransferToBoat();
          }
        }
      }
    }
	}

  public virtual void TransferToBank(string whichBank){

  }

  public virtual void TransferToBoat(){
    animalStatus = BankStatus.Boat;
    transform.position = Boat_LP.Instance.cargoPosition.position;
    transform.SetParent(Boat_LP.Instance.cargoPosition);
    Boat_LP.Instance.cargo = gameObject;
  }

  protected void ChooseTextToDisplay()
  {
    if (Player_LP.Instance.playerStatus != PlayerStatus.DraggingBoat)
    {
      //if the boat's too far away...we tell the player to get the boat...
      if (Boat_LP.Instance.boatStatus != animalStatus)
      {
        StartCoroutine(UIManager_LP.Instance.InstructionsTextIncoming(String.Format("First bring the boat closer")));
      }
      //if the boat is close enough to the animal's bank...
      else if (Boat_LP.Instance.boatStatus == animalStatus)
      {
        //if the animal is not in the boat...
        if (animalStatus != BankStatus.Boat)
        {
          StartCoroutine(UIManager_LP.Instance.InstructionsTextIncoming(String.Format("Press Space to place the {0} in the boat", myId)));
        }
      }
    }
  }
}
