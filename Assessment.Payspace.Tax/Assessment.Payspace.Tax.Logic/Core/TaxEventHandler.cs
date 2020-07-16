namespace Assessment.Payspace.Tax.Logic.Core
{
    public abstract class TaxEventHandler
    {
        protected readonly string channel;

        public string Channel
        {
            get { return channel; }
        }

        protected TaxEventHandler()
        {
            this.channel = this.GetType().Name;
        }

        public abstract decimal TaxCalculator(decimal amount);
    }
}
