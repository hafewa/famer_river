using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerScript : MonoBehaviour {
  
  public Text myText;
  public Text myEndText;
  public static bool inBoat;
  public static bool beStill;
  public static bool noMouse;

  public enum MyState{EastBank, WestBank, InBoat};
  public static MyState my_state;


  public delegate void Press_C();
  public static event Press_C OnPress_C;
  public delegate void Press_W();
  public static event Press_W OnPress_W;

  public delegate void Press_G();
  public static event Press_G OnPress_G;
  
  public delegate void Press_B();
  public static event Press_B OnPress_B;

  public delegate void Press_RW();
  public static event Press_RW OnPress_RW;
  public delegate void Press_RC();
  public static event Press_RC OnPress_RC;
  public delegate void Press_RG();
  public static event Press_RG OnPress_RG;
	public delegate void PlayerBoatEvent();
	public static event PlayerBoatEvent OnPlayerLaunchBoat;
	public delegate void ResetEvent();
	public static event ResetEvent OnReset;
	private bool touchingWolf;
  private bool touchingChx;
  private bool touchingCab;
  private bool touchingBoat;
  private bool startMounting;
  private bool startDismounting;
  public GameObject playerBoatSpot;
  public GameObject stareAtFrontOfBoat;
  public GameObject myExitSpotFromBoat;
	private bool success;

  public float RotationSpeed;
  
  //values for internal use
  private Quaternion _lookRotation;
  private Vector3 _direction;
  
	void OnEnable(){
		BoatScript.OnBoatLand += DismountBoat;
		GameManager_FailureChecker.OnFailMet += EndScreen;
		UI_Master.OnRestart += ResetPlayerFunctionality;
		GameManager_FailureChecker.OnSuccess += DisplaySuccessMessage;
	}

	void OnDisable(){
		BoatScript.OnBoatLand -= DismountBoat;
		GameManager_FailureChecker.OnFailMet -= EndScreen;
		UI_Master.OnRestart -= ResetPlayerFunctionality;
		GameManager_FailureChecker.OnSuccess += DisplaySuccessMessage;

	}

	/// <summary>
	/// Singleton for player. 
	/// </summary>
	public static PlayerScript instance = null;

	public static PlayerScript GetInstance()
	{
		if(instance == null)
		{
			instance = new PlayerScript();
		}
		
		return instance;
	}



  public void OnCollisionEnter(Collision other){
    switch(other.gameObject.tag){
      case "Wolf":
      touchingWolf = true;
        if(!WolfScript.inBoat){ 
         // Debug.Log("Wolf (not in boat) Collider");
          myText.text = "Press space to place the wolf in the boat";
        } else {
          myText.text = "Press space to remove from boat";
         // Debug.Log("Wolf -- in boat -- Collider");
        }
        break;
      case "Chicken":
      touchingChx = true;
        if(!ChickenScript.inBoat){
        //  Debug.Log("Chicken Collider");        
          myText.text = "Press space to place the chicken in the boat";
        }else {
          myText.text = "Press space to remove from boat";
        }
        break;
      case "Cabbage":
      touchingCab = true;
        if(!CabbageScript.inBoat){
        //  Debug.Log("Cabbage Collider");
          myText.text = "Press space to place the cabbage in the boat"; 
        } else {
          myText.text = "Press space to remove from boat";          
        }
        break;
      case "River_Wall":
        //Debug.Log("River Collider");
        if(!inBoat)
          myText.text = "The water is too deep to cross";
        break;
      case "Boat":
      touchingBoat = true;
        //Debug.Log("Boat Collider");
        myText.text = "Press space to enter the boat";
        break;
      default: 
        //Debug.Log("No collider identity");      
        break;
    }
  }
  public void OnTriggerEnter(Collider other){
    switch (other.gameObject.tag) {
    case "PlayerInBoatSpot":
      startMounting = false;
      inBoat = true;
      myText.text = "Press \"p\" to push off. \"d\" to step out of the boat";
      transform.parent = GameObject.FindGameObjectWithTag("Boat").transform;
      break;
    default:
      break;
    }
  }

  public void OnCollisionStay(Collision other){
    switch(other.gameObject.tag){
    case "Wolf":
      touchingWolf = true;
      if(!WolfScript.inBoat){ 
        //Debug.Log("Wolf (not in boat) Collider");
        myText.text = "Press space to place the wolf in the boat";
      } else {
        myText.text = "Press space to remove from boat";
        Debug.Log("Wolf -- in boat -- Collider");
      }
      break;
    case "Chicken":
      touchingChx = true;
      if(!ChickenScript.inBoat){
        //Debug.Log("Chicken Collider");        
        myText.text = "Press space to place the chicken in the boat";
      }else {
        myText.text = "Press space to remove from boat";
      }
      break;
    case "Cabbage":
      touchingCab = true;
      if(!CabbageScript.inBoat){
        //Debug.Log("Cabbage Collider");
        myText.text = "Press space to place the cabbage in the boat"; 
      } else {
        myText.text = "Press space to remove from boat";          
      }
      break;
    case "River_Wall":
      //Debug.Log("River Collider");
      if(!inBoat)
        myText.text = "The water is too deep to cross";
      break;
    case "Boat":
      //Debug.Log("Boat Collider");
      myText.text = "Press space to enter the boat";
      break;
    default: 
      //Debug.Log("No collider identity");      
      break;
    }
  }
 
  public void OnCollisionExit(Collision other){
    myText.text = ""; 
    if(other.gameObject.tag == "Boat"){
      inBoat = false;
    }
    touchingCab = false;
    touchingChx = false;
    touchingWolf = false;
    touchingBoat = false;
  }

	public void OnTriggerExit(Collider other){
		myText.text = ""; 
		if(other.gameObject.tag == "Boat"){
			inBoat = false;
		}
		touchingCab = false;
		touchingChx = false;
		touchingWolf = false;
		touchingBoat = false;
	}

  void Update(){

    if (startMounting) {
      float step = 1.5f * Time.deltaTime;
      transform.position = Vector3.MoveTowards(transform.position, playerBoatSpot.transform.position, step);
      //transform.LookAt(stareAtFrontOfBoat.transform);
      //find the vector pointing from our position to the target
      _direction = (stareAtFrontOfBoat.transform.position - transform.position).normalized;
      
      //create the rotation we need to be in to look at the target
      _lookRotation = Quaternion.LookRotation(_direction);
      
      //rotate us over time according to speed until we are in the required rotation
      transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * RotationSpeed);
    }

    if (Input.GetKeyUp ("space") && touchingWolf) {
			myText.text = "";
			OnPress_W (); 
			OnPress_RC ();
			OnPress_RG ();
		} else if (Input.GetKeyUp ("space") && touchingChx) {
			myText.text = "";
			OnPress_C ();
			OnPress_RG ();
			OnPress_RW ();
		} else if (Input.GetKeyUp ("space") && touchingCab) {
			myText.text = "";
			OnPress_G ();
			OnPress_RW ();
			OnPress_RC ();
		} else if (Input.GetKeyUp ("space") && touchingWolf) {
			myText.text = "";
			OnPress_RW ();
		} else if (Input.GetKeyUp ("space") && touchingChx) {
			myText.text = "";
			OnPress_RC ();
		} else if (Input.GetKeyUp ("space") && touchingCab) {
			myText.text = "";
			OnPress_RG ();
		} else if (Input.GetKeyUp ("space") && touchingBoat) {
			myText.text = "";
			if (OnPress_B != null) {
				OnPress_B ();
			}
			MountBoat ();
		} else if (Input.GetKeyUp ("p") && inBoat) {
			myText.text = "";
			noMouse = false;
			if (OnPlayerLaunchBoat != null) {
				OnPlayerLaunchBoat ();
			}
		} else if (Input.GetKeyUp ("d") && inBoat) {
			//if(BoatScript.
			DismountBoat("");
		}

		if (success == true) {
			GameObject.FindGameObjectWithTag ("Darkness_Warshing_Panel").GetComponent<CanvasGroup> ().alpha += 0.002f;
			myEndText.GetComponent<CanvasGroup> ().alpha += 0.001f;
		}
	
  } 

  public void MountBoat(){
    beStill = true;
	  noMouse = true;
    startMounting = true;
    GetComponent<CapsuleCollider> ().isTrigger = true;
		my_state = MyState.InBoat;
	//	transform.position = new Vector3(transform.position.x, transform.position.y+5.0f, transform.position.z);
  }

  public void DismountBoat(string bank){
    //Debug.Log ("DISMOUNT FIRED");
	  ResetPlayerFunctionality ();
	  transform.position = new Vector3(transform.position.x+2, transform.position.y +2, transform.position.z);
		//Debug.Log ("cabbage state = " +  CabbageScript.my_state.ToString () + "wolf state " + WolfScript.my_state.ToString () + " chx state " + ChickenScript.my_state.ToString () + " player state " + PlayerScript.my_state.ToString()+" boat state "+ BoatScript.boat_state.ToString());

	  GameManager_FailureChecker.CheckSuccess();
	}

	public void EndScreen(string failString){

		myEndText.text = failString;
		BoatScript.moving = false;
		GameObject.FindGameObjectWithTag ("Darkness_Warshing_Panel").GetComponent<CanvasGroup> ().alpha += 0.05f;
		myEndText.GetComponent<CanvasGroup> ().alpha += 0.01f;

	}

	public void ResetPlayerFunctionality(){
		inBoat = false;
		beStill = false; 
		startDismounting = true;
		GetComponent<CapsuleCollider> ().isTrigger = false;
		transform.parent = null;
		noMouse = false;
		if (BoatScript.boat_state == BoatState.EastBank) {
			my_state = MyState.EastBank;
			GameObject.FindGameObjectWithTag("EastBankBoatSpot").GetComponent<Collider>().enabled = false;
			GameObject.FindGameObjectWithTag("WestBankBoatSpot").GetComponent<Collider>().enabled = true;
		} else if (BoatScript.boat_state == BoatState.WestBank) {
			my_state = MyState.WestBank;
			GameObject.FindGameObjectWithTag("EastBankBoatSpot").GetComponent<Collider>().enabled = true;
			GameObject.FindGameObjectWithTag("WestBankBoatSpot").GetComponent<Collider>().enabled = false;
		}
	}

	public void DisplaySuccessMessage(){
		//Debug.Log ("SUCCESSS DISPLAY");
		myEndText.text = "Success";
		success = true;
	}


}
