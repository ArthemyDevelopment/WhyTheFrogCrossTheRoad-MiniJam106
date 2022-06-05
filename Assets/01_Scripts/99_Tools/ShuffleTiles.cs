using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class ShuffleTiles : MonoBehaviour
{

    public GameObject ShuffleParent;
    public GameObject RotateParent;
    private Queue<Transform> Shuffle;

    private List<Transform> shuffle;
    private List<Transform> rotate;


    [Button]
    public void shuffleTiles()
    {
        shuffle = new List<Transform>(ShuffleParent.GetComponentsInChildren<Transform>());
        shuffle.Remove(ShuffleParent.transform);
        Shuffle = ScriptsTools.ShuffleList(shuffle);
        List<Vector3> positions = new List<Vector3>();
        foreach (Transform t in shuffle)
            positions.Add(t.position);
        for (int i = 0; i < positions.Count; i++)
        {
            Shuffle.Dequeue().position = positions[i];
            int temp = Random.Range(0, 4);
            shuffle[i].rotation=Quaternion.Euler(shuffle[i].rotation.eulerAngles.x, shuffle[i].rotation.eulerAngles.y,temp*90); 
        }
        
    }
     
    
    [Button]
    public void RotateTiles()
    {
        rotate = new List<Transform>(RotateParent.GetComponentsInChildren<Transform>());

        rotate.Remove(RotateParent.transform);
        for (int i = 0; i < rotate.Count; i++)
        {
            int temp = Random.Range(0, 4);
            rotate[i].rotation=Quaternion.Euler(rotate[i].rotation.eulerAngles.x, rotate[i].rotation.eulerAngles.y,temp*90); 
        }
        
        
        
    }
}
