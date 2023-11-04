using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class SimpleObjects : PickUpObjects
{
    Rigidbody rb;
    [SerializeField] Transform PickPoint;
    [SerializeField] GameObject Cum;

    public float koefForce = 0;
    float kolbasitsa = 0, kolbasitsaRot = 0, zPick = 2;
    int koefKolbasitsa = -1;

    public float noiseLevel;
    public float damage;

    Vector3 soundPosition;
    float timer = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Physics.IgnoreLayerCollision(11, 11);
    }

    void Update()
    {
        if (rb.velocity == Vector3.zero)
        {
            rb.drag = 0;
            rb.angularDrag = 0;
        }
    }

    private void LateUpdate()
    {
        if (rb.isKinematic && !Input.GetKey(KeyCode.W))
        {
            PickPoint.transform.localPosition = new Vector3(0, 0, zPick);
            rb.transform.position = Vector3.Lerp(rb.transform.position, PickPoint.position, 7f * Time.deltaTime);
        }
        else if (rb.isKinematic && Input.GetKey(KeyCode.W))
        {
            PickPoint.transform.localPosition = new Vector3(0, 0, 1.2f * zPick);
            rb.transform.position = Vector3.Lerp(rb.transform.position, PickPoint.position, 7f * Time.deltaTime);
        }
    }


    public override void PickUp()
    {
        Debug.Log("Simple Object is picked up");

        rb.isKinematic = true;
    }

    public override void ThrowOut()
    {
        Debug.Log("Simple Object is threw out");

        StartCoroutine(Throw());
    }


    IEnumerator Throw()
    {
        while (Input.GetButton("Pick") && koefForce < 1000)
        {
            yield return new WaitForSeconds(.02f);
            koefForce += 20;
            zPick -= .0075f;
            kolbasitsa += .0005f;
            rb.transform.position = new Vector3(rb.transform.position.x + kolbasitsa * koefKolbasitsa, rb.transform.position.y - kolbasitsa * koefKolbasitsa, rb.transform.position.z + kolbasitsa * koefKolbasitsa);
            koefKolbasitsa *= -1;
        }

        if (koefForce == 1000)
        {
            StartCoroutine(Shaker());
            yield return new WaitUntil(() => Input.GetButtonUp("Pick"));
            rb.isKinematic = false;
            rb.AddForce(Cum.transform.forward * koefForce);
            noiseLevel = koefForce * rb.mass / 100;
            damage = koefForce * rb.mass / 500;
            koefForce = 0;
            zPick = 2;
            kolbasitsa = 0;
            yield return new WaitForSeconds(.5f);
            gameObject.layer = 6;

        }
        else
        {
            rb.isKinematic = false;
            rb.AddForce(Cum.transform.forward * koefForce);
            noiseLevel = koefForce * rb.mass / 100;
            damage = koefForce * rb.mass / 500;
            koefForce = 0;
            zPick = 2;
            kolbasitsa = 0;
            yield return new WaitForSeconds(.5f);
            gameObject.layer = 6;
        }
    }
    IEnumerator Shaker()
    {
        while (Input.GetButton("Pick"))
        {
            rb.transform.position = new Vector3(rb.transform.position.x + kolbasitsa * koefKolbasitsa, rb.transform.position.y - kolbasitsa * koefKolbasitsa, rb.transform.position.z + kolbasitsa * koefKolbasitsa);
            koefKolbasitsa *= -1;
            yield return new WaitForSeconds(.02f);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        timer = .3f;

        if (noiseLevel > 0)
        {
            Collider[] hitcollider = Physics.OverlapSphere(rb.transform.position, noiseLevel);
            foreach (var item in hitcollider)
            {
                if (item.CompareTag("Enemy") && !collision.gameObject.CompareTag("Enemy"))
                {
                    soundPosition = new Vector3(rb.transform.position.x, PickPoint.transform.position.y - .5f, rb.transform.position.z);
                    item.SendMessage("HearSomeThing", soundPosition);
                    item.SendMessage("SetSpeed", 3.5f);
                    break;
                }

            }
            if (collision.gameObject.CompareTag("Enemy"))
            {
                Enemy enemy = collision.transform.GetComponent<Enemy>();
                enemy.TakeDamage(damage);
            }
            noiseLevel = 0;
            damage = 0;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            rb.drag = 1;
            rb.angularDrag = 1;
        }
    }
}
