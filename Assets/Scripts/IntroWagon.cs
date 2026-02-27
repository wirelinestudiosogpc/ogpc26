using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IntroWagon : MonoBehaviour
{
    public GameObject playerKeeper;
    public GameObject player;
    public GameObject playerCam;
    public GameObject moveToPoint;
    public float speed;
    public GameObject visibleWagon;

    public GameObject npc;
    float timer1;
    float timer2;
    float timer3;

    public Image fadeInImg;
    public float fadeInTimer = 1f;

    private Quaternion previousSourceRotation;

    private float yVelocity = 0.0f;
    bool endDoDone;
    public GameObject uiElement1;
    public GameObject uiElement2;
    public GameObject uiElement3;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        previousSourceRotation = transform.rotation;

    }

    IEnumerator FadeIn()
    {
        float timer = 0f;
        Color currentColor = fadeInImg.color;

        while (timer < fadeInTimer)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, timer / fadeInTimer);
            currentColor.a = alpha;
            fadeInImg.color = currentColor;
            yield return null;
        }

        currentColor.a = 0f;
        fadeInImg.color = currentColor;
        fadeInImg.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        timer2 += Time.deltaTime;
        if (timer2 > 1.1 && timer2 < 1.2)
        {
            StartCoroutine(FadeIn());
        }
        if (timer2 > 1.7 && timer2 < 1.8)
        {
            playerCam.GetComponent<PlayerCam>().canLook = true;
        }
        if (timer2 > 2.5 && timer2 < 2.6)
        {
            npc.gameObject.GetComponent<MouthOpenClose>().interacted = true;
            npc.gameObject.GetComponent<MouthOpenClose>().prompt.SetActive(false);
            npc.gameObject.GetComponent<MouthOpenClose>().text.SetActive(true);
            npc.gameObject.GetComponent<MouthOpenClose>().text.GetComponent<TextMeshProUGUI>().text = npc.gameObject.GetComponent<MouthOpenClose>().whatToSay;
            npc.gameObject.GetComponent<MouthOpenClose>().timerDeactivate = npc.gameObject.GetComponent<MouthOpenClose>().initialTimerDeactivate;
        }

        
        if (moveToPoint == null && GameObject.FindGameObjectsWithTag("WagonPoint").Length > 0)
        {
            var allPoints = GameObject.FindGameObjectsWithTag("WagonPoint");
            var pos = transform.position;

            float dist = float.PositiveInfinity;
            GameObject nearest = null;
            foreach (var point in allPoints)
            {
                var distprev = (point.transform.position - pos).sqrMagnitude;
                if (distprev < dist)
                {
                    nearest = point;
                    dist = distprev;
                }
            }
            moveToPoint = nearest;
        }
        else if (GameObject.FindGameObjectsWithTag("WagonPoint").Length == 0)
        {
            moveToPoint = this.gameObject;
            visibleWagon.gameObject.GetComponent<itemBob>().enabled = false;
        }
        if (GameObject.FindGameObjectsWithTag("WagonPoint").Length > 0)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveToPoint.transform.position - transform.position);
            Vector3 currentEuler = transform.rotation.eulerAngles;
            float targetYAngle = targetRotation.eulerAngles.y;
            currentEuler.y = Mathf.SmoothDampAngle(currentEuler.y, targetYAngle, ref yVelocity, .7f);
            transform.rotation = Quaternion.Euler(currentEuler);

            transform.position += transform.forward * 2.25f * Time.deltaTime;
            if (Time.deltaTime > 0)
            {
                player.transform.position = Vector3.MoveTowards(player.transform.position, playerKeeper.transform.position, Mathf.Infinity * Time.deltaTime);
            }
            else{
                player.transform.position = Vector3.MoveTowards(player.transform.position, playerKeeper.transform.position, Mathf.Infinity);
            }

            player.GetComponent<PlayerMovement>().energy = 0;

            player.GetComponent<PlayerMovement>().HP = 10;
            if (npc.GetComponent<MouthOpenClose>().interacted == false && npc.GetComponent<MouthOpenClose>().whatToSay == "Hey, you. You're finally awake." && timer2 > 2.6)
            {
                timer3 += Time.deltaTime;
                if (timer3 > .8 && timer3 < .9)
                {
                    npc.GetComponent<MouthOpenClose>().whatToSay = "You had me scared for a moment there!";

                    npc.gameObject.GetComponent<MouthOpenClose>().interacted = true;
                    npc.gameObject.GetComponent<MouthOpenClose>().prompt.SetActive(false);
                    npc.gameObject.GetComponent<MouthOpenClose>().text.SetActive(true);
                    npc.gameObject.GetComponent<MouthOpenClose>().text.GetComponent<TextMeshProUGUI>().text = npc.gameObject.GetComponent<MouthOpenClose>().whatToSay;
                    npc.gameObject.GetComponent<MouthOpenClose>().timerDeactivate = npc.gameObject.GetComponent<MouthOpenClose>().initialTimerDeactivate;
                }

                
            }
        }
        else
        {
            npc.GetComponent<MouthOpenClose>().whatToSay = "Go on ahead, save or town! I'll catch up later.";
            if (timer1 < .7)
            {
                player.transform.position = Vector3.MoveTowards(player.transform.position, playerKeeper.transform.position, Mathf.Infinity * Time.deltaTime);
            }
            timer1 += Time.deltaTime;
            visibleWagon.GetComponent<BoxCollider>().enabled = true;

            if (timer1 < 1.5)
            {
                player.GetComponent<PlayerMovement>().energy = 0;
            }
        }
    }
    private void LateUpdate()
    {
        
        if (GameObject.FindGameObjectsWithTag("WagonPoint").Length > 0)
        {
            if (Vector3.Distance(npc.transform.position, player.transform.position) < 5)
            {
                npc.gameObject.GetComponent<MouthOpenClose>().prompt.SetActive(false);
            }

            uiElement1.SetActive(false);
            uiElement2.SetActive(false);
            uiElement3.SetActive(false);
        }
        if (GameObject.FindGameObjectsWithTag("WagonPoint").Length == 0 && !endDoDone && timer1 > 1.1)
        {
            uiElement1.SetActive(true);
            uiElement2.SetActive(true);
            uiElement3.SetActive(true);
            endDoDone = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "WagonPoint")
        {
            Destroy(other.gameObject);
            moveToPoint = null;
        }
    }
}
