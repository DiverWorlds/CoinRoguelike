using UnityEngine;

[CreateAssetMenu(fileName = "EnemySkillData", menuName = "Scriptable Objects/EnemySkillData")]
public class EnemySkillData : ScriptableObject
{
    [SerializeField] public string skillName;
    [SerializeField] public int power;//攻撃力を示す。0の場合攻撃が行われない。
    [SerializeField] public bool destroyEffect;//攻撃後にコインを破壊するかどうか
}
