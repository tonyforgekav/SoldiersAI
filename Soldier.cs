using UnityEngine;
using System.Collections.Generic;

public class Soldier
{
    public enum Team { Red, Blue }

    public Team team; // ������� ���������
    public Vector2 position; // ������� �� �����
    public float speed; // �������� �����������
    public bool inCover; // � ������� �� ��������
    public bool inBuilding; // � ������ �� ��������
    public Weapon weapon; // ������ ���������
    public Equipment equipment; // ������������ ���������
    public Squad squad; // �����, � �������� ����������� ��������
    public float visionRange; // ������ ������
    public float bodyRadius; // ������ ���� ���������
    public List<EnemySighting> enemySightings; // ������ ������������ ������
    public Profession profession; // ��������� ���������
    public SoldierParameters parameters; // ��������� ���������
    public SoldierState state; // ��������� ���������

    public Soldier(Team team, Vector2 startPosition, float speed, Weapon weapon, Equipment equipment, float visionRange, float bodyRadius, Profession profession, SoldierParameters parameters)
    {
        this.team = team;
        this.position = startPosition;
        this.speed = speed;
        this.inCover = false;
        this.inBuilding = false;
        this.weapon = weapon;
        this.equipment = equipment;
        this.squad = null;
        this.visionRange = visionRange;
        this.bodyRadius = bodyRadius;
        this.enemySightings = new List<EnemySighting>();
        this.profession = profession;
        this.parameters = parameters;
        this.state = new SoldierState();
    }

    public void Move(Vector2 direction)
    {
        if (state.currentState == SoldierState.State.Alive || state.currentState == SoldierState.State.Wounded)
        {
            position += direction.normalized * speed * Time.deltaTime;
        }
    }

    public void UpdateStatus(Map map)
    {
        inCover = map.IsInCover(position);
        inBuilding = map.IsInBuilding(position);
        state.UpdateState(parameters.health);
    }

    public void DetectEnemies(Soldier[] allSoldiers)
    {
        if (state.currentState == SoldierState.State.Alive)
        {
            foreach (var soldier in allSoldiers)
            {
                if (soldier.team != team && Vector2.Distance(position, soldier.position) <= visionRange)
                {
                    bool isEnemy = true;

                    if (equipment != null && equipment.type == Equipment.EquipmentType.Radio)
                    {
                        // ������ �� ����� ��� �������������
                        isEnemy = !IsFriendly(soldier);
                    }

                    if (isEnemy)
                    {
                        enemySightings.Add(new EnemySighting(soldier.position, Time.time, 10f)); // ���������� ���������� ����� 10 ������
                    }
                }
            }
        }
    }

    private bool IsFriendly(Soldier soldier)
    {
        if (squad != null)
        {
            return squad.members.Contains(soldier);
        }
        return false;
    }

    public void Attack(Vector2 targetPosition)
    {
        if (state.currentState == SoldierState.State.Alive && weapon != null && weapon.CanAttack())
        {
            weapon.Attack();
            // ������ ��������� �����
            foreach (var enemy in enemySightings)
            {
                if (Vector2.Distance(targetPosition, enemy.position) <= weapon.attackRange)
                {
                    // ��������� ���������
                    if (IsHit(targetPosition, enemy.position))
                    {
                        // ������� ����
                        // ������ ��������� ����� (����� ��������� �����)
                    }
                }
            }
        }
    }

    private bool IsHit(Vector2 shooterPosition, Vector2 targetPosition)
    {
        float spread = Random.Range(-0.1f, 0.1f) * (1 - profession.accuracyModifier); // ������� ��� �������� � ������ ���������
        Vector2 direction = (targetPosition - shooterPosition).normalized;
        Vector2 shotDirection = direction + new Vector2(spread, spread);

        RaycastHit2D hit = Physics2D.Raycast(shooterPosition, shotDirection, weapon.attackRange);
        if (hit.collider != null)
        {
            // ���������, ������ �� �� � ����
            return Vector2.Distance(hit.point, targetPosition) <= bodyRadius;
        }
        return false;
    }

    public void UpdateEnemySightings()
    {
        enemySightings.RemoveAll(sighting => sighting.IsExpired(Time.time));
    }

    public void TakeDamage(float damage)
    {
        if (equipment != null && equipment.type == Equipment.EquipmentType.Armor)
        {
            // ���������� ����� ������� ����
            if (Random.value > 0.5f) // 50% ���� ������� ����
            {
                damage *= 0.5f;
            }
        }
        parameters.TakeDamage(damage);
        state.UpdateState(parameters.health);
    }

    public void GainExperience(float amount)
    {
        parameters.GainExperience(amount);
    }

    public void UpdateFear(float amount)
    {
        parameters.UpdateFear(amount);
    }

    public void UpdateHeroism(float amount)
    {
        parameters.UpdateHeroism(amount);
    }

    public void Heal(float amount)
    {
        if (state.currentState == SoldierState.State.Wounded || state.currentState == SoldierState.State.SeverelyWounded)
        {
            parameters.health += amount;
            if (parameters.health > 100) parameters.health = 100;
            state.UpdateState(parameters.health);
        }
    }

    public void Rescue(Soldier rescuer)
    {
        if (state.currentState == SoldierState.State.Wounded || state.currentState == SoldierState.State.SeverelyWounded || state.currentState == SoldierState.State.Dead)
        {
            // ������ �������� (����������� � ���������� �����)
            position = rescuer.position;
        }
    }
}