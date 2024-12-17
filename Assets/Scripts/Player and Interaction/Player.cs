using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float interactionRadius = 1f; // Raio de interação

    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator animator;
    private CircleCollider2D interactionCollider;
    private IInteractable currentInteractable;
    private List<IInteractable> interactablesInRange = new List<IInteractable>();

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // Configurar o collider de interação
        interactionCollider = gameObject.AddComponent<CircleCollider2D>();
        interactionCollider.isTrigger = true;
        interactionCollider.radius = interactionRadius;
    }

    private void Update()
    {
        // Captura input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Atualiza o Animator
        UpdateAnimator();

        // Verifica interação
        if (Input.GetKeyDown(KeyCode.E) && currentInteractable != null)
        {
            currentInteractable.Interact(this);
        }

        // Atualiza o interactable mais próximo
        UpdateClosestInteractable();
    }

    private void FixedUpdate()
    {
        // Move o player
        rb.linearVelocity = movement.normalized * moveSpeed;
    }

    private void UpdateAnimator()
    {
        if (movement != Vector2.zero)
        {
            animator.SetBool("IsWalking", true);
            animator.SetFloat("MoveX", movement.x);
            animator.SetFloat("MoveY", movement.y);
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }
    }

    private void UpdateClosestInteractable()
    {
        currentInteractable = null;
        float closestDistance = float.MaxValue;

        foreach (IInteractable interactable in interactablesInRange)
        {
            if (interactable == null) continue;

            float distance = Vector2.Distance(transform.position, ((MonoBehaviour)interactable).transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                currentInteractable = interactable;
            }
        }

        if (currentInteractable != null)
        {
            Debug.Log("Objeto interativo próximo!");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        IInteractable interactable = other.GetComponent<IInteractable>();
        if (interactable != null && !interactablesInRange.Contains(interactable))
        {
            interactablesInRange.Add(interactable);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        IInteractable interactable = other.GetComponent<IInteractable>();
        if (interactable != null)
        {
            interactablesInRange.Remove(interactable);
        }
    }
}
