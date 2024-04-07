using UnityEngine;

public class FloatToPosition : MonoBehaviour
{
    private Animation animator;

    void Start()
    {
        // Get the Animator component attached to this GameObject
        animator = GetComponent<Animation>();
    }

    public void InitializeMovement()
    {
        // Trigger the animation by playing the animation clip's name
        animator.Play("FixPipe");
    }

    // You might want to detect when the animation is done to destroy the object or trigger other events
}
