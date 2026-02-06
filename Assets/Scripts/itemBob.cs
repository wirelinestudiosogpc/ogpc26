using UnityEngine;

public class itemBob : MonoBehaviour
{
    

    public float xFrequency;
    public float xAmplitude;
    public float yFrequency;
    public float yAmplitude;
    public float zFrequency;
    public float zAmplitude;

    public GameObject player;
    public float vel;
    public float vel2;
    public bool swapped;
    public bool wagon;
    private Vector3 initialLocalPosition;

    void Start()
    {
        initialLocalPosition = transform.localPosition;
    }

    void Update()
    {

        //Debug.Log(player.GetComponent<Rigidbody>().linearVelocity.magnitude);
        vel = Mathf.Clamp(player.GetComponent<Rigidbody>().linearVelocity.magnitude, 0, 10);

        if (wagon)
        {
            float offsetX = Mathf.Sin(Time.time * xFrequency) * xAmplitude * vel2;
            float offsetY = Mathf.Sin(Time.time * yFrequency) * yAmplitude * vel2;
            float offsetZ = Mathf.Sin(Time.time * zFrequency) * zAmplitude * vel2;
            transform.localPosition = initialLocalPosition + new Vector3(offsetX, offsetY, offsetZ);
        }
        else if (swapped)
        {
            float offsetX = Mathf.Sin(Time.time * xFrequency*-1) * xAmplitude * vel;
            float offsetY = Mathf.Sin(Time.time * yFrequency*-1) * yAmplitude * vel;
            float offsetZ = Mathf.Sin(Time.time * zFrequency*-1) * zAmplitude * vel;
            transform.localPosition = initialLocalPosition + new Vector3(offsetX, offsetY, offsetZ);
        }
        else
        {
            float offsetX = Mathf.Sin(Time.time * xFrequency) * xAmplitude * vel;
            float offsetY = Mathf.Sin(Time.time * yFrequency) * yAmplitude * vel;
            float offsetZ = Mathf.Sin(Time.time * zFrequency) * zAmplitude * vel;
            transform.localPosition = initialLocalPosition + new Vector3(offsetX, offsetY, offsetZ);
        }
    
        
    }
}
