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
        maxLife = maxLife_Serialize;
    }
}