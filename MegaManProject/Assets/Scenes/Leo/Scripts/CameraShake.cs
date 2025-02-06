using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] Transform cameraTransform;
    [SerializeField] float shakeDuration = 0.1f;
    [SerializeField] float shakeStrength = 0.2f;


    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 shotDirection = transform.forward;
            ShakeCamera(shotDirection);
        }
    }

    public void ShakeCamera(Vector3 shotDirection)
    {
        StartCoroutine(Shake(shotDirection));
    }

    public IEnumerator Shake(Vector3 direction) //Works when moving
    {
        Debug.Log("camera shaking!");
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

