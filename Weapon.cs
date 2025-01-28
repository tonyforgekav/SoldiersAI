using UnityEngine;

public class Weapon
{
    public enum WeaponType { Knife, Pistol, MachineGun, SniperRifle, GrenadeLauncher, Grenade }

    public WeaponType type; // ��� ������
    public float attackRange; // ������ �����
    public float attackSpeed; // �������� ����� (��������� � �������)
    public int maxAmmo; // ������������ ���������� ��������
    public int currentAmmo; // ������� ���������� ��������
    public float damage; // ���� �� �����
    public bool isExplosive; // ������� �� ���� �� �������
    public bool isPenetrating; // ����������� �� ����

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
            // ������ ����� (����� ��������� �����)
        }
    }

    public void Reload()
    {
        currentAmmo = maxAmmo;
    }
}