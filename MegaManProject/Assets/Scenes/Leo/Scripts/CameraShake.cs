using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Transform cameraTransform;
    public float shakeDuration = 0.1f;
    public float shakeStrength = 0.2f;

    public void ShakeCamera(Vector3 shotDirection)
    {
        StartCoroutine(ShakeRoutine(shotDirection));
    }

    private IEnumerator ShakeRoutine(Vector3 direction)
    {
        Vector3 originalPos = cameraTransform.localPosition;
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            float strength = (1f - (elapsed / shakeDuration)) * shakeStrength;
            cameraTransform.localPosition = originalPos + (direction.normalized * strength);
            elapsed += Time.deltaTime;
            yield return null;
        }

        cameraTransform.localPosition = originalPos;
    }
}

