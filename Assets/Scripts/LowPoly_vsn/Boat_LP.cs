using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat_LP : MonoBehaviour {

  //public enum BoatStatus { Idle, YellowBank, RedBank};
  public BankStatus boatStatus;

  public GameObject playerBoatHolder;
  public Transform cargoPosition;

  public GameObject cargo;

  public static Boat_LP Instance;

  void OnEnable()
  {
    PlayerGaze.OnGazeHitBoat += GazeOnBoat;
  }

  void OnDisable()
  {
    PlayerGaze.OnGazeHitBoat -= GazeOnBoat;
  }

  void Awake(){
    if (Instance == null)
      Instance = this;
    else
      Destroy(Instance);
  }

  void Start(){
    boatStatus = BankStatus.None;
  }

  void Update()
  {
    float distToRedBank = Vector3.Distance(transform.position, GameManager_LP.Instance.RedBankMarker.position);
    float distToYellowBank = Vector3.Distance(transform.position, GameManager_LP.Instance.YellowBankMarker.position);
    if (distToYellowBank < 7.0f)
    {
      boatStatus = BankStatus.YellowBank;
      Debug.Log("Boat on Yellow Coast");
      if (cargo)
      {
        cargo.GetComponent<Animal_LP>().myStatus = boatStatus;
        UnloadTheBoat("yellowBank");
      }
    }

    if (distToRedBank < 7.0f)
    {
      boatStatus = BankStatus.RedBank;
      Debug.Log("Boat on Red Coast");
      if(cargo){
        cargo.GetComponent<Animal_LP>().myStatus = boatStatus;
        UnloadTheBoat("redBank");

      }
    }
  }

  void UnloadTheBoat(string bankType){
    cargo.GetComponent<Animal_LP>().TransferToBank(bankType);
  }

  public void GazeOnBoat()
  {
    Debug.Log("Boat stuff happening");

  }

}
