using UnityEngine;
using System.Collections;

public class CreatePickups2 : MonoBehaviour {
	/* IMPORTANT: This script should be used in conjunction with PlayerController2 */

	public static CreatePickups2 control;
	private Vector3 spawnLocation;
	public GameObject objects;
	public int maxObjects;
	public bool deleteObjectOnPickup;

	// Use this for initialization
	void Start () {
		CreateCube();
		control = this;
	}

	void CreateCube() {
		for(int i = 0; i < maxObjects; i++){

			spawnLocation = new Vector3(Random.Range(-9.0F, 9.0F), 0.5F, Random.Range(-9.0F,9.0F));
			Instantiate(objects, spawnLocation, Quaternion.identity);
		}
	}
}
