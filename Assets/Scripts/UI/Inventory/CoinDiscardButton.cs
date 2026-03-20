using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CoinDiscardButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private Color disableColor = new Color(0.5f, 0.5f, 0.5f, 1.0f);
    [SerializeField] private Image trashIcon;
	private Image detailPanel;
    private Coin coin;
	private Button button;
	private Image buttonImage;
	private bool isHovering;
	private bool isPinned;
	private RectTransform buttonRect;
	private RectTransform panelRect;
	private Color activeButtonColor;
	private Color activeTrashColor;
	private bool isActive;

    public Coin Coin
	{
		set
		{
            Logger.Log("Setting CoinDiscardButton.Coin: " + (value != null ? $"{value.FrontSideValue} / {value.BackSideValue}" : "null"));
			coin = value;
			SetButtonActive(coin != null);
		}
	}

	private void Awake()
	{
        detailPanel = transform.parent.GetComponent<CoinDiscardButtons>().DetailPanel;
		button = GetComponent<Button>();
		buttonImage = GetComponent<Image>();
		activeButtonColor = buttonImage != null ? buttonImage.color : Color.white;
		activeTrashColor = trashIcon != null ? trashIcon.color : Color.white;
		buttonRect = transform as RectTransform;
		panelRect = detailPanel.transform as RectTransform;
		SetButtonActive(false);
	}

	private void Update()
	{
		if (!isActive)
		{
			return;
		}

		if (!isPinned || detailPanel == null || !detailPanel.gameObject.activeSelf)
		{
			return;
		}

		if (!Input.GetMouseButtonDown(0))
		{
			return;
		}

		Vector2 pointer = Input.mousePosition;
		if (IsInside(buttonRect, pointer) || IsInside(panelRect, pointer))
		{
			return;
		}

		isPinned = false;
		RefreshPanel();
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (!isActive)
		{
			return;
		}

		isHovering = true;
		RefreshPanel();
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (!isActive)
		{
			return;
		}

		isHovering = false;
		RefreshPanel();
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (!isActive)
		{
			return;
		}

		isPinned = true;
		RefreshPanel();
	}

	private void RefreshPanel()
	{
		if (detailPanel == null)
		{
			return;
		}

		detailPanel.gameObject.SetActive(isHovering || isPinned);
	}

	private bool IsInside(RectTransform rect, Vector2 pointer)
	{
		if (rect == null)
		{
			return false;
		}

		Canvas canvas = rect.GetComponentInParent<Canvas>();
		Camera camera = canvas != null && canvas.renderMode != RenderMode.ScreenSpaceOverlay ? canvas.worldCamera : null;
		return RectTransformUtility.RectangleContainsScreenPoint(rect, pointer, camera);
	}
    private void SetButtonActive(bool isActive)
    {
		this.isActive = isActive;

		if (button != null)
		{
			button.interactable = isActive;
		}

		if (buttonImage != null)
		{
			buttonImage.color = isActive ? activeButtonColor : disableColor;
		}

		if (trashIcon != null)
		{
			trashIcon.color = isActive ? activeTrashColor : disableColor;
		}

		if (!isActive)
		{
			isHovering = false;
			isPinned = false;
			RefreshPanel();
		}
    }
}
