﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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

  public override void TransferToBank()
  {
    //transition the animal to the bank
    switch (Boat_LP.Instance.boatStatus)
    {
      case BankStatus.RedBank:
        animalStatus = BankStatus.RedBank;
        transform.position = GameManager_LP.Instance.chickenSpotRedBank;
        transform.SetParent(null);
        break;
      case BankStatus.YellowBank:
        animalStatus = BankStatus.YellowBank;
        transform.position = GameManager_LP.Instance.chickenSpotYellowBank;
        transform.SetParent(null);
        break;
    }
  }

  public void GazeOnChicken()
  {
    Debug.Log("Chicken stuff happening");
    if (PlayerGaze.Instance.myGazeStatus != GazeStatus.Chicken)
    {
      ChooseTextToDisplay();
      PlayerGaze.Instance.myGazeStatus = GazeStatus.Chicken;
    }
  }

}
