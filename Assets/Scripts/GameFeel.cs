using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class GameFeel : MonoBehaviour
{
    public static GameFeel Instance;

    [Header("Camera")]
    public Transform cam;

    [Header("Shake")]
    public float defaultShakeDuration = 0.2f;
    public float defaultShakeMagnitude = 0.2f;

    private Vector3 originalCamPos;

    void Awake()
    {
        Instance = this;

        if (cam != null)
            originalCamPos = cam.localPosition;
    }

    // 🔥 FONCTION PRINCIPALE
    public void PlayJuice(float intensity = 1f, float vibration = 0.5f)
    {
        StartCoroutine(ScreenShake(defaultShakeDuration * intensity, defaultShakeMagnitude * intensity));
        StartCoroutine(FreezeFrame(0.05f * intensity));
        StartCoroutine(SlowMotion(0.1f * intensity, 0.5f));
        StartCoroutine(Vibrate(0.1f * intensity, vibration));
    }

    // 🎥 SHAKE
    IEnumerator ScreenShake(float duration, float magnitude)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            cam.localPosition = originalCamPos + new Vector3(x, y, 0);

            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        cam.localPosition = originalCamPos;
    }

    // ⏸️ FREEZE
    IEnumerator FreezeFrame(float duration)
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1f;
    }

    // 🐢 SLOW MOTION
    IEnumerator SlowMotion(float duration, float slowFactor)
    {
        Time.timeScale = slowFactor;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1f;
    }

    // 🎮 VIBRATION
    IEnumerator Vibrate(float duration, float strength)
    {
        if (Gamepad.current == null) yield break;

        Gamepad.current.SetMotorSpeeds(strength, strength);

        yield return new WaitForSecondsRealtime(duration);

        Gamepad.current.SetMotorSpeeds(0f, 0f);
    }

    // 💡 FLASH
    public CanvasGroup flashCanvas;

    public void Flash(float duration = 0.1f)
    {
        if (flashCanvas != null)
            StartCoroutine(FlashRoutine(duration));
    }

    IEnumerator FlashRoutine(float duration)
    {
        flashCanvas.alpha = 1;

        float t = 0;
        while (t < duration)
        {
            t += Time.unscaledDeltaTime;
            flashCanvas.alpha = 1 - (t / duration);
            yield return null;
        }

        flashCanvas.alpha = 0;
    }
}