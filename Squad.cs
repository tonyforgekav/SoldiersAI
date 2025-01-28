using System.Collections.Generic;

public class Squad
{
    public List<Soldier> members; // ����� ������
    public Soldier leader; // ����� ������

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
        // ��������� ������� (����� ������� ������)
        Soldier newLeader = members[0];
        foreach (var member in members)
        {
            if (member.parameters.experience > newLeader.parameters.experience)
            {
                newLeader = member;
            }
        }
        leader = newLeader;

        // ��������� ��������� � ����������� �� ������������ � �����
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
        // ������ ������ ��������� (����� ���������)
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
            // ������ �������� ����������� ������� (����� ���������)
            if (leader.parameters.heroism > 0.5f)
            {
                // ���������
            }
            else if (leader.parameters.fear > 0.5f)
            {
                // ���������
            }
            else
            {
                // ������ �������
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
                            rescuer.Heal(50); // ������� �� 50 ������ ��������
                        }
                        rescuer.Rescue(member); // ��������
                    }
                }
            }
        }
    }
}