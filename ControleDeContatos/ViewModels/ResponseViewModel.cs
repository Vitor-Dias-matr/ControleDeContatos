namespace ControleDeContatos.ViewModels
{
    public class ResponseViewModel
    {
        public object data { get; set; }
        public StatusViewResponse status { get; set; }
    }

    public class StatusViewResponse
    {
        public int code { get; set; }
        public string message { get; set; }
    }
}