using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerPickUpAndThrow : MonoBehaviour
{
    [SerializeField] GameObject Cum;
    RaycastHit hit;
    PickUpObjects objectToThrow;

    private void Update()
    {
        if (Input.GetButtonDown("Pick") && objectToThrow == null)
        {
            _PickUp();
        }
        else if (Input.GetButtonDown("Pick") && objectToThrow != null)
        {
            _ThrowOut();
        }
    }

    void _PickUp() // локальный метод PickUp()
    {
        if (Physics.Raycast(Cum.transform.position, Cum.transform.forward, out hit, 3))
        {
            if (hit.transform.TryGetComponent<PickUpObjects>(out var objectToPick))
            {
                objectToPick.PickUp();
                objectToPick.gameObject.layer = 11;
                objectToThrow = objectToPick;
            }
        }
    }

    void _ThrowOut() // локальный метод ThrowOut()
    {
        objectToThrow.ThrowOut();
        objectToThrow = null;
    }

}
