using UnityEngine;
using UnityEngine.UI;

public class PlayerCam : MonoBehaviour
{
    float sens;
    public Transform orientation;
    public Transform player;
    float xRotation;
    float yRotation;
    public Vector2 lookInput;
    public Transform wagonRot;
    private Quaternion previousSourceRotation;

    public bool canLook;

    public Slider Sensitivity;
    private void Awake()
    {
        //if (GameObject.FindGameObjectsWithTag("Sens").Length != 0)
        //    sens = GameObject.FindGameObjectWithTag("Sens").GetComponent<SensitivityValue>().cameraSens;
        //else
        //    sens = 1.75558f;
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        previousSourceRotation = wagonRot.rotation;

        yRotation += 30;
        xRotation += 20;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 1)
        {
            if (wagonRot  != null && GameObject.FindGameObjectsWithTag("WagonPoint").Length > 0)
            {
                Quaternion rotationDelta = wagonRot.rotation * Quaternion.Inverse(previousSourceRotation);
                transform.rotation *= rotationDelta;
                previousSourceRotation = wagonRot.rotation;
                Vector3 deltaEuler = rotationDelta.eulerAngles;
                yRotation += deltaEuler.y;
            }
            
            
            sens = Sensitivity.value;
            if (canLook)
            {
                yRotation += lookInput.x * sens * 50 * Time.deltaTime;
                xRotation -= lookInput.y * sens * 50 * Time.deltaTime;
            }
            

            
            xRotation = Mathf.Clamp(xRotation, -87f, 87f);

            

            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            orientation.rotation = Quaternion.Euler(0, yRotation, 0);
            player.rotation = Quaternion.Euler(0, yRotation, 0);
        }
        
    }
}
