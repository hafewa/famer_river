using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Animal_LP : MonoBehaviour {


  //Responsibilities: 
  //Keep track of my state (ie which bank)
  //Create Transitions to go on and off of boat
  //Have a "Selectable" state

  [HideInInspector]
  public string myId; //ie "Wolf"

  public BankStatus animalStatus;

  public Vector3 mySpotOnRedBank;
  public Vector3 mySpotOnYellowBank;

  public Vector3 myRotRedBank;
  public Vector3 myRotYellowBank;

  public bool gazeIsOnMe;
  public bool spacebarDown;

  protected float parabolaAnimation;

  bool canceledParabola;


	// Use this for initialization
	public virtual void Start () {
    myId = gameObject.name;
    animalStatus = BankStatus.YellowBank;
    //transform.Find("ParticleHolder").GetComponent<ParticleSystem>().Play();

	}
	
	// Update is called once per frame
	void Update () {

    if(Input.GetKeyDown(KeyCode.Space)){
      spacebarDown = true;
      if(CheckMoveAnimalToBoat()){
        TransferToBoat();
      }
    }

    if(Input.GetKeyUp(KeyCode.Space)){
      spacebarDown = false;
    }
	}

  public virtual void TransferToBank(){
    Debug.Log("Transfer to bank");
    StartCoroutine(TransferToBankParabola(transform.position, FindMyBankPosition(Boat_LP.Instance.boatStatus)));
    ////////transform.position = FindMyBankPosition(Boat_LP.Instance.boatStatus);
  }

  public virtual bool CheckMoveAnimalToBoat(){
    if (Player_LP.Instance.playerStatus != PlayerStatus.DraggingBoat)
    {
      if (PlayerGaze.Instance.lastGazeSelect && PlayerGaze.Instance.lastGazeSelect.Equals(gameObject))
      {
          if (!animalStatus.Equals(BankStatus.Boat))
          {
            if (Boat_LP.Instance.boatStatus == animalStatus)
            {
            return true;
            }
          }
      }
    }
    return false;
  }

  public virtual void TransferToBoat(){
    if(Boat_LP.Instance.cargo){
      Boat_LP.Instance.UnloadTheBoat();
    }
    StartCoroutine(TransferToBoatParabola(transform.position, Boat_LP.Instance.cargoPosition.position));
  }


  IEnumerator TransferToBankParabola(Vector3 startPos, Vector3 endPos){
    float dist = 0;
    transform.Find("ParticleHolder").GetComponent<ParticleSystem>().Play();
    Debug.Log("Parabolizzzzzzzzing to BANK");
    dist = Vector3.Distance(transform.position, endPos);
    while (dist > 0.1f)
    {
      Debug.Log("Inside the while lOOP -- attempting to transfer to bank...");
      parabolaAnimation += Time.deltaTime;
      parabolaAnimation = parabolaAnimation % 5f;
      dist = Vector3.Distance(transform.position, endPos);
      transform.position = MathParabola.Parabola(startPos, endPos, 1.2f, parabolaAnimation / 5f);
      yield return null;
    }
    transform.Find("ParticleHolder").GetComponent<ParticleSystem>().Stop();
    transform.position = FindMyBankPosition(Boat_LP.Instance.boatStatus);
    transform.SetParent(null);
    transform.GetComponent<Collider>().enabled = true;

  }


  IEnumerator TransferToBoatParabola(Vector3 startPos, Vector3 endPos){
    float dist = 0;
    transform.Find("ParticleHolder").GetComponent<ParticleSystem>().Play();
    Debug.Log("Moving into Boat");
    dist = Vector3.Distance(transform.position, endPos);
    while(dist > 0.1f){
      parabolaAnimation += Time.deltaTime;
      parabolaAnimation = parabolaAnimation % 5f;
      if (spacebarDown)
      {
        endPos = Boat_LP.Instance.cargoPosition.position;
        Debug.Log("we're inside the parabola loop");
        dist = Vector3.Distance(transform.position, endPos);
        transform.position = MathParabola.Parabola(startPos, endPos, 1.2f, parabolaAnimation / 5f);
      } else {
        canceledParabola = true;
        dist = Vector3.Distance(transform.position, startPos);
        transform.position = MathParabola.Parabola(transform.position, startPos, 1.2f, parabolaAnimation / 5f);
        Debug.Log("Canceled Parabola");
      }
      yield return null;
    }
    transform.Find("ParticleHolder").GetComponent<ParticleSystem>().Stop();

    if(canceledParabola){
      Debug.Log("Canceled Parabola so we're returning to start position.");
      transform.position = startPos;
      canceledParabola = false;
      PlayerGaze.Instance.ClearGaze();
      yield break;
    }

    transform.position = endPos;
    animalStatus = BankStatus.Boat;
    transform.SetParent(Boat_LP.Instance.transform.Find("Boat"));
    transform.GetComponent<Collider>().enabled = false;
    Boat_LP.Instance.cargo = gameObject;
    PlayerGaze.Instance.ClearGaze();
    Debug.Log("Made it to the part where the boat Cargo is set...");
  }

  protected void ChooseTextToDisplay()
  {

    if (animalStatus != BankStatus.Boat)
    {
      if (Player_LP.Instance.playerStatus != PlayerStatus.DraggingBoat)
      {
        //if the boat's too far away...we tell the player to get the boat...
        if (Boat_LP.Instance.boatStatus != animalStatus)
        {
          StartCoroutine(UIManager_LP.Instance.InstructionsTextIncoming(String.Format("Bring the boat closer")));
        }
        //if the boat is close enough to the animal's bank...
        else if (Boat_LP.Instance.boatStatus == animalStatus)
        {
          //if the animal is not in the boat...
          if (animalStatus != BankStatus.Boat)
          {
            StartCoroutine(UIManager_LP.Instance.InstructionsTextIncoming(String.Format("Press Space to place the {0} in the boat", myId)));
          }
        }
      }
    }
  }

  public Vector3 FindMyBankPosition(BankStatus bankStat){
    animalStatus = bankStat;

    switch(transform.name){
      case "Wolf":
        if(bankStat.Equals(BankStatus.YellowBank)){
          return GameManager_LP.Instance.wolfSpotYellowBank;
        } else {
          return GameManager_LP.Instance.wolfSpotRedBank;
        }
      case "Chicken":
        if (bankStat.Equals(BankStatus.YellowBank))
          return GameManager_LP.Instance.chickenSpotYellowBank;
        else
          return GameManager_LP.Instance.chickenSpotRedBank;
      case "Cabbage":
        if (bankStat.Equals(BankStatus.YellowBank))
          return GameManager_LP.Instance.cabbageSpotYellowBank;
        else
          return GameManager_LP.Instance.cabbageSpotRedBank;
      default:
        Debug.Log("No bank position found");
        return transform.position;
    }
  }


}
