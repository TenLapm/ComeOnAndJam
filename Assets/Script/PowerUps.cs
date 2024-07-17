using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum powerUpType
{
    Default, TypeA, TypeB
}

[CreateAssetMenu(fileName = "New PowerUp", menuName = "PowerUp")]


public class PowerUps : ScriptableObject
{
    public new string name;
    public powerUpType type;
    public Sprite artwork;
    public float duration;
}
