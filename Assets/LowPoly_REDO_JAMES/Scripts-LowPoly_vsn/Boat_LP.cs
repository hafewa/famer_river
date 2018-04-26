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

  float distToPlayer;

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
    distToPlayer = Vector3.Distance(transform.position, Player_LP.Instance.transform.position);
    //Debug.Log("Dist to player is: " + distToPlayer);
    if (Player_LP.Instance.playerStatus == PlayerStatus.DraggingBoat)
    {
      if (boatStatus != BankStatus.YellowBank)
      {
        if (distToYellowBank < 8.0f)
        {
          boatStatus = BankStatus.YellowBank;
          Debug.Log("BOAT Arrived on Yellow Coast!");
          if (cargo && distToYellowBank < 7.0f)
          {
            Debug.Log("UNLOADING BOAT ON THE YELLOW COAST");
            cargo.GetComponent<Animal_LP>().animalStatus = boatStatus;
            UnloadTheBoat();
          }
        }
      }

      if (boatStatus != BankStatus.RedBank)
      {
        if (distToRedBank < 8.0f)
        {
          boatStatus = BankStatus.RedBank;
          Debug.Log("BOAT Arrived on Red Coast!");
          if (cargo && distToRedBank < 7.0f)
          {
            Debug.Log("UNLOADING BOAT ON THE RED COAST");
            cargo.GetComponent<Animal_LP>().animalStatus = boatStatus;
            UnloadTheBoat();
          }
        }
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
    PlayerGaze.Instance.myGazeStatus = GazeStatus.None;

    ChooseTextToDisplay();
    Player_LP.Instance.pullingBoat = true;
    Debug.Log("Start PULL BOAT");
    //PLAY PARTICLE EFFECTS HERE
    transform.Find("Boat").transform.Find("Particle System").GetComponent<ParticleSystem>().Play();
    //originalPosNeedsSet = true;
  }

  void DetachBoatFromPlayer(){
    //release the boat object from the player
    Player_LP.Instance.thingToPull = null;
    Player_LP.Instance.playerStatus = PlayerStatus.None;
    PlayerGaze.Instance.myGazeStatus = GazeStatus.None;

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
    Debug.Log("Cargo wasss.... = " + cargo.name);
    cargo = null;

  }

  public void GazeOnBoat()
  {
    Debug.Log("Boat subscribes to OnGazeHItBoat event...this is it firing...");
    if (distToPlayer < 5.0f)
    {
      Debug.Log("Boat stuff happening");
      if (PlayerGaze.Instance.myGazeStatus != GazeStatus.Boat)
      {
        ChooseTextToDisplay();
        PlayerGaze.Instance.myGazeStatus = GazeStatus.Boat;
        Debug.Log("Setting the player gaze status to Boat");
      }
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
