using UnityEngine;
using System.Collections;

public class SetSortingLayerForObjectAndChildren : MonoBehaviour {

    public string LayerName;
    public int SortingOrder = 1000;
	void Start () {
        Renderer[] rends = transform.GetComponentsInChildren<Renderer>();

        for (int i = 0; i < rends.Length; i ++)
        {
            rends[i].sortingLayerName = LayerName;
            rends[i].sortingOrder = SortingOrder;
        }

        GetComponent<Renderer>().sortingLayerName = LayerName;
        GetComponent<Renderer>().sortingOrder = SortingOrder;

    }
	
	void Update () {
	
	}
}
