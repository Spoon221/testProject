using TMPro;
using UnityEngine;

public class Gate : MonoBehaviour
{
    private enum OperationType
    { 
        addition,
        difference,
        multiplication,
        division
    }

    [Header("Operation")]
    [SerializeField] private OperationType gateOperation;
    [SerializeField] private int value;

    [Header("References")]
    [SerializeField] private TextMeshPro operationText;
    [SerializeField] private MeshRenderer forceField;
    [SerializeField] private Material[] operationTypeMaterial;
    [SerializeField] private TextMeshPro gateText;

    private void Awake()
    {
        AssignOperation();
    }
    private void AssignOperation()
    {
        if (gateOperation == OperationType.addition || gateOperation == OperationType.multiplication)
            forceField.material = operationTypeMaterial[0];
        else
            forceField.material = operationTypeMaterial[1];
    }
}