using UnityEngine;

public class SteampunkBoss : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public GameObject Player;
    public int randomNumber = 0;
    public float stallTimer = 0;
    public bool setTimer = false;
    public Vector3 TargetPosition;
    public bool setposition = false;

    public GameObject JumpHitbox;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (stallTimer <= 0)
        {
            randomNumber = Random.Range(1, 6);
            setTimer = false;
        }
        
        if (randomNumber == 1)
        {
            JumpAttack();
        }
        else if (randomNumber == 2)
        {
            RocketAttack();
        }
        else if (randomNumber == 3)
        {
            LungeAttack();
        }
        else if (randomNumber == 4)
        {
            SlashAttack();
        }
        else if (randomNumber == 5)
        {
            Block();
        }
    }

    void JumpAttack()
    {
        if (!setTimer)
        {
            stallTimer = 1;
            setTimer = true;
        }
        if (!setposition)
        {
            TargetPosition = Player.transform.position;
            setposition = true;
        }
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(TargetPosition.x, 20, TargetPosition.z), 20 * Time.deltaTime);
        if ((transform.position.x == TargetPosition.x) && (transform.position.z == TargetPosition.z))
        {
            transform.position = Vector3.MoveTowards(transform.position, TargetPosition, 80 * Time.deltaTime);
            JumpHitbox.SetActive(true);
        }
        if (transform.position == TargetPosition)
        {
            JumpHitbox.SetActive(false);
            stallTimer -= Time.deltaTime;
        }
        if (stallTimer <= 0)
        {
            setposition = false;
        }
    }

    void RocketAttack()
    {
        if (!setTimer)
        {
            stallTimer = 3;
            setTimer = true;
        }
        if (!setposition)
        {
            TargetPosition = Player.transform.position;
            setposition = true;
        }
        if (stallTimer >= 2.6 && randomNumber == 2)
        {
            transform.position = Vector3.MoveTowards(transform.position, TargetPosition, -30 * Time.deltaTime);
        }
        stallTimer -= Time.deltaTime;
        if (stallTimer <= 0)
        {
            setposition = false;
        }
    }

    void LungeAttack()
    {
        if (!setTimer)
        {
            stallTimer = 0.5f;
            setTimer = true;
        }
        if (!setposition)
        {
            TargetPosition = Player.transform.position;
            setposition = true;
        }
        transform.position = Vector3.MoveTowards(transform.position, TargetPosition, 30 * Time.deltaTime);
        if (transform.position == TargetPosition)
        {
            stallTimer -= Time.deltaTime;
        }
        if (stallTimer <= 0)
        {
            setposition = false;
        }
    }

    void SlashAttack()
    {
        if (!setTimer)
        {
            stallTimer = 1;
            setTimer = true;
        }

        stallTimer -= Time.deltaTime;
    }

    void Block()
    {
        if (!setTimer)
        {
            stallTimer = 1;
            setTimer = true;
        }

        stallTimer -= Time.deltaTime;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (randomNumber == 1)
        {
            playerMovement.HP -= 1;
        }
    }
}
