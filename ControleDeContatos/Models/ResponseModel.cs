namespace ControleDeContatos.Models
{
    public class ResponseModel
    {
        public object data { get; set; }
        public StatusResponse status { get; set; }
    }

    public class StatusResponse
    {
        public int code { get; set; }
        public string message { get; set; }
    }
}