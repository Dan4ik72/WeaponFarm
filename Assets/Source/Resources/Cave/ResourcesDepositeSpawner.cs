using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourcesDepositeSpawner : MonoBehaviour
{
    [SerializeField] private List<DepositeSetUp> _deposites;
    [SerializeField] private List<Transform> _spawnPoints;

    private void Awake() => Spawn();
    
    public void Spawn()
    {
        foreach (var deposite in _deposites)
        {
            for (int i = 0; i < deposite.Count; i++)
            {
                if(_spawnPoints.Count == 0)
                    return;

                var created = Instantiate(deposite.Prefab, _spawnPoints.First().position, Quaternion.identity, transform);
                created.gameObject.SetActive(true);
                _spawnPoints.Remove(_spawnPoints.First());
            }
        }
    }
}

[System.Serializable]
public class DepositeSetUp
{
    [SerializeField] private ResourcesDeposite _prefab;
    [SerializeField] private int _count;

    public ResourcesDeposite Prefab => _prefab;
    public int Count => _count;
}
