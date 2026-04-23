 using UnityEngine;
using static AudioStuff;

public class PocketBird : MonoBehaviour
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
        rotationSpeed = 30;

        direction = (TargetPosition - transform.position).normalized;
        targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 30 * Time.deltaTime);

        
        transform.Translate(Vector3.forward * Time.deltaTime * (1.5f+8));

        if (transform.position == TargetPosition)
        {
            GameObject.Destroy(this.gameObject);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Bird Hit Player");
            playerMovement.HP -= 2f;
            PlaySFX(sfxPlayerHurt, playerMovement.gameObject.transform);
            GameObject.Destroy(this.gameObject);
        }
        else if (other.CompareTag("Boss")){
            Debug.Log("Shooting");
        }
        else if (other.CompareTag("Ground")){
            Destroy(this.gameObject);
        }
    }
}
