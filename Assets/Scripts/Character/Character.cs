using UnityEngine;
using UnityEngine.TextCore.Text;

abstract public class Character : MonoBehaviour
{
    //TODO: 死をBattleManagerに通知する
    [SerializeField] private int maxLife;
    private int currentLife;
    private int sleepCounter = 0;

    void Start()
    {
        currentLife = maxLife;
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