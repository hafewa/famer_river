using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGaze : MonoBehaviour
{


  public GameObject objectOfMyGaze;
  public enum GazeStatus { None, Wolf, Chicken, Cabbage, Boat };
  public GazeStatus myGazeStatus;

  public Button instructionTextBottom;
  public bool textInstructionsPresent;

  public static PlayerGaze Instance;

  //Responsibilities:
  /// <summary>
  /// This script's bgi responsibility is shooting out a ray from the players gaze (ie the forward direction of the normal)
  /// and seeing if it hits an object of type Animal_LP and if so, which animal it is...
  /// </summary>

  void Awake(){
    if (Instance == null)
      Instance = this;
    else
      Destroy(Instance);
  }

  // Use this for initialization
  void Start()
  {
    objectOfMyGaze = null;
    myGazeStatus = GazeStatus.None;

  }


  // Update is called once per frame
  void FixedUpdate()
  {

    RaycastHit hit;
    // Does the ray intersect any objects excluding the player layer
    if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 5.0f))
    {

      if (Player_LP.Instance.playerStatus != Player_LP.PlayerStatus.DraggingBoat)
      {
        switch (hit.collider.gameObject.name)
        {
          case "Wolf":
            //Debug.Log("Gaze hit wolf");
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            objectOfMyGaze = hit.collider.gameObject;
            if(Boat_LP.Instance.boatStatus != objectOfMyGaze.GetComponent<Animal_LP>().myStatus){
              StartCoroutine(InstructionsTextIncoming(String.Format("Bring the Boat Closer First")));
              break;
            }

            if (myGazeStatus != GazeStatus.Wolf && objectOfMyGaze.GetComponent<Animal_LP>().myStatus != BankStatus.Boat)
            {
              StartCoroutine(InstructionsTextIncoming(String.Format("Press Space to place the {0} in the boat", hit.collider.gameObject.name)));
              myGazeStatus = GazeStatus.Wolf;
            }
            break;
          case "Chicken":
            //Debug.Log("Gaze hit Chicken");
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            objectOfMyGaze = hit.collider.gameObject;
            if (Boat_LP.Instance.boatStatus != objectOfMyGaze.GetComponent<Animal_LP>().myStatus)
            {
              StartCoroutine(InstructionsTextIncoming(String.Format("Bring the Boat Closer First")));
              break;
            }
            if (myGazeStatus != GazeStatus.Chicken && objectOfMyGaze.GetComponent<Animal_LP>().myStatus != BankStatus.Boat)
            {
              StartCoroutine(InstructionsTextIncoming(String.Format("Press Space to place the {0} in the boat", hit.collider.gameObject.name)));
              myGazeStatus = GazeStatus.Chicken;
            }
            break;
          case "Cabbage":
            //Debug.Log("Gaze hit Cabbage");
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            objectOfMyGaze = hit.collider.gameObject;
            if (Boat_LP.Instance.boatStatus != objectOfMyGaze.GetComponent<Animal_LP>().myStatus)
            {
              StartCoroutine(InstructionsTextIncoming(String.Format("Bring the Boat Closer First")));
              break;
            }
            if (myGazeStatus != GazeStatus.Cabbage && objectOfMyGaze.GetComponent<Animal_LP>().myStatus != BankStatus.Boat)
            {
              StartCoroutine(InstructionsTextIncoming(String.Format("Press Space to place the {0} in the boat", hit.collider.gameObject.name)));
              myGazeStatus = GazeStatus.Cabbage;
            }
            break;
          case "Boat":
            /////////Debug.Log("Gaze Hit Boat");
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            objectOfMyGaze = hit.collider.gameObject;
            if (myGazeStatus != GazeStatus.Boat)
            {
              StartCoroutine(InstructionsTextIncoming(String.Format("Press Space to pull the boat")));
              myGazeStatus = GazeStatus.Boat;

            }
            break;
          default:
            break;
        }
      }
    } else {
      if (myGazeStatus != GazeStatus.None)
      {
        StartCoroutine(InstructionsTextOutgoing());
        myGazeStatus = GazeStatus.None;
      }
    }
  }

  public IEnumerator InstructionsTextIncoming(string instr)
  {
    if (textInstructionsPresent)
    {
      StartCoroutine(SwapInstructions(instr));
      yield break;
    }
    instructionTextBottom.GetComponent<Animator>().SetTrigger("SlideInTrig");
    textInstructionsPresent = true;
    yield return new WaitForSeconds(0.15f);
    instructionTextBottom.transform.Find("Text").GetComponent<Text>().text = instr;
  }

  public IEnumerator InstructionsTextOutgoing()
  {
    instructionTextBottom.GetComponent<Animator>().SetTrigger("SlideOutTrig");
    textInstructionsPresent = false;
    yield return new WaitForSeconds(0.75f);
    instructionTextBottom.transform.Find("Text").GetComponent<Text>().text = "";
  }

  public IEnumerator SwapInstructions(string instr){
    instructionTextBottom.GetComponent<Animator>().SetTrigger("Swap");
    textInstructionsPresent = true;
    yield return new WaitForSeconds(0.75f);
    instructionTextBottom.transform.Find("Text").GetComponent<Text>().text = instr;
  }
}
