namespace Pagos.Backend.DTO
{
    public class GenericDTO
    {
        public int Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public GenericDTO()
        {
            this.Success = 0;
        }
    }
}
