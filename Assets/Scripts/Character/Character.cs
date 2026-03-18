using UnityEngine;
using UnityEngine.TextCore.Text;

abstract public class Character : MonoBehaviour
{
    //TODO: 死をBattleManagerに通知する
    protected int maxLife;
    private int currentLife;
    private int sleepCounter = 0;

    protected int MaxLife
    {
        get => maxLife;
        set => maxLife = value;
    }


    public void TakeDamage(int damage)
    {
        currentLife -= damage;
        if (currentLife < 0)
        {
            currentLife = 0;
        }
        
    }
    public void TakeStay()
    {
        return;
    }
}