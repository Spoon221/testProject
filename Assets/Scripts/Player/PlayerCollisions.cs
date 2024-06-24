using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    private Animator playerAnim;

    private void Awake()
    {
        playerAnim = GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Size")
        {
            GameEvents.instance.playerSize.Value += 5;
            other.GetComponent<Collider>().enabled = false;
            Destroy(other.gameObject);
        }
        if (other.tag == "Obstacle")
        {
            playerAnim.SetTrigger("kick");
            other.GetComponent<Block>().CheckHit();
            Destroy(other.gameObject);
        }
        if (other.tag == "Gate")
            other.GetComponent<Gate>().ExecuteOperation();
        if (other.tag == "Finish")
        {
            GameEvents.instance.gameWon.SetValueAndForceNotify(true);
        }
    }
}