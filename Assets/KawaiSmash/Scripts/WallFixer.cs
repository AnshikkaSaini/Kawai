using UnityEngine;

public class WallFixer : MonoBehaviour
{
    [SerializeField] private Transform rightWall;
    [SerializeField] private Transform leftWall;
   // public float wallThickness = 0.5f; // in Unity units

    void Start()
    {
        Camera mainCamera = Camera.main;
        float aspectRatio = (float)Screen.width / (float)Screen.height; // correct aspect ratio
        float halfHorizontalPov = mainCamera.orthographicSize * aspectRatio;

        rightWall.position = new Vector3(halfHorizontalPov + 0.5f, 0, 0);
        leftWall.position = new Vector3(-(halfHorizontalPov + 0.5f), 0, 0);
    }


}



   
    

