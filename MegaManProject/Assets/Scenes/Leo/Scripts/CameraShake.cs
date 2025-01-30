using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Transform cameraTransform;
    public float shakeDuration = 0.1f;
    public float shakeStrength = 0.2f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 shotDirection = transform.forward;
            ShakeCamera(shotDirection);
        }
    }

    public void ShakeCamera(Vector3 shotDirection)
    {
        StartCoroutine(Shake(shotDirection));
    }

    public IEnumerator Shake(Vector3 direction)
    {
        Vector3 originalPos = cameraTransform.position;
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

