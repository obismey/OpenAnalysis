using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vivei.Tools.Core.UI.Docking
{
    public interface IDockingService
    {
        void ShowView(INavigationView view);

        void ShowView(INavigationView view, DockingSide side);

        void RegisterWindow(INavigationView view);
    }

    public enum DockingSide
    {
        Document,
        Floating,
        Center,
        Left,
        Top,
        Bottom,
        Right
    }
}
