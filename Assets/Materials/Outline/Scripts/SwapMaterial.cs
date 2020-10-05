using UnityEngine;

public enum CurrentMaterial
{
    ORIGINAL,
    OUTLINE,
    OUTLINE_SELECTED
}

public class SwapMaterial : MonoBehaviour {

    public SkinnedMeshRenderer meshRenderer;
    public Material outlineMaterial;
    public Material outlineSelectedMaterial;
    private Material originalMaterial;
    private Texture texture;

    public CurrentMaterial currentMaterial;

    private void Start()
    {
        originalMaterial = meshRenderer.material;
        texture = originalMaterial.mainTexture;
        outlineMaterial.mainTexture = texture;
    }

    public void SwapToOutline()
    {
        meshRenderer.material = outlineMaterial;
        currentMaterial = CurrentMaterial.OUTLINE;
    }

    public void SwapToOutlineSelected()
    {
        meshRenderer.material = outlineSelectedMaterial;
        currentMaterial = CurrentMaterial.OUTLINE_SELECTED;
    }

    public void SwapToOriginal()
    {
        meshRenderer.material = originalMaterial;
        currentMaterial = CurrentMaterial.ORIGINAL;
    }
}
