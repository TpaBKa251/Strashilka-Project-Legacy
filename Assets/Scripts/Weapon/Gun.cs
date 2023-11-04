using UnityEngine;

public class Gun : MonoBehaviour
{
    Transform DefValera;
    Transform ZoomValera;
    public Weapon Properties;

    float damage;
    float range;
    float impact;
    float fireRate;
    string name;
    int id;
    public static bool isShoot;

    public float noiseLevel;
    Vector3 soundPosition;

    float nextTimeToFire = 0f;

    
    static int layerGun = 1 << 11;
    int layerWithoutGun = ~layerGun;
    public static RaycastHit shot;
    public Camera fpsCum;

    void Start()
    {
        damage = Properties.damage;
        range = Properties.range;
        impact = Properties.impact;
        fireRate = Properties.fireRate;
        name = Properties.name;
        id = Properties.id;
        noiseLevel = 100;
    }

    void Update()
    {
        if (Input.GetButtonDown("Attack") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
            isShoot = true;
            
        }
    }

    void Shoot()
    {
        if (Physics.Raycast(fpsCum.transform.position, fpsCum.transform.forward, out shot, range, layerWithoutGun))
        {
            Debug.Log(shot.transform.name);

            Enemy enemy = shot.transform.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            if (shot.rigidbody != null)
            {
                shot.rigidbody.AddForce(-shot.normal * impact);
            }
        }

        Collider[] hitcollider = Physics.OverlapSphere(transform.position, noiseLevel);
        foreach (var item in hitcollider)
        {
            if (item.CompareTag("Enemy"))
            {
                soundPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                item.SendMessage("HearSomeThing", soundPosition);
                item.SendMessage("SetSpeed", 6f);
                break;
            }

        }

        isShoot = false;
    }
}
