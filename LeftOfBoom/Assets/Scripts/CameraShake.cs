/* Adapted from Daniel Moore's (Firedan1176) Camera Shake script http://wiki.unity3d.com/index.php/Camera_Shake
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
	public float shakeAmount;
	public float shakeDuration;
	float shakePercentage;
	float startAmount;
	float startDuration;
	bool isRunning = false;
	public bool smooth;
	public float smoothAmount = 5f;
	private Vector3 direction;

	public void ShakeCamera(float amount, float duration, Vector3 dir)
	{

		shakeAmount = amount;
		startAmount = shakeAmount;
		shakeDuration = duration;
		startDuration = shakeDuration;
		direction = dir;

		if (!isRunning) {
			StartCoroutine(Shake());
		}
	}


	IEnumerator Shake()
	{
		isRunning = true;

		while (shakeDuration > 0.01f) {
			Vector3 rotationAmount = Random.insideUnitSphere * shakeAmount;//A Vector3 to add to the Local Rotation
			rotationAmount.z = 0;//Don't change the Z; it looks funny.

			Vector3 translationAmount = (Random.insideUnitSphere + direction) * shakeAmount * 0.25f;

			shakePercentage = shakeDuration / startDuration;//Used to set the amount of shake (% * startAmount).
			shakeAmount = startAmount * shakePercentage;//Set the amount of shake (% * startAmount).
			shakeDuration = Mathf.Lerp(shakeDuration, 0, Time.deltaTime);//Lerp the time, so it is less and tapers off towards the end.

			Vector3 beforePosition = transform.position;

			if (smooth) {
				//transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(rotationAmount), Time.deltaTime * smoothAmount);
				transform.position = Vector3.Lerp(transform.position, transform.position + translationAmount, Time.deltaTime * smoothAmount);
			} else {
				//transform.localRotation = Quaternion.Euler(rotationAmount);//Set the local rotation the be the rotation amount.
				transform.Translate(translationAmount);
			}

			Vector3 difference = transform.position - beforePosition;
			Vector2 diff2 = new Vector2(difference.x, difference.y);

			diff2 *= GetComponent<CameraControl>().parallaxSpeed;

			MeshRenderer[] meshRenderers = GetComponentsInChildren<MeshRenderer>();

			int i = 2;
			foreach (var item in meshRenderers) {
				Vector2 newOffset = item.material.mainTextureOffset + (diff2 / (i + 1));
				item.material.mainTextureOffset = newOffset;
				i++;
			}

			yield return null;
		}

		transform.localRotation = Quaternion.identity;//Set the local rotation to 0 when done, just to get rid of any fudging stuff.
		isRunning = false;
	}

}
