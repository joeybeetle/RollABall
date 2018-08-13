using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController2 : MonoBehaviour {
	/* IMPORTANT: This script should be used in conjunction with CreatePickups2 AND CreateEnemies*/

	public float speed;
	private Rigidbody rb;
	private int pickupsLeft;
	private int currentLevel;

	void Start ()
	{
		//This function runs once each time the level starts or restarts. It sets up some variables.
		rb = GetComponent<Rigidbody>();
		GameObject pickUps = GameObject.Find("Pickups");
		CreatePickups2 pickupsTotal = pickUps.GetComponent<CreatePickups2>();
		currentLevel = SceneManager.GetActiveScene ().buildIndex;
		pickupsLeft = pickupsTotal.maxObjects;
	}

	void FixedUpdate ()
	{
		//This function controls player movement
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rb.AddForce (movement * speed * Time.deltaTime);
	}

	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "PickUp") {
			PlayerCollidedWithPickup (other);
		}
		else if (other.gameObject.tag == "Enemy") { //Player collided with an Enemy object
			PlayerCollidedWithEnemy ();
		}
	}

	void PlayerCollidedWithEnemy ()
	{
		/* this section deals with each enemy hit */
		GameController.control.lives -= 1; //subtract one from lives
		LevelInfo.display.livesText.text = "Lives: " + GameController.control.lives.ToString ();
		SceneManager.LoadScene(currentLevel); //reload the current level
		if (GameController.control.lives == 0){ 
			/* this section deals with the end of game condition */
			LevelInfo.display.messageText.text = "Game Over!";
			gameObject.SetActive(false); //set Player as inactive ... makes it disappear
		}
	}

	void PlayerCollidedWithPickup (Collision thisPickup)
		{
		/* this section determines whether to change the objects' motion or visibility when hit */
		if (CreatePickups2.control.deleteObjectOnPickup) { // deleteObjectOnPickup flag is checked
			thisPickup.gameObject.SetActive (false); //make the pickup disappear
		} 
		else { // deleteObjectOnPickup flag is not checked
			thisPickup.gameObject.tag = "Untagged"; //reset tag for this pickup so it is only scored once
			switch (CreatePickups2.control.pickupBehaviour) {
			case 1:
				ReplaceObjectWithPrefab (thisPickup.gameObject); //display the replacement
				print ("Replacing object"); //debug log
				thisPickup.gameObject.SetActive (false); //make the original pickup disappear
				break;
			case 2:
				var targetScript = thisPickup.gameObject.GetComponent<ObjectShake>();
				targetScript.shakeObject = !targetScript.shakeObject; //switch the flag to turn shake on or off
				break;
			}
		}

		/* this section modifies values for scoring */
		GameController.control.score += 1; //increase score by one
		pickupsLeft -= 1; //decrease pickups remaining by one
		LevelInfo.display.scoreText.text = "Score: " + GameController.control.score.ToString ();

		/* this section loads the next level if all pickups have been collected */
		if (pickupsLeft == 0){ //no pickups remaining
			currentLevel += 1; //increase the level by one
			SceneManager.LoadScene(currentLevel); //load the new level
		}
	}

	void ReplaceObjectWithPrefab (GameObject thisObject)
	{
		//Instantiate the replacement object at the same position as the current object
		Instantiate(CreatePickups2.control.replacement, thisObject.transform.position, thisObject.transform.rotation);
	}

} 