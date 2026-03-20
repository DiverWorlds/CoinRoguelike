using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SpriteRenderer))]
public class Boss : Enemy
{
    [SerializeField] private Sprite currentSprite;
    [SerializeField] private Sprite[] spritesOnSituation = new Sprite[2];

    private void Start()
    {
        //初期化
        currentSprite = spritesOnSituation[0];
        GetComponent<SpriteRenderer>().sprite = currentSprite;
    }

    private void FixedUpdate()
    {
        //HPが最大HPの1/3以下になったら見た目を変える
        if (this.CurrentLife <= MaxLife / 3)
        {
            currentSprite = spritesOnSituation[1];
            GetComponent<SpriteRenderer>().sprite = currentSprite;
        }
    }
}