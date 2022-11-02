using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OutputGrid : MonoBehaviour
{
    public List<OutputGridElement> grid = new List<OutputGridElement>();

    public void Start()
    {
        foreach (Transform XBazier in transform.GetChild(0).GetChild(0))
            foreach (Transform YBazier in transform.GetChild(0).GetChild(1))
            {

                Vector2 A = new Vector2(XBazier.GetChild(0).localPosition.x, XBazier.GetChild(0).localPosition.z) + new Vector2(XBazier.localPosition.x, XBazier.localPosition.z);
                Vector2 B = new Vector2(XBazier.GetChild(XBazier.childCount - 1).localPosition.x, XBazier.GetChild(XBazier.childCount - 1).localPosition.z) +
                    new Vector2(XBazier.localPosition.x, XBazier.localPosition.z);
                Vector2 C = new Vector2(YBazier.GetChild(0).localPosition.x, YBazier.GetChild(0).localPosition.z) + new Vector2(YBazier.localPosition.x, YBazier.localPosition.z);
                Vector2 D = new Vector2(YBazier.GetChild(YBazier.childCount - 1).localPosition.x, YBazier.GetChild(YBazier.childCount - 1).localPosition.z) +
                    new Vector2(YBazier.localPosition.x, YBazier.localPosition.z);
                if (Bezier.areCrossing(A, B, C, D))
                {
                    GameObject go = new GameObject("GridElement", new System.Type[] { typeof(OutputGridElement) });
                    go.GetComponent<OutputGridElement>().pos = new Vector2(XBazier.GetComponent<Bezier>().position, YBazier.GetComponent<Bezier>().position);
                    go.transform.parent = transform.GetChild(1);
                    go.transform.localPosition = (Bezier.GetPointByZPosition(XBazier.GetComponent<Bezier>(), Bezier.GetLinesCrossVector(A, B, C, D).y) +
                        Bezier.GetPointByXPosition(YBazier.GetComponent<Bezier>(), Bezier.GetLinesCrossVector(A, B, C, D).x)) / 2;


                    Vector3 XYDerivative = Bezier.GetDerivativeByXPosition(YBazier.GetComponent<Bezier>(), Bezier.GetLinesCrossVector(A, B, C, D).x);
                    Vector3 YZDerivative = Bezier.GetDerivativeByZPosition(XBazier.GetComponent<Bezier>(), Bezier.GetLinesCrossVector(A, B, C, D).y);
                    go.transform.rotation = ((Quaternion.LookRotation(XYDerivative) * Quaternion.Euler(0, -90, 0)) * Quaternion.LookRotation(YZDerivative));

                    grid.Add(go.GetComponent<OutputGridElement>());
                }
            }
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
