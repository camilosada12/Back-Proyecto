using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Services
{
    public class VerificationCache
    {
        private readonly IMemoryCache _cache;

        public VerificationCache(IMemoryCache cache)
        {
            _cache = cache;
        }

        // Guardar código con duración dinámica (24h, 3d, etc.)
        public void SaveCode(string email, string code, TimeSpan duration)
        {
            _cache.Set(email, code, duration);
        }

        public bool ValidateCode(string email, string code)
        {
            if (_cache.TryGetValue(email, out string? savedCode))
            {
                return savedCode == code;
            }
            return false;
        }
    }

}
