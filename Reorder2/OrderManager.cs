namespace Reorder
{
    public class OrderManager<T>
    {
        public List<T> MoveObject(IEnumerable<T> objects, Func<T, string> getId, Func<T, uint> getOrder, Action<T, uint> setOrder, string objectId, int newPosition)
        {
            List<T> copiedObjects = objects.ToList();
            T objToMove = copiedObjects.FirstOrDefault(obj => getId(obj) == objectId);

            if (objToMove != null)
            {
                uint currentOrder = getOrder(objToMove);
                uint newOrder = (uint)newPosition;

                if (currentOrder == newOrder)
                {
                    return copiedObjects;
                }

                newOrder = Math.Max(1, Math.Min((uint)copiedObjects.Count, newOrder));

                copiedObjects.Remove(objToMove);
                copiedObjects.Insert((int)newOrder - 1, objToMove);

                ReorderObjects(copiedObjects, getOrder, setOrder);
            }

            return copiedObjects;
        }

        public List<T> AddObjectAtFirst(IEnumerable<T> objects, T newObject)
        {
            List<T> copiedObjects = objects.ToList();
            copiedObjects.Insert(0, newObject);

            return copiedObjects;
        }

        public List<T> AddObjectAtLast(IEnumerable<T> objects, T newObject)
        {
            List<T> copiedObjects = objects.ToList();
            copiedObjects.Add(newObject);

            RemoveOrderGaps(copiedObjects);

            return copiedObjects;
        }

        public List<T> AddObjectAfter(IEnumerable<T> objects, Func<T, string> getId, string objectId, T newObject)
        {
            List<T> copiedObjects = objects.ToList();
            int index = copiedObjects.FindIndex(obj => getId(obj) == objectId);

            if (index != -1)
            {
                copiedObjects.Insert(index + 1, newObject);

                RemoveOrderGaps(copiedObjects);
            }

            return copiedObjects;
        }

        public List<T> AddObjectBefore(IEnumerable<T> objects, Func<T, string> getId, string objectId, T newObject)
        {
            List<T> copiedObjects = objects.ToList();
            int index = copiedObjects.FindIndex(obj => getId(obj) == objectId);

            if (index != -1)
            {
                copiedObjects.Insert(index, newObject);

                RemoveOrderGaps(copiedObjects);
            }

            return copiedObjects;
        }

        public List<T> RemoveOrderGaps(List<T> objects)
        {
            uint currentOrder = 1;

            foreach (T obj in objects)
            {
                if (obj is IOrderable orderable)
                {
                    orderable.Order = currentOrder;
                    currentOrder++;
                }
            }

            return objects;
        }

        private void ReorderObjects(List<T> objects, Func<T, uint> getOrder, Action<T, uint> setOrder)
        {
            uint currentOrder = 1;

            foreach (T obj in objects)
            {
                setOrder(obj, currentOrder);
                currentOrder++;
            }
        }
    }
}
