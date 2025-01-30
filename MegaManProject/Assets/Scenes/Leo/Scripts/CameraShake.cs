using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] Vector3 mousePosition;
    [SerializeField] UnityEngine.Camera mainCamera;
    [SerializeField] Transform targetCamera;

    private void Update()
    {
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = (mousePosition - targetCamera.position).normalized;
    }

    //public IEnumerator Shake(float duration, )
    //{
    //    Vector3 direction = (mousePosition - targetCamera.position).normalized;

    //}
    //public IEnumerator Shake(float duration, float magnitude)
    //{
    //    Vector3 originalPos = transform.position;
    //    float elasped = 1f;
    //    float xValue = 1;
    //    float yValue = 1;

    //    while (elasped < duration)
    //    {

    //    }
    //    elasped += Time.deltaTime;



    //}

}