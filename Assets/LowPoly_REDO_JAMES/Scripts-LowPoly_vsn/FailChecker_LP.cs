using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailChecker_LP : MonoBehaviour {

  public delegate void FailEvent(string failMessage);
  public static event FailEvent OnFail;
  public delegate void SuccessEvent();
  public static event SuccessEvent OnSuccess;

  Wolf_LP wolf;
  Chicken_LP chicken;
  Cabbage_LP cabbage;
  Player_LP player;

  void Start(){
    wolf = (Wolf_LP)GameManager_LP.Instance.wolf;
    chicken = (Chicken_LP)GameManager_LP.Instance.chicken;
    cabbage = (Cabbage_LP)GameManager_LP.Instance.cabbage;
    player = Player_LP.Instance;
    InvokeRepeating("CheckSuccessAndFailure", 20.0f, 20.0f);
  }

  void CheckSuccessAndFailure(){
    FailCheck();
    SuccessCheck();
  }

  void FailCheck()
  {

    if(wolf.animalStatus == chicken.animalStatus){
      if (wolf.animalStatus == cabbage.animalStatus)
      {
        //Game has not officially begun... so let the player explore...
        return;
      }
    }

    if (chicken.animalStatus == wolf.animalStatus)
    {
      if (player.playerBank != chicken.animalStatus)
      {
        Debug.Log("FAIL STATE!");
        if (OnFail != null)
          OnFail("You left the chicken alone with the wolf");
      }
    }

    if (chicken.animalStatus == cabbage.animalStatus)
    {
      if (player.playerBank != cabbage.animalStatus)
      {
        Debug.Log("FAIL STATE!");
        if(OnFail!=null)
          OnFail("You left the cabbage alone with the chicken");
      }
    }
  }

  //call this script when boat encounters WestBank. 
  void SuccessCheck()
  {
    if(wolf.animalStatus == BankStatus.RedBank && chicken.animalStatus == BankStatus.RedBank 
       && cabbage.animalStatus == BankStatus.RedBank && player.playerBank == BankStatus.RedBank){
      if (OnSuccess!=null)
      {
        Debug.Log("SUCCESS!");
        OnSuccess();
      }
    }
  }
}
