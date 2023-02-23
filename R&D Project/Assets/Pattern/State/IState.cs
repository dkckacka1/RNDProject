using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Pattern.State
{
    public interface IState
    {
        void Handle(Controller controller);
    }
}
