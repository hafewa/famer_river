using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Boat_LP : MonoBehaviour {

  //public enum BoatStatus { Idle, YellowBank, RedBank};
  public BankStatus boatStatus;

  public GameObject playerBoatHolder;
  public Transform cargoPosition;

  public GameObject cargo;

  public static Boat_LP Instance;

  public bool originalPosNeedsSet;

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
        cargo.GetComponent<Animal_LP>().animalStatus = boatStatus;
        UnloadTheBoat();
      }
    }

    if (distToRedBank < 7.0f)
    {
      boatStatus = BankStatus.RedBank;
      Debug.Log("Boat on Red Coast");
      if(cargo){
        cargo.GetComponent<Animal_LP>().animalStatus = boatStatus;
        UnloadTheBoat();
      }
    }


    if (Input.GetKeyDown(KeyCode.Space))
    {
      if (Player_LP.Instance.playerStatus == PlayerStatus.DraggingBoat)
      {
        DetachBoatFromPlayer();
      }
      //if we hit the space bar and we're looking at the boat...
      if (PlayerGaze.Instance.myGazeStatus.Equals(GazeStatus.Boat) && Player_LP.Instance.playerStatus != PlayerStatus.DraggingBoat)
      {
        AttachBoatToPlayer();
      }
    }
  }

  void AttachBoatToPlayer(){
    Debug.Log("Attaching boat to player");
    if (cargo)
    {
      cargo.transform.SetParent(transform);
    }
    //Make player drag the boat...
    Player_LP.Instance.thingToPull = gameObject;
    Player_LP.Instance.playerStatus = PlayerStatus.DraggingBoat;
    ChooseTextToDisplay();
    Player_LP.Instance.pullingBoat = true;
    //PLAY PARTICLE EFFECTS HERE
    transform.Find("Boat").transform.Find("Particle System").GetComponent<ParticleSystem>().Play();
    //originalPosNeedsSet = true;

  }

  void DetachBoatFromPlayer(){
    //release the boat object from the player
    Player_LP.Instance.thingToPull = null;
    Player_LP.Instance.playerStatus = PlayerStatus.None;
    PlayerGaze.Instance.ClearGaze();
    Player_LP.Instance.pullingBoat = false;
    transform.Find("Boat").transform.Find("Particle System").GetComponent<ParticleSystem>().Stop();
    if (cargo)
    {
      cargo.transform.SetParent(transform.Find("Boat"));
    }
  }

  public void UnloadTheBoat(){
    cargo.GetComponent<Animal_LP>().TransferToBank();
  }

  public void GazeOnBoat()
  {
    Debug.Log("Boat stuff happening");
    if (PlayerGaze.Instance.myGazeStatus != GazeStatus.Boat)
    {
      ChooseTextToDisplay();
      PlayerGaze.Instance.myGazeStatus = GazeStatus.Boat;
    }
  }

  void ChooseTextToDisplay()
  {
    if (Player_LP.Instance.playerStatus != PlayerStatus.DraggingBoat)
    {
      StartCoroutine(UIManager_LP.Instance.InstructionsTextIncoming(String.Format("Press Space to pull the boat")));
    } else if(Player_LP.Instance.playerStatus == PlayerStatus.DraggingBoat){
      StartCoroutine(UIManager_LP.Instance.InstructionsTextIncoming(String.Format("Press Space to release the boat")));
    }
  }

}
