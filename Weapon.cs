using UnityEngine;

public class Weapon
{
    public enum WeaponType { Knife, Pistol, MachineGun, SniperRifle, GrenadeLauncher, Grenade }

    public WeaponType type; // Тип оружия
    public float attackRange; // Радиус атаки
    public float attackSpeed; // Скорость атаки (выстрелов в секунду)
    public int maxAmmo; // Максимальное количество патронов
    public int currentAmmo; // Текущее количество патронов
    public float damage; // Урон за атаку
    public bool isExplosive; // Наносит ли урон по площади
    public bool isPenetrating; // Проникающий ли урон

    public Weapon(WeaponType type, float attackRange, float attackSpeed, int maxAmmo, float damage, bool isExplosive, bool isPenetrating)
    {
        this.type = type;
        this.attackRange = attackRange;
        this.attackSpeed = attackSpeed;
        this.maxAmmo = maxAmmo;
        this.currentAmmo = maxAmmo;
        this.damage = damage;
        this.isExplosive = isExplosive;
        this.isPenetrating = isPenetrating;
    }

    public bool CanAttack()
    {
        return currentAmmo > 0;
    }

    public void Attack()
    {
        if (CanAttack())
        {
            currentAmmo--;
            // Логика атаки (будет добавлена позже)
        }
    }

    public void Reload()
    {
        currentAmmo = maxAmmo;
    }
}