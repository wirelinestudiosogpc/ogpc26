using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR;
using static AudioStuff;

public class InfectedEnemy : MonoBehaviour
{
    public float hp;
    public float hurtAmount;
    public SkinnedMeshRenderer MeshRenderer;
    public Material def;
    public Material hur;
    public bool isInside;

    private NavMeshAgent agent;
    public Transform target;
    public Transform trueTarget;

    public Animator animator;


    public bool isInRange;
    public bool isTouchingPlayer;
    private bool isDead;
    private float deadTimer;
    public float deadTimerMax;

    public Collider hand1, hand2, head;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //MeshRenderer = GetComponent<MeshRenderer>();
        agent = GetComponent<NavMeshAgent>();
        MeshRenderer.material = def;
        trueTarget = target;
        target = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {

        if (isDead)
        {
            deadTimer += Time.deltaTime;
            if (deadTimer >= deadTimerMax)
                Destroy(gameObject);
            return;
        }

        if (agent.remainingDistance < 1.5f)
        {
            Debug.Log("near");
            animator.SetBool("Walk", false);
            animator.SetBool("Attack", true);
            agent.isStopped = true;
        }
        else if (agent.remainingDistance < 20)
        {
            Debug.Log("mid");
            animator.SetBool("Walk", true);
            animator.SetBool("Attack", false);
            agent.isStopped = false;
        }
        else
        {
            Debug.Log("far");
            animator.SetBool("Walk", false);
            animator.SetBool("Attack", false);
            agent.isStopped = true;
        }
        agent.SetDestination(target.position);
        transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));

        



        if (hp < 0)
        {
            animator.SetBool("Die", true);
            gameObject.tag = "EnemyDead";
            Destroy(gameObject.GetComponent<Collider>());
            Destroy(hand1);
            Destroy(hand2);
            Destroy(head);
            PlaySFX(sfxEnemyDie, transform);
            isDead = true;
        }

        if (!isInside && hp != 0)
            MeshRenderer.material = def;

        
        
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
            PlaySFX(sfxEnemyHurt, transform);
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
