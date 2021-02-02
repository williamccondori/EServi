﻿using System;

namespace EServi.Shared.Helpers
{
    public static class ActivationCode
    {
        public static string Generate()
        {
            var generator = new Random();

            var activationCode = generator.Next(0, 1000000).ToString("D6");

            return activationCode;
        }
    }
}