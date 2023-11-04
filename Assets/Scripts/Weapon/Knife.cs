using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    public Transform DefKnife;
    public Transform CutKnife;

    public float damage = 5f;
    public float range = 1.8f;
    public float impact = 300f;
    public float fireRate = 1f;

    float nextTimeToCut = 0f;


    static int layerGun = 1 << 9;
    int layerWithoutGun = ~layerGun;
    public static RaycastHit cut;
    public Camera fpsCum;


    void Update()
    {
        if (Input.GetButtonDown("Attack") && (transform.parent == DefKnife || transform.parent == CutKnife) && Time.time >= nextTimeToCut)
        {
            nextTimeToCut = Time.time + 1f / fireRate;
            StartCoroutine(CutAnim());
            Cut();
            Collider[] hitcollider = Physics.OverlapSphere(transform.position, 10);
            foreach (var item in hitcollider)
            {
                if (item.CompareTag("Enemy"))
                {
                    item.SendMessage("HearSomeThing", transform.position);
                    item.SendMessage("SetSpeed", 6);
                    break;
                }

            }
        }
    }

    void Cut()
    {
        if (Physics.Raycast(fpsCum.transform.position, fpsCum.transform.forward, out cut, range, layerWithoutGun))
        {
            Debug.Log(cut.transform.name);

            Enemy enemy = cut.transform.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            if (cut.rigidbody != null)
            {
                cut.rigidbody.AddForce(-cut.normal * impact);
            }
        }
    }


    IEnumerator CutAnim()
    {
        transform.SetPositionAndRotation(CutKnife.transform.position, CutKnife.transform.rotation);
        transform.SetParent(CutKnife);
        yield return new WaitForSeconds(.5f);
        transform.SetPositionAndRotation(DefKnife.transform.position, DefKnife.transform.rotation);
        transform.SetParent(DefKnife);

    }
}
