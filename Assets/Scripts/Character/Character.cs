using UnityEngine;

abstract public class Character : MonoBehaviour
{
    protected int maxLife;
    private int currentLife;
    protected int sleepCounter = 0;

    public int CurrentLife => currentLife;
    public bool IsDead => currentLife <= 0;

    public int SleepCounter
    {
        get => sleepCounter;
        set
        {
            sleepCounter = Mathf.Max(0, value);
        }
    }
    public int MaxLife
    {
        get => maxLife;
        set
        {
            maxLife = Mathf.Max(1, value);
            currentLife = maxLife;
        }
    }

    protected void Heal(int amount)
    {
        if (amount <= 0)
        {
            return;
        }

        currentLife = Mathf.Min(currentLife + amount, maxLife);
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

    public void TakeSleep(int turns)
    {
        SleepCounter = turns;
    }

    public void TakeHeal(int amount)
    {
        Heal(amount);
    }
}