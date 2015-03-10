using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.ComponentModel;

namespace EPocalipse.Json.Viewer
{
    [DebuggerDisplay("Type={GetType().Name} Text = {Text}")]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class JsonObject
    {
        static JsonObject()
        {
            TypeDescriptor.AddProvider(new JsonObjectTypeDescriptionProvider(), typeof(JsonObject));
        }

        private string _id;
        private object _value;
        private JsonType _jsonType;
        private JsonFields _fields;
        private JsonObject _parent;
        private string _text;

        public JsonObject()
        {
            _fields=new JsonFields(this);
        }

        public string Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        public object Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }

        public JsonType JsonType
        {
            get
            {
                return _jsonType;
            }
            set
            {
                _jsonType = value;
            }
        }

        public JsonObject Parent
        {
            get
            {
                return _parent;
            }
            set
            {
                _parent = value;
            }
        }

        public string Text
        {
            get
            {
                if (_text == null)
                {
                    if (JsonType == JsonType.Value)
                    {
                        string val = (Value == null ? "<null>" : Value.ToString());
                        if (Value is string)
                            val = "\"" + val + "\"";
                        _text = String.Format("{0} : {1}", Id, val);
                    }
                    else
                        _text = Id;
                }
                return _text;
            }
        }

        public JsonFields Fields
        {
            get
            {
                return _fields;
            }
        }

        internal void Modified()
        {
            _text = null;
        }

        public bool ContainsFields(params string[] ids )
        {
            foreach (string s in ids)
            {
            if (!_fields.ContainId(s))
                return false;
            }
            return true;
        }

        public bool ContainsField(string id, JsonType type)
        {
            JsonObject field = Fields[id];
            return (field != null && field.JsonType == type);
        }
    }
}
