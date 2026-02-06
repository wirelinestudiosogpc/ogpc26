using System.Threading;
using UnityEngine;

public class SwordSlash : MonoBehaviour
{
    public float timer1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer1 -= Time.deltaTime;
        if (timer1 <= 0)
        {
            Destroy(gameObject);
        }
        transform.Rotate(Vector3.right * 850 * Time.deltaTime, Space.Self);
    }

    //public void OnTriggerEnter(Collider other)
    //{
    //    Debug.Log("hit");
    //}
}
