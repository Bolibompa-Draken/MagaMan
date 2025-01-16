using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;

        float elasped = 0f;

        float xValue = 1;
        float yValue = 1;

        while (elasped < duration)
        {
            xValue = Random.Range(-0.5f, 0.5f) * magnitude;
            yValue = Random.Range(-0.5f, 0.5f) * magnitude;
        }

        transform.localPosition = new Vector3(xValue, yValue, originalPos.z);

        elasped += Time.deltaTime;

        yield return null;
    }
}