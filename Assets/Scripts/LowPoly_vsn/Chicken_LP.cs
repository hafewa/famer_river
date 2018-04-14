using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken_LP : Animal_LP {

  void OnEnable()
  {
    PlayerGaze.OnGazeHitChicken += GazeOnChicken;
  }

  void OnDisable()
  {
    PlayerGaze.OnGazeHitChicken -= GazeOnChicken;
  }

	// Use this for initialization
  public override void Start()
  {
    base.Start();
    mySpotOnYellowBank = transform.position;
    GameManager_LP.Instance.chickenSpotYellowBank = mySpotOnYellowBank;
    mySpotOnRedBank = GameManager_LP.Instance.chickenSpotRedBank;
    myRotYellowBank = transform.rotation.eulerAngles;
    myRotRedBank = GameManager_LP.Instance.chickenRotationRedBank;
	}

  public override void TransferToBank(string whichBank)
  {
    //transition the animal to the bank
    switch (whichBank)
    {
      case "redBank":
        myStatus = BankStatus.RedBank;
        //if()
        transform.position = GameManager_LP.Instance.chickenSpotRedBank;
        break;
      case "yellowBank":
        myStatus = BankStatus.YellowBank;
        transform.position = GameManager_LP.Instance.chickenSpotYellowBank;
        break;
    }
  }

  public void GazeOnChicken()
  {
    Debug.Log("Chicken stuff happening");
  }

}
