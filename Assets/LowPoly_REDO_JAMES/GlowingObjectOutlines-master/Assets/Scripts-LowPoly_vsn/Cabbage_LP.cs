﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Cabbage_LP : Animal_LP {


  void OnEnable()
  {
    PlayerGaze.OnGazeHitCabbage += GazeOnCabbage;
  }

  void OnDisable()
  {
    PlayerGaze.OnGazeHitCabbage -= GazeOnCabbage;
  }

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

  //public override void TransferToBank()
  //{
  //  //base.TransferToBank();
  //  //transition the animal to the bank
  //  transform.SetParent(null);
  //  transform.GetComponent<Collider>().enabled = true;
  //  switch (Boat_LP.Instance.boatStatus)
  //  {
  //    case BankStatus.RedBank:
  //      animalStatus = BankStatus.RedBank;
  //      transform.position = GameManager_LP.Instance.cabbageSpotRedBank;
  //      break;
  //    case BankStatus.YellowBank:
  //      animalStatus = BankStatus.YellowBank;
  //      transform.position = GameManager_LP.Instance.cabbageSpotYellowBank;
  //      break;
  //  }
  //}


  public void GazeOnCabbage()
  {
    Debug.Log("Cabbage stuff happening");
    if (PlayerGaze.Instance.myGazeStatus != GazeStatus.Cabbage)
    {
      ChooseTextToDisplay();
      PlayerGaze.Instance.myGazeStatus = GazeStatus.Cabbage;
      //GetComponent<GlowObject>().GazeEnter();

    }
  }
}
