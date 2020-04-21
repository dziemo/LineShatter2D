using System;
using UnityEngine;

[CreateAssetMenu]
public class BoolVariable : ScriptableObject, ISerializationCallbackReceiver
{
    public bool InitialValue;

    [NonSerialized]
    public bool RuntimeValue;

    public void OnAfterDeserialize()
    {
        RuntimeValue = InitialValue;
    }

    public void OnBeforeSerialize() { }
}
