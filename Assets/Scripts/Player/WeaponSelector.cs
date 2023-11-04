
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelector : MonoBehaviour
{
    [SerializeField] GameObject Valera;
    [SerializeField] GameObject Finka;

    Inventory inventory;
    Weapon weapon;

    bool isChangeToValera;
    bool isChangeToFinka;
    private void Start()
    {
        inventory = Inventory.Instance;
    }
    private void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            for (int i = 0; i < inventory.items.Count; i++)
            {
                weapon = inventory.items[i] as Weapon;
                if (weapon?.id == 0 && !Valera.activeInHierarchy)
                {
                    Valera.transform.localPosition = new Vector3(.5f, -.3f, 1.5f);
                    isChangeToValera = true;
                    isChangeToFinka = false;
                    break;
                }
            }
        }
        else if (Input.GetKeyDown("2"))
        {
            for (int i = 0; i < inventory.items.Count; i++)
            {
                weapon = inventory.items[i] as Weapon;
                if (weapon?.id == 1 && !Finka.activeInHierarchy)
                {
                    Finka.transform.localPosition = new Vector3(.5f, -.3f, 1.5f);
                    isChangeToValera = false;
                    isChangeToFinka = true;
                    break;
                }
            }
        }
    }

    private void LateUpdate()
    {
        if (isChangeToValera)
        {
            Finka.GetComponent<Knife>().enabled = false;
            Finka.transform.localPosition = Vector3.Lerp(Finka.transform.localPosition, new Vector3(.5f, -.3f, 1.5f), 3 * Time.deltaTime);
            if (Mathf.Abs(Finka.transform.localPosition.x - .5f) <= .01f)
            {
                Valera.SetActive(true);
                Valera.GetComponent<Gun>().enabled = false;
                Valera.GetComponent<Scope>().enabled = false;
                Finka.SetActive(false);
                Valera.transform.localPosition = Vector3.Lerp(Valera.transform.localPosition, Vector3.zero, 3 * Time.deltaTime);

                if (Mathf.Abs(Valera.transform.localPosition.x) <= .001f)
                {
                    Valera.GetComponent<Gun>().enabled = true;
                    Valera.GetComponent<Scope>().enabled = true;
                    Valera.transform.localPosition = Vector3.zero;
                    isChangeToValera = false;
                }
            }
        }
        else if (isChangeToFinka)
        {
            Valera.GetComponent<Gun>().enabled = false;
            Valera.GetComponent<Scope>().enabled = false;
            Valera.transform.localPosition = Vector3.Lerp(Valera.transform.localPosition, new Vector3(.5f, -.3f, 1.5f), 3 * Time.deltaTime);
            if (Mathf.Abs(Valera.transform.localPosition.x - .5f) <= .01f)
            {
                Valera.SetActive(false);
                Finka.SetActive(true);
                Finka.GetComponent<Knife>().enabled = false;
                Finka.transform.localPosition = Vector3.Lerp(Finka.transform.localPosition, Vector3.zero, 3 * Time.deltaTime);

                if (Mathf.Abs(Finka.transform.localPosition.x) <= .001f)
                {
                    isChangeToFinka = false;
                    Finka.transform.localPosition = Vector3.zero;
                    Finka.GetComponent<Knife>().enabled = true;
                }
            }
        }
    }



}
