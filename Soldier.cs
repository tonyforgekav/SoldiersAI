using UnityEngine;
using System.Collections.Generic;

public class Soldier
{
    public enum Team { Red, Blue }

    public Team team; // Команда солдатика
    public Vector2 position; // Позиция на карте
    public float speed; // Скорость перемещения
    public bool inCover; // В укрытии ли солдатик
    public bool inBuilding; // В здании ли солдатик
    public Weapon weapon; // Оружие солдатика
    public Equipment equipment; // Оборудование солдатика
    public Squad squad; // Отряд, к которому принадлежит солдатик
    public float visionRange; // Радиус обзора
    public float bodyRadius; // Радиус тела солдатика
    public List<EnemySighting> enemySightings; // Список обнаруженных врагов
    public Profession profession; // Профессия солдатика
    public SoldierParameters parameters; // Параметры солдатика
    public SoldierState state; // Состояние солдатика

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
                        // Запрос по радио для идентификации
                        isEnemy = !IsFriendly(soldier);
                    }

                    if (isEnemy)
                    {
                        enemySightings.Add(new EnemySighting(soldier.position, Time.time, 10f)); // Информация устаревает через 10 секунд
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
            // Логика нанесения урона
            foreach (var enemy in enemySightings)
            {
                if (Vector2.Distance(targetPosition, enemy.position) <= weapon.attackRange)
                {
                    // Проверяем попадание
                    if (IsHit(targetPosition, enemy.position))
                    {
                        // Наносим урон
                        // Логика нанесения урона (будет добавлена позже)
                    }
                }
            }
        }
    }

    private bool IsHit(Vector2 shooterPosition, Vector2 targetPosition)
    {
        float spread = Random.Range(-0.1f, 0.1f) * (1 - profession.accuracyModifier); // Разброс при стрельбе с учетом профессии
        Vector2 direction = (targetPosition - shooterPosition).normalized;
        Vector2 shotDirection = direction + new Vector2(spread, spread);

        RaycastHit2D hit = Physics2D.Raycast(shooterPosition, shotDirection, weapon.attackRange);
        if (hit.collider != null)
        {
            // Проверяем, попали ли мы в цель
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
            // Бронежилет может снизить урон
            if (Random.value > 0.5f) // 50% шанс снизить урон
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
            // Логика спасения (перемещение в безопасное место)
            position = rescuer.position;
        }
    }
}