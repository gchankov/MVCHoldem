namespace MVCHoldem.Web.ViewModels.Home
{
    using System.Collections.Generic;

    public class HomeViewModel
    {
        public ICollection<MostRecentPostViewModel> MostRecentPosts { get; set; }
    }
}