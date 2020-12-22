using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class HousePlacer : MonoBehaviour
{
    [SerializeField]
    private float _housePlantArea = 250;
    [SerializeField]
    private float _housePlantRadius = 5;
    [SerializeField]
    private float _houseMaxHeight = 5;
    [SerializeField]
    private int _count;
    [SerializeField]
    private GameObject _housePrefab;
    [SerializeField]
    private LayerMask _houseLayer;

    public void PlaceHouses()
    {
#if UNITY_EDITOR
        var rootGo = new GameObject();
        rootGo.transform.parent = transform;
        rootGo.name = $"{_housePrefab.name}_Root";

        for(int i = 0; i < _count; i++)
        {
            Vector2 areaPoint = Random.insideUnitCircle * _housePlantArea;
            Vector3 point = transform.position + new Vector3(areaPoint.x, 0, areaPoint.y);

            if(Physics.Raycast(point + Vector3.up * 500, -Vector3.up, out RaycastHit hit, 1000, _houseLayer, QueryTriggerInteraction.Ignore))
            {
                point = hit.point;
            }

            if(NavMesh.SamplePosition(point, out NavMeshHit navHit, _housePlantRadius, NavMesh.AllAreas))
            {
                GameObject house = PrefabUtility.InstantiatePrefab(_housePrefab, rootGo.transform) as GameObject;
                house.transform.position = navHit.position;
                float rot = Random.Range(0, 360);
                house.transform.Rotate(0, rot, 0);
            }
        }
#endif
    }
}
