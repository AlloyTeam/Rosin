using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace EPocalipse.Json.Viewer
{
    public class JsonFields : IEnumerable<JsonObject>
    {
        private List<JsonObject> _fields = new List<JsonObject>();
        private Dictionary<string, JsonObject> _fieldsById = new Dictionary<string, JsonObject>();
        private JsonObject _parent;

        public JsonFields(JsonObject parent)
        {
            _parent = parent;
        }

        public IEnumerator<JsonObject> GetEnumerator()
        {
            return _fields.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(JsonObject field)
        {
            field.Parent = _parent;
            _fields.Add(field);
            _fieldsById[field.Id] = field;
            _parent.Modified();
        }

        public int Count
        {
            get
            {
                return _fields.Count;
            }
        }

        public JsonObject this[int index]
        {
            get
            {
                return _fields[index];
            }
        }

        public JsonObject this[string id]
        {
            get
            {
                JsonObject result;
                if (_fieldsById.TryGetValue(id, out result))
                    return result;
                return null;
            }
        }

        public bool ContainId(string id)
        {
            return _fieldsById.ContainsKey(id);
        }
    }
}
