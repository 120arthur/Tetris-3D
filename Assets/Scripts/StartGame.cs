using UnityEngine;
using Context;

public class StartGame : MonoBehaviour
{
    private void Start()
    {
        var context = new Context.Context();
        ContextProvider.Context.GameManager.gameIsOver = false;
    }
}