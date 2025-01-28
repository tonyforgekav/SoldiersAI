using UnityEngine;

public class EnemySighting
{
    public Vector2 position; // ������� ������������� �����
    public float timestamp; // ����� �����������
    public float expirationTime; // �����, ����� ������� ���������� ����������

    public EnemySighting(Vector2 position, float timestamp, float expirationTime)
    {
        this.position = position;
        this.timestamp = timestamp;
        this.expirationTime = expirationTime;
    }

    public bool IsExpired(float currentTime)
    {
        return currentTime - timestamp > expirationTime;
    }
}