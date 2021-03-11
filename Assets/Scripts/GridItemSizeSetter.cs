// using UnityEngine;
// using UnityEngine.UI;
// using UniRx;
//
// public class GridItemSizeSetter : MonoBehaviour
// {
//
//     [SerializeField]
//     private int rowCount = 3;
//     [SerializeField]
//     private int columnCount = 3;
//     
//     public float CellHeight
//     {
//         get
//         {
//             var anchorMax = rectTransform.anchorMax;
//             var anchorMin  = rectTransform.anchorMin;
//             var preOffsetMax = rectTransform.offsetMax;
//             var preOffsetMin = rectTransform.offsetMin;
//             float rectSizeY = 0;
//             if ((anchorMax - anchorMin).y != 0)
//             {
//                 Transform parent = rectTransform.transform.parent;
//                 Vector2 parentRectSize = parent.gameObject.GetComponent<RectTransform>().sizeDelta;
//                 rectSizeY = parentRectSize.y - preOffsetMin.y + preOffsetMax.y;
//                 Debug.Log($"y : {rectSizeY}");
//             }
//             else
//             {
//                 rectSizeY = rectTransform.sizeDelta.y;
//             }
//             return (int)((rectSizeY - (gridLayout.padding.top + gridLayout.padding.bottom)
//                 - gridLayout.spacing.y * (rowCount - 1)) / rowCount);
//         }
//     }
//
//     public int CellWidth
//     {
//         get
//         {
//             var anchorMax = rectTransform.anchorMax;
//             var anchorMin = rectTransform.anchorMin;
//             var preOffsetMax = rectTransform.offsetMax;
//             var preOffsetMin = rectTransform.offsetMin;
//             float rectSizeX = 0;
//             if ((anchorMax - anchorMin).x != 0)
//             {
//                 Transform parent = rectTransform.transform.parent;
//                 Vector2 parentRectSize = parent.gameObject.GetComponent<RectTransform>().sizeDelta;
//                 rectSizeX = parentRectSize.x - preOffsetMin.x + preOffsetMax.x;
//                 Debug.Log($"x : {rectSizeX}");
//             }
//             else
//             {
//                 rectSizeX = rectTransform.sizeDelta.x;
//             }
//             return (int)((rectSizeX - (gridLayout.padding.left + gridLayout.padding.right)
//                 - gridLayout.spacing.x * (columnCount - 1)) / columnCount);
//         }
//     }
//
//     private RectTransform rectTransform;
//     private GridLayoutGroup gridLayout;
//
//     void Start()
//     {
//         rectTransform = GetComponent<RectTransform>();
//         gridLayout = GetComponent<GridLayoutGroup>();
//         gameObject.ObserveEveryValueChanged(_ => rectTransform.sizeDelta).Subscribe(_ => UpdateCellSize());
//         gameObject.ObserveEveryValueChanged(_ => gridLayout.spacing).Subscribe(_ => UpdateCellSize());
//         gameObject.ObserveEveryValueChanged(_ => gridLayout.padding.GetType().GetProperties()).Subscribe(_ => UpdateCellSize());
//     }
//
//     private void UpdateCellSize()
//     {
//         gridLayout.cellSize = new Vector2(CellWidth, CellHeight);
//     }
//
// }
