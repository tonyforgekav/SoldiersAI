public class SoldierState
{
    public enum State { Alive, Wounded, SeverelyWounded, Dead }

    public State currentState; // Текущее состояние
    public float timeSinceInjury; // Время с момента получения ранения

    public SoldierState()
    {
        currentState = State.Alive;
        timeSinceInjury = 0;
    }

    public void UpdateState(float health)
    {
        if (health <= 0)
        {
            if (currentState == State.Alive)
            {
                // Определяем, умер ли солдатик или получил тяжелое ранение
                currentState = (UnityEngine.Random.value > 0.5f) ? State.Dead : State.SeverelyWounded;
            }
            else if (currentState == State.Wounded || currentState == State.SeverelyWounded)
            {
                // Если солдатик уже был ранен, он умирает
                currentState = State.Dead;
            }
        }
        else if (health < 50)
        {
            currentState = State.Wounded;
        }
        else
        {
            currentState = State.Alive;
        }
    }
}