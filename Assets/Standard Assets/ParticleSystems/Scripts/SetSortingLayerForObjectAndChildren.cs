using UnityEngine;
using System.Collections;

public class SetSortingLayerForObjectAndChildren : MonoBehaviour {

    public string LayerName;
	void Start () {
        Renderer[] rends = transform.GetComponentsInChildren<Renderer>();

        for (int i = 0; i < rends.Length; i ++)
        {
            rends[i].sortingLayerName = LayerName;
            rends[i].sortingOrder = 1000;
        }

        GetComponent<Renderer>().sortingLayerName = LayerName;
        GetComponent<Renderer>().sortingOrder = 1000;

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
