public class Equipment
{
    public enum EquipmentType { Radio, Armor, Medkit }

    public EquipmentType type; // ��� ������������
    public float durability; // ��������� (��� �����������) ��� ���������� ������������� (��� �������)

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
            // ������ ������������� (����� ��������� �����)
        }
    }
}