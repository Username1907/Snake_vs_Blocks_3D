using TMPro;
using UnityEngine;
using System.Collections;

public class SnakeHeadMovement : MonoBehaviour
{
    public float Speed = 20;
    public float xSpeed;
    public float Sensitivity = 1;

    public TextMeshPro ScoreText;
    public UIController UI;
    public Game game;

    public AudioClip BounceSound;

    private Rigidbody componentRigidBody;
    private SnakeTail componentSnakeTail;
    private Vector3 _previousMousePosition;

    public int Lenght;

    void Start()
    {
        xSpeed = Speed;
        componentRigidBody = GetComponent<Rigidbody>();
        componentSnakeTail = GetComponent<SnakeTail>();

        ScoreText.SetText(Lenght.ToString());

        StartCoroutine(DestroySegments());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Cube cube)) xSpeed = 0;

        if (collision.collider.TryGetComponent(out Collectibles collectibles))
        {
            componentSnakeTail.AddSegments(collectibles.ScoreBoost);
            Lenght += collectibles.ScoreBoost;
            ScoreText.SetText(Lenght.ToString());
        }

        if (collision.collider.TryGetComponent(out Finish finish)) game.LevelComplete();
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Cube cube))
        {
            xSpeed = Speed;
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 delta = Input.mousePosition - _previousMousePosition;
            if (Mathf.Abs(delta.x * Sensitivity) < 50) componentRigidBody.velocity = new Vector3(xSpeed, 0, -delta.x * Sensitivity);
            else componentRigidBody.velocity = new Vector3(xSpeed, 0, -50 * Mathf.Sign(delta.x));
        }
        else componentRigidBody.velocity = new Vector3(xSpeed, 0, 0);
        _previousMousePosition = Input.mousePosition;
    }

    private IEnumerator DestroySegments()
    {
        if (Lenght == 1 & xSpeed == 0)
        {
            game.ScorePerLevel++;
            game.LevelLose();
        }
        else if (xSpeed == 0)
        {
            AudioSource bounceSound = GetComponent<AudioSource>();
            bounceSound.PlayOneShot(BounceSound);
            SegmentRemove();
            Collider[] overlappedColliders = Physics.OverlapBox(transform.position, new Vector3(1.1f, 1f, 0.9f));
            for (int i = 0; i < overlappedColliders.Length; i++)
            {
                if (overlappedColliders[i].TryGetComponent(out Cube cube))
                {
                    cube.LoseDurability();
                    game.ScorePerLevel++;
                    UI.ScorePerLevelTextUI.SetText(game.ScorePerLevel.ToString());
                    break;
                }
            }
        }
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(DestroySegments());
    }

    private void SegmentRemove()
    {
        componentSnakeTail.RemoveSegment();
        Lenght--;
        ScoreText.SetText(Lenght.ToString());
    }

    public void ResetSnake()
    {
        componentSnakeTail.RemoveTail();
        transform.position = new Vector3(0, 1f, 0);
        xSpeed = Speed;
        Lenght = 3;
        ScoreText.SetText(Lenght.ToString());
        componentSnakeTail.segmentPositions.RemoveAt(0);
        componentSnakeTail.segmentPositions.Add(componentSnakeTail.SnakeHead.position);
        componentSnakeTail.AddSegments(componentSnakeTail.StartSegmentsCount);
    }
}
