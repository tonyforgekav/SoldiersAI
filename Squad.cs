using System.Collections.Generic;

public class Squad
{
    public List<Soldier> members; // Члены отряда
    public Soldier leader; // Лидер отряда

    public Squad(Soldier leader)
    {
        this.leader = leader;
        members = new List<Soldier> { leader };
        AssignRoles();
    }

    public void AddMember(Soldier soldier)
    {
        if (!members.Contains(soldier))
        {
            members.Add(soldier);
            AssignRoles();
        }
    }

    public void RemoveMember(Soldier soldier)
    {
        if (members.Contains(soldier))
        {
            members.Remove(soldier);
            AssignRoles();
        }
    }

    private void AssignRoles()
    {
        // Назначаем офицера (самый опытный солдат)
        Soldier newLeader = members[0];
        foreach (var member in members)
        {
            if (member.parameters.experience > newLeader.parameters.experience)
            {
                newLeader = member;
            }
        }
        leader = newLeader;

        // Назначаем профессии в зависимости от предпочтений и опыта
        foreach (var member in members)
        {
            if (member.profession == null)
            {
                member.profession = GetBestProfessionForSoldier(member);
            }
        }
    }

    private Profession GetBestProfessionForSoldier(Soldier soldier)
    {
        // Логика выбора профессии (можно расширить)
        if (soldier.weapon.type == Weapon.WeaponType.SniperRifle)
        {
            return new Profession(Profession.ProfessionType.Sniper, 0.9f, Weapon.WeaponType.SniperRifle);
        }
        else if (soldier.weapon.type == Weapon.WeaponType.GrenadeLauncher)
        {
            return new Profession(Profession.ProfessionType.Grenadier, 0.8f, Weapon.WeaponType.GrenadeLauncher);
        }
        else if (soldier.weapon.type == Weapon.WeaponType.MachineGun)
        {
            return new Profession(Profession.ProfessionType.MachineGunner, 0.7f, Weapon.WeaponType.MachineGun);
        }
        else
        {
            return new Profession(Profession.ProfessionType.Scout, 0.6f, Weapon.WeaponType.Pistol);
        }
    }

    public void MakeTacticalDecision()
    {
        if (leader != null && leader.profession.type == Profession.ProfessionType.Officer)
        {
            // Логика принятия тактических решений (можно расширить)
            if (leader.parameters.heroism > 0.5f)
            {
                // Атаковать
            }
            else if (leader.parameters.fear > 0.5f)
            {
                // Отступить
            }
            else
            {
                // Занять оборону
            }
        }
    }

    public void RescueAndHealMembers()
    {
        foreach (var member in members)
        {
            if (member.state.currentState == SoldierState.State.Wounded || member.state.currentState == SoldierState.State.SeverelyWounded || member.state.currentState == SoldierState.State.Dead)
            {
                foreach (var rescuer in members)
                {
                    if (rescuer.state.currentState == SoldierState.State.Alive && rescuer.profession.type == Profession.ProfessionType.Medic)
                    {
                        if (member.state.currentState == SoldierState.State.Wounded || member.state.currentState == SoldierState.State.SeverelyWounded)
                        {
                            rescuer.Heal(50); // Лечение на 50 единиц здоровья
                        }
                        rescuer.Rescue(member); // Спасение
                    }
                }
            }
        }
    }
}