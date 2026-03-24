using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Cutin : MonoBehaviour
{
    [SerializeField] private GameObject cutinImages;
    [SerializeField] private float startX = 4010f;
    [SerializeField] private float stopX = 0f;
    [SerializeField] private float endX = -4000f;
    [SerializeField] private float enterTime = 0.15f;
    [SerializeField] private float stopTime = 0.65f;
    [SerializeField] private float exitTime = 0.1f;
    private Coroutine cutinRoutine;

    public void ShowCutin()
    {
        Logger.Log("Cutin.ShowCutin called");
        cutinImages.SetActive(true);
        Logger.Log("Cutin.SetActive(true)");

        if (cutinRoutine != null)
        {
            Logger.Log("Cutin.StopCoroutine(existing routine)");
            StopCoroutine(cutinRoutine);
        }

        cutinRoutine = StartCoroutine(PlayCutin());
        Logger.Log("Cutin.StartCoroutine(PlayCutin)");
    }

    private IEnumerator PlayCutin()
    {
        Logger.Log("Cutin.PlayCutin start");
        SetCutinX(startX);
        Logger.Log("Cutin.SetCutinX(startX)");

        Logger.Log("Cutin.Enter move start");
        yield return MoveCutinX(startX, stopX, enterTime, EaseOutCubic);
        Logger.Log("Cutin.Enter move complete");

        Logger.Log("Cutin.Stop wait start");
        yield return new WaitForSeconds(stopTime);
        Logger.Log("Cutin.Stop wait complete");

        Logger.Log("Cutin.Exit move start");
        yield return MoveCutinX(stopX, endX, exitTime, t => t);
        Logger.Log("Cutin.Exit move complete");

        cutinImages.SetActive(false);
        Logger.Log("Cutin.SetActive(false) in PlayCutin");
        cutinRoutine = null;
        Logger.Log("Cutin.PlayCutin end");
    }

    private IEnumerator MoveCutinX(float fromX, float toX, float duration, System.Func<float, float> easing)
    {
        Logger.Log("Cutin.MoveCutinX start: fromX=" + fromX + ", toX=" + toX + ", duration=" + duration);
        if (duration <= 0f)
        {
            SetCutinX(toX);
            Logger.Log("Cutin.MoveCutinX skip duration<=0");
            yield break;
        }

        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            float eased = easing(t);
            float x = Mathf.Lerp(fromX, toX, eased);
            SetCutinX(x);
            yield return null;
        }

        SetCutinX(toX);
        Logger.Log("Cutin.MoveCutinX end");
    }

    private void SetCutinX(float x)
    {
        RectTransform rectTransform = cutinImages.GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            Vector2 anchored = rectTransform.anchoredPosition;
            anchored.x = x;
            rectTransform.anchoredPosition = anchored;
            return;
        }

        Vector3 local = cutinImages.transform.localPosition;
        local.x = x;
        cutinImages.transform.localPosition = local;
    }

    private float EaseOutCubic(float t)
    {
        return 1f - Mathf.Pow(1f - t, 3f);
    }
}