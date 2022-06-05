using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class ShuffleTiles : MonoBehaviour
{

    private Queue<Transform> Shuffle;

    public List<Transform> shuffle;
    public List<Transform> rotate;


    [Button]
    public void shuffleTiles()
    {
        Shuffle = ScriptsTools.ShuffleList(shuffle);
        List<Vector3> positions = new List<Vector3>();
        foreach (Transform t in shuffle)
            positions.Add(t.position);
        for (int i = 0; i < positions.Count; i++)
        {
            Shuffle.Dequeue().position = positions[i];
            int temp = Random.Range(0, 4);
            shuffle[i].rotation=Quaternion.Euler(shuffle[i].rotation.eulerAngles.x,temp*90,shuffle[i].rotation.eulerAngles.z); 
        }
        
    }
     
    
    [Button]
    public void RotateTiles()
    {
        for (int i = 0; i < rotate.Count; i++)
        {
            int temp = Random.Range(0, 4);
            rotate[i].rotation=Quaternion.Euler(rotate[i].rotation.eulerAngles.x, temp*90,rotate[i].rotation.eulerAngles.z); 
        }
        
        
        
    }
}
