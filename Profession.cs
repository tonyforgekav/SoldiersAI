public class Profession
{
    public enum ProfessionType { Sniper, Grenadier, MachineGunner, Scout, Radioman, Officer, Medic }

    public ProfessionType type; // ��� ���������
    public float accuracyModifier; // ����������� ��������
    public Weapon.WeaponType preferredWeapon; // �������������� ������

    public Profession(ProfessionType type, float accuracyModifier, Weapon.WeaponType preferredWeapon)
    {
        this.type = type;
        this.accuracyModifier = accuracyModifier;
        this.preferredWeapon = preferredWeapon;
    }
}