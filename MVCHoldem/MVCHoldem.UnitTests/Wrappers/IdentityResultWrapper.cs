namespace MVCHoldem.UnitTests.Wrappers
{
    using System.Collections.Generic;
    using Microsoft.AspNet.Identity;

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
