using UnityEngine;
using UnityEngine.UI;

public class CanvasActiveSetterButton : MonoBehaviour
{
    [SerializeField] private Canvas targetCanvas;
    [SerializeField] private bool setActive = true;
    [SerializeField] private Button button;

    private void Awake()
    {
        if (button == null)
        {
            button = GetComponent<Button>();
        }

        if (button != null)
        {
            button.onClick.RemoveListener(ApplyCanvasActiveState);
            button.onClick.AddListener(ApplyCanvasActiveState);
        }
        else
        {
            Debug.LogWarning("[CanvasActiveSetterButton] Button component was not found. OnClick will not fire automatically.");
        }
    }

    public void ApplyCanvasActiveState()
    {
        if (targetCanvas == null)
        {
            Debug.LogWarning("[CanvasActiveSetterButton] Target canvas is not assigned.");
            return;
        }

        bool before = targetCanvas.gameObject.activeSelf;
        targetCanvas.gameObject.SetActive(setActive);
        Debug.Log($"[CanvasActiveSetterButton] Canvas '{targetCanvas.name}' active state changed: {before} -> {setActive}");
    }
}
