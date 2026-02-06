using UnityEngine;

public class GunThrown : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject player;
    public Vector3 TargetPosition;
    public float playerDistance;

    public Vector3 currentRotation;
    public Quaternion targetRotation;

    public bool isStuck;
    public float despawnTimer = 10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player");
        TargetPosition = player.transform.position;
        transform.LookAt(player.transform.position);
        currentRotation = transform.eulerAngles;
        transform.eulerAngles = new Vector3(60, currentRotation.y, currentRotation.z);

        playerDistance = Vector3.Distance(transform.position, TargetPosition);
        if (playerDistance < 9)
        {
            playerDistance = playerDistance * 1.2f;
        }
        if (playerDistance > 17)
        {
            playerDistance = playerDistance / 1.4f;
        }
        else if (playerDistance > 13)
        {
            playerDistance = playerDistance / 1.2f;
        }
        rb.AddForce(transform.up * playerDistance, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = Vector3.MoveTowards(transform.position, TargetPosition, 10 * Time.deltaTime);
        if (Time.timeScale == 1)
        {
            targetRotation = Quaternion.Euler(-20f, currentRotation.y, currentRotation.z);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, -0.8f);
        }

        if (transform.position == TargetPosition)
        {
            isStuck = true;
        }

        if (isStuck)
        {
            despawnTimer -= Time.deltaTime;
            if (despawnTimer <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && !isStuck)
        {
            Destroy(this.gameObject);
        }
    }
}
