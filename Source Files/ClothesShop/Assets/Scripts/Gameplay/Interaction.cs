using UnityEngine;
using UnityEngine.Events;

public class Interaction : MonoBehaviour
{

    static public UnityEvent<bool, string> isInteractable = new UnityEvent<bool, string>();
    internal UnityEvent interacted = new UnityEvent();

    [SerializeField] private string interactionPrompt;

    private bool interactable = false;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && interactable)
        {
            interacted.Invoke();

            isInteractable.Invoke(false, interactionPrompt);
            interactable = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isInteractable.Invoke(true, interactionPrompt);
        interactable = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isInteractable.Invoke(false, interactionPrompt);
        interactable = false;
    }
}
