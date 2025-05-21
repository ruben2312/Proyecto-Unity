using UnityEngine;

public class SceneController_Pinball_1 : MonoBehaviour
{
    public GameObject cargadorNivel = null;
    private int bricksDestroyed = 0; // Counter for destroyed bricks
    private int totalBricksToDestroy = 8; // Number of bricks to destroy before loading the next scene

    // This method should be called whenever a brick is destroyed
    public void BrickDestroyed()
    {
        bricksDestroyed++; // Increment the counter
        if (bricksDestroyed >= totalBricksToDestroy)
        {
            LoadNextScene(); // Load the next scene when the required number of bricks is destroyed
        }
    }

    private void LoadNextScene()
    {
        cargadorNivel.GetComponent<CargadorNivel>().CargarSiguiente();
    }
}
