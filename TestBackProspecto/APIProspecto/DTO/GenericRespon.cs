using APIProspecto.DTO.Interfaces;

namespace APIProspecto.DTO
{
    public class GenericRespon : IGenericRespon
    {
        public int State { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public async Task<GenericRespon> Error(int state, string message)
        {
            var task = await Task<GenericRespon>.Factory.StartNew(() => 
            {
                var obj = new GenericRespon()
                {
                    State = state,
                    Message = message
                };

                return obj;
            });

            return task;
        }

        public async Task<GenericRespon> SuccesFull(int state, string message, object? data)
        {
            var task = await Task<GenericRespon>.Factory.StartNew(() =>
            {
                var obj = new GenericRespon()
                {
                    State = state,
                    Message = message,
                };

                if(data != null)
                    obj.Data = data;

                return obj;
            });

            return task;
        }
    }
}
