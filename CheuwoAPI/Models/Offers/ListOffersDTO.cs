namespace CheuwoAPI.Models.Offers
{
    public class ListOffersDTO
    {
        public int Count { get; set; }
        public int Offset { get; set; }

        public ListOffersDTO()
        {
            Count = 10;
        }
    }
}
