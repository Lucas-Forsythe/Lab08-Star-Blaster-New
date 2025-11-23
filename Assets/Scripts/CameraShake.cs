using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    [SerializeField] float shakeDuration = 0.5f;
    [SerializeField] float shakeMagnitude = 0.1f;

    Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.localPosition;
    }

    public void Play()
    {
        StartCoroutine(ShakeCamera());
    }

    IEnumerator ShakeCamera()
    {
        float timeElapsed = 0f;

        while (timeElapsed < shakeDuration)
        {
            transform.localPosition =
                initialPosition + (Vector3)Random.insideUnitCircle * shakeMagnitude;

            timeElapsed += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        transform.localPosition = initialPosition;
    }
}

