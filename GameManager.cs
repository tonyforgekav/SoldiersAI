using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int mapSize = 10;
    public int numberOfSoldiers = 5;

    private Map map;
    public Soldier[] soldiers;

    void Start()
    {
        map = new Map(mapSize);
        InitializeSoldiers();
        FormSquads();
    }

    void Update()
    {
        MoveSoldiers();
        UpdateSoldierStatuses();
        DetectEnemies();
        UpdateEnemySightings();
        MakeTacticalDecisions();
        RescueAndHealSoldiers();
    }

    private void InitializeSoldiers()
    {
        soldiers = new Soldier[numberOfSoldiers];
        for (int i = 0; i < numberOfSoldiers; i++)
        {
            Vector2 startPosition = new Vector2(Random.Range(0, mapSize), Random.Range(0, mapSize));
            Soldier.Team team = (i % 2 == 0) ? Soldier.Team.Red : Soldier.Team.Blue;
            Weapon weapon = GetRandomWeapon();
            Equipment equipment = GetRandomEquipment();
            Profession profession = GetRandomProfession();
            SoldierParameters parameters = new SoldierParameters(100, 0, 0, 0);
            soldiers[i] = new Soldier(team, startPosition, 2.0f, weapon, equipment, 5f, 0.5f, profession, parameters);
        }
    }

    private Weapon GetRandomWeapon()
    {
        Weapon.WeaponType[] weaponTypes = (Weapon.WeaponType[])System.Enum.GetValues(typeof(Weapon.WeaponType));
        Weapon.WeaponType randomType = weaponTypes[Random.Range(0, weaponTypes.Length)];

        return new Weapon(randomType, Random.Range(1f, 10f), Random.Range(0.5f, 2f), Random.Range(10, 30), Random.Range(10, 50), Random.value > 0.5f, Random.value > 0.5f);
    }

    private Equipment GetRandomEquipment()
    {
        Equipment.EquipmentType[] equipmentTypes = (Equipment.EquipmentType[])System.Enum.GetValues(typeof(Equipment.EquipmentType));
        Equipment.EquipmentType randomType = equipmentTypes[Random.Range(0, equipmentTypes.Length)];

        return new Equipment(randomType, Random.Range(1, 5));
    }

    private Profession GetRandomProfession()
    {
        Profession.ProfessionType[] professionTypes = (Profession.ProfessionType[])System.Enum.GetValues(typeof(Profession.ProfessionType));
        Profession.ProfessionType randomType = professionTypes[Random.Range(0, professionTypes.Length)];

        return new Profession(randomType, Random.Range(0.5f, 1f), Weapon.WeaponType.Pistol);
    }

    private void FormSquads()
    {
        for (int i = 0; i < soldiers.Length; i += 2)
        {
            if (i + 1 < soldiers.Length)
            {
                Squad squad = new Squad(soldiers[i]);
                squad.AddMember(soldiers[i + 1]);
            }
        }
    }

    private void MoveSoldiers()
    {
        foreach (var soldier in soldiers)
        {
            Vector2 direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            soldier.Move(direction);
        }
    }

    private void UpdateSoldierStatuses()
    {
        foreach (var soldier in soldiers)
        {
            soldier.UpdateStatus(map);
        }
    }

    private void DetectEnemies()
    {
        foreach (var soldier in soldiers)
        {
            soldier.DetectEnemies(soldiers);
        }
    }

    private void UpdateEnemySightings()
    {
        foreach (var soldier in soldiers)
        {
            soldier.UpdateEnemySightings();
        }
    }

    private void MakeTacticalDecisions()
    {
        foreach (var soldier in soldiers)
        {
            if (soldier.squad != null)
            {
                soldier.squad.MakeTacticalDecision();
            }
        }
    }

    private void RescueAndHealSoldiers()
    {
        foreach (var soldier in soldiers)
        {
            if (soldier.squad != null)
            {
                soldier.squad.RescueAndHealMembers();
            }
        }
    }
}