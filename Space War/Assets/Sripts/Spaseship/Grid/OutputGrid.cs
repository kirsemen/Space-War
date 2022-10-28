using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OutputGrid : MonoBehaviour
{
    public List<OutputGridElement> grid = new List<OutputGridElement>();
    static public OutputGrid S1;

    public void Start()
    {
        S1 = this;
        foreach (Transform XBazier in transform.GetChild(0).GetChild(0))
            foreach (Transform YBazier in transform.GetChild(0).GetChild(1))
            {
                Vector2 A = new Vector2(XBazier.GetChild(0).position.x, XBazier.GetChild(0).position.z);
                Vector2 B = new Vector2(XBazier.GetChild(XBazier.childCount - 1).position.x, XBazier.GetChild(XBazier.childCount - 1).position.z);
                Vector2 C = new Vector2(YBazier.GetChild(0).position.x, YBazier.GetChild(0).position.z);
                Vector2 D = new Vector2(YBazier.GetChild(YBazier.childCount - 1).position.x, YBazier.GetChild(YBazier.childCount - 1).position.z);

                GameObject go = new GameObject("GridElement", new System.Type[] { typeof(OutputGridElement) });
                go.GetComponent<OutputGridElement>().pos = new Vector2(XBazier.GetComponent<Bezier>().position, YBazier.GetComponent<Bezier>().position);
                go.transform.parent = transform.GetChild(1);
                go.transform.position = Bezier.GetPointByZPosition(XBazier.GetComponent<Bezier>(), Bezier.GetLinesCrossVector(A, B, C, D).y);

                if(XBazier.GetComponent<Bezier>().position==0.5f&& YBazier.GetComponent<Bezier>().position == 0.5f)
                {
                Debug.Log(Bezier.GetDerivativeByZPosition(XBazier.GetComponent<Bezier>(), Bezier.GetLinesCrossVector(A, B, C, D).y));

                }
            }

    }
    private void Update()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }
    public OutputGridElement Find(Vector2 pos)
    {
        foreach (var element in grid)
        {
            if (element.pos == pos)
            {
                return element;
            }
        }
        return null;
    }
}
