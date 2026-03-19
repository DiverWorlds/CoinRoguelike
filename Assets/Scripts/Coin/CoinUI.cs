using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider2D))]
public class CoinUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
	public void OnPointerEnter(PointerEventData eventData)
	{
		Debug.Log($"{name}: hover(pointer)");
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		Debug.Log($"{name}: exit(pointer)");
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		Debug.Log($"{name}: click(pointer)");
	}

	private void OnMouseEnter()
	{
		Debug.Log($"{name}: hover(mouse)");
	}

	private void OnMouseDown()
	{
		Debug.Log($"{name}: click(mouse)");
	}
}