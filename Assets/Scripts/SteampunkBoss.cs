using UnityEngine;

public class SteampunkBoss : MonoBehaviour
{
    public RectTransform HPBar;

    public PlayerMovement playerMovement;
    public GameObject Player;
    public float HP;
    public int randomNumber = 0;
    public float stallTimer = 0;
    public bool setTimer = false;
    public Vector3 TargetPosition;
    public bool setposition = false;

    public GameObject JumpHitbox;

    public GameObject LungeHitbox;
    public float lungeBackup;

    public GameObject Rocket;
    public bool rocketShot = false;

    public bool parry = false;
    public GameObject CounterAttack;

    public GameObject SlashHitbox;

    public int jumpLungePhase;

    public GameObject RocketBird;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (stallTimer <= 0 && HP > 34)
        {
            transform.LookAt(Player.transform);
            randomNumber = Random.Range(1, 6);
            setTimer = false;
        }
        else if (stallTimer <= 0)
        {
            transform.LookAt(Player.transform);
            randomNumber = Random.Range(6, 9);
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
        else if (randomNumber == 6)
        {
            JumpLunge();
        }
        else if (randomNumber == 7)
        {
            RocketRain();
        }
        else if (randomNumber == 8)
        {
            SlashParry();
        }

        HPBar.sizeDelta = new Vector2(HP*3.9f, HPBar.sizeDelta.y);
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
        else if (!rocketShot)
        {
            Instantiate(Rocket, transform.position, transform.rotation);
            rocketShot = true;
        }
        stallTimer -= Time.deltaTime;
        if (stallTimer <= 0)
        {
            setposition = false;
            rocketShot = false;
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
        if (lungeBackup > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, TargetPosition, -10 * Time.deltaTime);
            lungeBackup -= Time.deltaTime;
        }
        if (lungeBackup <= 0)
        {
            LungeHitbox.SetActive(true);
            transform.position = Vector3.MoveTowards(transform.position, TargetPosition, 30 * Time.deltaTime);
        }
        if (transform.position == TargetPosition)
        {
            LungeHitbox.SetActive(false);
            stallTimer -= Time.deltaTime;
        }
        if (stallTimer <= 0)
        {
            lungeBackup = 1;
            setposition = false;
        }
    }

    void SlashAttack()
    {
        if (!setTimer)
        {
            stallTimer = 1.5f;
            setTimer = true;
        }
        if (stallTimer < 1 && stallTimer > 0.5)
        {
            SlashHitbox.SetActive(true);
        }
        else if (stallTimer < 0.5)
        {
            SlashHitbox.SetActive(false);
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
        if (parry && stallTimer > 0.5f)
        {   
            stallTimer = 0.5f;
            CounterAttack.SetActive(true);
        }

        stallTimer -= Time.deltaTime;
        if (stallTimer <= 0)
        {
            parry = false;
            CounterAttack.SetActive(false);
        }
    }

    void JumpLunge()
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
        if (jumpLungePhase == 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, 20, transform.position.z), 20 * Time.deltaTime);
        }
        if ((transform.position.y == 20) && jumpLungePhase == 0)
        {
            jumpLungePhase = 1;
        }
        if (jumpLungePhase == 1)
        {
            transform.position = Vector3.MoveTowards(transform.position, TargetPosition, 80 * Time.deltaTime);
            JumpHitbox.SetActive(true);
            LungeHitbox.SetActive(true);
        }
        if (transform.position == TargetPosition && jumpLungePhase == 1)
        {
            jumpLungePhase = 2;
            TargetPosition = Player.transform.position;
            transform.LookAt(Player.transform);
        }
        if (jumpLungePhase == 2)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, 20, transform.position.z), 20 * Time.deltaTime);
        }
        if ((transform.position.y == 20) && jumpLungePhase == 2)
        {
            jumpLungePhase = 3;
        }
        if (jumpLungePhase == 3)
        {
            transform.position = Vector3.MoveTowards(transform.position, TargetPosition, 80 * Time.deltaTime);
            JumpHitbox.SetActive(true);
            LungeHitbox.SetActive(true);
        }
        if (transform.position == TargetPosition && jumpLungePhase == 3)
        {
            jumpLungePhase = 4;
        }
        if (jumpLungePhase == 4)
        {
            JumpHitbox.SetActive(false);
            LungeHitbox.SetActive(false);
            stallTimer -= Time.deltaTime;
        }
        if (stallTimer <= 0)
        {
            jumpLungePhase = 0;
            setposition = false;
        }
    }

    void RocketRain()
    {
        if (!setTimer)
        {
            stallTimer = 2.5f;
            setTimer = true;
        }
        if (!setposition)
        {
            TargetPosition = Player.transform.position;
            setposition = true;
        }
        if (stallTimer >= 2.2f && randomNumber == 2)
        {
            transform.position = Vector3.MoveTowards(transform.position, TargetPosition, -50 * Time.deltaTime);
        }
        else if (!rocketShot)
        {
            Instantiate(RocketBird, transform.position, transform.rotation);
            rocketShot = true;
        }
        stallTimer -= Time.deltaTime;
        if (stallTimer <= 0)
        {
            setposition = false;
            rocketShot = false;
        }
    }

    void SlashParry()
    {
        if (!setTimer)
        {
            stallTimer = 1.2f;
            setTimer = true;
        }
       if (parry && stallTimer > 0.5f)
        {   
            stallTimer = 0.5f;
            CounterAttack.SetActive(true);
        }

        if (stallTimer < 0.6f && stallTimer > 0.3f)
        {
            SlashHitbox.SetActive(true);
        }
        else if (stallTimer < 0.3f)
        {
            SlashHitbox.SetActive(false);
        }

        stallTimer -= Time.deltaTime;
        if (stallTimer <= 0)
        {
            parry = false;
            CounterAttack.SetActive(false);
        }
    }
}
