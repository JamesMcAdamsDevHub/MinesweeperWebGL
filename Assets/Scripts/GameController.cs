using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private BoardController board;

    void Start()
    {
        board.Setup();
    }
}
