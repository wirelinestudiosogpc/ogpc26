using UnityEngine;
using UnityEngine.AI;
using static AudioStuff;

public class ThrowerEnemy : MonoBehaviour
{
    public float hp;
    public float hurtAmount;
    public SkinnedMeshRenderer MeshRenderer;
    public Material def;
    public Material hur;
    public bool isInside;

    public float playerDistance;
    public GameObject GunThrow;
    public bool canThrow = false;
    public float throwTimer = 5;

    private NavMeshAgent agent;
    public Transform target;
    public Animator animator;
    private bool isDead;
    private float deadTimer;
    public float deadTimerMax;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        MeshRenderer.material = def;
        target = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {

        if (isDead)
        {
            canThrow = false;
            deadTimer += Time.deltaTime;
            if (deadTimer >= deadTimerMax)
                Destroy(gameObject);
            if (!isInside && hp != 0)
                MeshRenderer.material = def;
            return;
        }

        agent.SetDestination(target.position);
        transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
        //if (hp < 0)
        //{
        //    Destroy(gameObject);
        //}

        if (!isInside && hp != 0)
            MeshRenderer.material = def;

        if (!canThrow)
        {
            throwTimer -= Time.deltaTime;

            if (throwTimer < 0)
            {
                canThrow = true;
                throwTimer = 5;
            }
        }
        if (agent.velocity.magnitude >= 0.5f)
        {
            animator.SetBool("Walk", true);
        }
        else
        {
            animator.SetBool("Walk", false);
        }
        if (hp < 0)
        {
            animator.SetBool("Die", true);
            gameObject.tag = "EnemyDead";
            Destroy(gameObject.GetComponent<Collider>());
            PlaySFX(sfxEnemyDie, 100, transform);
            isDead = true;
        }

        //if (!isInside && hp != 0)
        //    MeshRenderer.material = def;
    }
    void FixedUpdate()
    {
        playerDistance = Vector3.Distance(transform.position, target.position);
        if (playerDistance < 25 && canThrow)
        {
            animator.SetTrigger("Throw");
            Instantiate(GunThrow, transform.position, Quaternion.identity);
            canThrow = false;
        }
    }
    private void LateUpdate()
    {
        if (hp == 0)
            hp -= 1;
        isInside = false;
    }
    public void OnTriggerEnter(Collider other)
    {
        if (isDead) return;
        if (other.gameObject.CompareTag("Sword"))
        {
            hp -= hurtAmount;
            MeshRenderer.material = hur;
            PlaySFX(sfxEnemyHurt, 100, transform);
        }
    }
    public void OnTriggerStay(Collider other)
    {
        if (isDead) return;
        if (other.gameObject.CompareTag("Sword"))
            isInside = true;
    }
    public void OnTriggerExit(Collider other)
    {
        if (isDead) return;
        if (other.gameObject.CompareTag("Sword") && hp != 0)
            MeshRenderer.material = def;
    }
}
