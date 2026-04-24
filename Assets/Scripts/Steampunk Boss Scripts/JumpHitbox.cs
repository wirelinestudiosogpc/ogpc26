using UnityEngine;
using static AudioStuff;

public class JumpHitbox : MonoBehaviour
{
    public PlayerMovement playerMovement;
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
            PlaySFX(sfxPlayerHurt, 100, playerMovement.gameObject.transform);
        }
        else{
            Debug.Log("Failed");
        }
    }
}
