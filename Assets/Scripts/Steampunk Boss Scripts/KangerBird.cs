 using UnityEngine;
using static AudioStuff;

public class KangerBird : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public GameObject player;
    public GameObject Boss;
    public Vector3 TargetPosition;
    public float playerDistance;

    public Vector3 direction;
    public Quaternion targetRotation;
    public float rotationSpeed = 30;

    public bool isStuck;
    public GameObject PocketBird;

    public float despawnTimer = 10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player");
        Boss = GameObject.Find("Boss");
        playerMovement = FindObjectOfType<PlayerMovement>();
        TargetPosition = player.transform.position;
        transform.LookAt(player.transform);

        direction = (player.transform.position - transform.position).normalized;
        targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 30 * Time.deltaTime);
        playerDistance = Vector3.Distance(transform.position, player.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        rotationSpeed = 60;
        
        

        if (playerDistance > 10 && !isStuck)
        {
            direction = (player.transform.position - transform.position).normalized;
            targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 30 * Time.deltaTime);

            playerDistance = Vector3.Distance(transform.position, player.transform.position);
            transform.Translate(Vector3.forward * Time.deltaTime * (playerDistance/1.5f+8));
        }
        else if (playerDistance <= 10 && !isStuck)
        {
            Instantiate(PocketBird, transform.position, transform.rotation);
            Instantiate(PocketBird, transform.position, transform.rotation * Quaternion.Euler(0, -30, 0));
            Instantiate(PocketBird, transform.position, transform.rotation * Quaternion.Euler(0, 30, 0));
            isStuck = true;
        }
        else if (isStuck)
        {
            transform.LookAt(Boss.transform);
            direction = (Boss.transform.position - transform.position).normalized;
            targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 30 * Time.deltaTime);
            transform.Translate(Vector3.forward * Time.deltaTime * (playerDistance/1.5f+8));
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Hit Player");
            playerMovement.HP -= 2f;
            PlaySFX(sfxPlayerHurt, playerMovement.gameObject.transform);
        }
        else if (other.CompareTag("Ground")){
            Destroy(this.gameObject);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Boss") && isStuck)
        {
            Destroy(this.gameObject);
            Debug.Log("Shooting");
        }
    }
}
