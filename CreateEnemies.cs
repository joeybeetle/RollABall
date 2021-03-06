using UnityEngine;
using System.Collections;

public class CreateEnemies : MonoBehaviour {
	/* IMPORTANT: This script should be used in conjunction with PlayerController2 
	              Enemy prefab must be tagged "Enemy"*/

	public static CreateEnemies control;
	private Vector3 spawnLocation;
	public GameObject enemies;
	public int numEnemies;
	//public int pickupBehaviour;

	// Use this for initialization
	void Start () {
		CreateEnemy();
		control = this;
	}

	void CreateEnemy() {
		for (int i = 0; i < numEnemies; i++) {

			spawnLocation = new Vector3 (Random.Range (-9.0F, 9.0F), 0.5F, Random.Range (-9.0F, 9.0F));
			Instantiate (enemies, spawnLocation, Quaternion.identity);
		}

	}

}