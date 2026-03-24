using System.Collections.Generic;
using UnityEngine;

public class InitialCoinsServer : MonoBehaviour
{
    [SerializeField] private SidesServer sidesServer;
    [SerializeField] private Coin coinPrefab;
    [SerializeField] private SideInventory sideInventory;
    [SerializeField] private CoinInventory coinInventory;
    [SerializeField] private List<Transform> coinSlots;
    [SerializeField] private CoinFactory coinFactory;
    void Start()
    {
        //初期コインの生成、1枚目
        BaseSide side1 = sidesServer.GetSpecificSide(1, "攻撃");
        BaseSide side2 = sidesServer.GetSpecificSide(1, "ミス");
        coinFactory.CreateCoin(side1, side2);

        //初期コインの生成、2枚目
        side1 = sidesServer.GetSpecificSide(1, "回復");
        side2 = sidesServer.GetSpecificSide(1, "暴発");
        coinFactory.CreateCoin(side1, side2);

    }
}