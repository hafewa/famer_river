using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Wolf_LP : Animal_LP {


  void OnEnable(){
    PlayerGaze.OnGazeHitWolf += GazeOnWolf;
  }

  void OnDisable(){
    PlayerGaze.OnGazeHitWolf -= GazeOnWolf;
  }

	// Use this for initialization
	public override void Start () {
    base.Start();
    mySpotOnYellowBank = transform.position;
    GameManager_LP.Instance.wolfSpotYellowBank = mySpotOnYellowBank;
    mySpotOnRedBank = GameManager_LP.Instance.wolfSpotRedBank;
    myRotYellowBank = transform.rotation.eulerAngles;
    myRotRedBank = GameManager_LP.Instance.wolfRotationRedBank;
	}


  public override void TransferToBank(string whichBank)
  {
    //transition the animal to the bank
    switch (whichBank)
    {
      case "redBank":
        animalStatus = BankStatus.RedBank;
        transform.position = GameManager_LP.Instance.wolfSpotRedBank;
        break;
      case "yellowBank":
        animalStatus = BankStatus.YellowBank;
        transform.position = GameManager_LP.Instance.wolfSpotYellowBank;
        break;
    }
  }

  public void GazeOnWolf()
  {
    Debug.Log("Wolf stuff happening");
    if (PlayerGaze.Instance.myGazeStatus != GazeStatus.Wolf)
    {
      ChooseTextToDisplay();
      PlayerGaze.Instance.myGazeStatus = GazeStatus.Wolf;
    }
  }



}
