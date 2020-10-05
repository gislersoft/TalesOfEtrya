using UnityEngine;
using EZCameraShake;
public class FX_Manager : MonoBehaviour {

    public static FX_Manager Instance;
    
    private CameraShaker cameraShaker;
    [Header("Camera Shaking")]
    public float roughness;
    public float magnitude;
    public float fadeIn;
    public float fadeOut;
    [Header("Time scale")]
    public float desiredTimeScale = 0.5f;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        cameraShaker = CameraShaker.Instance;
    }

    public void Shake()
    {
        cameraShaker.ShakeOnce(magnitude, roughness, fadeIn, fadeOut);
    }

    public void ShakeOnce()
    {
        cameraShaker.ShakeOnce(7f, 4f, 0f, 1f);
    }

    public void ReduceGameTimeScale(){
        
        Time.timeScale = Mathf.Lerp(Time.timeScale, desiredTimeScale, Time.deltaTime);
    }

    public void ResetTimeScale(){
        Time.timeScale = 1f;
    }
}
