using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Module
{
    // Start is called before the first frame update

    public new string name;
    public Sprite sprite;
    public string objectName;
    public string description;
    public enum Type { Head, RightArm, LeftArm, Body, Leg, Module };
    public Type type;
    public enum ArmType { Sword, Gun }
    public ArmType weapon;
    public GameObject bulletPrefab;

    public int Life;
    public int Attack;
    public int Defense;
    public int Speed;

    public int lvl;

    public Module(string name, int type, Sprite sprite)
    {
        this.name = name;
        this.lvl = type;
        this.sprite = sprite;
        this.type = Type.Module;
    }
}
