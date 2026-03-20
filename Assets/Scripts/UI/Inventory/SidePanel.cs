using UnityEngine;

public class SidePanel : Singleton<SidePanel>
{
    [SerializeField] private FrontAndBack frontOrBack;
    public FrontAndBack FrontOrBack => frontOrBack;
}
