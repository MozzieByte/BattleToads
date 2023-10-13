using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    private bool localPlayerSpawned = false;

    private void Awake()
    {
        Player.OnAnyPlayerSpawned += delegate { 
            if (Player.LocalInstance) 
                localPlayerSpawned = true; 
        };
    }
    private void Update()
    {
        if (localPlayerSpawned)
        {
            Vector2 screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

            // This is done to keep the player from seeing past what they are supposed to if they are capable of moving their mouse outside the bounds of the gamescreen/viewport.
/*            var vertExtent = OrthographicBounds(Camera.main).max.y;
            var horzExtent = OrthographicBounds(Camera.main).max.x;
            worldPosition = new Vector2(Mathf.Clamp(worldPosition.x, 0, horzExtent), Mathf.Clamp(worldPosition.y, 0, vertExtent));*/

            // 1/4th the distance from the player to the mouse position
            Vector2 calculatedPosition = Vector2.Lerp(Player.LocalInstance.transform.position, worldPosition, 0.125f);
            transform.position = calculatedPosition;
        }
    }

    public static Bounds OrthographicBounds(Camera camera)
    {
        float screenAspect = (float)Screen.width / (float)Screen.height;
        float cameraHeight = camera.orthographicSize * 2;
        Bounds bounds = new Bounds(
            camera.transform.position,
            new Vector3(cameraHeight * screenAspect, cameraHeight, 0));
        return bounds;
    }
}
