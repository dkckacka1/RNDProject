using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Pattern.Command
{
    class Main : MonoBehaviour
    {
        Command zCommand;
        Command xCommand;
        Command cCommand;
        Command vCommand;

        private void Start()
        {
            SetCommand();
        }

        private void SetCommand()
        {
            zCommand = new JumpCommand();
            xCommand = new FireCommand();
            cCommand = new MoveCommand();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                if (zCommand != null)
                {
                    zCommand.Excute(this);
                }
            }

            if (Input.GetKeyDown(KeyCode.X))
            {
                if (xCommand != null)
                {
                    xCommand.Excute(this);
                }
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                if (cCommand != null)
                {
                    cCommand.Excute(this);
                }
            }

            if (Input.GetKeyDown(KeyCode.V))
            {
                if (vCommand != null)
                {
                    vCommand.Excute(this);
                }
            }
        }
    }

}
