using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleHierarchies.Gui
{
    public class MenuManager
    {
        private List<string> menuPath = new List<string>();

        public void AddToMenuPath(string menuName)
        {
            if (!menuPath.Contains(menuName))
            {
                menuPath.Add(menuName);
            } 
        }

        public void RemoveLastFromMenuPath()
        {
            if (menuPath.Count > 0)
            {
                menuPath.RemoveAt(menuPath.Count - 1);
            }
        }

        public string GetCurrentMenuPath()
        {
            return string.Join(" -> ", menuPath);
        }
    }
}
