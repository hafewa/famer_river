using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerScript : MonoBehaviour {
  
  public Text myText;
  public static bool inBoat;
  public static bool beStill;
  public static bool noMouse;


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
	private bool touchingWolf;
  private bool touchingChx;
  private bool touchingCab;
  private bool touchingBoat;
  private bool startMounting;
  private bool startDismounting;
  public GameObject playerBoatSpot;
  public GameObject stareAtFrontOfBoat;
  public GameObject myExitSpotFromBoat;

  public float RotationSpeed;
  
  //values for internal use
  private Quaternion _lookRotation;
  private Vector3 _direction;
  
	void OnEnable(){
		BoatScript.OnBoatLand += DismountBoat;
	}

	void OnDisable(){
		BoatScript.OnBoatLand -= DismountBoat;
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
          Debug.Log("Wolf (not in boat) Collider");
          myText.text = "Press \"w\" to place the wolf in the boat";
        } else {
          myText.text = "Press \"r\" to remove from boat";
          Debug.Log("Wolf -- in boat -- Collider");
        }
        break;
      case "Chicken":
      touchingChx = true;
        if(!ChickenScript.inBoat){
          Debug.Log("Chicken Collider");        
          myText.text = "Press \"c\" to place the chicken in the boat";
        }else {
          myText.text = "Press \"r\" to remove from boat";
        }
        break;
      case "Cabbage":
      touchingCab = true;
        if(!CabbageScript.inBoat){
          Debug.Log("Cabbage Collider");
          myText.text = "Press \"g\" to place the cabbage in the boat"; 
        } else {
          myText.text = "Press \"r\" to remove from boat";          
        }
        break;
      case "River_Wall":
        Debug.Log("River Collider");
        if(!inBoat)
          myText.text = "The water is too deep to cross";
        break;
      case "Boat":
      touchingBoat = true;
        Debug.Log("Boat Collider");
        myText.text = "Press \"b\" to enter the boat";
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
      myText.text = "Press \"p\" to push off. \"b\" to step out of the boat";
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
        myText.text = "Press \"w\" to place the wolf in the boat";
      } else {
        myText.text = "Press \"r\" to remove from boat";
        Debug.Log("Wolf -- in boat -- Collider");
      }
      break;
    case "Chicken":
      touchingChx = true;
      if(!ChickenScript.inBoat){
        //Debug.Log("Chicken Collider");        
        myText.text = "Press \"c\" to place the chicken in the boat";
      }else {
        myText.text = "Press \"r\" to remove from boat";
      }
      break;
    case "Cabbage":
      touchingCab = true;
      if(!CabbageScript.inBoat){
        //Debug.Log("Cabbage Collider");
        myText.text = "Press \"g\" to place the cabbage in the boat"; 
      } else {
        myText.text = "Press \"r\" to remove from boat";          
      }
      break;
    case "River_Wall":
      //Debug.Log("River Collider");
      if(!inBoat)
        myText.text = "The water is too deep to cross";
      break;
    case "Boat":
      //Debug.Log("Boat Collider");
      myText.text = "Press \"b\" to enter the boat";
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


    if (Input.GetKeyUp ("w") && touchingWolf) {
      myText.text = "";
      OnPress_W (); 
	  OnPress_RC();
			OnPress_RG();
    } else if (Input.GetKeyUp ("c") && touchingChx) {
      myText.text = "";
      OnPress_C ();
			OnPress_RG();
			OnPress_RW();
    } else if (Input.GetKeyUp ("g") && touchingCab) {
      myText.text = "";
      OnPress_G ();
			OnPress_RW();
			OnPress_RC();
    } else if (Input.GetKeyUp ("r") && touchingWolf) {
      myText.text = "";
      OnPress_RW ();
    } else if (Input.GetKeyUp ("r") && touchingChx) {
      myText.text = "";
      OnPress_RC ();
    } else if (Input.GetKeyUp ("r") && touchingCab) {
      myText.text = "";
      OnPress_RG ();
    } else if (Input.GetKeyUp ("b") && touchingBoat) {
      myText.text = "";
      if(OnPress_B != null){
        OnPress_B ();
      }
      MountBoat ();
    } else if (Input.GetKeyUp ("p") && inBoat) {
	  myText.text = "";
	  noMouse = false;
		if(OnPlayerLaunchBoat != null){
			OnPlayerLaunchBoat();
		}
    }
  } 

  public void MountBoat(){
    beStill = true;
	noMouse = true;
    startMounting = true;
    GetComponent<CapsuleCollider> ().isTrigger = true;
  }

  public void DismountBoat(){
		inBoat = false;
    beStill = false; 
    startDismounting = true;
    GetComponent<CapsuleCollider> ().isTrigger = false;
	transform.parent = null;
	}
}
