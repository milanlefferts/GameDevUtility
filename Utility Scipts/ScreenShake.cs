using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour {

	float shakeTime = 0.0f;
	public bool isShaking;

    Vector3 originalPos;
    Vector3 originalRot;

    public static ScreenShake Instance {
		get{
			return instance;
		}
	}
	private static ScreenShake instance;

	void Start () {
		isShaking = false;
		instance = this;

        originalPos = gameObject.transform.position;
        originalRot = gameObject.transform.eulerAngles;

    }

    public void ShakeScreen(float str, float dur)
    {
        StartCoroutine(ScreenShaker(str, dur));
    }

   private IEnumerator ScreenShaker (float strength, float duration) {
		isShaking = true;

		float shakeDuration = duration;
		float shakeStrength = strength;

		while (shakeTime < shakeDuration) {


			float percentComplete = shakeTime / shakeDuration;         

			float dampener = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);

			
			float x = (Random.value * 2.0f - 1.0f) * dampener * shakeStrength;
			float y = (Random.value * 2.0f - 1.0f) * dampener * shakeStrength;

			transform.localPosition = new Vector3 (originalPos.x + x, originalPos.y + y, originalPos.z);
			transform.localEulerAngles = new Vector3 (originalRot.x, originalRot.y, x);
			shakeTime += Time.deltaTime;  

			yield return null;
		}

		// Reset
		shakeTime = 0.0f;
		transform.localPosition = originalPos;
        transform.eulerAngles = originalRot;
		isShaking = false;

	}
}
