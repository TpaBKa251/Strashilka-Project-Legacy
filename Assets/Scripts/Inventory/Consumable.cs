using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Consumables/Properties")]
public class Consumable: Item
{
    public int maxVolume;
    public int volume;
    public string name;
    public string description;

    public Sprite icon;

    private void OnValidate()
    {
        volume = Random.Range(1, 9);
    }
}


