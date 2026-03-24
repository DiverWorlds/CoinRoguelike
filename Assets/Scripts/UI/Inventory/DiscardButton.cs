using UnityEngine;
using UnityEngine.UI;

public class DiscardButton : MonoBehaviour
{
    [SerializeField] private GameObject discardPanel;
    [SerializeField] private CoinDetail discardCoinDetail;
    [SerializeField] private Button button;

    void Start()
    {
        button.onClick.AddListener(() =>
        {
            Logger.Log("DiscardButton clicked");
            InventoryManager.Instance.CoinInventory.Remove(discardCoinDetail.Coin);
            discardPanel.SetActive(false);
        });
    }
    
}