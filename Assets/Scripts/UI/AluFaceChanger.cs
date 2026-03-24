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
