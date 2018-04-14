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

  public delegate void GazeHitWolf();
  public static event GazeHitWolf OnGazeHitWolf;

  public delegate void GazeHitBoat();
  public static event GazeHitBoat OnGazeHitBoat;

  public delegate void GazeHitChicken();
  public static event GazeHitChicken OnGazeHitChicken;

  public delegate void GazeHitCabbage();
  public static event GazeHitCabbage OnGazeHitCabbage;

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
    if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 10.0f))
    {
      switch (hit.collider.gameObject.name)
      {
        case "Wolf":
          if(OnGazeHitWolf!=null)
            OnGazeHitWolf();
          break;
        case "Chicken":
          if (OnGazeHitChicken != null)
            OnGazeHitChicken();
          break;
        case "Cabbage":
          if (OnGazeHitCabbage != null)
            OnGazeHitCabbage();
          break;
        case "Boat":
          if (OnGazeHitBoat != null)
            OnGazeHitBoat();
          break;
      }
    } else {
      if (myGazeStatus != GazeStatus.None)
      {
        myGazeStatus = GazeStatus.None;
        StartCoroutine(InstructionsTextOutgoing());
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


