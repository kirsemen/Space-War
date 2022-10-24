using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class GridElement : MonoBehaviour
{
    public Vector2Int position = new Vector2Int(0, 0);


    //static public void ConnectModule(Module module, Vector2Int _position)
    //{
    //    _position -= module.size / 2;
    //    var size = module.size;
    //    size -= module.size / 2;

    //    if (_position.x < 0)
    //        _position.x = 0;
    //    if (_position.y < 0)
    //        _position.y = 0;
    //    for (int x = _position.x; x > _position.x - size.x; x--)
    //        for (int y = _position.y; y > _position.y - size.y; y--)
    //        {
    //            var position = new Vector2Int(x, y);
    //            if ((position + module.size).x <= maxSize.x && position.x >= 0 && (position + module.size).y <= maxSize.y && position.y >= 0)
    //            {
    //                bool isIn = false;
    //                foreach (var item in modules)
    //                {
    //                    if ((
    //                        ((position.x <= item.gridPos.x && item.gridPos.x <= (position.x + module.size.x - 1)) ||
    //                        (position.x <= (item.gridPos.x + item.size.x - 1) && (item.gridPos.x + item.size.x - 1) <= (position.x + module.size.x - 1))) &&
    //                        ((position.y <= item.gridPos.y && item.gridPos.y <= (position.y + module.size.y - 1)) ||
    //                        (position.y <= (item.gridPos.y + item.size.y - 1) && (item.gridPos.y + item.size.y - 1) <= (position.y + module.size.y - 1)))
    //                        )||(
    //                        ((item.gridPos.x<= position.x && position.x <= (item.gridPos.x + item.size.x - 1)) ||
    //                        (item.gridPos.x<= (position.x + module.size.x - 1)&& (position.x + module.size.x - 1)<= (item.gridPos.x + item.size.x - 1)))&&
    //                        ((item.gridPos.y <= position.y && position.y <= (item.gridPos.y + item.size.y - 1)) ||
    //                        (item.gridPos.y <= (position.y + module.size.y - 1) && (position.y + module.size.y - 1) <= (item.gridPos.y + item.size.y - 1)))
    //                        ))
    //                    {
    //                        isIn = true;
    //                        break;
    //                    }
    //                }
    //                if (isIn)
    //                {
    //                    module.gridPos.x = -1;
    //                    module.gridPos.y = -1;
    //                    return;
    //                }


    //                module.gameObject.transform.position = list[position.y, position.x].gameObject.transform.position + module.margin;
    //                for (int i = 0; i < module.size.x; i++)
    //                    for (int j = 0; j < module.size.y; j++)
    //                    {
    //                        list[j + position.y, i + position.x].gameObject.transform.Find("Cube").gameObject.SetActive(true);
    //                        list[j + position.y, i + position.x].gameObject.transform.Find("Plane").gameObject.SetActive(false);
    //                    }
    //                module.gridPos.x = x;
    //                module.gridPos.y = y;
    //                return;
    //            }
    //        }
    //    module.gridPos.x = -1;
    //    module.gridPos.y = -1;
    //}
}
