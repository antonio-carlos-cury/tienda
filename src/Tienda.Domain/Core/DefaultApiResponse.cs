namespace Tienda.Domain.Core
{
    #region Interface
    public interface IDefaultApiResponse<TFilter, TResults>
    {
        IEnumerable<string> Errors { get; set; }
        TFilter Filter { get; set; }
        TResults Results { get; set; }
        bool Success { get; set; }
    }
    #endregion

    public class DefaultApiResponse<TFilter, TResults> : IDefaultApiResponse<TFilter, TResults>
    {
        public bool Success { get; set; }
        public TResults Results { get; set; }
        public IEnumerable<string>? Errors { get; set; }
        public TFilter Filter { get; set; }

        public DefaultApiResponse(TFilter filters, TResults results) 
        {
            this.Filter = filters;
            this.Results = results;
            this.Success = true;
        }

        public DefaultApiResponse()
        {
            this.Filter = default;
            this.Results = default;
        }

    }
}
