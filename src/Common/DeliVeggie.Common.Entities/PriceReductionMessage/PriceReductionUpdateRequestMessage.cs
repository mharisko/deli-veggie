namespace DeliVeggie.Common.MessageTypes.PriceReductionMessage
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="PriceReductionMessageBase" />
    public class PriceReductionUpdateRequestMessage : PriceReductionMessageBase
    {
        public int StatusCode { get; set; } = 204;
    }
}
