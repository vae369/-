using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mir2.Helper
{
    public class TreeHelper
    {
        /// <summary>
        /// 存储每个节点的状态
        /// </summary>
        public static Hashtable StoreNodeState = new Hashtable();
        /// <summary>
        /// 将节点的状态存起来 
        /// </summary>
        /// <param name="fullPath"></param>
        /// <param name="isExpend"></param>
        public static void AddNodeStateToHashTable(string fullPath, bool? isExpend)
        {
            if (!StoreNodeState.ContainsKey(fullPath))
            {
                StoreNodeState.Add(fullPath, isExpend);
            }
        }
        /// <summary>
        /// 清空一下hashtable
        /// </summary>
        public static void ClearStoreNodeState()
        {
            StoreNodeState.Clear();
        }

        /// <summary>
        /// true:展开；null：选择并展开，false：折叠
        /// </summary>
        /// <param name="fullPath"></param>
        /// <returns></returns>
        public static bool? FindNodeStateByFullPath(string fullPath)
        {
            if (StoreNodeState.ContainsKey(fullPath))
            {
                return (bool?)StoreNodeState[fullPath];
            }
            return false;
        }

        /// <summary>
        /// 在刷新前执行这个方法.
        /// value为空，说明isSelected而且展开
        /// 优化：如果是折叠的，那么不用存储他的子节点了
        /// </summary>
        /// <param name="treeNode"></param>
        public static void ExecStoreNodeState(TreeNode treeNode)
        {
            if (treeNode.Nodes == null || treeNode.Nodes.Count == 0)
                return;
            foreach (TreeNode node in treeNode.Nodes)
            {
                if (node.IsSelected)
                {
                    AddNodeStateToHashTable(node.FullPath, null);
                    if (node.IsExpanded)
                    {
                        ExecStoreNodeState(node);
                        continue;
                    }
                }
                else if (node.IsExpanded)
                {
                    AddNodeStateToHashTable(node.FullPath, node.IsExpanded);
                    ExecStoreNodeState(node);
                }
                else if (!node.IsExpanded)
                {
                    AddNodeStateToHashTable(node.FullPath, node.IsExpanded);
                    continue;
                }
            }
        }
        /// <summary>
        /// 刷新后,执行这个方法,恢复到以前状态
        /// </summary>
        /// <param name="treeNode"></param>
        /// <param name="treeView"></param>
        public static void SetTreeNodeState(TreeNode treeNode, TreeView treeView)
        {
            if (treeNode.Nodes == null || treeNode.Nodes.Count == 0)
                return;
            bool? findFlag = null;
            foreach (TreeNode node in treeNode.Nodes)
            {
                findFlag = FindNodeStateByFullPath(node.FullPath);
                if (findFlag == null)
                {
                    node.ExpandAll();
                    treeView.SelectedNode = node;
                    continue;
                }
                else if (findFlag == false)
                {
                    node.Collapse(true);
                    continue;
                }
                else if (findFlag == true)
                {
                    node.Expand();
                }
                SetTreeNodeState(node, treeView);
            }
        }
    }
}
