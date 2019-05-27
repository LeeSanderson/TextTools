// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CircularBuffer.cs" company="Lee Sanderson">
//   Copyright (c) Lee Sanderson.
// </copyright>
// <summary>
//   A circular buffer is a fixed sized list.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TextAnalysis
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// A circular buffer is a fixed sized <see cref="IList{T}"/>.
    /// Any number of items can be added to the list but once the capacity is exceeded any additional
    /// items overwrite previously added items.
    /// </summary>
    /// <typeparam name="T">The type of items in the list</typeparam>
    public class CircularBuffer<T> : IList<T>, IReadOnlyCollection<T>
    {
        /// <summary>
        /// The items in the buffer
        /// </summary>
        private readonly T[] items;

        /// <summary>
        /// The index of the first item in the list.
        /// This is zero until we start overwriting previously added items.
        /// Once overwriting starts this value increments to indicate the position of the first items in the items array
        /// </summary>
        private int first;

        /// <summary>
        /// The index of the next item to be added. This is incremented every time and item is added and wraps
        /// back to zero once it gets to items.Length.
        /// </summary>
        private int next;

        /// <summary>
        /// The number of items added to the list.
        /// This is incremented every time an item is added until we start overwriting previously added items.
        /// Once overwriting starts this value is fixed to the items.Length.
        /// </summary>
        private int count;

        /// <summary>
        /// Initializes a new instance of the <see cref="CircularBuffer{T}"/> class.
        /// </summary>
        /// <param name="capacity">The capacity - the maximum number of items to be concurrently in the buffer.</param>
        public CircularBuffer(int capacity)
        {
            if (capacity < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(capacity));
            }

            this.items = new T[capacity];
        }

        /// <summary>
        /// Gets the count of the number of items in the buffer.
        /// </summary>
        public int Count => this.count;

        /// <inheritdoc />
        public bool IsReadOnly => false;

        /// <inheritdoc />
        public T this[int index]
        {
            get => this.items[this.WrapIndex(index)];
            set => this.items[this.WrapIndex(index)] = value;
        }

        /// <inheritdoc />
        public IEnumerator<T> GetEnumerator()
        {
            // Get the items from [first..count)
            for (int i = this.first; i < this.count; i++)
            {
                yield return this.items[i];
            }

            // Get the items from [0..first) if we have overwritten any
            if (this.first > 0)
            {
                for (int i = 0; i < this.first; i++)
                {
                    yield return this.items[i];
                }
            }
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <inheritdoc />
        public void Add(T item)
        {
            this.items[this.next++] = item;
            if (this.count < this.items.Length)
            {
                // Buffer has capacity - increase count.
                this.count++;
                if (this.count == this.items.Length)
                {
                    this.next = 0;
                }
            }
            else
            {
                // Buffer full - count is now fixed.
                this.first++;
                if (this.first == this.count)
                {
                    this.first = 0;
                }

                if (this.next == this.count)
                {
                    this.next = 0;
                }
            }
        }

        /// <inheritdoc />
        public void Clear()
        {
            this.first = 0;
            this.count = 0;
            this.next = 0;
        }

        /// <inheritdoc />
        public bool Contains(T item)
        {
            return this.IndexOf(item) != -1;
        }

        /// <inheritdoc />
        public void CopyTo(T[] array, int arrayIndex)
        {
            foreach (var item in this.items)
            {
                array[arrayIndex++] = item;
            }
        }

        /// <inheritdoc />
        public bool Remove(T item)
        {
            throw new NotSupportedException("CircularBuffer does not support explict removal of items");
        }

        /// <inheritdoc />
        public int IndexOf(T obj)
        {
            // O(N) index of by itterating over items.
            int index = 0;
            foreach (var item in this)
            {
                if (EqualityComparer<T>.Default.Equals(item, obj))
                {
                    return index;
                }

                index++;
            }

            // Item not found
            return -1;
        }

        /// <inheritdoc />
        public void Insert(int index, T item)
        {
            throw new NotSupportedException("CircularBuffer does not support insertion of items");
        }

        /// <inheritdoc />
        public void RemoveAt(int index)
        {
            throw new NotSupportedException("CircularBuffer does not support explict removal of items");
        }

        /// <summary>
        /// Add a range of items
        /// </summary>
        /// <param name="objs">The items to add</param>
        public void AddRange(params T[] objs)
        {
            foreach (var obj in objs)
            {
                this.Add(obj);
            }
        }

        /// <summary>
        /// Wrap an index value to an array index based on the position of the first element
        /// </summary>
        /// <param name="index">The external index (0..count-1)</param>
        /// <returns>The item index offset by the first element position</returns>
        private int WrapIndex(int index)
        {
            if (index < 0 || index >= this.count)
            {
                throw new IndexOutOfRangeException($"index (value = {index})must be between 0 and {this.count - 1}");
            }

            return this.first == 0 ? index : (this.first + index) % this.count;
        }
    }
}
