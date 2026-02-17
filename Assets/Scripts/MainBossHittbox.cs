using UnityEngine;

public class MainBossHittbox : MonoBehaviour
{
    public SteampunkBoss steampunkBoss;
    public PlayerMovement playerMovement;
    public GameObject Player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Hit Player");
            playerMovement.HP -= 1;
        }
        else if (other.gameObject.tag == "Sword" && steampunkBoss.randomNumber < 5)
        {
            steampunkBoss.HP -= 1;
            Debug.Log("Hit");
        }
        else if (other.CompareTag("Sword") && steampunkBoss.randomNumber == 5 && !steampunkBoss.parry)
        {
            Debug.Log("Parried");
            transform.LookAt(Player.transform);
            steampunkBoss.parry = true;
        }
        else{
            Debug.Log("Failed");
        }
    }
}
