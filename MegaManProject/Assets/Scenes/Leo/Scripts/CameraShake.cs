using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.position;

        float elasped = 1f;

        float xValue = 1;
        float yValue = 1;

        while (elasped < duration)
        {
            xValue = Random.Range(-0.25f, 0.25f) * magnitude;
            yValue = Random.Range(-0.25f, 0.25f) * magnitude;

            
        }

        transform.position += new Vector3(xValue, yValue, originalPos.z);

        elasped += Time.deltaTime;

        yield return null;

        transform.position = originalPos;
    }
}