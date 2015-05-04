using System.Collections;

namespace Winnemen.Core.Image.Exif
{
    internal class ExifExtractorEnumerator : IEnumerator
    {
        private IDictionaryEnumerator _index;

        public object Current
        {
            get { return _index.Entry; }
        }

        internal ExifExtractorEnumerator(IDictionary exif)
        {
            this.Reset();
            this._index = exif.GetEnumerator();
        }

        public void Reset()
        {
            _index = null;
        }



        public bool MoveNext()
        {
            return this._index != null && this._index.MoveNext();
        }
    }
}
