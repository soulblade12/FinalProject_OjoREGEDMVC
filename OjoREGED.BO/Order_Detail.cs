namespace OjoREGED.BO
{
    public class Order_Detail
    {
        public int OrderDetail_ID { get; set; }
        public int Order_ID { get; set; }
        public decimal Weight { get; set; }
        public string Size { get; set; }
        public string Order_Instruction { get; set; }
        public int Order_Status { get; set; }
        public string Order_Img { get; set; }


        public Order_Placed OrderPlaced { get; set; }
        public Order_Status OrderStatus { get; set; }
    }

}
