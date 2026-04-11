namespace CardFile.Wpf.ViewModels
{
    public class CardEditViewModel : BaseViewModel
    {
       

        public void IsWorkingTillNowChecked()
        {
            
        }

        public void IsWorkingTillNowUnchecked()
        {
            
        }

       
        public void LoadViewModel(CardFile.Business.Models.Company company)
        {
            this.Company = company;
        }

        public CardFile.Business.Models.Company Company { get; set; }
    }
}