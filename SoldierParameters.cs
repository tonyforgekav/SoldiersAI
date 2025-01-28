public class SoldierParameters
{
    public float health; // Здоровье
    public float experience; // Опыт
    public float fear; // Страх
    public float heroism; // Героизм

    public SoldierParameters(float health, float experience, float fear, float heroism)
    {
        this.health = health;
        this.experience = experience;
        this.fear = fear;
        this.heroism = heroism;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health < 0) health = 0;
    }

    public void GainExperience(float amount)
    {
        experience += amount;
    }

    public void UpdateFear(float amount)
    {
        fear += amount;
        if (fear < 0) fear = 0;
        if (fear > 1) fear = 1;
    }

    public void UpdateHeroism(float amount)
    {
        heroism += amount;
        if (heroism < 0) heroism = 0;
        if (heroism > 1) heroism = 1;
    }
}