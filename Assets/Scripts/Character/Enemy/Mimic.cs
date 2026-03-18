using UnityEngine;

public class Mimic : Enemy
{
    public override void Act(Player player)
    {
        // Mimicの行動ルーティンを定義する
        // 例: プレイヤーに攻撃する、アイテムをドロップするなど
        Attack(player, power);
    }
}