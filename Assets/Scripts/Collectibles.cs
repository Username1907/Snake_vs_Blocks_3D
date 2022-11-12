using TMPro;
using UnityEngine;

public class Collectibles : MonoBehaviour
{
    public int ScoreBoost;
    public TextMeshPro ScoreBoostText;

    void Awake()
    {
        ScoreBoostText.SetText(ScoreBoost.ToString());
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
