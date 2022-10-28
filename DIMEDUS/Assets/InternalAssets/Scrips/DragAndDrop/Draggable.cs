using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DIMEDUS.RimuruDev
{
    public sealed class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private SceneDataContainer dataContainer = null;
        private Transform parentToReturnTo = null;
        private Transform placeHolderParent = null;

        private CanvasGroup canvasGroup = null;
        private GameObject placeHolder = null;

        public Transform ParentToReturnTo { get => parentToReturnTo; set => parentToReturnTo = value; }
        public Transform PlaceHolderParent { get => placeHolderParent; set => placeHolderParent = value; }

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            dataContainer = FindObjectOfType<SceneDataContainer>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            CreatePlaceHolder();

            ParentToReturnTo = transform.parent;
            PlaceHolderParent = ParentToReturnTo;

            transform.SetParent(transform.parent.parent);

            canvasGroup.blocksRaycasts = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = eventData.position;

            if (placeHolder.transform.parent != PlaceHolderParent)
            {
                placeHolder.transform.SetParent(PlaceHolderParent);
            }

            int newSiblingIndex = PlaceHolderParent.childCount;

            for (int i = 0; i < PlaceHolderParent.childCount; i++)
            {
                if (transform.position.y > PlaceHolderParent.GetChild(i).position.y)
                {
                    newSiblingIndex = i;

                    if (placeHolder.transform.GetSiblingIndex() < newSiblingIndex)
                        newSiblingIndex--;

                    break;
                }
            }

            placeHolder.transform.SetSiblingIndex(newSiblingIndex);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            transform.SetParent(ParentToReturnTo);
            transform.SetSiblingIndex(placeHolder.transform.GetSiblingIndex());

            canvasGroup.blocksRaycasts = true;

            Destroy(placeHolder);
        }

        private void CreatePlaceHolder()
        {
            placeHolder = new GameObject();
            placeHolder.transform.SetParent(transform.parent);

            LayoutElement layoutElement = placeHolder.AddComponent<LayoutElement>();
            LayoutElement thisLayoutElement = GetComponent<LayoutElement>();

            layoutElement.preferredWidth = thisLayoutElement.preferredWidth;
            layoutElement.preferredHeight = thisLayoutElement.preferredHeight;
            layoutElement.flexibleWidth = 0;
            layoutElement.flexibleHeight = 0;

            placeHolder.transform.SetSiblingIndex(transform.GetSiblingIndex());
        }
    }
}