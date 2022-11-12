using TMPro;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public int Durability;
    public TextMeshPro DurabilityText;

    void Awake()
    {
        Color newColor = new Color((float)Durability / 50, (50 - (float)Durability) / 50, 0);
        GetComponent<Renderer>().material.color = newColor;
        DurabilityText.SetText(Durability.ToString());
    }

    public void LoseDurability()
    {
        Durability--;
        if (Durability == 0)
        {
            Collider[] overlappedColliders = Physics.OverlapBox(transform.position, new Vector3(2.1f, 2.1f, 2.1f));
            for (int i = 0; i < overlappedColliders.Length; i++)
            {
                if (overlappedColliders[i].TryGetComponent(out SnakeHeadMovement snakeHeadMovement))
                {
                    snakeHeadMovement.xSpeed = snakeHeadMovement.Speed;
                    break;
                }
            }
            Destroy(gameObject);
            return;
        }
        Color newColor = new Color((float)Durability / 50, (50 - (float)Durability) / 50, 0);
        GetComponent<Renderer>().material.color = newColor;
        DurabilityText.SetText(Durability.ToString());
    }
}
