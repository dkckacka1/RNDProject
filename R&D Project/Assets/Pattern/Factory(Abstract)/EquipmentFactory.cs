using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Pattern.Factory_Abstract_
{
    public abstract class EquipmentFactory : MonoBehaviour
    {
        public abstract T CreateEquipment<T>(int equipmentID ,Transform transform) where T : Equipment;
    }
}
