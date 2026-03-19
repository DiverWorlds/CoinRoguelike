using System.Collections.Generic;
using UnityEngine;

public class InitialCoinsServer : MonoBehaviour
{
    [SerializeField] private SidesGacha sidesGacha;
    [SerializeField] private Coin coinPrefab;
    [SerializeField] private CoinInventory coinInventory;
    [SerializeField] private CoinFactory coinFactory;

    void Start()
    {
        List<BaseSide> sides = sidesGacha.GetSides();
        //TODO: 雑に作ってるので直す
        for (int i = 0; i < 3; i++)
        {
            Coin newCoin = coinFactory.CreateCoin(sides[0], sides[1]);
            newCoin.SetSides(sides[0], sides[1]);
            coinInventory.Add(newCoin);
        }
    }
}