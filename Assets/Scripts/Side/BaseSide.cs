using UnityEngine;

abstract public class BaseSide : MonoBehaviour
{
    protected FrontAndBack frontOrBack;
    protected int strength;
    [SerializeField] protected int minStrength;
    [SerializeField] protected int maxStrength;
    protected int weight;
    [SerializeField] protected int minWeight;
    [SerializeField] protected int maxWeight;
    protected Rank rank;
    protected string effectName;

    public FrontAndBack FrontOrBack => frontOrBack;
    public int Strength => strength;
    public int Weight => weight;
    public Rank Rank => rank;
    public string EffectName => effectName;
    

    void Start()
    {
    }
    public virtual void Initialize()
    {
        int randomizedStrength = CalcStrength() + Random.Range(-1, 2);
        this.strength = Mathf.Clamp(randomizedStrength, minStrength, maxStrength);

        int randomizedWeight = CalcWeight() + Random.Range(-1, 2);
        this.weight = Mathf.Clamp(randomizedWeight, minWeight, maxWeight);
    }
    public virtual void Effect(int value, Character target)
    {
        return;
    }
    private int CalcStrength()
    {
        //TODO: ちゃんと作る
        return Mathf.RoundToInt((minStrength + maxStrength) / 2f);
    }
    private int CalcWeight()
    {
        //TODO: ちゃんと作る
        return Mathf.RoundToInt((minWeight + maxWeight) / 2f);
    }

}