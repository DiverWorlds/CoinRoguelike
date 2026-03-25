using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AluFaceChanger : Singleton<AluFaceChanger>
{
    /*
    0: 通常
    1: 真剣
    2: 喜び
    3: むすっ
    4: 泣き
    5: 痛い
    6: ばたんきゅー
    */
    [SerializeField] private List<Sprite> faceSprites;
    [SerializeField] private Image aluImage;
    [SerializeField] private Player player;
    [SerializeField] private float painDuration = 0.5f;
    [SerializeField] private float happyDuration = 0.5f;

    private float painTimer = 0f;
    private float happyTimer = 0f;
    private int previousLife = -1;

    private void Start()
    {
        if (player != null)
        {
            previousLife = player.CurrentLife;
        }
    }

    private void Update()
    {
        CheckLifeChange();
        UpdateTimers();
        UpdateFace();
    }

    private void UpdateTimers()
    {
        painTimer -= Time.deltaTime;
        happyTimer -= Time.deltaTime;
    }

    private void UpdateFace()
    {
        if (happyTimer > 0f)
        {
            ChangeHappy();
            return;
        }

        if (painTimer > 0f)
        {
            ChangePain();
            return;
        }

        UpdateFaceByLife();
    }

    private void CheckLifeChange()
    {
        int currentLife = player.CurrentLife;
        if (previousLife > currentLife)
        {
            OnPlayerDamaged();
        }
        previousLife = currentLife;
    }

    private void OnPlayerDamaged()
    {
        painTimer = painDuration;
    }

    public void OnEnemyDefeated()
    {
        happyTimer = happyDuration;
    }

    private void UpdateFaceByLife()
    {
        int life = player.CurrentLife;
        if (life >= 600)
        {
            ChangeNormal();
        }
        else if (life >= 300)
        {
            ChangeSerious();
        }
        else
        {
            ChangeCrying();
        }
    }

    public void ChangeNormal()
    {
        aluImage.sprite = faceSprites[0];
    }
    public void ChangeSerious()
    {
        aluImage.sprite = faceSprites[1];
    }
    public void ChangeHappy()
    {
        aluImage.sprite = faceSprites[2];       
    }
    public void ChangeSullen()
    {
        aluImage.sprite = faceSprites[3];
    }
    public void ChangeCrying()
    {
        aluImage.sprite = faceSprites[4];
    }
    public void ChangePain()
    {
        aluImage.sprite = faceSprites[5];
    }
    public void ChangeFainted()
    {
        aluImage.sprite = faceSprites[6];
    }
}
