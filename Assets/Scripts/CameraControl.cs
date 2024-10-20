using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private Camera[] cameras;
    private int currentCameraIndex = 0;

    private void Start()
    {
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].gameObject.SetActive(i == currentCameraIndex);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            SwitchCamera();
        }
    }

    private void SwitchCamera()
    {
        cameras[currentCameraIndex].gameObject.SetActive(false);

        currentCameraIndex = (currentCameraIndex + 1) % cameras.Length;

        cameras[currentCameraIndex].gameObject.SetActive(true);
    }
}
