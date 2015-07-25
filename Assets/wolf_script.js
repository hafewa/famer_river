#pragma strict

function Start () {

}

function Update () {
{
 if(Input.GetKeyDown("1"))
 {
  // Plays the reload animation - stops all other animations
  GetComponent.<Animation>().Play("wolf_run", PlayMode.StopAll);
 }
  if(Input.GetKeyDown("2"))
 {
  // Plays the reload animation - stops all other animations
  GetComponent.<Animation>().Play("wolf_death", PlayMode.StopAll);
 }
  if(Input.GetKeyDown("3"))
 {
  // Plays the reload animation - stops all other animations
  GetComponent.<Animation>().Play("wolf_idle", PlayMode.StopAll);
 }
  if(Input.GetKeyDown("4"))
 {
  // Plays the reload animation - stops all other animations
  GetComponent.<Animation>().Play("wolf_idle2", PlayMode.StopAll);
 }
  if(Input.GetKeyDown("5"))
 {
  // Plays the reload animation - stops all other animations
  GetComponent.<Animation>().Play("wolf_attack", PlayMode.StopAll);
 }
}
}