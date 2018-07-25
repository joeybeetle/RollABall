using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController2 : MonoBehaviour {
	/* IMPORTANT: This script should be used in conjunction with CreatePickups2 */

	public float speed;
	private Rigidbody rb;
	private int pickupsLeft;
	private int currentLevel;

	void Start ()
	{
		rb = GetComponent<Rigidbody>();
		GameObject pickUps = GameObject.Find("Pickups");
		CreatePickups2 pickupsTotal = pickUps.GetComponent<CreatePickups2>();
		currentLevel = SceneManager.GetActiveScene ().buildIndex;
		pickupsLeft = pickupsTotal.maxObjects;
	}

	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		rb.AddForce (movement * speed * Time.deltaTime);
	}

	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "PickUp") {
			/*this section determines whether to change the objects motion or visibility when hit,
				increases score by one for each object hit, adjusts number of objects left to collect
				and modifies the score displayed on screen */
			if (CreatePickups2.control.deleteObjectOnPickup) {
				other.gameObject.SetActive (false);} //only delete the pickup if flag is set to true
			else {
				other.gameObject.tag = "Untagged";} //reset tag for this pickup so it is only scored once
			GameController.control.score += 1;
			pickupsLeft -= 1;
			LevelInfo.display.scoreText.text = "Score: " + GameController.control.score.ToString ();

			if (pickupsLeft == 0){
				currentLevel += 1;
				SceneManager.LoadScene(currentLevel);
			}
		}
		else if (other.gameObject.tag == "Enemy") {
			if (GameController.control.lives <= 0){
				LevelInfo.display.messageText.text = "Game Over!";
				gameObject.SetActive(false);
			}
			else{
				GameController.control.lives -= 1;
				LevelInfo.display.livesText.text = "Lives: " + GameController.control.lives.ToString ();
				SceneManager.LoadScene(currentLevel);
			}
		}
	}
} 