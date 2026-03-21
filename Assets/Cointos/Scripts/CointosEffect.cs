using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator)), RequireComponent(typeof(SpriteRenderer))]
public class CointosEffect : MonoBehaviour
{
    private Image image;
    private Animator animator;

    private void Awake()
    {
        image = GetComponent<Image>();
        animator = GetComponent<Animator>();
    }

    public void ResetTrigger()
    {
        animator.SetTrigger("Reset");
    }

    public void ResultFront()
    {
        animator.SetTrigger("Front");
    }

    public void ResultBack()
    {
        animator.SetTrigger("Back");
    }
}
