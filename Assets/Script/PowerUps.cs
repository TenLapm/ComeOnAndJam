using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum powerUpType
{
    Default, UpSize, Explosion, Clone, 
}

public enum Instant
{
    Default, on , off
}

[CreateAssetMenu(fileName = "New PowerUp", menuName = "PowerUp")]


public class PowerUps : ScriptableObject
{
    public new string name;
    public powerUpType type;
    public Sprite artwork;
    public float duration;
    public float scale;
    public GameObject Clone;
    public Instant instant;
}
