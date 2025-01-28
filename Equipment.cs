public class Equipment
{
    public enum EquipmentType { Radio, Armor, Medkit }

    public EquipmentType type; // Тип оборудования
    public float durability; // Прочность (для бронежилета) или количество использований (для аптечки)

    public Equipment(EquipmentType type, float durability)
    {
        this.type = type;
        this.durability = durability;
    }

    public void Use()
    {
        if (durability > 0)
        {
            durability--;
            // Логика использования (будет добавлена позже)
        }
    }
}