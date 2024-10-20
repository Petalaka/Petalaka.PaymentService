﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petalaka.Payment.Repository.CustomSettings
{
    public class JwtSettings
    {
        public string? Key { get; set; }
        public string? Issuer { get; set; }
        public string? Audience { get; set; }
        public int AccessTokenExpirationMinutes { get; set; }
        public int RefreshTokenExpirationHours { get; set; }

        public bool IsValid()
        {
            if (string.IsNullOrEmpty(Key))
            {
                throw new ArgumentException("Cannot read settings or Key cannot be null or empty.");
            }

            if (string.IsNullOrEmpty(Issuer))
            {
                throw new ArgumentException("Cannot read settings or Issuer cannot be null or empty.");
            }

            if (string.IsNullOrEmpty(Audience))
            {
                throw new ArgumentException("Cannot read settings or Audience cannot be null or empty.");
            }

            if (AccessTokenExpirationMinutes <= 0)
            {
                throw new ArgumentException("Cannot read settings or AccessTokenExpirationMinutes must be greater than 0.");
            }

            if (RefreshTokenExpirationHours <= 0)
            {
                throw new ArgumentException("Cannot read settings or RefreshTokenExpirationHours must be greater than 0.");
            }

            return true;
        }
    }
}
