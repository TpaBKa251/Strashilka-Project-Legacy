using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumablesObjects : PickUpObjects
{
    [SerializeField] Consumable Consumable;
    [SerializeField] Transform PickPoint;

    bool isPickedUp;

    private void Start()
    {

    }

    public override void PickUp()
    {
        Debug.Log("Consumable Object is picked up");

        _PickUp();
    }

    private void LateUpdate()
    {
        if (isPickedUp)
        {
            transform.position = Vector3.Lerp(transform.position, PickPoint.position - new Vector3(0, .4f, 0),
                9f * Time.deltaTime);
            if (Mathf.Abs(transform.position.x - PickPoint.position.x) <= .01f)
            {
                Destroy(gameObject);
                isPickedUp = false;
            }

        }
    }

    public override void ThrowOut()
    {
        
    }

    void _PickUp()
    {
        isPickedUp = true;

        if (!Inventory.Instance.items.Contains(Consumable))
        {
            Inventory.Instance.AddItem(Consumable);
        }

        if (Consumable.name == "Ammo for Valera")
        {
            Inventory.Instance.ammosPistol += Random.Range(1, 9);
            
        }
        else
        {
            Inventory.Instance.med += Random.Range(1, 3);
        }
    }
}
