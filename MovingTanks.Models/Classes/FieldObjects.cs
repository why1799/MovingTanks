using MovingTanks.Models.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MovingTanks.Models.Classes
{
    public class FieldObjects : IFieldObjects
    {
        private List<IFieldObject> _fieldobjects;

        public FieldObjects()
        {
            _fieldobjects = new List<IFieldObject>();
        }

        public int Count => _fieldobjects.Count();

        public bool IsReadOnly => false;

        public void Add(IFieldObject item)
        {
            _fieldobjects.Add(item);
        }

        public void Clear()
        {
            _fieldobjects.Clear();
        }

        public bool Contains(IFieldObject item)
        {
            bool contains = false;
            foreach(var o in _fieldobjects)
            {
                if(Equals(o, item))
                {
                    contains = true;
                    break;
                }
            }
            return contains;
        }

        public void CopyTo(IFieldObject[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException("The array cannot be null.");
            if (arrayIndex < 0)
                throw new ArgumentOutOfRangeException("The starting array index cannot be negative.");
            if (Count > array.Length - arrayIndex + 1)
                throw new ArgumentException("The destination array has fewer elements than the collection.");

            for (int i = 0; i < _fieldobjects.Count; i++)
            {
                array[i + arrayIndex] = _fieldobjects[i];
            }
        }

        public IEnumerator<IFieldObject> GetEnumerator()
        {
            return new FieldObjectEnumerator(this);
        }

        public void Move()
        {
            throw new NotImplementedException();
        }

        public bool Remove(IFieldObject item)
        {
            bool result = false;

            for (int i = 0; i < _fieldobjects.Count; i++)
            {
                var curFieldObject = _fieldobjects[i];
                if (Equals(curFieldObject, item))
                {
                    _fieldobjects.RemoveAt(i);
                    result = true;
                    break;
                }
            }
            return result;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new FieldObjectEnumerator(this);
        }
        public IFieldObject this[int index]
        {
            get { return _fieldobjects[index]; }
            set { _fieldobjects[index] = value; }
        }
    }
}
