using UnityEngine;
using UnityEngine.TextCore.Text;

public class Player : Character
{
    [SerializeField] private int maxLife_Serialize;
    [SerializeField] private SideInventory sideInventory;
    [SerializeField] private CoinInventory coinInventory;

    public CoinInventory CoinInventory => coinInventory;

    private void Awake()
    {
        MaxLife = maxLife_Serialize;
    }

    public void RecoverLifeOnBattleWin()
    {
        int recoverAmount = Mathf.Max(1, Mathf.RoundToInt(maxLife * 0.2f));
        Heal(recoverAmount);
    }
}