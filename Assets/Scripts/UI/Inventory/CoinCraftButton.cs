using UnityEngine;
using UnityEngine.UI;

public class CoinCraftButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Image buttonImage;
    [SerializeField] private Sprite enableSprite;
    [SerializeField] private Sprite disableSprite;
    [SerializeField] private CoinDetail coinDetail;
    [SerializeField] private CoinFactory coinFactory;

    void Start()
    {
        button.onClick.AddListener(OnClick);
    }
    void FixedUpdate()
    {
        bool canCraft = InventoryManager.Instance.CoinInventory.CoinCount < 3;
        button.interactable = canCraft;
        buttonImage.sprite = canCraft ? enableSprite : disableSprite;
    }
    public void OnClick()
    {
        if (coinDetail.Coin == null)
        {
            return;
        }
    
        coinFactory.MoveToSlot(coinDetail.Coin);
        coinDetail.Coin = null;
    }
}
