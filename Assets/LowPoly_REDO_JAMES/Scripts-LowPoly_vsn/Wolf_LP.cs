﻿using System.Collections;
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


  public override void TransferToBank()
  {
    //base.TransferToBank();
    transform.SetParent(null);
//    Debug.Log("Wolf_LP PARENT SHOULD BE: "+transform.parent.name);
    transform.GetComponent<Collider>().enabled = true;
    //transition the animal to the bank
    switch (Boat_LP.Instance.boatStatus)
    {
      case BankStatus.RedBank:
        animalStatus = BankStatus.RedBank;
        transform.position = GameManager_LP.Instance.wolfSpotRedBank;
        break;
      case BankStatus.YellowBank:
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
      //GetComponent<GlowObject>().GazeEnter();
    }
  }



}
