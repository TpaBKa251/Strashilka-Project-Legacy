using Unity.VisualScripting;
using UnityEngine;

public class PickUpConsumables : MonoBehaviour
{
    [SerializeField] GameObject Cum;
    public Consumable Con;


    RaycastHit hit;
    
    void Update()
    {
        if (Input.GetButtonDown("Pick"))
        {
            PickUp();
        }
    }

    void PickUp()
    {
        if (Physics.Raycast(Cum.transform.position, Cum.transform.forward, out hit, 3))
        {
            if (hit.collider.gameObject.layer == 8)
            {
                Destroy(hit.collider.gameObject);   

                //Inventory.Instance.AddConsumables(hit.transform.gameObject.GetComponent<PickUpConsumables>().Con);
            }
        }
    }
}
