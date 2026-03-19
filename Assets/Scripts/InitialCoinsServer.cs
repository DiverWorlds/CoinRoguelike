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
            sides.Add(newSide);
        }
        //TODO: 雑に作ってるので直す
        for (int i = 0; i < 3; i++)
        {
            Coin newCoin = coinFactory.CreateCoin(sides[0], sides[1]);
            coinInventory.Add(newCoin);
        }
        Debug.Log("InitialCoinsServer.Start() finished");
    }
}