namespace Task03
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Dynamic array class.
    /// </summary>
    public class DynamicArray<T>
    {
        /// <summary>
        /// Initializes a new instance of the default Dynamic array class.
        /// </summary>
        public DynamicArray()
        {
            this.Value = new T[8];
        }

        /// <summary>
        /// Initializes a new instance of the Dynamic array class with preset length.
        /// </summary>
        /// <param name="inputNumber">Array length.</param>
        public DynamicArray(int inputNumber)
        {
            this.Value = new T[inputNumber];
        }

        /// <summary>
        /// Initializes a new instance of the Dynamic array class from collection.
        /// </summary>
        /// <param name="inputCollection">Input collection.</param>
        public DynamicArray(IEnumerable<T> inputCollection)
        {
            this.Value = inputCollection.ToArray();
        }

        /// <summary>
        /// Gets dynamic array value.
        /// </summary>
        public T[] Value { get; set; }

        /// <summary>
        /// Gets dynamic array length.
        /// </summary>
        public int Length
        {
            get
            {
                if (this.Value.Last().Equals(default(T)))
                {
                    for (int i = this.Capacity - 2; i >= 0; i--)
                    {
                        if (this.Value[i].Equals(default(T)))
                        {
                            if (i == 0)
                            {
                                return 0;
                            }

                            continue;
                        }
                        else
                        {
                            return i + 1;
                        }
                    }
                }

                return this.Capacity;
            }
        }

        /// <summary>
        /// Gets real dynamic array length.
        /// </summary>
        public int Capacity
        {
            get
            {
                return this.Value.Length;
            }
        }

        /// <summary>
        /// Adds element to array.
        /// </summary>
        /// <param name="element">Passes element value.</param>
        public void Add(T element)
        {
            if (this.Length != this.Capacity)
            {
                this.Value[this.Length] = element;
            }
            else
            {
                var newValue = this.Value;
                int oldValueLength = this.Capacity;
                Array.Resize(ref newValue, newValue.Length * 2); //todo не очень хорошее решение с т.з. производительности. Представь, что у тебя в массиве 1млн элементов, а добавить нужно штук 100. Для чего выделять дополнительно ещё 1млн пустых записей? Лучше задать константой какое-то значение, на которое ты увеличиваешь массив при переполнении.
				newValue[oldValueLength] = element;
                this.Value = newValue;
            }
        }

        /// <summary>
        /// Adds collection to array.
        /// </summary>
        /// <param name="collection">Passes collection value.</param>
        public void AddRange(IEnumerable<T> collection)
        {
            var collectionCount = collection.Count();
            if (this.Length != this.Capacity)
            {
                this.Value = this.AddCollection(this.Value, collection, this.Length, collectionCount);
            }
            else
            {
                this.Value = this.AddCollection(this.Value, collection, this.Capacity, collectionCount);
            }
        }

        /// <summary>
        /// Removes element from array.
        /// </summary>
        /// <param name="element">Passes element for removal.</param>
        /// <returns>Success or failure of removal.</returns>
        public bool Remove(T element)
        {
            var newValue = this.Value;
            if (newValue.Contains(element))
            {
                newValue = newValue.Where(x => !x.Equals(element)).ToArray();
                Array.Resize(ref newValue, this.Capacity);
                this.Value = newValue;
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Inserts element to array.
        /// </summary>
        /// <param name="element">Passes element for inserting.</param>
        /// <param name="index">Passes index for inserting.</param>
        /// <returns>Success or failure of inserting.</returns>
        public bool Insert(T element, int index)
        {
            if (index > 0 && index < this.Capacity)
            {
                var firstArray = this.Value.Take(index - 1).ToList();
                var secondArray = this.Value.Skip(index - 1);
                firstArray.Add(element);
                var newArray = firstArray.ToArray();
                this.Value = this.AddCollection(newArray, secondArray, newArray.Length, secondArray.Count());
                return true;
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Adds collection to array.
        /// </summary>
        /// <param name="array">Passes array value.</param>
        /// <param name="collection">Passes collection value.</param>
        /// <param name="arrayLenght">Passes array length value.</param>
        /// <param name="collectionCount">Passes collection length value.</param>
        /// <returns>Combined array and collection.</returns>
        private T[] AddCollection(T[] array, IEnumerable<T> collection, int arrayLenght, int collectionCount)
        {
            var newValue = array;
            Array.Resize(ref newValue, arrayLenght + collectionCount);
            for (int i = arrayLenght; i < newValue.Length; i++)
            {
                newValue[i] = collection.ToList()[i - arrayLenght];
            }

            return newValue;
        }
    }
}
