public class SoldierState
{
    public enum State { Alive, Wounded, SeverelyWounded, Dead }

    public State currentState; // ������� ���������
    public float timeSinceInjury; // ����� � ������� ��������� �������

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
                // ����������, ���� �� �������� ��� ������� ������� �������
                currentState = (UnityEngine.Random.value > 0.5f) ? State.Dead : State.SeverelyWounded;
            }
            else if (currentState == State.Wounded || currentState == State.SeverelyWounded)
            {
                // ���� �������� ��� ��� �����, �� �������
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