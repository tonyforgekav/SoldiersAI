using UnityEngine;

public class EnemySighting
{
    public Vector2 position; // Позиция обнаруженного врага
    public float timestamp; // Время обнаружения
    public float expirationTime; // Время, через которое информация устаревает

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