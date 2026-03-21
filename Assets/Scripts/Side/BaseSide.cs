using UnityEngine;

abstract public class BaseSide : MonoBehaviour
{
    protected FrontAndBack frontOrBack;
    [SerializeField] protected int[] baseStrengthOnRank = new int[5];
    [SerializeField] protected int[] baseWeightOnRank = new int[5];
    protected int strength;
    protected int weight;
    protected string effectName;

    public FrontAndBack FrontOrBack => frontOrBack;
    public int Strength => strength;
    public int Weight => weight;
    public string EffectName => effectName;


    void Start()
    {
    }
    public virtual void Initialize(int currentStage, bool individualize)
    {
        int rank = (currentStage - 1) / 10 + 1;
        strength = baseStrengthOnRank[rank - 1];//いったん数値にブレなし
        if (individualize) weight = baseWeightOnRank[rank - 1] + Random.Range(-1, 1);//数値にブレあり（-1~1の範囲でブレる）
        else weight = baseWeightOnRank[rank - 1];//数値にブレなし

    }
    public virtual void Effect(int value, Character target, Character user)
    {
        return;
    }
}