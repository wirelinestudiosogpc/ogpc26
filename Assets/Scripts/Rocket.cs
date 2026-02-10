 using UnityEngine;

public class Rocket : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public GameObject player;
    public Vector3 TargetPosition;
    public float playerDistance;

    public Vector3 direction;
    public Quaternion targetRotation;
    public float rotationSpeed = 30;

    public bool isStuck;
    public float despawnTimer = 10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player");
        playerMovement = FindObjectOfType<PlayerMovement>();
        TargetPosition = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerDistance >= 25)
        {
            rotationSpeed = 60;
        }
        else{
            rotationSpeed = 30;
        }
        direction = (player.transform.position - transform.position).normalized;
        targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 30 * Time.deltaTime);

        playerDistance = Vector3.Distance(transform.position, player.transform.position);
        transform.Translate(Vector3.forward * Time.deltaTime * (playerDistance/1.5f+8));
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Hit Player");
            playerMovement.HP -= 2f;
        }
        else if (other.CompareTag("Boss")){
            Debug.Log("Shooting");
        }
        else if (other.CompareTag("Ground")){
            Destroy(this.gameObject);
        }
    }
}
