using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager_LP : MonoBehaviour {


  public static UIManager_LP Instance;

  public Button instructionTextBottom;
  public bool textInstructionsPresent;
  public Transform fadeToBlackPanel;
  public Text messageAtEnd;
  //public Button optionsAtEnd;

  void Awake(){
    if (Instance == null)
      Instance = this;
    else
      Destroy(Instance);
  }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
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

  public IEnumerator SwapInstructions(string instr)
  {
    instructionTextBottom.GetComponent<Animator>().SetTrigger("Swap");
    textInstructionsPresent = true;
    yield return new WaitForSeconds(0.75f);
    instructionTextBottom.transform.Find("Text").GetComponent<Text>().text = instr;
  }


  //this will be the success failure screens that are activated by the game manager
  public IEnumerator EndScreen(string messageToPlayer){
    while(fadeToBlackPanel.GetComponent<CanvasGroup>().alpha < 1){
      fadeToBlackPanel.GetComponent<CanvasGroup>().alpha += 0.01f;
      messageAtEnd.text = messageToPlayer;
      yield return null;
    }
  }


}
