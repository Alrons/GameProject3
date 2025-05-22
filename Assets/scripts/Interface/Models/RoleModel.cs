
using UnityEngine;

namespace Assets.scripts.Interface.Models
{
    public class RoleModel
    {
        public int Index;
        public string Group;
        public GameObject RoleObject;

        public RoleModel(int index, string group, GameObject roleObject)
        {
            Index = index;
            Group = group;
            RoleObject = roleObject;
        }
    }
}
