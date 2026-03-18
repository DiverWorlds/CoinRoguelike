using TMPro;
using UnityEngine;

/// <summary>
/// Displays a character's current HP in the format "label: XX".
/// Automatically searches for and tracks character references by name if initial reference becomes null.
/// </summary>
[RequireComponent(typeof(TMP_Text))]
public class CharacterHpText : MonoBehaviour
{
    [SerializeField] private string label = GameConstants.DefaultHealthLabel;
    [SerializeField] private Character character;
    [SerializeField] private string findObjectName;

    private TMP_Text hpText;

    private void Awake()
    {
        hpText = GetComponent<TMP_Text>();
    }

    private void Start()
    {
        TryFindCharacter();
        UpdateHpText();
    }

    private void Update()
    {
        UpdateHpText();
    }

    private void UpdateHpText()
    {
        if (character == null)
        {
            TryFindCharacter();

            if (character == null)
            {
                hpText.text = $"{label}: 00";
                return;
            }
        }

        hpText.text = $"{label}: {Mathf.Max(0, Mathf.RoundToInt(character.CurrentLife)):00}";
    }

    private void TryFindCharacter()
    {
        if (character != null)
        {
            return;
        }

        if (string.IsNullOrWhiteSpace(findObjectName))
        {
            return;
        }

        GameObject targetObject = GameObject.Find(findObjectName);
        if (targetObject == null)
        {
            targetObject = GameObject.Find(findObjectName + "(Clone)");
        }

        if (targetObject == null)
        {
            hpText.text = $"{label}: 00";
            return;
        }

        character = targetObject.GetComponent<Character>();
    }
}