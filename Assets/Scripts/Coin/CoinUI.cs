using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider2D))]
public class CoinUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
	private CoinDescriptionWindow coinDescriptionWindow;
	private BattleManager battleManager;
	private CoinInventory coinInventory;
	private ExitManager exitManager;
	private DungeonManager dungeonManager;
	private DungeonConstructor dungeonConstructor;
	private Coin coin;

	private void Awake()
	{
		coinDescriptionWindow = FindFirstObjectByType<CoinDescriptionWindow>();
		battleManager = FindFirstObjectByType<BattleManager>();
		Player player = FindFirstObjectByType<Player>();
		exitManager = FindFirstObjectByType<ExitManager>();
		dungeonManager = FindFirstObjectByType<DungeonManager>();
		dungeonConstructor = FindFirstObjectByType<DungeonConstructor>();
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
		if (dungeonConstructor != null && dungeonConstructor.IsMoving) return;
		if (EnemyParent.Instance.Enemy.IsAnimationPlaying) return;
		if (dungeonManager.CurrentStage % 10 == 1)
		{
			exitManager.HideExit();
		}
		int coinIndex = coinInventory.IndexOf(coin);
		battleManager.ExecutePlayerCoinEffect(coinIndex);
	}
}