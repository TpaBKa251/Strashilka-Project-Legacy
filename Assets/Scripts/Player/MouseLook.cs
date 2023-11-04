using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] GameObject Head;

    public float mouseSensevity = 101f;

    public Transform playerBody;
    public Transform Target;

    float xRotation = 0f;

    float mouseX;
    float mouseY;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        mouseX = 0;
        mouseY = 0;
    }

   
    void Update()
    {
        if (!Pause.pause && !EnemyStatesManager.isDead && !Inventory.isOpen)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            mouseX = 0;
            mouseY = 0;
        }
    }

    private void LateUpdate()
    {
        mouseX = Mathf.Lerp(mouseX, Input.GetAxis("Mouse X") * mouseSensevity * Time.deltaTime, 20 * Time.deltaTime);
        mouseY = Mathf.Lerp(mouseY, Input.GetAxis("Mouse Y") * mouseSensevity * Time.deltaTime, 20 * Time.deltaTime);
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);


        if (PlayerMovement.isSquat)
        {
            Head.transform.localPosition = Vector3.zero;
        }
        else
        {
            Head.transform.localPosition = new Vector3(0, 0.8f, 0);
        }

        transform.localPosition = Vector3.Lerp(transform.localPosition, Target.localPosition, 4 * Time.deltaTime);
    }
}
