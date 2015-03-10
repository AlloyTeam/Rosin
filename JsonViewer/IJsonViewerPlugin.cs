using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace EPocalipse.Json.Viewer
{
    public interface IJsonViewerPlugin
    {
        string DisplayName {get;}
        bool CanVisualize(JsonObject jsonObject);
    }

    public interface ICustomTextProvider : IJsonViewerPlugin
    {
        string GetText(JsonObject jsonObject);
    }

    public interface IJsonVisualizer : IJsonViewerPlugin
    {
        Control GetControl(JsonObject jsonObject);
        void Visualize(JsonObject jsonObject);
    }
}
