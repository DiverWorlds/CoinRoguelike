using UnityEngine;

public class FPSInit : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate = 60;
    }
}
