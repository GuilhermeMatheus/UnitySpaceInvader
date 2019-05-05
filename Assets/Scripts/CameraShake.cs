using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    Coroutine currentShake;

    public void Shake(float duration, float magnitude)
    {
        StopShake();
        currentShake = StartCoroutine(ShakeRoutine(duration, magnitude));
    }

    IEnumerator ShakeRoutine(float duration, float magnitude)
    {
        var originalPosition = transform.localPosition;

        var elapsed = 0f;

        while (elapsed < duration)
        {
            var x = Random.Range(-1f, 1f) * magnitude;
            var z = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, originalPosition.y, z);

            elapsed += Time.deltaTime;
            yield return new WaitForSeconds(0.1f);
        }

        transform.localPosition = originalPosition;
        StopShake();
    }

    private void StopShake()
    {
        if (currentShake != null)
        {
            StopCoroutine(currentShake);
            currentShake = null;
        }
    }
}
