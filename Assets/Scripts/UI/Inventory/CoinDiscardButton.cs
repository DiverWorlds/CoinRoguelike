using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CoinDiscardButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private Color disableColor = new Color(0.5f, 0.5f, 0.5f, 1.0f);
    [SerializeField] private Image trashIcon;
    [SerializeField] private EventTrigger eventTrigger;
    [SerializeField] private Button PanelCloseArea;
	[SerializeField] private GameObject discardCoinDetail;
    [SerializeField] private CoinDiscardButtons coinDiscardButtons;
    private Coin coin;
	private Button button;
	private Image buttonImage;
    private bool isClicked;
    private bool isActive;
	private Color activeButtonColor;
	private Color activeTrashColor;

    public Coin Coin
	{
        get => coin;
		set
		{
            Logger.Log("Setting CoinDiscardButton.Coin: " + (value != null ? $"{value.FrontSideValue} / {value.BackSideValue}" : "null"));
			coin = value;
			SetButtonActive(coin != null);
		}
	}

	private void Awake()
	{
		button = GetComponent<Button>();
		buttonImage = GetComponent<Image>();
		activeButtonColor = buttonImage != null ? buttonImage.color : Color.white;
		activeTrashColor = trashIcon != null ? trashIcon.color : Color.white;
		SetButtonActive(false);
	}
    void Start()
    {
        PanelCloseArea.onClick.AddListener(() =>
        {
            Logger.Log("PanelCloseArea clicked");
            isClicked = false;
            ClosePanel();
        });
    }

	public void OnPointerEnter(PointerEventData eventData)
	{
        if (!isActive) return;
        Logger.Log("Pointer entered CoinDiscardButton");
		OpenPanel();
	}

	public void OnPointerExit(PointerEventData eventData)
	{
        if (!isActive) return;
        Logger.Log("Pointer exited CoinDiscardButton");
        if (isClicked) return;
		ClosePanel();
	}

	public void OnPointerClick(PointerEventData eventData)
	{
        if (!isActive) return;
        isClicked = true;
		OpenPanel();
	}
    public void CancelIsClicked()
    {
        if (!isActive) return;
        isClicked = false;
        ClosePanel();   
    }
	private void OpenPanel()
	{
        Logger.Log("OpenPanel called for CoinDiscardButton");
		discardCoinDetail.SetActive(true);
        coinDiscardButtons.CoinDetail.Coin = coin;
	}
    private void ClosePanel()
    {
        discardCoinDetail.SetActive(false);
    }

    public void SetButtonActive(bool isActive)
    {
        Logger.Log("SetButtonActive called for CoinDiscardButton: " + isActive);
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
    }
}
