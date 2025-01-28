using UnityEngine;

public class SoldierGizmoDrawer : MonoBehaviour
{
    public GameManager gameManager;

    private void OnDrawGizmos()
    {
        if (gameManager == null || gameManager.soldiers == null)
            return;

        foreach (var soldier in gameManager.soldiers)
        {
            if (soldier == null)
                continue;

            // Устанавливаем цвет в зависимости от команды
            Gizmos.color = soldier.team == Soldier.Team.Red ? Color.red : Color.blue;

            // Рисуем точку на позиции солдата
            Gizmos.DrawSphere(new Vector3(soldier.position.x, soldier.position.y, 0), 0.2f);
        }
    }
}