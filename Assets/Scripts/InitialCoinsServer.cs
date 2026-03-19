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
        List<BaseSide> sides = new();
        foreach (var side in sidesServer.GetSides())
        {
            BaseSide newSide = Instantiate(side.gameObject, sideInventory.transform).GetComponent<BaseSide>();
            newSide.Initialize();
            sides.Add(newSide);
        }

        //TODO: デバッグ用に足りないSideを雑に生成してるので、ちゃんとした方法で初期コインを生成するようにする
        for (int i = 0; i < 2; i++)
        {
            foreach (var side in sidesServer.GetSides())
            {
                BaseSide newSide = Instantiate(side.gameObject, sideInventory.transform).GetComponent<BaseSide>();
                newSide.Initialize();
                sides.Add(newSide);
            }
        }

        //TODO: 雑に作ってるので直す
        for (int i = 0; i < 3; i++)
        {
            coinFactory.CreateCoin(sides[2*i], sides[2*i+1]);
        }
    }
}