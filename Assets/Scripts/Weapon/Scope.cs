using UnityEngine;


public class Scope : MonoBehaviour
{

    public Transform Default;
    public Transform Zoom;
    public static bool isScoped;

    void Update()
    {
        if (Input.GetButtonDown("Scope") && transform.parent == Default)
        {
            transform.position = Zoom.transform.position;
            transform.SetParent(Zoom);
            isScoped = true;
        }
        if (Input.GetButtonUp("Scope") && transform.parent == Zoom)
        {
            transform.position = Default.transform.position;
            transform.SetParent(Default);
            isScoped = false;
        }
    }
}
