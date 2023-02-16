using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Pattern.Factory_Simple_
{
    class Main : MonoBehaviour
    {
        MonsterFactory<orcType> orcFactory;

        private void Awake()
        {
            orcFactory = FindObjectOfType<OrcFactory>();
        }

        private void Start()
        {
            orcFactory.CreateMonster(orcType.Normal);
            orcFactory.CreateMonster(orcType.Normal);
            orcFactory.CreateMonster(orcType.Rare);
            orcFactory.CreateMonster(orcType.King);
        }
    }
}
