using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class UsableObjects : PickUpObjects
{
    [SerializeField] GameObject Valera;
    [SerializeField] GameObject Finka;

    [SerializeField] Weapon weapon;

    [SerializeField] Transform Parent;
    bool isPickedUp;

    private void LateUpdate()
    {
        if (isPickedUp)
        {
            if (name == "ValeraModel")
            {
                Valera.transform.localPosition = Vector3.zero;
                Finka.transform.localPosition = Vector3.Lerp(Finka.transform.localPosition, Parent.localPosition + new Vector3(.5f, -.3f, 1.5f), 3 * Time.deltaTime);
            }
            else if (name == "FinkaNKVDModel")
            {
                Finka.transform.localPosition = Vector3.zero;
                Valera.transform.localPosition = Vector3.Lerp(Valera.transform.localPosition, Parent.localPosition + new Vector3(.5f, -.3f, 1.5f), 3 * Time.deltaTime);
            }

            transform.localPosition = Vector3.Lerp(transform.localPosition, weapon.HandPos,
                9f * Time.deltaTime);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, weapon.HandRot, 9 * Time.deltaTime);

            if (Mathf.Abs(transform.localPosition.x - weapon.HandPos.x) <= .001f)
            {
                if (name == "ValeraModel")
                {
                    Valera.SetActive(true);
                    Finka.SetActive(false);
                }
                else if (name == "FinkaNKVDModel")
                {
                    Finka.SetActive(true);
                    Valera.SetActive(false);
                }
                Inventory.Instance.AddItem(weapon);
                Destroy(gameObject);
                isPickedUp = false;
            }
        }
    }

    public override void PickUp()
    {
        Debug.Log("Usable Object is picked up");

        _PickUp();
    }

    public override void ThrowOut()
    {
        Debug.Log("Usable Object is threw out");
    }

    void _PickUp()
    {
        isPickedUp = true;
        transform.SetParent(Parent);
    }
}
