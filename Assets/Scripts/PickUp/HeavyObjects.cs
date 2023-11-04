using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class HeavyObjects : PickUpObjects
{
    Rigidbody rb;
    [SerializeField] Transform PickPoint;
    [SerializeField] GameObject Cum;
    [SerializeField] PlayerStatesManager playerStatesManager;

    public float koefForce = 0;
    float kolbasitsa = 0, kolbasitsaRot = 0;
    int koefKolbasitsa = -1;


    public float noiseLevel;
    float koefNoiseLevel = 1;

    Vector3 soundPosition;
    float timer = 0;

    bool isInHands;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        Physics.IgnoreLayerCollision(11, 11);
    }

    private void Update()
    {

        if (noiseLevel > 0)
        {
            Collider[] hitcollider = Physics.OverlapSphere(rb.transform.position, noiseLevel);
            foreach (var item in hitcollider)
            {
                if (item.CompareTag("Enemy"))
                {
                    soundPosition = new Vector3(rb.transform.position.x, PickPoint.transform.position.y - .5f, rb.transform.position.z);
                    item.SendMessage("HearSomeThing", soundPosition);
                    item.SendMessage("SetSpeed", 3.5f);
                    break;
                }
            }
        }
    }

    private void LateUpdate()
    {
        if (isInHands)
        {
            rb.transform.position = Vector3.Lerp(rb.transform.position, 
                new Vector3(PickPoint.transform.position.x, rb.transform.position.y, PickPoint.transform.position.z), 
                7f * Time.deltaTime);

            rb.transform.rotation = playerStatesManager.transform.rotation;
        }
    }
    public override void PickUp()
    {
        Debug.Log("Heavy Object is picked up");

        isInHands = true;
        Cum.GetComponent<MouseLook>().mouseSensevity = 25;

        playerStatesManager.SendMessage("Drag");

    }
    public override void ThrowOut()
    {
        Debug.Log("Heavy Object is put down");

        isInHands = false;
        Cum.GetComponent<MouseLook>().mouseSensevity = 300;

        gameObject.layer = 6;

        playerStatesManager.SendMessage("Drag");
    }

    private void OnCollisionEnter(Collision collision)
    {
        koefNoiseLevel = 1.5f;
    }

    private void OnCollisionExit(Collision collision)
    {
        koefNoiseLevel = 1;
    }


}
