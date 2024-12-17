using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mir.Core.controller
{
    public static class TreeNodeExtensions
    {
        private static readonly Dictionary<TreeNode, Dictionary<string, object>> _customProperties =
       new Dictionary<TreeNode, Dictionary<string, object>>();

        public static void SetProperty(this TreeNode node, string key, object value)
        {
            if (!_customProperties.TryGetValue(node, out var properties))
            {
                properties = new Dictionary<string, object>();
                _customProperties[node] = properties;
            }
            properties[key] = value;
        }

        public static object GetProperty(this TreeNode node, string key)
        {
            if (_customProperties.TryGetValue(node, out var properties) &&
                properties.TryGetValue(key, out var value))
            {
                return value;
            }
            return null;
        }
    }
}
