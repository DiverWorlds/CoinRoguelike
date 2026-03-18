using UnityEngine;

public abstract class SideActionBase : MonoBehaviour
{
    [SerializeField] private Character defaultActor;
    [SerializeField] private Side defaultSide;

    public void Execute(Character target)
    {
        Execute(defaultActor, target, defaultSide);
    }

    public void Execute(Character actor, Character target, Side side)
    {
        Execute(actor, target, side, side != null ? side.Value : 0f);
    }

    public void Execute(Character actor, Character target, Side side, float effectPower)
    {
        if (actor == null || side == null)
        {
            return;
        }

        ExecuteCore(actor, target, side, effectPower);
    }

    protected abstract void ExecuteCore(Character actor, Character target, Side side, float effectPower);
}