using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat_LP : MonoBehaviour {

  public enum BoatStatus { Idle, YellowBank, RedBank};
  public static BoatStatus boatStatus;

  void Start(){
    boatStatus = BoatStatus.Idle;
  }

  void Update(){
    float distToRedBank = Vector3.Distance(transform.position, GameManager_LP.Instance.RedBankMarker.position);
    float distToYellowBank = Vector3.Distance(transform.position, GameManager_LP.Instance.YellowBankMarker.position);
    if(distToYellowBank < 5.0f){
      boatStatus = BoatStatus.YellowBank;
      Debug.Log("Boat on Yellow Coast");

    }

    if (distToRedBank < 5.0f)
    {
      boatStatus = BoatStatus.RedBank;
      Debug.Log("Boat on Red Coast");
    }


  }


}
