using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider2D))]
public class CoinUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
	private CoinDescriptionWindow coinDescriptionWindow;
	private BattleManager battleManager;
	private CoinInventory coinInventory;
    private Coin coin;

	private void Awake()
	{
		coinDescriptionWindow = FindFirstObjectByType<CoinDescriptionWindow>();
		battleManager = FindFirstObjectByType<BattleManager>();
		Player player = FindFirstObjectByType<Player>();
		coinInventory = player.CoinInventory;
	}

	void Start()
    {
        coin = GetComponent<Coin>();
    }
    public void OnPointerEnter(PointerEventData eventData)
	{
        if (coinDescriptionWindow != null && coin != null)
        {
            coinDescriptionWindow.Set(coin.FrontEffectName, coin.BackEffectName, coin.FrontSideValue, coin.BackSideValue, coin.FrontSideProbability);
        }
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		coinDescriptionWindow.Hide();
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		int coinIndex = coinInventory.IndexOf(coin);
		battleManager.ExecutePlayerCoinEffect(coinIndex);
	}


}