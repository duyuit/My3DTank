using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace Assets.Scripts
{
    public class GameGlobal
    {
        //public GameObject localPlayer;
        public Vector2 lastJoystickPosition;
        public Vector2 lastFirePosition;
        public GameObject joystickMoveByDrag;
      
        private static GameGlobal instace;

        public static GameGlobal Instance
        {
            get
            {
                if (instace == null)
                    instace = new GameGlobal();
                return instace;
            }
        }
    }
}
