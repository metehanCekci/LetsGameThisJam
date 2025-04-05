using UnityEngine;
using Unity.Cinemachine;

public class CameraShake : MonoBehaviour
{

    private CinemachineCamera cinemachineCamera;
    private float shakeTimer;

    private void Awake()
    {
        cinemachineCamera = GetComponent<CinemachineCamera>();
    }

    public void ShakeCamera(float intensity, float time)
    {
        var noise = cinemachineCamera.GetComponent<CinemachineBasicMultiChannelPerlin>();
        noise.AmplitudeGain = intensity;
        shakeTimer = time;
    }

    private void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;

            if (shakeTimer <= 0f)
            {
                var noise = cinemachineCamera.GetComponent<CinemachineBasicMultiChannelPerlin>();
                noise.AmplitudeGain = 0f;
            }
        }
    }
}
