using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpHeavyObjects : MonoBehaviour
{
    [SerializeField] GameObject Cum;

    public Transform PickPoint;
    Rigidbody rb;

    public float koefForce = 0;
    float kolbasitsa = 0, kolbasitsaRot = 0;
    int koefKolbasitsa = -1;
    bool isInHand;

    RaycastHit hit;

    public float noiseLevel;
    public float damage;

    Vector3 soundPosition;

    float timer = 0;

    public PlayerStatesManager playerStatesManager;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        if (Input.GetButtonUp("Pick") && !isInHand)
        {
            PickUp();
        }

        else if (Input.GetButtonDown("Pick") && isInHand)
        {
            isInHand = false;
        }

        if (rb.velocity == Vector3.zero)
        {
            rb.drag = 0;
            rb.angularDrag = 0;
        }
    }

    private void LateUpdate()
    {
        if (isInHand)
        {
            hit.rigidbody.transform.position = Vector3.Lerp(hit.rigidbody.transform.position, new Vector3(PickPoint.transform.position.x, hit.rigidbody.transform.position.y, PickPoint.transform.position.z), 7f * Time.deltaTime);
        }
    }

    void PickUp()
    {
        if (Physics.Raycast(Cum.transform.position, Cum.transform.forward, out hit, 3))
        {
            if (hit.collider.gameObject.layer == 6)
            {
                isInHand = true;

                playerStatesManager.SendMessage("Drag");
            }
        }
    }
}
