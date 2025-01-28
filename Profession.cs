public class Profession
{
    public enum ProfessionType { Sniper, Grenadier, MachineGunner, Scout, Radioman, Officer, Medic }

    public ProfessionType type; // Тип профессии
    public float accuracyModifier; // Модификатор меткости
    public Weapon.WeaponType preferredWeapon; // Предпочитаемое оружие

    public Profession(ProfessionType type, float accuracyModifier, Weapon.WeaponType preferredWeapon)
    {
        this.type = type;
        this.accuracyModifier = accuracyModifier;
        this.preferredWeapon = preferredWeapon;
    }
}