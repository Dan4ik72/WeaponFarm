using UnityEngine;

public class Resource : MonoBehaviour, ICollectable
{
    [SerializeField] private int _level;
    [SerializeField] private int _cost;
    [SerializeField] private ResourceType _type;

    public ResourceType Type => _type;
    
    public Transform GetTransform() => transform;

    public void OnCollect(){}
    
}

public enum ResourceType
{
    Iron,
    Copper,
    Silver,
    Gold
}

public interface ICollectable
{
    public Transform GetTransform();
    public void OnCollect();
}