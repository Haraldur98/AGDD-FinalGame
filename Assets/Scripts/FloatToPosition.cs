using UnityEngine;

public class FloatToPosition : MonoBehaviour
{
    private Animation animator;
    private bool isMoving = false;
    void Start()
    {
        // Get the Animator component attached to this GameObject
        animator = GetComponent<Animation>();
    }

    public void InitializeMovement()
    {
        // Trigger the animation by playing the animation clip's name
        isMoving = true;
        animator.Play("AddPipe");
    }

    // You might want to detect when the animation is done to destroy the object or trigger other events
    void Update()
    {
        // Check if the animation is done
        if (!animator.isPlaying && isMoving)
        {
            isMoving = false;
            FindObjectOfType<MiniGame2Manager>().ReappearBoundary();
        }
    }
}
