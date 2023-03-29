namespace APIProspecto.DTO.Interfaces
{
    public interface IGenericRespon
    {
        Task<GenericRespon> SuccesFull(int state, string message,object? data);
        Task<GenericRespon> Error(int state, string message);

    }
}
