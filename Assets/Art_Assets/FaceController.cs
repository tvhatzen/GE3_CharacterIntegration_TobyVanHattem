using UnityEngine;
using System.Collections.Generic;

public class FaceController : MonoBehaviour
{
    [Header("References")]
    public SkinnedMeshRenderer faceRenderer;

    [Header("Transition Settings")]
    public float transitionSpeed = 120f; // higher = faster transition

    private Mesh faceMesh;
    private float[] targetWeights;

    void Start()
    {
        if (faceRenderer == null)
        {
            Debug.LogError("FaceController: No SkinnedMeshRenderer assigned.");
            return;
        }

        faceMesh = faceRenderer.sharedMesh;

        if (faceMesh == null)
        {
            Debug.LogError("FaceController: No mesh found on the SkinnedMeshRenderer.");
            return;
        }

        targetWeights = new float[faceMesh.blendShapeCount];

        // Start with current weights as targets
        for (int i = 0; i < faceMesh.blendShapeCount; i++)
        {
            targetWeights[i] = faceRenderer.GetBlendShapeWeight(i);
        }
    }

    void Update()
    {
        if (faceRenderer == null || faceMesh == null) return;

        HandleInput();
        UpdateBlendShapesSmoothly();
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ResetTargets();
            SetTargetShape("EyebrowFurrow.L", 100f);
            SetTargetShape("EyebrowFurrow.R", 100f);
            SetTargetShape("EyebrowTilt.L", 83.8f);
            SetTargetShape("EyebrowTilt.R", 83.8f);
            SetTargetShape("Blink.L", 18.4f);
            SetTargetShape("Blink.R", 18.4f);
            SetTargetShape("Eyelashesblink.L", 18.4f);
            SetTargetShape("Eyelashesblink.R", 18.4f);
            SetTargetShape("Smile", 0f);
            SetTargetShape("Frown", 100f);
            SetTargetShape("LipRaise.L", -30.2f);
            SetTargetShape("LipRaise.R", -30.2f);
            SetTargetShape("MouthOpenTop", 0f);
            SetTargetShape("MouthOpenBottom", 0f);
            SetTargetShape("TeethTopUp", 0f);
            SetTargetShape("TeethBottomDown", 0f);
            SetTargetShape("TongueMoveDown", 0f);
            SetTargetShape("BodyCorrector", 64.2f);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ResetTargets();
            SetTargetShape("EyebrowFurrow.L", 0f);
            SetTargetShape("EyebrowFurrow.R", 0f);
            SetTargetShape("EyebrowTilt.L", 0f);
            SetTargetShape("EyebrowTilt.R", 0f);
            SetTargetShape("Blink.L", 0f);
            SetTargetShape("Blink.R", 0f);
            SetTargetShape("Eyelashesblink.L", 0f);
            SetTargetShape("Eyelashesblink.R", 0f);
            SetTargetShape("Smile", 100f);
            SetTargetShape("Frown", 0f);
            SetTargetShape("LipRaise.L", -20.1f);
            SetTargetShape("LipRaise.R", -20.1f);
            SetTargetShape("MouthOpenTop", 0f);
            SetTargetShape("MouthOpenBottom", 0f);
            SetTargetShape("TeethTopUp", 0f);
            SetTargetShape("TeethBottomDown", 0f);
            SetTargetShape("TongueMoveDown", 0f);
            SetTargetShape("BodyCorrector", 64.2f);

        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ResetTargets();
            SetTargetShape("EyebrowFurrow.L", 0f);
            SetTargetShape("EyebrowFurrow.R", 100f);
            SetTargetShape("EyebrowTilt.L", 100f);
            SetTargetShape("EyebrowTilt.R", 69.3f);
            SetTargetShape("Blink.L", 0f);
            SetTargetShape("Blink.R", 9.5f);
            SetTargetShape("Eyelashesblink.L", 0f);
            SetTargetShape("Eyelashesblink.R", 9.5f);
            SetTargetShape("Smile", 0f);
            SetTargetShape("Frown", 100f);
            SetTargetShape("LipRaise.L", 59.2f);
            SetTargetShape("LipRaise.R", -33.5f);
            SetTargetShape("MouthOpenTop", 0f);
            SetTargetShape("MouthOpenBottom", 0f);
            SetTargetShape("TeethTopUp", 0f);
            SetTargetShape("TeethBottomDown", 0f);
            SetTargetShape("TongueMoveDown", 0f);
            SetTargetShape("BodyCorrector", 64.2f);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ResetTargets();
            SetTargetShape("EyebrowFurrow.L", 0f);
            SetTargetShape("EyebrowFurrow.R", 0f);
            SetTargetShape("EyebrowTilt.L", 0f);
            SetTargetShape("EyebrowTilt.R", 0f);
            SetTargetShape("Blink.L", 0f);
            SetTargetShape("Blink.R", 0f);
            SetTargetShape("Eyelashesblink.L", 0f);
            SetTargetShape("Eyelashesblink.R", 0f);
            SetTargetShape("Smile", 0f);
            SetTargetShape("Frown", 0f);
            SetTargetShape("LipRaise.L", 0f);
            SetTargetShape("LipRaise.R", 0f);
            SetTargetShape("MouthOpenTop", 0f);
            SetTargetShape("MouthOpenBottom", 0f);
            SetTargetShape("TeethTopUp", 0f);
            SetTargetShape("TeethBottomDown", 0f);
            SetTargetShape("TongueMoveDown", 0f);
            SetTargetShape("BodyCorrector", 64.2f);

        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            ResetTargets();
        }
    }

    void UpdateBlendShapesSmoothly()
    {
        for (int i = 0; i < faceMesh.blendShapeCount; i++)
        {
            float currentWeight = faceRenderer.GetBlendShapeWeight(i);
            float newWeight = Mathf.MoveTowards(
                currentWeight,
                targetWeights[i],
                transitionSpeed * Time.deltaTime
            );

            faceRenderer.SetBlendShapeWeight(i, newWeight);
        }
    }

    void SetTargetShape(string shapeName, float weight)
    {
        int index = faceMesh.GetBlendShapeIndex(shapeName);

        if (index >= 0)
        {
            targetWeights[index] = weight;
        }
        else
        {
            Debug.LogWarning("Blend shape not found: " + shapeName);
        }
    }

    void ResetTargets()
    {
        for (int i = 0; i < targetWeights.Length; i++)
        {
            targetWeights[i] = 0f;
        }
    }
}