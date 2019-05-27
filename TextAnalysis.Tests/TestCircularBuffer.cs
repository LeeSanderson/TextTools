// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestCircularBuffer.cs" company="Lee Sanderson">
//   Copyright (c) Lee Sanderson.
// </copyright>
// <summary>
//   Test the CircularBuffer class
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TextAnalysis.Tests
{
    using System;

    using Xunit;

    /// <summary>
    /// Test the <see cref="CircularBuffer{T}"/> class
    /// </summary>
    public class TestCircularBuffer
    {
        /// <summary>
        /// When a new buffer is created it is empty
        /// </summary>
        [Fact]
        public void WhenCreateBufferThenBufferIsEmpty()
        {
            // Given
            // ReSharper disable once CollectionNeverUpdated.Local
            var buffer = new CircularBuffer<int>(3);

            // Then
            Assert.Equal(0, buffer.Count);
            Assert.Equal(new int[0], buffer);
        }

        /// <summary>
        /// When fewer than capacity items are added then all items exist
        /// </summary>
        [Fact]
        public void WhenFewerThanCapacityItemsAreAddedThenAllItemsExists()
        {
            // Given
            var buffer = new CircularBuffer<int>(3);

            // When
            buffer.AddRange(1, 2);

            // Then
            Assert.Equal(2, buffer.Count);
            Assert.Equal(new[] { 1, 2 }, buffer);
            Assert.Equal(1, buffer[0]);
            Assert.Equal(2, buffer[1]);
        }

        /// <summary>
        /// When more than capacity items are added then only last items exist
        /// </summary>
        [Fact]
        public void WhenMoreThanCapacityItemsAreAddedThenOnlyLatestItemsExists()
        {
            // Given
            var buffer = new CircularBuffer<int>(3);

            // When
            buffer.AddRange(1, 2, 3, 4, 5, 6);

            // Then
            Assert.Equal(3, buffer.Count);
            Assert.Equal(new[] { 4, 5, 6 }, buffer);
            Assert.Equal(-1, buffer.IndexOf(1));
            Assert.Equal(-1, buffer.IndexOf(2));
            Assert.Equal(-1, buffer.IndexOf(3));
            Assert.Equal(0, buffer.IndexOf(4));
            Assert.Equal(1, buffer.IndexOf(5));
            Assert.Equal(2, buffer.IndexOf(6));
            Assert.Equal(-1, buffer.IndexOf(7));
            Assert.Equal(4, buffer[0]);
            Assert.Equal(5, buffer[1]);
            Assert.Equal(6, buffer[2]);
        }

        /// <summary>
        /// When more than capacity items are added then index offsets are calculated correctly.
        /// </summary>
        [Fact]
        public void WhenMoreThanCapacityItemsAreAddedThenIndexOffsetsAreCalculatedCorrectly()
        {
            // Given
            var buffer = new CircularBuffer<int>(3);

            // When - add more than capactity but less than a full multiple of capacity so offsets need to be calculated.
            buffer.AddRange(1, 2, 3, 4);

            // Then
            Assert.Equal(3, buffer.Count);
            Assert.Equal(new[] { 2, 3, 4 }, buffer);
            Assert.Equal(-1, buffer.IndexOf(1));
            Assert.Equal(0, buffer.IndexOf(2));
            Assert.Equal(1, buffer.IndexOf(3));
            Assert.Equal(2, buffer.IndexOf(4));
            Assert.Equal(-1, buffer.IndexOf(5));
            Assert.Equal(2, buffer[0]);
            Assert.Equal(3, buffer[1]);
            Assert.Equal(4, buffer[2]);
        }

        /// <summary>
        /// The when more than capacity items are added then enumeration returns then items in the correct order.
        /// </summary>
        [Fact]
        public void WhenMoreThanCapacityItemsAreAddedThenEnumerationReturnsThenItemsInTheCorrectOrder()
        {
            // Given
            var buffer = new CircularBuffer<int>(4);

            // When
            buffer.AddRange(1, 2, 3, 4, 5, 6);

            // Then
            Assert.Equal(4, buffer.Count);
            Assert.Equal(new[] { 3, 4, 5, 6 }, buffer);
        }

        /// <summary>
        /// The when create circular buffer with no capacity then an argument out of range exception is thrown.
        /// </summary>
        [Fact]
        public void WhenCreateCircularBufferWithNoCapacityThenAnArgumentOutOfRangeExceptionIsThrown()
        {
            // Then
            Assert.Throws<ArgumentOutOfRangeException>(() => new CircularBuffer<int>(0));
            Assert.Throws<ArgumentOutOfRangeException>(() => new CircularBuffer<int>(-1));
        }

        /// <summary>
        /// The when create circular buffer then it is not read only.
        /// </summary>
        [Fact]
        public void WhenCreateCircularBufferThenItIsNotReadOnly()
        {
            // Given
            // ReSharper disable once CollectionNeverUpdated.Local
            var buffer = new CircularBuffer<int>(2);

            // Then
            Assert.False(buffer.IsReadOnly);
        }

        /// <summary>
        /// The when attempt remove from circular buffer then exception is thrown.
        /// </summary>
        [Fact]
        public void WhenAttemptRemoveFromCircularBufferThenExceptionIsThrown()
        {
            // Given
            // ReSharper disable once CollectionNeverQueried.Local
            var buffer = new CircularBuffer<int>(2);

            // When
            buffer.Add(1);

            // Then
            Assert.Throws<NotSupportedException>(() => buffer.Remove(1));
        }

        /// <summary>
        /// The when attempt remove from circular buffer then exception is thrown.
        /// </summary>
        [Fact]
        public void WhenAttemptRemoveAtFromCircularBufferThenExceptionIsThrown()
        {
            // Given
            // ReSharper disable once CollectionNeverQueried.Local
            var buffer = new CircularBuffer<int>(2);

            // When
            buffer.Add(1);

            // Then
            Assert.Throws<NotSupportedException>(() => buffer.RemoveAt(0));
        }

        /// <summary>
        /// The when attempt insert into circular buffer then exception is thrown.
        /// </summary>
        [Fact]
        public void WhenAttemptInsertIntoCircularBufferThenExceptionIsThrown()
        {
            // Given
            // ReSharper disable once CollectionNeverQueried.Local
            var buffer = new CircularBuffer<int>(2);

            // When
            buffer.Add(1);

            // Then
            Assert.Throws<NotSupportedException>(() => buffer.Insert(0, 2));
        }

        /// <summary>
        /// The when item added to circular buffer then contains returns expected value.
        /// </summary>
        [Fact]
        public void WhenItemAddedToCircularBufferThenContainsReturnsExpectedValue()
        {
            // Given
            var buffer = new CircularBuffer<int>(2);

            // When
            buffer.Add(1);

            // Then
            Assert.True(buffer.Contains(1));
            Assert.False(buffer.Contains(2));
        }

        /// <summary>
        /// The when set item at index less than zero then exception is thrown.
        /// </summary>
        [Fact]
        public void WhenSetItemAtIndexLessThanZeroThenExceptionIsThrown()
        {
            // Given
            // ReSharper disable once CollectionNeverQueried.Local
            var buffer = new CircularBuffer<int>(2);

            // When
            buffer.Add(1);

            // Then
            Assert.Throws<IndexOutOfRangeException>(() => buffer[-1] = 2);
        }

        /// <summary>
        /// The when set item at index greater than or equal to count then exception is thrown.
        /// </summary>
        [Fact]
        public void WhenInsertItemAtIndexGreaterThanOrEqualToCountThenExceptionIsThrown()
        {
            // Given
            // ReSharper disable once CollectionNeverQueried.Local
            var buffer = new CircularBuffer<int>(2);

            // When
            buffer.Add(1);

            // Then
            Assert.Throws<IndexOutOfRangeException>(() => buffer[1] = 2);
            Assert.Throws<IndexOutOfRangeException>(() => buffer[2] = 2);
        }

        /// <summary>
        /// The when insert item at valid index then item is updated.
        /// </summary>
        [Fact]
        public void WhenInsertItemAtValidIndexThenItemIsUpdated()
        {
            // Given
            var buffer = new CircularBuffer<int>(2);

            // When
            buffer.Add(1);
            buffer.Add(2);
            buffer[0] = 3;

            // Then
            Assert.Equal(3, buffer[0]);
            Assert.Equal(2, buffer[1]);
        }

        /// <summary>
        /// The when clear then buffer is reset.
        /// </summary>
        [Fact]
        public void WhenClearThenBufferIsReset()
        {
            // Given
            var buffer = new CircularBuffer<int>(2);

            // When
            buffer.Add(1);
            buffer.Add(2);
            buffer.Clear();

            // Then
            Assert.Equal(0, buffer.Count);
            Assert.Equal(-1, buffer.IndexOf(1));
        }

        /// <summary>
        /// The when copy to array then array contains items in the order they were added to the buffer.
        /// </summary>
        [Fact]
        public void WhenCopyToArrayThenArrayContainsItemsInTheOrderTheyWereAddedToTheBuffer()
        {
            // Given
            var buffer = new CircularBuffer<int>(2);
            int[] a = new int[2];

            // When
            buffer.Add(1);
            buffer.Add(2);
            buffer.CopyTo(a, 0);

            // Then
            Assert.Equal(1, a[0]);
            Assert.Equal(2, a[1]);
        }
    }
}
