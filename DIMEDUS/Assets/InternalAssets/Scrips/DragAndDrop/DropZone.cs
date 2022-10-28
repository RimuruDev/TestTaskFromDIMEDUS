using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace DIMEDUS.RimuruDev
{
    public sealed class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
    {
        private SceneDataContainer dataContainer = null;
        private UIController uiController = null;

        private void Awake()
        {
            dataContainer = FindObjectOfType<SceneDataContainer>();
            uiController = FindObjectOfType<UIController>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (eventData.pointerDrag == null)
                return;

            if (eventData.pointerDrag.TryGetComponent<Draggable>(out Draggable draggable))
                draggable.PlaceHolderParent = transform;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (eventData.pointerDrag == null)
                return;

            Draggable draggable = eventData.pointerDrag.GetComponent<Draggable>();

            if (draggable != null && draggable.PlaceHolderParent == transform)
            {
                draggable.PlaceHolderParent = draggable.ParentToReturnTo;
            }
        }

        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag.TryGetComponent<Draggable>(out Draggable draggable))
                draggable.ParentToReturnTo = transform;

            uiController.HeadersElementCounterUpdate();
        }
    }
}