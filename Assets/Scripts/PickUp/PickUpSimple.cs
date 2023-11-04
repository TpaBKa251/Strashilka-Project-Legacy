using System.Collections;
using System.Transactions;
using Unity.VisualScripting;
using UnityEngine;

public class PickUpSimple : MonoBehaviour
{
    // нужен класс для перетаскиваемых по полу объектов

    [SerializeField] GameObject Cum;

    public Transform PickPoint;
    Rigidbody rb;

    public float koefForce = 0;
    float kolbasitsa = 0, kolbasitsaRot = 0, zPick = 2;
    int koefKolbasitsa = -1;
    bool isInHand;

    RaycastHit hit;

    public float noiseLevel;
    public float damage;

    Vector3 soundPosition;

    float timer = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        Physics.IgnoreLayerCollision(6, 11);
    }

    void Update()
    {
        if (Input.GetButtonDown("Pick") && !isInHand)
        {
            PickUp();
        }

        else if (Input.GetButtonDown("Pick") && isInHand)
        {
            StartCoroutine(Throw());
        }    
        
        if (rb.velocity == Vector3.zero)
        {
            rb.drag = 0;
            rb.angularDrag = 0;
        }
    }

    
    void LateUpdate()
    {
        if (rb.isKinematic && !Input.GetKey(KeyCode.W) && rb.mass < 10)
        {
            PickPoint.transform.localPosition = new Vector3(0, 0, zPick);
            rb.transform.position = Vector3.Lerp(rb.transform.position, PickPoint.position, 7f * Time.deltaTime);
        }
        else if (rb.isKinematic && Input.GetKey(KeyCode.W) && rb.mass < 10)
        {
            PickPoint.transform.localPosition = new Vector3(0, 0, 1.2f * zPick);
            rb.transform.position = Vector3.Lerp(rb.transform.position, PickPoint.position, 7f * Time.deltaTime);
        }
        //else if (isInHand && hit.rigidbody.mass >= 10)
        //{
        //    hit.rigidbody.transform.position = Vector3.Lerp(hit.rigidbody.transform.position, new Vector3(PickPoint.transform.position.x, hit.rigidbody.transform.position.y, PickPoint.transform.position.z), 1f * Time.deltaTime);
        //}

        
    }

    void PickUp()
    {
        if (Physics.Raycast(Cum.transform.position, Cum.transform.forward, out hit, 3))
        {
            if (hit.collider.gameObject.layer == 6)
            {
                hit.rigidbody.isKinematic = true;

                isInHand = true;
            }
        }
    }


    IEnumerator Throw()
    {
        while (Input.GetButton("Pick") && koefForce < 500)
        {
            yield return new WaitForSeconds(.02f);
            koefForce += 10;
            zPick -= .0075f;
            kolbasitsa += .0005f;
            hit.rigidbody.transform.position = new Vector3(hit.rigidbody.transform.position.x + kolbasitsa * koefKolbasitsa, hit.rigidbody.transform.position.y - kolbasitsa * koefKolbasitsa, hit.rigidbody.transform.position.z + kolbasitsa * koefKolbasitsa);
            koefKolbasitsa *= -1;
        }

        if (koefForce == 500)
        {
            StartCoroutine(Shaker());
            yield return new WaitUntil(() => Input.GetButtonUp("Pick"));
            hit.rigidbody.isKinematic = false;
            isInHand = false;
            hit.rigidbody.AddForce(Cum.transform.forward * koefForce);
            noiseLevel = koefForce * hit.rigidbody.mass / 100;
            damage = koefForce * hit.rigidbody.mass / 500;
            koefForce = 0;
            zPick = 2;
            kolbasitsa = 0;
            yield return new WaitForSeconds(.5f);

        }
        else 
        {
            isInHand = false;
            hit.rigidbody.isKinematic = false;
            hit.rigidbody.AddForce(Cum.transform.forward * koefForce);
            noiseLevel = koefForce * hit.rigidbody.mass / 100;
            damage = koefForce * hit.rigidbody.mass / 500;
            koefForce = 0;
            zPick = 2;
            kolbasitsa = 0;
            yield return new WaitForSeconds(.5f);
        }
    }
    IEnumerator Shaker()
    {
        while (Input.GetButton("Pick"))
        {
            hit.rigidbody.transform.position = new Vector3(hit.rigidbody.transform.position.x + kolbasitsa * koefKolbasitsa, hit.rigidbody.transform.position.y - kolbasitsa * koefKolbasitsa, hit.rigidbody.transform.position.z + kolbasitsa * koefKolbasitsa);
            koefKolbasitsa *= -1;
            yield return new WaitForSeconds(.02f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        timer = .3f;

        if (noiseLevel > 0)
        {
            Collider[] hitcollider = Physics.OverlapSphere(hit.rigidbody.transform.position, noiseLevel);
            foreach (var item in hitcollider)
            {
                if (item.CompareTag("Enemy") && !collision.gameObject.CompareTag("Enemy"))
                {
                    soundPosition = new Vector3(hit.rigidbody.transform.position.x, PickPoint.transform.position.y - .5f, hit.rigidbody.transform.position.z);
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
