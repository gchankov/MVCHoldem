namespace MVCHoldem.Data.Models.Contracts
{
    using System;

    public interface IAuditable
    {
        DateTime CreatedOn { get; set; }

        DateTime? ModifiedOn { get; set; }
    }
}
