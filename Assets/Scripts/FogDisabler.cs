using UnityEngine;
using UnityEngine.Rendering;

public class FogDisabler : MonoBehaviour
{
    private bool originalFogState;

    void OnEnable()
    {
        RenderPipelineManager.beginCameraRendering += OnBeginCameraRendering;
        RenderPipelineManager.endCameraRendering += OnEndCameraRendering;
    }

    void OnDisable()
    {
        RenderPipelineManager.beginCameraRendering -= OnBeginCameraRendering;
        RenderPipelineManager.endCameraRendering -= OnEndCameraRendering;
    }

    void OnBeginCameraRendering(ScriptableRenderContext context, Camera camera)
    {
        if (camera == this.GetComponent<Camera>())
        {
            originalFogState = RenderSettings.fog; // Store original state
            RenderSettings.fog = false; // Disable fog for this camera
        }
    }

    void OnEndCameraRendering(ScriptableRenderContext context, Camera camera)
    {
        if (camera == this.GetComponent<Camera>())
        {
            RenderSettings.fog = originalFogState; // Revert fog to original state
        }
    }
}