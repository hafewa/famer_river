#pragma strict

function Start () {

}

function Update () {
{
 if(Input.GetKeyDown("1"))
 {
  // Plays the reload animation - stops all other animations
  GetComponent.<Animation>().Play("Crow_Fly", PlayMode.StopAll);
 }
  if(Input.GetKeyDown("2"))
 {
  // Plays the reload animation - stops all other animations
  GetComponent.<Animation>().Play("Crow_Glide", PlayMode.StopAll);
 }
  if(Input.GetKeyDown("3"))
 {
  // Plays the reload animation - stops all other animations
  GetComponent.<Animation>().Play("Crow_Trot", PlayMode.StopAll);
 }
  if(Input.GetKeyDown("4"))
 {
  // Plays the reload animation - stops all other animations
  GetComponent.<Animation>().Play("Crow_Death", PlayMode.StopAll);
 }
  if(Input.GetKeyDown("5"))
 {
  // Plays the reload animation - stops all other animations
  GetComponent.<Animation>().Play("Crow_Idle", PlayMode.StopAll);
 }
}
}