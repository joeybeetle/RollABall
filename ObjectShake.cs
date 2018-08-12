using UnityEngine;
using System.Collections;

// Code downloaded from:
// http://www.mikedoesweb.com/2012/camera-shake-in-unity/
// and modified to shake without allowing object to move position.

public class ObjectShake : MonoBehaviour {

	private Quaternion originRotation;
	public float shake_decay = 0.002f;
	public float shake_intensity = .3f;
	public bool shakeObject;

	private float temp_shake_intensity = 0;
	
	void OnGUI (){
		if (shakeObject){
			Shake ();
		}
	}
	
	void Update (){
		if (temp_shake_intensity > 0){
			transform.rotation = new Quaternion(
				originRotation.x + Random.Range (-temp_shake_intensity,temp_shake_intensity) * .2f,
				originRotation.y,
				originRotation.z + Random.Range (-temp_shake_intensity,temp_shake_intensity) * .2f,
				originRotation.w + Random.Range (-temp_shake_intensity,temp_shake_intensity) * .2f);
			temp_shake_intensity -= shake_decay;
		}
	}
	
	void Shake(){
		originRotation = transform.rotation;
		temp_shake_intensity = shake_intensity;

	}
}