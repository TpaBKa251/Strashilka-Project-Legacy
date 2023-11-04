using Unity.VisualScripting;
using UnityEngine;

public class UseAndPickUpU : MonoBehaviour
{
    [SerializeField] GameObject Cum;
    [SerializeField] GameObject Valera;
    [SerializeField] GameObject Finka;
    public Weapon WeaponData;

    RaycastHit hit;

    private void Update()
    {
        if (Input.GetButtonDown("Pick"))
        {
            PickUp();
        }
    }

    private void PickUp()
    {
        if (Physics.Raycast(Cum.transform.position, Cum.transform.forward, out hit, 3))
        {
            if (hit.collider.gameObject.layer == 7)
            {
                if (hit.collider.name == "ValeraModel")
                {
                    Destroy(hit.collider.gameObject);
                    Valera.SetActive(true);
                    Finka.SetActive(false);


                }
                else if (hit.collider.name == "FinkaNKVDModel")
                {
                    Destroy(hit.collider.gameObject);
                    Finka.SetActive(true);
                    Valera.SetActive(false);

                }

                //Inventory.Instance.AddWeapon(hit.collider.GetComponent<UseAndPickUpU>().WeaponData);
            }
            
        } 
    }

   
}
