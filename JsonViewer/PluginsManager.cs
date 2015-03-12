using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;
using System.Configuration;
using System.Reflection;
using System.IO;

namespace EPocalipse.Json.Viewer
{
    class PluginsManager
    {
        List<IJsonViewerPlugin> plugins = new List<IJsonViewerPlugin>();
        List<ICustomTextProvider> textVisualizers = new List<ICustomTextProvider>();
        List<IJsonVisualizer> visualizers = new List<IJsonVisualizer>();
        IJsonVisualizer _defaultVisualizer;

        public PluginsManager()
        {
        }

        public void Initialize()
        {
            InitDefaults();
        }

        private void InitDefaults()
        {
            if (this._defaultVisualizer == null)
            {
                AddPlugin(new JsonObjectVisualizer());
                AddPlugin(new AjaxNetDateTime());
                AddPlugin(new CustomDate());
            }
        }

        private void AddPlugin(IJsonViewerPlugin plugin)
        {
            plugins.Add(plugin);
            if (plugin is ICustomTextProvider)
                textVisualizers.Add((ICustomTextProvider)plugin);
            if (plugin is IJsonVisualizer)
            {
                if (_defaultVisualizer == null)
                    _defaultVisualizer = (IJsonVisualizer)plugin;
                visualizers.Add((IJsonVisualizer)plugin);
            }
        }

        public IEnumerable<ICustomTextProvider> TextVisualizers
        {
            get
            {
                return textVisualizers;
            }
        }

        public IEnumerable<IJsonVisualizer> Visualizers
        {
            get
            {
                return visualizers;
            }
        }

        public IJsonVisualizer DefaultVisualizer
        {
            get
            {
                return _defaultVisualizer;
            }
        }
    }
}
