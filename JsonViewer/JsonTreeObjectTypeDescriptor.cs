using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace EPocalipse.Json.Viewer
{
    class JsonObjectTypeDescriptionProvider : TypeDescriptionProvider
    {
        public override ICustomTypeDescriptor GetTypeDescriptor(Type objectType, object instance)
        {
            return new JsonTreeObjectTypeDescriptor((JsonObject)instance);
        }
    }

    class JsonTreeObjectTypeDescriptor : CustomTypeDescriptor, ICustomTypeDescriptor
    {
        JsonObject _jsonObject;
        PropertyDescriptorCollection _propertyCollection;

        public JsonTreeObjectTypeDescriptor(JsonObject jsonObject)
        {
            _jsonObject = jsonObject;
            InitPropertyCollection();
        }

        private void InitPropertyCollection()
        {
            List<PropertyDescriptor> propertyDescriptors = new List<PropertyDescriptor>();

            if (_jsonObject != null)
            {
                foreach (JsonObject field in _jsonObject.Fields)
                {
                    PropertyDescriptor pd = new JsonTreeObjectPropertyDescriptor(field);
                    propertyDescriptors.Add(pd);
                }
            }
            _propertyCollection = new PropertyDescriptorCollection(propertyDescriptors.ToArray());
        }

        PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
        {
            return _propertyCollection;
        }

        PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
        {
            return ((ICustomTypeDescriptor)this).GetProperties(null);
        }

        object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
        {
            return _jsonObject;
        }
    }

    class JsonTreeObjectPropertyDescriptor : PropertyDescriptor
    {
        JsonObject _jsonObject;
        JsonObject[] _jsonObjects;

        public JsonTreeObjectPropertyDescriptor(JsonObject jsonObject)
            : base(jsonObject.Id, null)
        {
            _jsonObject = jsonObject;
            if (_jsonObject.JsonType == JsonType.Array)
                InitJsonObject();
        }

        private void InitJsonObject()
        {
            List<JsonObject> jsonObjectList = new List<JsonObject>();
            foreach (JsonObject field in _jsonObject.Fields)
            {
                jsonObjectList.Add(field);
            }
            _jsonObjects = jsonObjectList.ToArray();
        }

        public override bool CanResetValue(object component)
        {
            return false;
        }

        public override Type ComponentType
        {
            get
            {
                return null;
            }
        }

        public override object GetValue(object component)
        {
            switch (_jsonObject.JsonType)
            {
                case JsonType.Array:
                    return "JsonArray";
                case JsonType.Object:
                    return "JsonObject";
                default:
                    return _jsonObject.Value;
            }
        }

        public override bool IsReadOnly
        {
            get
            {
                if (_jsonObject.JsonType == JsonType.Value)
                    return false;
                return true;
            }
        }

        public override Type PropertyType
        {
            get
            {
                switch (_jsonObject.JsonType)
                {
                    case JsonType.Array:
                        return typeof(string);
                    case JsonType.Object:
                        return typeof(string);
                    default:
                        return _jsonObject.Value == null ? typeof(string) : _jsonObject.Value.GetType();
                }
            }
        }

        public override void ResetValue(object component)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override void SetValue(object component, object value)
        {
            //TODO: Implement?
        }

        public override bool ShouldSerializeValue(object component)
        {
            return false;
        }

        public override object GetEditor(Type editorBaseType)
        {
            return base.GetEditor(editorBaseType);
        }
    }
}
