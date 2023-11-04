using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;
    public GameObject interfaceInventory;
    public static bool isOpen;

    public Transform ItemContent;
    public GameObject InventoryItem;
    public GameObject InventoryInformation;

    public List<Item> items = new List<Item>();

    public int ammosPistol;
    public int med;

    public void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        if (!Pause.pause) //открытие меню инвенторя
        {
            if (Input.GetButtonDown("Inventory") && !isOpen)
            {
                interfaceInventory.SetActive(true);
                isOpen = true;
                ListItems();
                Time.timeScale = 0;
            }
            else if (Input.GetButtonDown("Inventory") && isOpen)
            {
                interfaceInventory.SetActive(false);
                isOpen = false;
                Time.timeScale = 1;
            }
        }
        
    }

    public void AddItem(Item item)
    {
        items.Add(item);

    }
    public void RemoveItem(Item item)
    {
        items.Remove(item);
    }


    public void ListItems()
    {
        foreach (Transform item in ItemContent)//удаление дублированных элементов
        {
            Destroy(item.gameObject);
        }

        foreach (var item in items)// добавление новых предметов
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
            var itemText = obj.transform.Find("ItemVolume").GetComponent<Text>();

            if (item is Weapon weapon)
            {
                itemIcon.sprite = weapon.icon;
            }
            else if (item is Consumable consumable)
            {
                itemIcon.sprite = consumable.icon;
                if (consumable.name == "Ammo for Valera")
                {
                    itemText.text = ammosPistol.ToString();
                }
                else
                {
                    itemText.text = med.ToString();
                }
            }
        }
    }
}


