using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class CompassPointer : MonoBehaviour
{
    public Transform playerTransform;

    public bool rotateCompass;
    public bool realisticCompass;
    public bool opposite;

    public Toggle compass;

    public bool brainController;
    public bool wasRotating;
    public GameObject cinBrain;

    void Start()
    {
        
    }


    void Update()
    {
        rotateCompass = compass.isOn;
        if (rotateCompass)
        {
            if (realisticCompass)
            {
                Vector3 northDirection = Vector3.forward;
                Vector3 compassForward = playerTransform.forward;
                compassForward.y = 0;
                float angle = Vector3.SignedAngle(compassForward, northDirection, Vector3.up);
                transform.localRotation = Quaternion.Euler(0, angle, 0);
            }
            else
            {
                Vector3 northDirection = Vector3.forward;
                Vector3 compassForward = playerTransform.forward;
                compassForward.y = 0;
                float angle = Vector3.SignedAngle(northDirection, compassForward, Vector3.up);
                transform.localRotation = Quaternion.Euler(0, angle, 0);
            }
            if (opposite)
            {
                if (!realisticCompass)
                {
                    Vector3 northDirection = Vector3.forward;
                    Vector3 compassForward = playerTransform.forward;
                    compassForward.y = 0;
                    float angle = Vector3.SignedAngle(compassForward, northDirection, Vector3.up);
                    transform.localRotation = Quaternion.Euler(0, angle, 0);
                }
                else
                {
                    Vector3 northDirection = Vector3.forward;
                    Vector3 compassForward = playerTransform.forward;
                    compassForward.y = 0;
                    float angle = Vector3.SignedAngle(northDirection, compassForward, Vector3.up);
                    transform.localRotation = Quaternion.Euler(0, angle, 0);
                }
            }
        }
        else if (!rotateCompass)
        {
            Vector3 northDirection = Vector3.forward;
            Vector3 compassForward = playerTransform.forward;
            compassForward.y = 0;
            float angle = Vector3.SignedAngle(northDirection, northDirection, Vector3.up);
            transform.localRotation = Quaternion.Euler(0, angle, 0);
        }
        if (brainController)
        {
            if (rotateCompass)
            {
                if (wasRotating)
                {
                    //cinBrain.SetActive(false);
                    //cinBrain.transform.rotation = Quaternion.Euler(90, 0, 0);
                    cinBrain.GetComponent<CinemachineRotateWithFollowTarget>().enabled = rotateCompass;
                    //cinBrain.SetActive(true);
                    wasRotating = false;
                }
            }
            else if (!rotateCompass)
            {
                if (!wasRotating)
                {
                    cinBrain.SetActive(false);
                    cinBrain.transform.rotation = Quaternion.Euler(90, 0, 0);
                    cinBrain.GetComponent<CinemachineRotateWithFollowTarget>().enabled = rotateCompass;
                    cinBrain.SetActive(true);
                    wasRotating = true;
                }
            }
        }
            
           
    }
}
