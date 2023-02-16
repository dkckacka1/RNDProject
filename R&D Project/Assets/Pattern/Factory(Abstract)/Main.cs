using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Pattern.Factory_Abstract_
{
    class Main : MonoBehaviour
    {
        public GameObject humanPrefab = null;

        private void Start()
        {
            var human = Instantiate(humanPrefab.GetComponent<Human>());
            human.SetEquipment(FindObjectOfType<HumanFactory>());
        }
    }
}
