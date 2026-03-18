using UnityEngine;

[CreateAssetMenu(fileName = "New Side", menuName = "CoinRoguelike/Side", order = 1)]
public class Side : ScriptableObject
{
   [SerializeField] private string sideName;
   [SerializeField] private FrontAndBack frontOrBack;
   [SerializeField] private Rank rank;
   [SerializeField] private int value;
   [SerializeField] private int weight;

   public string SideName => sideName;
   public FrontAndBack FrontOrBack => frontOrBack;
   public Rank Rank => rank;
   public int Value => value;
   public int Weight => weight;

   public Coin CreateCoinWith(Side otherSide, float frontBackCoefficient = 1.0f)
   {
      return CoinFactory.Create(this, otherSide, frontBackCoefficient);
   }

   public static Coin CreateCoin(Side firstSide, Side secondSide, float frontBackCoefficient = 1.0f)
   {
      return CoinFactory.Create(firstSide, secondSide, frontBackCoefficient);
   }
}