using Microsoft.AspNet.Identity;
using System.Collections.Generic;

namespace MVCHoldem.UnitTests.Wrappers
{
    public class IdentityResultWrapper : IdentityResult
    {
        public IdentityResultWrapper(bool success)
            : base(success)
        {
        }

        public IdentityResultWrapper(string[] errors)
            : base(errors)
        {
        }

        public IdentityResultWrapper(IEnumerable<string> errors)
            : base(errors)
        {
        }

        public IdentityResult GetIdentityResult()
        {
            return this;
        }
    }
}
