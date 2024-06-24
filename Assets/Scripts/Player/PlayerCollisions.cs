using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Size")
        {
            GameEvents.instance.playerSize.Value += 15;
            other.GetComponent<Collider>().enabled = false;
            Destroy(other.gameObject);
        }
        if (other.tag == "Obstacle")
        {
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