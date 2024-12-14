using UnityEngine;

public class CollectableItem : MonoBehaviour, IInteractable
{
    public int value = 1; // Valor da moeda

    public void Interact(Player player)
    {
        Collect(player);
    }

    private void Collect(Player player)
    {
        Debug.Log($"Item coletado!");

        // Destruir a moeda após a coleta
        Destroy(gameObject);
    }
}
