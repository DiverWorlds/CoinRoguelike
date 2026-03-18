using UnityEngine;
using UnityEngine.InputSystem;

public class CoinKeyboardUser : MonoBehaviour
{
    [SerializeField] private PlayerCoinInventory coinInventory;

    public void LogCoinsWithSlots()
    {
        if (coinInventory == null)
        {
            Debug.LogWarning("[CoinKeyboardUser] PlayerCoinInventory is not assigned.");
            return;
        }

        coinInventory.LogCoins();
    }

    private void Update()
    {
        if (coinInventory == null)
        {
            return;
        }

        if (Keyboard.current == null)
        {
            return;
        }

        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            coinInventory.UseCoinAtSlot(1);
        }

        if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            coinInventory.UseCoinAtSlot(2);
        }

        if (Keyboard.current.digit3Key.wasPressedThisFrame)
        {
            coinInventory.UseCoinAtSlot(3);
        }
    }
}
