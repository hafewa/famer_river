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
    wolf = GameManager_LP.Instance.wolf.GetComponent<Wolf_LP>();
    chicken = GameManager_LP.Instance.wolf.GetComponent<Chicken_LP>();
    cabbage = GameManager_LP.Instance.wolf.GetComponent<Cabbage_LP>();
    player = Player_LP.Instance;
    InvokeRepeating("CheckSuccessAndFailure", 20.0f, 20.0f);
  }

  void CheckSuccessAndFailure(){
    FailCheck();
    SuccessCheck();
  }

  void FailCheck()
  {

    if((wolf.animalStatus) == chicken.animalStatus){
      if (wolf.animalStatus == cabbage.animalStatus)
      {
        //Game has not officially begun... so let the player explore...
        return;
      }
    }

    if ((chicken.animalStatus == wolf.animalStatus))
    {
      if (player.playerBank != chicken.animalStatus)
      {
        OnFail("You left the chicken alone with the wolf");
      }
    }

    if ((chicken.animalStatus == cabbage.animalStatus))
    {
      if (player.playerBank != cabbage.animalStatus)
      {
        OnFail("You left the cabbage alone with the chicken");
      }
    }
  }

  //call this script when boat encounters WestBank. 
  void SuccessCheck()
  {
    if(wolf.animalStatus == BankStatus.RedBank && chicken.animalStatus == BankStatus.RedBank 
       && cabbage.animalStatus == BankStatus.RedBank && player.playerBank == BankStatus.RedBank){
      OnSuccess();
    }
  }
}
