namespace Assessment.Payspace.Tax.Interface.Logic
{
    public interface ITaxController<in K, in T>
    {
        void AddEventHandler(K key, T typeHandler);

        decimal ExecuteEvent(string source, decimal amount);
    }
}
