using System.ComponentModel;

namespace BookStore.Data
{
    public class Book
    {
        private bool isModified = false;
        private bool isDeleted = false;
        [Browsable(false)]
        public bool IsDeleted
        { 
            get { return isDeleted; }
            set { isDeleted = value; }
        }
        [Browsable(false)]
        public bool IsValueChanged
        {
            get { return isModified; }
            set { isModified = value; }
        }
        public int? ID { get; set; }
        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (name != value)
                {
                    name = value;
                    isModified = true;
                }
            }
        }
        private decimal? price;
        public decimal? Price
        {
            get
            {
                return price;
            }
            set
            {
                if (price != value)
                {
                    price = value;
                    isModified = true;
                }
            }
        }
        private int? year;
        public int? Year
        {
            get
            {
                return year;
            }
            set
            {
                if (year != value)
                {
                    year = value;
                    isModified = true;
                }
            }
        }
    }
}