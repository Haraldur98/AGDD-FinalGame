using UnityEngine;
public class PipeSide : MonoBehaviour
{
    private MiniGameUnoPipe pipe;

    private void Start()
    {
        pipe = transform.parent.GetComponent<MiniGameUnoPipe>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        MiniGameUnoPipe otherPipe = other.transform.parent.GetComponent<MiniGameUnoPipe>();

        // Establish connection if not already connected
        if (otherPipe != null && pipe.connectionOne != otherPipe && pipe.connectionTwo != otherPipe)
        {
            if (pipe.connectionOne == null)
            {
                pipe.connectionOne = otherPipe;
            }
            else if (pipe.connectionTwo == null)
            {
                pipe.connectionTwo = otherPipe;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        MiniGameUnoPipe otherPipe = other.transform.parent.GetComponent<MiniGameUnoPipe>();

        // Sever the connection
        if (otherPipe != null)
        {
            if (pipe.connectionOne == otherPipe)
            {
                pipe.connectionOne = null;
            }
            else if (pipe.connectionTwo == otherPipe)
            {
                pipe.connectionTwo = null;
            }
        }
    }
}
