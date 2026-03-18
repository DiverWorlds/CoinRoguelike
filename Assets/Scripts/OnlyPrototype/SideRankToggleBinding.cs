using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SideRankToggleBinding : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Side side;
    [SerializeField] private Toggle toggle;
    [SerializeField] private Image targetImage;
    [SerializeField] private TextMeshProUGUI rankCountText;
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color selectedColor = Color.yellow;

    public Side Side => side;
    public bool IsSelected => toggle != null && toggle.isOn;

    private void Awake()
    {
        if (toggle == null)
        {
            toggle = GetComponent<Toggle>();
        }

        if (targetImage == null)
        {
            targetImage = GetComponent<Image>();
        }

        if (toggle != null)
        {
            toggle.onValueChanged.RemoveListener(OnToggleValueChanged);
            toggle.onValueChanged.AddListener(OnToggleValueChanged);
            ApplyVisualState(toggle.isOn);
        }
        else
        {
            Debug.LogWarning("[SideRankToggleBinding] Toggle component is not assigned/found.", this);
        }
    }

    public void Refresh(PlayerSideInventory inventory)
    {
        int count = inventory != null ? inventory.GetOwnedSideCount(side) : 0;
        string rankLabel = side != null ? side.Rank.ToString() : "?";
        string sideName = side != null ? side.SideName : "(null)";

        if (rankCountText != null)
        {
            rankCountText.text = $"{sideName}: {rankLabel}({count})";
        }

        if (toggle != null)
        {
            toggle.interactable = count > 0;
            if (count <= 0)
            {
                toggle.isOn = false;
            }

            ApplyVisualState(toggle.isOn);
        }
    }

    public void SetSelected(bool selected)
    {
        if (toggle != null)
        {
            toggle.isOn = selected;
        }

        ApplyVisualState(selected);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (side == null)
        {
            Debug.LogWarning("[SideRankToggleBinding] Clicked, but side is not assigned.");
            return;
        }

        bool isOn = toggle != null && toggle.isOn;
        Debug.Log($"[SideRankToggleBinding] Clicked: {side.SideName} ({side.FrontOrBack}, Rank={side.Rank}, Value={side.Value}, Weight={side.Weight}, Selected={isOn})");
    }

    private void OnToggleValueChanged(bool isOn)
    {
        if (side == null)
        {
            Debug.Log($"[SideRankToggleBinding] Toggle changed: side is null, Selected={isOn}");
            return;
        }

        Debug.Log($"[SideRankToggleBinding] Toggle changed: {side.SideName} Selected={isOn}");
        ApplyVisualState(isOn);
    }

    private void ApplyVisualState(bool isSelected)
    {
        if (targetImage != null)
        {
            targetImage.color = isSelected ? selectedColor : normalColor;
        }
    }
}
