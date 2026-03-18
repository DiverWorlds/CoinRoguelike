using UnityEngine;
using UnityEngine.UI;

public class CoinDiscardSlotButton : MonoBehaviour
{
    [SerializeField] private PlayerCoinInventory coinInventory;
    [SerializeField] [Range(1, 3)] private int slot = 1;
    [SerializeField] private Button button;

    private void Awake()
    {
        if (button == null)
        {
            button = GetComponent<Button>();
        }

        if (button != null)
        {
            button.onClick.RemoveListener(OnClickDiscardAssignedSlot);
            button.onClick.AddListener(OnClickDiscardAssignedSlot);
        }
        else
        {
            Debug.LogWarning("[CoinDiscardSlotButton] Button component was not found. OnClick will not fire automatically.", this);
        }
    }

    public void OnClickDiscardAssignedSlot()
    {
        if (coinInventory == null)
        {
            Debug.LogWarning("[CoinDiscardSlotButton] PlayerCoinInventory is not assigned.");
            return;
        }

        Debug.Log($"[CoinDiscardSlotButton] Attempting discard at slot {slot}.");
        coinInventory.DiscardCoinAtSlot(slot);
    }
}
