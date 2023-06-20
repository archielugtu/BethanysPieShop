using BethanysPieShop.Models;

namespace BethanysPieShop.ViewModels
{
    /*
     * ViewMdoels represents the data that you want to pass in to a view. The ViewModel will be used to contain information that you want to be displayed in the View.
     */
    public class HomeViewModel
    {
        public IEnumerable<Pie> PiesOfTheWeek { get; set; }

        public HomeViewModel(IEnumerable<Pie> piesOfTheWeek)
        {
            PiesOfTheWeek = piesOfTheWeek;
        }
    }
}
