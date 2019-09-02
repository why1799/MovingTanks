using MovingTanks.Models.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MovingTanks.Models.Classes
{
    public class FieldObjectEnumerator : IEnumerator<IFieldObject>
    {
        private IFieldObjects _collection;
        private int curIndex;

        public FieldObjectEnumerator(IFieldObjects collection)
        {
            _collection = collection;
            curIndex = -1;
            Current = default(IFieldObject);

        }

        public bool MoveNext()
        {
            //Avoids going beyond the end of the collection.
            if (++curIndex >= _collection.Count)
            {
                return false;
            }
            else
            {
                // Set current box to next item in collection.
                Current = _collection[curIndex];
            }
            return true;
        }

        public void Reset() { curIndex = -1; }

        void IDisposable.Dispose() { }

        public IFieldObject Current { get; private set; }

        object IEnumerator.Current
        {
            get { return Current; }
        }

    }
}
