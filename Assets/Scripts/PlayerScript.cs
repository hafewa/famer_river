using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour {
  
  public Text myText;
  public static bool inBoat;
  public static bool beStill;
  public delegate void BoatEvent();
  public static event BoatEvent OnBoatLaunch;
  public delegate void BoatLandEvent();
  public static event BoatLandEvent OnBoatLand;
  public delegate void Press_C();
  public static event Press_C OnPress_C;
  public delegate void Press_W();
  public static event Press_W OnPress_W;
  public delegate void Press_G();
  public static event Press_G OnPress_G;
  public delegate void Press_RW();
  public static event Press_RW OnPress_RW;
  public delegate void Press_RC();
  public static event Press_RC OnPress_RC;
  public delegate void Press_RG();
  public static event Press_RG OnPress_RG;
  private bool touchingWolf;
  private bool touchingChx;
  private bool touchingCab;

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
        Debug.Log("Boat Collider");
        inBoat = true;
        beStill = true;
        myText.text = "Push off?";
        break;
      default: 
        Debug.Log("No collider identity");      
        break;
    }
  }

  public void OnCollisionStay(Collision other){
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
      Debug.Log("Boat Collider");
      inBoat = true;
      beStill = true;
      myText.text = "Push off?";
      break;
    default: 
      Debug.Log("No collider identity");      
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
  }
  void Update(){
    if(Input.GetKeyUp("w") && touchingWolf){
      OnPress_W(); 
    }
    else if (Input.GetKeyUp("c") && touchingChx) {
      OnPress_C();
    }
    else if (Input.GetKeyUp("g") && touchingCab) {
      OnPress_G();
    }
    else if (Input.GetKeyUp("r") && touchingWolf) {
      OnPress_RW();
    }
    else if (Input.GetKeyUp("r") && touchingChx) {
      OnPress_RC();
    }
    else if (Input.GetKeyUp("r") && touchingCab) {
      OnPress_RG();
    }
  } 

  //Boat launch script subscribes to this event. 
  public void BoatLaunch(){
    OnBoatLaunch();
    Debug.Log("Boat launch");
  }

  public void BoatLand(){
    OnBoatLand();
    Debug.Log("Boat land");
  }


}
