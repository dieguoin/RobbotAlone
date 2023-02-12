using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="InGameObject")]
public class InGameObjects : ScriptableObject
{
    public new string name;
    public Sprite sprite;
    public string objectName;
    public string description;
    public enum Type { Head, RightArm, LeftArm, Body, Leg};
    public Type type;

    public int Life;
    public int Attack;
    public int Defense;
    public int Speed;
}
