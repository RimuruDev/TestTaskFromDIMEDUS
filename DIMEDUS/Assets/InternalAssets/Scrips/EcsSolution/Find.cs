using UnityEngine;

namespace DIMEDUS.ECS
{
    public sealed class Find<T> where T : MonoBehaviour
    {
        public static T FastFind(string tag)
        {
            return GameObject.FindGameObjectWithTag(tag).GetComponent<T>();
        }
    }
}