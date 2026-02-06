using UnityEngine;
using UnityEngine.AI;

public class InfectedEnemy : MonoBehaviour
{
    public float hp;
    public float hurtAmount;
    public MeshRenderer MeshRenderer;
    public Material def;
    public Material hur;
    public bool isInside;

    private NavMeshAgent agent;
    public Transform target;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MeshRenderer = GetComponent<MeshRenderer>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target.position);
        if (hp < 0)
        {
            Destroy(gameObject);
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
        if (other.gameObject.tag == "Sword")
        {
            hp -= hurtAmount;
            MeshRenderer.material = hur;
        }
    }
    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Sword")
            isInside = true;
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Sword" && hp != 0)
            MeshRenderer.material = def;
    }
}
