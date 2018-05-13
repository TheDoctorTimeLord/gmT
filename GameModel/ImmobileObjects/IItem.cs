﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameThief.GameModel.Enums;

namespace GameThief.GameModel.ImmobileObjects
{
    public interface IItem
    {
        int Price { get; }
        DecorType Type { get; }
    }
}
