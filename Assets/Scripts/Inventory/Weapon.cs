using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon/WepProperties")]
public class Weapon : Item
{
    public Vector3 HandPos;
    public Quaternion HandRot;

    public float damage;
    public float range;
    public float impact;
    public float fireRate;
    public int id;
    public string name;
    public string description;

    public Sprite icon;
}
