using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinSlotButton : MonoBehaviour
{
    [SerializeField] private PlayerCoinInventory coinInventory;
    [SerializeField] [Range(1, 3)] private int slot = 1;
    [SerializeField] private TextMeshProUGUI label;
    [SerializeField] private Button button;
    [SerializeField] private string emptyLabel = "(empty)";

    private string lastLabelText;
    private bool lastInteractable;

    private void Awake()
    {
        if (button == null)
        {
            button = GetComponent<Button>();
        }

        if (button != null)
        {
            button.onClick.RemoveListener(OnClickUseCoinAtAssignedSlot);
            button.onClick.AddListener(OnClickUseCoinAtAssignedSlot);
        }
    }

    private void OnEnable()
    {
        if (coinInventory != null)
        {
            coinInventory.CoinsChanged += Refresh;
        }
    }

    private void OnDisable()
    {
        if (coinInventory != null)
        {
            coinInventory.CoinsChanged -= Refresh;
        }
    }

    private void Start()
    {
        Refresh();
    }

    private void Update()
    {
        Refresh();
    }

    public void OnClickUseCoinAtAssignedSlot()
    {
        if (coinInventory == null)
        {
            Debug.LogWarning("[CoinSlotButton] PlayerCoinInventory is not assigned.");
            return;
        }

        Debug.Log($"[CoinSlotButton] Attempting use at slot {slot}.");
        coinInventory.UseCoinAtSlot(slot);
    }

    private void Refresh()
    {
        string nextLabel = emptyLabel;
        bool hasCoin = false;

        if (coinInventory != null)
        {
            int index = slot - 1;
            var coins = coinInventory.GetCoins();
            if (index >= 0 && index < coins.Count)
            {
                Coin coin = coins[index];
                nextLabel = $"{coin.FrontSide.SideName}({coin.FrontSide.Rank})/{coin.BackSide.SideName}({coin.BackSide.Rank})";
                hasCoin = true;
            }
        }

        if (label != null && nextLabel != lastLabelText)
        {
            label.text = nextLabel;
            lastLabelText = nextLabel;
        }

        if (button != null && hasCoin != lastInteractable)
        {
            button.interactable = hasCoin;
            lastInteractable = hasCoin;
        }
    }
}
