using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cabbage_LP : Animal_LP {

	// Use this for initialization
  public override void Start()
  {
    base.Start();
    mySpotOnYellowBank = transform.position;
    GameManager_LP.Instance.cabbageSpotYellowBank = mySpotOnYellowBank;
    mySpotOnRedBank = GameManager_LP.Instance.cabbageSpotRedBank;
    myRotYellowBank = transform.rotation.eulerAngles;
    myRotRedBank = GameManager_LP.Instance.cabbageRotationRedBank;
	}
	


  public override void TransferToBank(string whichBank)
  {
    //transition the animal to the bank
    switch (whichBank)
    {
      case "redBank":
        myStatus = BankStatus.RedBank;
        transform.position = GameManager_LP.Instance.cabbageSpotRedBank;
        break;
      case "yellowBank":
        myStatus = BankStatus.YellowBank;
        transform.position = GameManager_LP.Instance.cabbageSpotYellowBank;
        break;
    }
  }
}
