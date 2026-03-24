using System.Collections.Generic;
using UnityEngine;

public class DropDisplays : MonoBehaviour
{
    [SerializeField] private Transform displayParent;
    [SerializeField] private DropDisplay dropDisplayPrefab;
    [SerializeField] private float displayDuration = 2f;

    public void CreateDropDisplays(BaseSide drops)
    {
        DropDisplay dropDisplay = Instantiate(dropDisplayPrefab, displayParent);
        dropDisplay.Initialize(drops.EffectName, drops.FrontOrBack);
        DestroyDisplay(dropDisplay.gameObject);
    }


    private void DestroyDisplay(GameObject displayObject)
    {
        if (displayDuration <= 0f)
        {
            displayObject.SetActive(false);
            Destroy(displayObject);
            return;
        }

        Destroy(displayObject, displayDuration);
    }
}