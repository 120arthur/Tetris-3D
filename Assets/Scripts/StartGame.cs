using UnityEngine;
using Context;

public class StartGame : MonoBehaviour
{
    private void Start()
    {
       var context = new Context.Context();
        context.GameManager.gameIsOver = false;
    }
}