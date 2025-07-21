using BankApplication.App.Helpers.Extensions;
namespace BankApplication.App.Helpers.Models
{
    public class GenericKeyValuePair
    {
        public int Key { get; set; }
        public string Value { get; set; }
        public string Name { get; set; }    


        public GenericKeyValuePair(Enum e)
        {
            Key = Convert.ToInt32(e);
            Value = e.ToString();
            Name = e.GetDescription();
        }

    }
}
