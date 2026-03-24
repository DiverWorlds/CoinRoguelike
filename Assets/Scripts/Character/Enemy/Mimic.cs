using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SpriteRenderer))]
public class Mimic : Enemy
{
    [SerializeField] private Sprite currentSprite;
    [SerializeField] private Sprite[] mimicSprites = new Sprite[3];

    public void Start()
    {
        // Mimicの見た目のランダム化処理
        int randomIndex = Random.Range(0, mimicSprites.Length);
        currentSprite = mimicSprites[randomIndex];
        GetComponent<SpriteRenderer>().sprite = currentSprite;
    }
}