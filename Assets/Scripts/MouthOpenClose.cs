using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class MouthOpenClose : MonoBehaviour
{
    public bool npcAwakener;
    public GameObject awakenerHead;
    public Material openMouth;
    public Material closeMouth;
    public Material closedMouth;
    public GameObject npc;
    public GameObject player;
    public GameObject prompt;
    public GameObject text;
    public bool interacted;
    public bool entered;
    public Animator animator;
    public float timer;
    public float timerDeactivate;
    public float initialTimer;
    public float initialTimerDeactivate;
    public bool isClosed;
    public string whatToSay;
    private void Start()
    {
        player = GameObject.Find("Player");
        npc.GetComponent<SkinnedMeshRenderer>().material = closedMouth;
        isClosed = true;
        timer = initialTimer;
    }
    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 5)
        {
            if (!npcAwakener)
            {
                animator.SetBool("playerNear", true);

                Vector3 targetDirection = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z) - transform.position;
                Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, 3 * Time.deltaTime, 0);
                transform.rotation = Quaternion.LookRotation(newDirection);
            }
            else
            {
                Vector3 targetDirection = new Vector3(player.transform.position.x, awakenerHead.transform.position.y, player.transform.position.z) - awakenerHead.transform.position;
                Vector3 newDirection = Vector3.RotateTowards(awakenerHead.transform.forward, targetDirection, 3 * Time.deltaTime, 0);
                awakenerHead.transform.rotation = Quaternion.LookRotation(newDirection);
            }

            
        }
        else animator.SetBool("playerNear", false);

        if (Vector3.Distance(transform.position, player.transform.position) < 3)
        {
            if (!interacted)
            {
                entered = true;
                prompt.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    //interacted = true;
                    //prompt.SetActive(false);
                    //text.SetActive(true);
                    //text.GetComponent<TextMeshProUGUI>().text = whatToSay;
                    //timerDeactivate = initialTimerDeactivate;
                }
            }
            else
            {
                
            }
            
        }
        else
        {

            if (!text.activeSelf)
            interacted = false;
            if (entered)
            {
                entered = false;
                prompt.SetActive(false);
            }

            
            //text.SetActive(false);
        }

        if (text.activeSelf && interacted)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = initialTimer;
                if (isClosed)
                {
                    npc.GetComponent<SkinnedMeshRenderer>().material = openMouth;
                    isClosed = false;
                }
                else
                {
                    npc.GetComponent<SkinnedMeshRenderer>().material = closeMouth;
                    isClosed = true;
                }
            }
            timerDeactivate -= Time.deltaTime;
            if (timerDeactivate <= 0)
            {
                text.SetActive(false);
                interacted = false;
            }
            
        }
        else
        {
            npc.GetComponent<SkinnedMeshRenderer>().material = closedMouth;
            isClosed = true;
        }
        if (text.GetComponent<TextMeshProUGUI>().text != whatToSay) interacted = false;

        if (Input.GetKeyDown(KeyCode.C))
        {
            //text.SetActive(false);
            //interacted = false;
        }
    }
    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed && !interacted && Vector3.Distance(transform.position, player.transform.position) < 3 && context.interaction is TapInteraction)
        {
            interacted = true;
            prompt.SetActive(false);
            text.SetActive(true);
            text.GetComponent<TextMeshProUGUI>().text = whatToSay;
            timerDeactivate = initialTimerDeactivate;
        }
        if (context.performed && context.interaction is HoldInteraction)
        {
            text.SetActive(false);
            interacted = false;
        }
        
    }

}
