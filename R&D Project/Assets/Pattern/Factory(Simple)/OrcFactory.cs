using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Pattern.Factory_Simple_
{
    public enum orcType
    {
        Normal,
        Rare,
        King
    }

    class OrcFactory : MonsterFactory<orcType>
    {
        [SerializeField] Orc normalOrc;
        [SerializeField] Orc rareOrc;
        [SerializeField] Orc kingOrc;


        public override Monster CreateMonster(orcType type)
        {
            Orc orc = null;

            switch (type)
            {
                case orcType.Normal:
                    orc = Instantiate<Orc>(normalOrc);
                    break;
                case orcType.Rare:
                    orc = Instantiate<Orc>(rareOrc);
                    break;
                case orcType.King:
                    orc = Instantiate<Orc>(kingOrc);
                    break;
            }

            return orc;
        }
    }
}
