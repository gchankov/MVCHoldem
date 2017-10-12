using System;

namespace MVCHoldem.Data.Model.Contracts
{
    public interface IAuditable
    {
        DateTime CreatedOn { get; set; }

        DateTime? ModifiedOn { get; set; }
    }
}
