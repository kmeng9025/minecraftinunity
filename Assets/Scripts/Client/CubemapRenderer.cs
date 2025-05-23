using UnityEngine;

public class CubemapRenderer : MonoBehaviour
{
    public Camera cam;
    public RenderTexture cubemapRenderTexture;

    void Start()
    {
        if (cam == null)
            cam = GetComponent<Camera>();

        // Assign a cubemap render texture
        cam.RenderToCubemap(cubemapRenderTexture);
        Debug.Log("Cubemap captured");
    }
}
