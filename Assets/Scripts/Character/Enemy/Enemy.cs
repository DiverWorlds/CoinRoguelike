using System;
using Unity.VisualScripting;
using UnityEngine;

//TODO: Enemyにsleep時の処理を追加
[RequireComponent(typeof(Animator))]
abstract public class Enemy : Character
{
    [SerializeField] protected SkillsOnRank[] skillRanks = new SkillsOnRank[5];
    [SerializeField] protected int[] maxLifesOnRank = new int[5];
    [SerializeField] private SpriteRenderer sleepIcon;
    private EnemySkillData nextSkill;
    public EnemySkillData NextSkill => nextSkill;
    private int currentRank = 1;
    protected Animator animator;
    private bool isAnimationPlaying = false;
    public bool IsAnimationPlaying => isAnimationPlaying;

    public void Initialize(int currentStage)
    {
        //最初にランクを設定
        this.currentRank = (currentStage - 1) / 10 + 1;
        if (currentRank > 5) currentRank = 5;

        //敵のHPに若干のブレを加える。ブレの幅は最大HPの5%ほどにする。
        MaxLife = maxLifesOnRank[currentRank - 1] + UnityEngine.Random.Range(-maxLifesOnRank[currentRank - 1] / 20, maxLifesOnRank[currentRank - 1] / 20);
        animator = GetComponent<Animator>();
        UpdateSleepIcon();
        SetNextSkill();
        Logger.Log("currentStage" + currentStage + " currentRank" + currentRank);
    }

    public void SetNextSkill()
    {
        int random = UnityEngine.Random.Range(0, 100);
        if (random > 50) nextSkill = skillRanks[currentRank - 1].skills[0];
        else if (random > 35) nextSkill = skillRanks[currentRank - 1].skills[1];
        else nextSkill = skillRanks[currentRank - 1].skills[2];
    }

    // 行動ルーティンを定義する
    public void Act(Player player)
    {
        if (SleepCounter > 0)
        {
            SleepCounter--;
            TakeStay();
            return;
        }
        UseSkill(player, nextSkill);
    }

    protected override void OnSleepCounterChanged()
    {
        UpdateSleepIcon();
    }

    private void UpdateSleepIcon()
    {
        if (sleepIcon == null)
        {
            return;
        }

        sleepIcon.enabled = SleepCounter > 0 && CurrentLife > 0;
    }
    protected void UseSkill(Character target, EnemySkillData skillData)
    {
        target.TakeDamage(skillData.power);
        if (skillData.power != 0)
        {
            isAnimationPlaying = true;
            animator.SetTrigger("Attack");
        }

        if (skillData.destroyEffect)
        {
            DestroyCoin(target);
        }
        SetNextSkill();
    }

    protected void DestroyCoin(Character target)
    {
        CoinInventory coinInventory = InventoryManager.Instance.CoinInventory;
        if (coinInventory.CoinCount == 0) return;
        int coinIndex = UnityEngine.Random.Range(0, coinInventory.CoinCount);
        coinInventory.RemoveRandom();
    }

    //Animationから呼ばれる
    public void ResetAnimation()
    {
        isAnimationPlaying = false;
        animator.SetTrigger("Reset");
    }
}

[Serializable]
public class SkillsOnRank
{
    public EnemySkillData[] skills = new EnemySkillData[3];
}