using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FantasyFootballDashboard.Models.Collections
{
    public class DataList<T> : IList<T> where T : IDataObject
    {
        private List<T> InternalList { get; set; } = new List<T>();

        public DataList()
        {

        }

        public T this[int index] { get => ((IList<T>)InternalList)[index]; set => ((IList<T>)InternalList)[index] = value; }

        public int Count => ((ICollection<T>)InternalList).Count;

        public bool IsReadOnly => ((ICollection<T>)InternalList).IsReadOnly;

        /// <summary>
        /// Checks if an item is unique before adding to the list. If it is not unique, it consolidates the data into a single record.
        /// </summary>
        /// <param name="item"></param>
        public void Add(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item", "Can't add null item.");
            }

            item = CheckUniqueness(item);

            InternalList.Add(item);
        }

        public void Clear()
        {
            ((ICollection<T>)InternalList).Clear();
        }

        public bool Contains(T item)
        {
            return ((ICollection<T>)InternalList).Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            ((ICollection<T>)InternalList).CopyTo(array, arrayIndex);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)InternalList).GetEnumerator();
        }

        public int IndexOf(T item)
        {
            return ((IList<T>)InternalList).IndexOf(item);
        }

        /// <summary>
        /// Checks if an item is unique before inserting it into the list. If it is not unique, it consolidates the data into a single record.
        /// </summary>
        /// <param name="item"></param>
        public void Insert(int index, T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item", "Can't add null item.");
            }

            item = CheckUniqueness(item);

            InternalList.Insert(index, item);
        }

        public bool Remove(T item)
        {
            return ((ICollection<T>)InternalList).Remove(item);
        }

        public void RemoveAt(int index)
        {
            ((IList<T>)InternalList).RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)InternalList).GetEnumerator();
        }

        private T CheckUniqueness(T item)
        {
            var duplicates = InternalList.
                Where(obj => obj.Key.Equals(item.Key));

            var indicesToRemove = new List<int>();

            foreach (var dup in duplicates)
            {
                item.Copy(dup);
                indicesToRemove.Add(InternalList.IndexOf(dup));
            }

            foreach (var dup in indicesToRemove)
            {
                InternalList.RemoveAt(dup);
            }

            return item;
        }
    }
}
